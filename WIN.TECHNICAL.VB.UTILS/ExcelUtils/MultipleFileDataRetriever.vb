Imports System.IO

Public Class MultipleFileDataRetriever
    Public Event BeginParsePath()
    Public Event EndParsePath(NumberOfFilesFound As Integer)
    Public Event BeginParseFile(filename As String, number As Integer)
    Public Event EndParseFile(filename As String, exception As Exception)
    Public Event EndCreatingRecord(sheet As Object, hash As Hashtable)

    Public Event BeginParse()
    Public Event EndParse(ByVal NumberOfRecords As Int32, ByVal NumberOfFields As Int32)
    Public Event BeginRetrieving()
    Public Event RetrievingRecord(ByVal IdRecord As Int32)
    Public Event EndRetrieving(ByVal NumberOfRecords As Int32)

    'path del percorso dove cercare i file
    Protected m_path As String


    Protected m_readerFactory As IBaseExcelReaderFactory
    Protected m_readerName As String

    Protected m_importResult As IList

    Protected m_CorrectedAnalyzedFiles As New List(Of String)
    Protected m_UncorrectedAnalyzedFiles As New List(Of String)

    Public ReadOnly Property CorrectedAnalyzedFiles As List(Of String)
        Get
            Return m_CorrectedAnalyzedFiles
        End Get
    End Property
    Public ReadOnly Property UncorrectedAnalyzedFiles As List(Of String)
        Get
            Return m_UncorrectedAnalyzedFiles
        End Get
    End Property


    'files to check
    Private m_files As String()

    'lettore dei file 
    Protected WithEvents m_infoRetriever As BaseExcelReader

    Public ReadOnly Property Importresult As IList
        Get
            Return m_importResult
        End Get
    End Property





    Public Sub New(path As String, readerFactory As IBaseExcelReaderFactory, readerName As String)
        m_path = path
        m_readerFactory = readerFactory
        m_readerName = m_readerName
    End Sub

    Public Sub ValidateAndParsePath()
        RaiseEvent BeginParsePath()
        'verifico se esite il percorso
        If Not Directory.Exists(m_path) Then
            Throw New Exception("Directory non esistente")
        End If

        'verifico la presenza dei file excel 

        Dim arr1 As String() = Directory.GetFiles(m_path, "*.xls")
        'Dim arr2 As String() = Directory.GetFiles(m_path, "?xlsx")

        If arr1.Length = 0 Then
            Throw New Exception("Nessun file trovato")
        End If

        Dim arr3 As String() = New String(arr1.Length - 1) {}
        arr1.CopyTo(arr3, 0)
        ' arr2.CopyTo(arr3, arr1.Length)

        

        m_files = arr3
        RaiseEvent EndParsePath(arr3.Length)
    End Sub

    Public Sub StartImport()

        Dim CompleteData As New ArrayList
        Dim number As Int32 = 1
        Dim excelOpened = False



        'per ogni file da verificare  creo il suo data retirever
        For Each elem As String In m_files
            'definisco l'insieme dei dati
            Dim Data As New ArrayList
            RaiseEvent BeginParseFile(elem, number)
            Try
                excelOpened = False
                m_infoRetriever = m_readerFactory.GetReader(m_readerName, elem)
                m_infoRetriever.OpenExcel()
                excelOpened = True
                m_infoRetriever.ParseData()
                Data = m_infoRetriever.RetrieveData()
                m_infoRetriever.Dispose()
                excelOpened = False
                m_CorrectedAnalyzedFiles.Add(elem)
            Catch ex As Exception
                m_UncorrectedAnalyzedFiles.Add(elem)
                RaiseEvent EndParseFile(elem, ex)
            Finally
                Try

                    If excelOpened Then
                        m_infoRetriever.Dispose()
                        excelOpened = False
                    End If
                Catch ex As Exception

                End Try
            End Try


            CompleteData.AddRange(Data)
            number = number + 1
            RaiseEvent EndParseFile(elem, Nothing)
        Next


        m_importResult = CompleteData

    End Sub



    Private Sub m_infoRetriever_BeginParse() Handles m_infoRetriever.BeginParse
        RaiseEvent BeginParse()
    End Sub

    Private Sub m_infoRetriever_BeginRetrieving() Handles m_infoRetriever.BeginRetrieving
        RaiseEvent BeginRetrieving()
    End Sub

    Private Sub m_infoRetriever_EndCreatingRecord(sheet As Object, hash As System.Collections.Hashtable) Handles m_infoRetriever.EndCreatingRecord
        RaiseEvent EndCreatingRecord(sheet, hash)
    End Sub

    Private Sub m_infoRetriever_EndParse(NumberOfRecords As Integer, NumberOfFields As Integer) Handles m_infoRetriever.EndParse
        RaiseEvent EndParse(NumberOfRecords, NumberOfFields)
    End Sub

    Private Sub m_infoRetriever_EndRetrieving(NumberOfRecords As Integer) Handles m_infoRetriever.EndRetrieving
        RaiseEvent EndRetrieving(NumberOfRecords)
    End Sub

    Private Sub m_infoRetriever_RetrievingRecord(IdRecord As Integer) Handles m_infoRetriever.RetrievingRecord
        RaiseEvent RetrievingRecord(IdRecord)
    End Sub
End Class
