Imports System.IO

Public Class BaseExcelReader
    Implements IDisposable
    Public Event BeginParse()
    Public Event EndParse(ByVal NumberOfRecords As Int32, ByVal NumberOfFields As Int32)
    Public Event BeginRetrieving()
    Public Event RetrievingRecord(ByVal IdRecord As Int32)
    Public Event EndRetrieving(ByVal NumberOfRecords As Int32)
    Public Event EndCreatingRecord(sheet As Object, hash As Hashtable)
    Protected m_numberOfRecords As Int32
    Protected m_numberOfFields As Int32
    Protected m_StrictFilename As String = ""
    Protected m_Filename As String = ""
    Protected m_Excel As Object 'Microsoft.Office.Interop.Excel.Application
    Protected m_workSheet As Object 'Microsoft.Office.Interop.Excel.Worksheet
    Protected m_workBook As Object 'Microsoft.Office.Interop.Excel.Workbook
    'Private WithEvents m_DataParser As DataParser
    Protected m_SheetStructure As New Hashtable
    'Private m_RetrivedData As ArrayList = New ArrayList


    Protected m_startRow As Int32 = 1

    Protected m_startColumn As Int32 = 1

    Friend Overridable Function GetTemplateHashTable() As Hashtable
        Return Nothing
    End Function


    Public Overridable Sub ParseImportFile()
        '
    End Sub


    Protected Function FindField(ByVal FieldName As String) As Boolean
        'Dim i As Int32 = 1
        'Do While m_workBook.Worksheets(1).Cells(1, i).value <> ""
        '    If FieldName = m_workBook.Worksheets(1).Cells(1, i).value Then
        '        Dim campo As Field = New Field
        '        campo.FileField = FieldName
        '        campo.KeyField = FieldName
        '        campo.Number = i
        '        m_SheetStructure.Add(FieldName, campo)
        '        Return True
        '    End If
        '    i = i + 1
        'Loop
        'Return False
        Dim i As Int32 = m_startColumn
        Do While m_workBook.Worksheets(1).Cells(m_startRow, i).value <> ""
            If FieldName = m_workBook.Worksheets(1).Cells(m_startRow, i).value Then
                Dim campo As Field = New Field
                campo.FileField = FieldName
                campo.KeyField = FieldName
                campo.Number = i
                m_SheetStructure.Add(FieldName, campo)
                Return True
            End If
            i = i + 1
        Loop
        Return False
    End Function

    Protected Function FindNumberOfFields() As Int32
        'Dim i As Int32 = 1
        'Do While m_workBook.Worksheets(1).Cells(1, i).value <> ""
        '    i = i + 1
        '    If m_workBook.Worksheets(1).Cells(1, i).value Is Nothing Then
        '        Return i - 1
        '    End If
        'Loop
        'Return i - 1
        Dim i As Int32 = m_startColumn
        Do While m_workBook.Worksheets(1).Cells(m_startRow, i).value <> ""
            i = i + 1
            If m_workBook.Worksheets(1).Cells(m_startRow, i).value Is Nothing Then
                Return i - 1
            End If
        Loop
        Return i - 1
    End Function

    Protected Function FindNumberOfRecords() As Int32

        'Dim i As Int32 = 1
        'Do While m_workBook.Worksheets(1).Cells(i, 1).value.ToString() <> ""
        '    i = i + 1
        '    If m_workBook.Worksheets(1).Cells(i, 1).value Is Nothing Then
        '        Return i - 2
        '    End If
        'Loop
        'Return i - 2
        Dim i As Int32 = m_startRow
        Dim j As Int32 = 1

        Do While m_workBook.Worksheets(1).Cells(i, m_startColumn).value.ToString() <> ""
            i = i + 1
            j = j + 1
            If m_workBook.Worksheets(1).Cells(i, m_startColumn).value Is Nothing Then
                Return j - 2
            End If
        Loop

        Return j - 2


    End Function



    Public Sub ParseData()
        RaiseEvent BeginParse()
        ParseImportFile()
        m_numberOfRecords = FindNumberOfRecords()
        m_numberOfFields = FindNumberOfFields()
        RaiseEvent EndParse(m_numberOfRecords, m_numberOfFields)
    End Sub
    Public Function RetrieveData() As ArrayList

        Dim f As New FileInfo(m_Filename)
        m_StrictFilename = f.Name

        'Dim list As New ArrayList
        'RaiseEvent BeginRetrieving()
        'Dim i As Int32 = 2
        'Do While m_workBook.Worksheets(1).Cells(i, 1).Value <> ""
        '    list.Add(GetRecord(i, m_workSheet))
        '    RaiseEvent RetrievingRecord(i - 1)
        '    i = i + 1

        'Loop
        'RaiseEvent EndRetrieving(i)
        'Return list

        Dim list As New ArrayList
        RaiseEvent BeginRetrieving()
        Dim i As Int32 = m_startRow + 1
        Do While Not m_workBook.Worksheets(1).Cells(i, m_startColumn).Value Is Nothing

            If (m_workBook.Worksheets(1).Cells(i, m_startColumn).Value.ToString <> "") Then
                list.Add(GetRecord(i, m_workSheet))
                RaiseEvent RetrievingRecord(i - 1)
                i = i + 1
            Else
                Exit Do
            End If



        Loop
        RaiseEvent EndRetrieving(i)
        Return list

    End Function

    Protected Function GetRecord(ByVal j As Int32, ByVal WorkSheet As Object) ' Microsoft.Office.Interop.Excel.Worksheet) As Hashtable
        'Dim i As Int32 = 1
        'Dim hash As New Hashtable
        'Dim enumer As IDictionaryEnumerator = m_SheetStructure.GetEnumerator
        'enumer.Reset()
        'Do While enumer.MoveNext
        '    hash.Add(enumer.Key, m_workBook.Worksheets(1).Cells(j, enumer.Value.Number).Value)
        'Loop
        'Return hash

        Dim hash As New Hashtable
        Dim enumer As IDictionaryEnumerator = m_SheetStructure.GetEnumerator
        enumer.Reset()
        Do While enumer.MoveNext
            hash.Add(enumer.Key, m_workBook.Worksheets(1).Cells(j, enumer.Value.Number).Value)
        Loop
        'aggiungo un campo che mi da l'informazione del file in cui mi trovo
        If Not hash.ContainsKey("FILE_NAME") Then
            hash.Add("FILE_NAME", m_StrictFilename)
        End If

        RaiseEvent EndCreatingRecord(m_workBook.Worksheets(1), hash)
        Return hash
    End Function

    Public Sub OpenExcel()
        Try
            m_Excel = CreateObject("Excel.Application") 'New Microsoft.Office.Interop.Excel.Application
            m_workBook = m_Excel.Workbooks.Add(m_Filename)
            'm_workSheet = m_workBook.Worksheets(1)
        Catch ex As Exception
            'm_Excel.Quit()
            'm_Excel = Nothing
        End Try
    End Sub

    Private disposedValue As Boolean = False      ' Per rilevare chiamate ridondanti

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: liberare le risorse gestite chiamate in modo esplicito

                m_workBook.Close(False)
                m_workBook = Nothing

                m_Excel.Quit()
                m_Excel = Nothing
            End If

            ' TODO: liberare le risorse non gestite condivise
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' Questo codice è aggiunto da Visual Basic per implementare in modo corretto il modello Disposable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Non modificare questo codice. Inserire il codice di pulitura in Dispose(ByVal disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
