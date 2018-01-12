Public Class OfficeWordHandler
    Private m_App As Object 'Interop.Word.Application
    Private m_WordAlreadyOpen As Boolean = False
    Private m_doc As Object 'Interop.Word.Document

    Public Event WordOpened()
    Public Event DocumentCreated()
    Public Event ReplacementError(ByVal TextToReplace As String, ByVal Replacement As String)

    Public Sub DocumentClose(ByVal Save As Boolean)
        m_doc.Close(Save)
    End Sub

    Public ReadOnly Property CurrentDocument() As Object 'Interop.Word.Document
        Get
            Return m_doc
        End Get
    End Property

    Public ReadOnly Property WordApplication() As Object 'Interop.Word.Application
        Get
            Return m_App
        End Get

    End Property

    Public Sub OpenWordApplication(ByVal newInstance As Boolean)

        Try
            If newInstance Then
                m_App = CreateObject("Word.Application") 'New Interop.Word.Application
                Return
            End If
            m_App = GetObject(, "Word.Application")
            'se non c'è nessuna istanza la creao
            If m_App Is Nothing Then
                m_App = CreateObject("Word.Application")
            End If
        Catch ex As Exception
            m_App = CreateObject("Word.Application")
        Finally
            RaiseEvent WordOpened()
        End Try

    End Sub


    Public Sub AddDocumentToWord(ByVal model As String)
        'ChechWordObjects()
        If My.Computer.FileSystem.FileExists(model) Then
            m_doc = m_App.Documents.Add(model)
        Else
            m_doc = m_App.Documents.Add()
        End If
        RaiseEvent DocumentCreated()
    End Sub

    Public Sub ActivateDocument()
        m_doc.Activate()
    End Sub

    Public Property Visible() As Boolean
        Get
            Return m_App.Visible
        End Get
        Set(ByVal value As Boolean)
            m_App.Visible = value
        End Set
    End Property

    Private Sub ChechWordObjects()
        If m_App Is Nothing Then
            Throw New InvalidOperationException("Nessuna istanza di word aperta")
        End If
        If m_doc Is Nothing Then
            Throw New InvalidOperationException("Nessun documento di word aperto")
        End If
    End Sub

    Public Sub PrintPreview()
        m_doc.PrintPreview()
    End Sub

    Public Sub Print()
        m_App.Options.PrintBackground = False
        m_doc.PrintOut()
    End Sub

   

    Public Sub SaveAs(ByVal fileName As String)
        If m_doc IsNot Nothing Then
            If fileName.ToLower.EndsWith("docx") Then
                m_doc.SaveAs(fileName, 16)
            Else
                m_doc.SaveAs(fileName, 0)
            End If
        End If
    End Sub

    Public Sub QuitApplication()
  
        If m_App IsNot Nothing Then
            m_App.Quit(False)
            m_App = Nothing
        End If
    End Sub


    Public Sub FindReplace(ByVal FindText As String, ByVal ReplaceText As String)


        'Ciclo anche negli header e nei footers
        Dim rngStory As Object

        For Each rngStory In CurrentDocument.StoryRanges

            With rngStory.Find

                .ClearFormatting()

                .MatchCase = False

                .Text = FindText

                .Replacement.Text = ReplaceText

                .Wrap = 1

                Try
                    .Execute(Replace:=2)
                Catch ex As Exception
                    RaiseEvent ReplacementError(FindText, ReplaceText)
                    .Replacement.Text = ""
                    .Execute(Replace:=2)
                End Try


            End With

        Next rngStory


    End Sub
    Public Sub FindReplaceOneTime(ByVal FindText As String, ByVal ReplaceText As String)


        'Ciclo anche negli header e nei footers
        Dim rngStory As Object

        For Each rngStory In CurrentDocument.StoryRanges

            With rngStory.Find

                .ClearFormatting()

                .MatchCase = False

                .Text = FindText

                .Replacement.Text = ReplaceText

                .Wrap = 1

                Try
                    .Execute(Replace:=1)
                Catch ex As Exception
                    RaiseEvent ReplacementError(FindText, ReplaceText)
                    .Replacement.Text = ""
                    .Execute(Replace:=1)
                End Try

            End With

        Next rngStory


    End Sub



    Public Sub KillAllWordProcesses()

        Try
            Me.DocumentClose(False)
            Me.QuitApplication()


            Dim P As Process() = Process.GetProcesses()

            For Each elem As Process In P
                If elem.ProcessName.ToLower = "winword" Then
                    elem.Kill()
                End If
            Next

        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Function countPlaceholder(ByVal FindText As String)
        Dim count As Decimal
        Dim r As Object
        Dim b As Boolean

        'mi riporto all'inizio del  file
        r = CurrentDocument.Range(CurrentDocument.Range.Start, CurrentDocument.Range.End)
        With r
            .Select()
        End With
        m_App.Selection.Collapse(1)

        'inizializzo le variabili e ciclo sulla ricerca della stringa da cercare
        count = -1
        b = True
        While (b = True)
            With m_App.Selection.Find
                count = count + 1
                .ClearFormatting()
                .Text = FindText
                .Execute(, , , , , , True)
                b = .Found
            End With
        End While

        Return count

    End Function
    Public Sub CopyDocument(ByVal numcopy As Integer)
        Dim r As Object

        'seleziono tutto tranne l'ultimo invio, faccio una copia e mi porto alla fine della selezione
        r = CurrentDocument.Range(CurrentDocument.Range.Start, CurrentDocument.Range.End - 1)
        With r
            .Select()
            .copy()
        End With
        m_App.Selection.Collapse(0)

        'incollo la parte copiate per le volte richieste
        For i As Int32 = 1 To numcopy Step 1
            m_App.Selection.PasteAndFormat(0)
        Next

    End Sub
    Public Sub ViewFromStart()
        m_App.ActiveWindow.ActivePane.VerticalPercentScrolled = 0
    End Sub

End Class
