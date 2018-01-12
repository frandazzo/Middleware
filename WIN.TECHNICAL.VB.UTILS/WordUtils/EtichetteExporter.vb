Imports System.Text

Public Class EtichetteExporter



    Public Event LabelPrinted(ByVal printed As Int32, ByVal totalToPrint As Int32, ByRef cancel As Boolean)
    Public Event DocumentOpening()
    Public Event DocumentClosing()
    Dim cancel As Boolean = False
    Dim WithEvents wordHandler As OfficeWordHandler
    Private m_fileErrorName As String


    Private m_ErrorString As StringBuilder
    Dim current As New Hashtable
    Dim evaluatedTag As String = ""



    Public ReadOnly Property FileErrorName() As String
        Get
            Return m_fileErrorName
        End Get
    End Property




    Public ReadOnly Property ErrorTest() As String
        Get
            Return m_ErrorString.ToString
        End Get
    End Property

    Public Sub New()

        m_ErrorString = New StringBuilder
        m_fileErrorName = "Errore-export-etichette_" + Format(DateTime.Now, "dd-MM-yyyy_hh-mm-ss") + ".txt"

    End Sub



    Public Function creaEtichette(ByVal exportOption As ExportOption) As String

        Dim documento As String = ""
        Dim numEtFoglio As Decimal
        Dim totPagine As Decimal
        Dim tagTable1 As Hashtable

        m_ErrorString = New StringBuilder
        wordHandler = New OfficeWordHandler
        current = New Hashtable

        'verifico la presenza di dati
        CheckMetadata(exportOption.Metadata)


        'Apro word
        Open()


        Try


            'aggiungo un documento 
            wordHandler.AddDocumentToWord(exportOption.NomeCompletoModello)


            'conto le occorrenze del segnaposto
            numEtFoglio = wordHandler.countPlaceholder(exportOption.TagRicercaOccorrenze)

            'verifico che il foglio presenti almeno una etichetta
            If (numEtFoglio < 1) Then
                'chiudo  l'applicazione e il documento

                Throw New InvalidOperationException("Foglio word non valido per la stampa delle etichette. Tag per il conteggio etichette non trovato! (" + exportOption.TagRicercaOccorrenze + ")")

            End If



            'Verifico che l'offset di partenza sia inferiore al numero di etichette presenti
            'nel primo foglio
            If (exportOption.OffsetPartenza > numEtFoglio) Then
                'chiudo  l'applicazione e il documento

                Throw New InvalidOperationException("Le etichette per foglio sono: " + numEtFoglio.ToString + _
                "! Impossibile partire dall'etichetta numero " + exportOption.OffsetPartenza.ToString + "!")


            End If


            ' calcolo dei fogli necessari
            totPagine = (exportOption.Metadata.Count * exportOption.Ripetizioni + exportOption.OffsetPartenza - 1) \ numEtFoglio
            If ((exportOption.Metadata.Count * exportOption.Ripetizioni + exportOption.OffsetPartenza - 1) Mod numEtFoglio > 0) Then
                totPagine = totPagine + 1
            End If


            Dim totalLabelToPrint As Int32 = (totPagine * numEtFoglio)




            Dim labelPrinted As Int32 = 1







            'ricopio le pagine necessarie. Devo tener conto che la prima pagina esiste già
            wordHandler.CopyDocument(totPagine - 1)

            ' mi sposto sull'etichetta di partenza sbiancando quelle iniziali
            tagTable1 = exportOption.Metadata.Item(0)
            For i As Int32 = 1 To exportOption.OffsetPartenza - 1
                Dim enumerator As IDictionaryEnumerator = tagTable1.GetEnumerator
                Do While enumerator.MoveNext
                    wordHandler.FindReplaceOneTime(enumerator.Key, "")
                Loop

                'verifico una richiesta dell'utente di fermare l'operazione
                RaiseEvent LabelPrinted(labelPrinted, totalLabelToPrint, cancel)
                If cancel Then
                    Return ""
                End If
                labelPrinted = labelPrinted + 1

            Next

            'popolo il foglio
            For Each tagTable As Hashtable In exportOption.Metadata
                Dim enumerator As IDictionaryEnumerator = tagTable.GetEnumerator

                current = tagTable

                Do While enumerator.MoveNext
                    For i As Int32 = 1 To exportOption.Ripetizioni Step 1
                        wordHandler.FindReplaceOneTime(enumerator.Key, enumerator.Value)
                    Next
                Loop
                'verifico una richiesta dell'utente di fermare l'operazione
                RaiseEvent LabelPrinted(labelPrinted, totalLabelToPrint, cancel)
                If cancel Then
                    Return ""
                End If
                labelPrinted = labelPrinted + exportOption.Ripetizioni
            Next

            current = New Hashtable

            ' sbianco le etichette restanti
            tagTable1 = exportOption.Metadata.Item(0)
            For i As Int32 = 1 To ((numEtFoglio * totPagine) - (exportOption.Metadata.Count * exportOption.Ripetizioni + exportOption.OffsetPartenza - 1)) Step 1
                Dim enumerator As IDictionaryEnumerator = tagTable1.GetEnumerator
                Do While enumerator.MoveNext
                    wordHandler.FindReplaceOneTime(enumerator.Key, "")
                Loop
                'verifico una richiesta dell'utente di fermare l'operazione
                RaiseEvent LabelPrinted(labelPrinted, totalLabelToPrint, cancel)
                If cancel Then

                    Return ""
                End If
                labelPrinted = labelPrinted + 1
            Next
            'torno all'inizio del foglio e lo rendo visibile


            wordHandler.SaveAs(exportOption.NomeCompletoFileDaSalvare)






            Return exportOption.NomeCompletoFileDaSalvare


        Catch ex As Exception
            Throw
        Finally
            Close()
        End Try
    End Function




    Private Sub Close()
        Try
            RaiseEvent DocumentClosing()
            wordHandler.DocumentClose(False)
            wordHandler.QuitApplication()
        Catch ex As Exception
            wordHandler.KillAllWordProcesses()
        End Try

    End Sub


    Private Sub CheckMetadata(ByVal lista As IList(Of Hashtable))
        If lista.Count = 0 Then
            Throw New InvalidOperationException("Nessuna etichetta da stampare")
            'Return
        End If
    End Sub



    Private Sub Open()
        RaiseEvent DocumentOpening()
        wordHandler.OpenWordApplication(True)
    End Sub




    Private Sub wordHandler_ReplacementError(ByVal TextToReplace As String, ByVal Replacement As String) Handles wordHandler.ReplacementError
        If current Is Nothing Then
            Return
        End If
        If current.Count > 0 Then
            'per correggere un comportamento strano di word
            If evaluatedTag <> "" Then
                If evaluatedTag = Replacement Then
                    Return
                End If
            End If
           

            m_ErrorString.AppendLine("Errore nella sostituzione del testo per il seguente elemento:")

            Dim enumerator As IDictionaryEnumerator = current.GetEnumerator
            Do While enumerator.MoveNext
                m_ErrorString.AppendLine(enumerator.Value.ToString)
            Loop
            m_ErrorString.AppendLine("Errore avvenuto nella sostrituzione del testo '" + TextToReplace + "' con il testo '" + Replacement + "'!  ")

            'per correggere un comportamento strano di word
            evaluatedTag = Replacement

        End If

    End Sub
End Class
