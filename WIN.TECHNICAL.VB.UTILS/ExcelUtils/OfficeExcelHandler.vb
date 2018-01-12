

Imports System.Threading
Imports System.Globalization

'Imports Microsoft.Office
'Imports Microsoft.Office.Interop.Excel

Public Class OfficeExcelHandler
    Implements IDisposable


    Private m_App As Object 'Interop.Excel.Application

    Private m_doc As Object 'Interop.Excel.Workbook
    Private HeadingStyle As Object 'Interop.Excel.Style
    Private m_CellStyle As Object 'Interop.Excel.Style
    Private m_sheet As Object 'Interop.Excel.Worksheet

    Dim OriginalCulture As System.Globalization.CultureInfo


    Public Event RowInserted(ByVal rownumber As Int32, ByVal cancel As Boolean)
    Public Event ExcelOpened()
    Public Event ExcelQuitted()
    Public Event WorkbookCreated()
    Public Event WorkbookClosed()



    Public ReadOnly Property CurrentSheet() As Object 'Interop.Excel.Worksheet
        Get
            Return m_sheet
        End Get
    End Property

    Public Sub SetCurrentSheetName(ByVal name As String)
        If m_sheet IsNot Nothing Then
            m_sheet.Name = name
        End If
    End Sub

    Public ReadOnly Property CellStyleName() As String
        Get
            If m_CellStyle IsNot Nothing Then
                Return m_CellStyle.Name
            End If
            Return ""
        End Get
    End Property

    Public ReadOnly Property HeaderStyleName() As String
        Get
            If HeadingStyle IsNot Nothing Then
                Return HeadingStyle.Name
            End If
            Return ""
        End Get
    End Property

    Public Overloads Sub AddSheet()
        If m_doc Is Nothing Then Throw New InvalidOperationException("Workshett nullo")

        m_doc.Sheets.Add()
        'imposto l'ultimo shhet inserito come quello corrente
        m_sheet = m_doc.Sheets.Item(m_doc.Sheets.Count)
    End Sub


    Public Overloads Sub AddSheet(ByVal sheetname As String)
        AddSheet()

        m_sheet.Name = sheetname
    End Sub

    Public Sub SelectSheetByIndex(ByVal index As Int32)
        If m_doc Is Nothing Then Throw New InvalidOperationException("Workshett nullo")


        'il numero degli sheet deve essere uguale all'indice
        '(ricordati che qui l'indice è a base 1)ets


        If m_doc.Sheets.Count = index - 1 Then
            'se sto cercando uno sheet successivo all'ultimo 
            'lo aggiungo...
            m_doc.Sheets.Add(After:=m_doc.Sheets(m_doc.Sheets.Count))

        End If
        m_sheet = m_doc.Sheets.Item(index)






    End Sub

    Public Sub SelectSheetByName(ByVal name As String)
        If m_doc Is Nothing Then Throw New InvalidOperationException("Workshett nullo")

        m_sheet = m_doc.Sheets.Item(name)


    End Sub


    Public Function ExistSheeByName(ByVal name As String) As Boolean
        If m_doc Is Nothing Then
            Return False
        End If


        For Each elem As Object In m_doc.Sheets
            If elem.Name.Equals(name) Then
                Return True
            End If
        Next


        Return False

    End Function


    Public Sub New()

        OriginalCulture = System.Threading.Thread.CurrentThread.CurrentCulture
        Thread.CurrentThread.CurrentCulture = New CultureInfo("it-IT")
    End Sub


    Public ReadOnly Property ExcelInstance() As Object 'Interop.Excel.Application
        Get
            Return m_App
        End Get
    End Property


    Public ReadOnly Property CurrentWorkbookSheetCount() As Int32
        Get
            If m_doc Is Nothing Then Throw New InvalidOperationException("Workshett nullo")
            Return m_doc.Sheets.Count
        End Get
    End Property



    Public Overloads Sub DefineDefaultStyles()
        If m_doc Is Nothing Then
            Return
        End If
        'Create default header and cell style
        Try
            HeadingStyle = m_doc.Styles("intestazione")
        Catch ex As Exception
            HeadingStyle = m_doc.Styles.Add("intestazione")
            HeadingStyle.Font.Size = 14
            HeadingStyle.Font.Bold = True
            'HeadingStyle.Font.Family = "Tahoma"
        End Try

        Try
            m_CellStyle = m_doc.Styles("cella")
        Catch ex As Exception
            m_CellStyle = m_doc.Styles.Add("cella")
            m_CellStyle.Font.Size = 12
            m_CellStyle.Font.Bold = False

        End Try

    End Sub

  
    Public Overloads Sub DefineDefaultStyles(ByVal IntestazioneFontSize As Int32, ByVal CellaFontSize As Int32)
        If m_doc Is Nothing Then
            Return
        End If
        'Create default header and cell style
        Try
            HeadingStyle = m_doc.Styles("intestazione")
        Catch ex As Exception
            HeadingStyle = m_doc.Styles.Add("intestazione")
            HeadingStyle.Font.Size = IntestazioneFontSize
            HeadingStyle.Font.Bold = True

        End Try

        Try
            m_CellStyle = m_doc.Styles("cella")
        Catch ex As Exception
            m_CellStyle = m_doc.Styles.Add("cella")
            m_CellStyle.Font.Size = CellaFontSize
            m_CellStyle.Font.Bold = False

        End Try

    End Sub




    Public Property CellStyle() As Object 'Style
        Get
            Return m_CellStyle
        End Get
        Set(ByVal value As Object) 'Style)
            m_CellStyle = value
        End Set
    End Property



    Public Property HeaderStyle() As Object 'Style
        Get
            Return HeadingStyle
        End Get
        Set(ByVal value As Object) 'Style)
            HeadingStyle = value
        End Set
    End Property





    Public Sub OpenExcelApplication(ByVal newInstance As Boolean)

        Try
            If newInstance Then
                m_App = CreateObject("Excel.Application") 'New Interop.Excel.Application
                m_App.DisplayAlerts = False
                Return
            End If
            m_App = GetObject(, "Excel.Application")
            'se non c'è nessuna istanza la creao
            If m_App Is Nothing Then
                m_App = CreateObject("Excel.Application")
            End If
            m_App.DisplayAlerts = False
        Catch ex As Exception

            m_App = CreateObject("Excel.Application")
        Finally
            RaiseEvent ExcelOpened()
        End Try

    End Sub



    Public Sub KillAllExcelProcesses()

        Try
            Me.CloseWorkbook()
            Me.QuitApplication()


            Dim P As Process() = Process.GetProcesses()

            For Each elem As Process In P
                If elem.ProcessName.ToLower = "excel" Then
                    elem.Kill()
                End If
            Next

        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Public Sub AddDocumentToExcel(ByVal model As String)
        If My.Computer.FileSystem.FileExists(model) Then
            m_doc = m_App.Workbooks.Add(model)
        Else
            m_doc = m_App.Workbooks.Add()
        End If
        RaiseEvent WorkbookCreated()
    End Sub

    Public ReadOnly Property CurrentWorkbook() As Object 'Interop.Excel.Workbook
        Get
            Return m_doc
        End Get
    End Property



    Public Sub ActivateWorkBook()
        If m_doc Is Nothing Then
            Return
        End If
        m_doc.Activate()

    End Sub



    Public Property IsVisible() As Boolean
        Get
            Return m_App.Visible
        End Get
        Set(ByVal value As Boolean)
            m_App.Visible = value
        End Set
    End Property

    Public Sub PrintPreview()
        m_doc.PrintPreview()
    End Sub


    Public Sub SaveAs(ByVal fileName As String)
        m_doc.SaveAs(fileName,-4143)
    End Sub

    Public Sub QuitApplication()

        If m_App IsNot Nothing Then
            m_App.Quit()
            m_App = Nothing
            RaiseEvent ExcelQuitted()
        End If
    End Sub

    Public Sub CloseWorkbook()

        If m_App IsNot Nothing Then
            m_App.Workbooks.Close()
            RaiseEvent WorkbookClosed()
        End If


    End Sub


    Public Function CellValue(ByVal worksheet As Object, ByVal row As Int32, ByVal col As Int32) As String
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")

        Dim o As Object = worksheet.Cells(row, col).Value

        If o Is Nothing Then
            Return ""
        End If

        Return o.ToString
    End Function



    Public Overloads Function FindReplaceCellValue(ByVal worksheet As Object, ByVal maxrow As Int32, ByVal maxcol As Int32, ByVal value As String, ByVal replace As Object, ByVal forAll As Boolean, ByVal findMode As FindMode) As Boolean
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        Dim found As Boolean = False


        If FindMode = OfficeExcelHandler.FindMode.Table Then

            For row As Integer = 1 To maxrow
                For col As Integer = 1 To maxcol

                    If worksheet.Cells(row, col).Value IsNot Nothing Then
                        If worksheet.Cells(row, col).Value.ToString = value Then
                            worksheet.Cells(row, col).Value = replace
                            found = True
                            If Not forAll Then
                                Return True
                            End If
                        End If
                    End If

                Next
            Next

        ElseIf FindMode = OfficeExcelHandler.FindMode.LockColumn Then

            For row As Integer = 1 To maxrow

                If worksheet.Cells(row, maxcol).Value IsNot Nothing Then
                    If worksheet.Cells(row, maxcol).Value.ToString = value Then
                        worksheet.Cells(row, maxcol).Value = replace
                        found = True
                        If Not forAll Then
                            Return True
                        End If
                    End If
                End If

            Next

        Else


            For col As Integer = 1 To maxcol

                If worksheet.Cells(maxrow, col).Value IsNot Nothing Then
                    If worksheet.Cells(maxrow, col).Value.ToString = value Then
                        worksheet.Cells(maxrow, col).Value = replace
                        found = True
                        If Not forAll Then
                            Return True
                        End If
                    End If
                End If


            Next

        End If

        

        Return found

    End Function

    Public Overloads Function FindReplaceCellValue(ByVal maxrow As Int32, ByVal maxcol As Int32, ByVal value As String, ByVal replace As Object, ByVal forAll As Boolean, ByVal FindMode As FindMode) As Boolean
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        Dim found As Boolean = False


        If FindMode = OfficeExcelHandler.FindMode.Table Then

            For row As Integer = 1 To maxrow
                For col As Integer = 1 To maxcol

                    If m_sheet.Cells(row, col).Value IsNot Nothing Then
                        If m_sheet.Cells(row, col).Value.ToString = value Then
                            m_sheet.Cells(row, col).Value = replace
                            found = True
                            If Not forAll Then
                                Return True
                            End If
                        End If
                    End If

                Next
            Next

        ElseIf FindMode = OfficeExcelHandler.FindMode.LockColumn Then

            For row As Integer = 1 To maxrow


                If m_sheet.Cells(row, maxcol).Value IsNot Nothing Then
                    If m_sheet.Cells(row, maxcol).Value.ToString = value Then
                        m_sheet.Cells(row, maxcol).Value = replace
                        found = True
                        If Not forAll Then
                            Return True
                        End If
                    End If
                End If


            Next

        Else


            For col As Integer = 1 To maxcol

                If m_sheet.Cells(maxrow, col).Value IsNot Nothing Then
                    If m_sheet.Cells(maxrow, col).Value.ToString = value Then
                        m_sheet.Cells(maxrow, col).Value = replace
                        found = True
                        If Not forAll Then
                            Return True
                        End If
                    End If
                End If

            Next


        End If





        

        Return found

    End Function


    Public Overloads Function FindCellValue(ByVal worksheet As Object, ByVal maxrow As Int32, ByVal maxcol As Int32, ByVal value As String, ByVal findMode As FindMode) As CellCoordinate
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")

        If FindMode = OfficeExcelHandler.FindMode.Table Then

            For row As Integer = 1 To maxrow
                For col As Integer = 1 To maxcol

                    If worksheet.Cells(row, col).Value IsNot Nothing Then
                        If worksheet.Cells(row, col).Value.ToString = value Then
                            Return New CellCoordinate(row, col)
                        End If
                    End If

                Next
            Next

        ElseIf FindMode = OfficeExcelHandler.FindMode.LockColumn Then

            For row As Integer = 1 To maxrow

                If worksheet.Cells(row, maxcol).Value IsNot Nothing Then
                    If worksheet.Cells(row, maxcol).Value.ToString = value Then
                        Return New CellCoordinate(row, maxcol)
                    End If
                End If

            Next

        Else


            For col As Integer = 1 To maxcol

                If worksheet.Cells(maxrow, col).Value IsNot Nothing Then
                    If worksheet.Cells(maxrow, col).Value.ToString = value Then
                        Return New CellCoordinate(maxrow, col)
                    End If
                End If

            Next



        End If


        

        Return Nothing

    End Function

    Public Overloads Function FindCellValue(ByVal maxrow As Int32, ByVal maxcol As Int32, ByVal value As String, ByVal findMode As FindMode) As CellCoordinate
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")

        If findMode = OfficeExcelHandler.FindMode.Table Then

            For row As Integer = 1 To maxrow
                For col As Integer = 1 To maxcol

                    If m_sheet.Cells(row, col).Value IsNot Nothing Then
                        If m_sheet.Cells(row, col).Value.ToString = value Then
                            Return New CellCoordinate(row, col)
                        End If
                    End If

                Next
            Next

        ElseIf findMode = OfficeExcelHandler.FindMode.LockColumn Then

            For row As Integer = 1 To maxrow

                If m_sheet.Cells(row, maxcol).Value IsNot Nothing Then
                    If m_sheet.Cells(row, maxcol).Value.ToString = value Then
                        Return New CellCoordinate(row, maxcol)
                    End If
                End If

            Next

        Else


            For col As Integer = 1 To maxcol

                If m_sheet.Cells(maxrow, col).Value IsNot Nothing Then
                    If m_sheet.Cells(maxrow, col).Value.ToString = value Then
                        Return New CellCoordinate(maxrow, col)
                    End If
                End If

            Next


        End If


        

        Return Nothing

    End Function



    Public Overloads Sub FillExcelCell(ByVal worksheet As Object, ByVal row As Int32, ByVal col As Int32, ByVal Value As Object, ByVal StyleName As String)
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Value Is Nothing Then
            Return
        End If



        worksheet.Cells(row, col) = Value




        Try
            Dim rng As Object = worksheet.Cells(row, col)
            rng.Select()
            rng.Style = StyleName
            rng.Columns.EntireColumn.AutoFit()
        Catch ex As Exception
            'non fa nulla
        End Try


        'rng.Borders.Weight = XlBorderWeight.xlThin
        'rng.Borders.LineStyle = XlLineStyle.xlContinuous
        'rng.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic
    End Sub


    Public Overloads Sub FillExcelCell(ByVal row As Int32, ByVal col As Int32, ByVal Value As Object, ByVal StyleName As String)
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Value Is Nothing Then
            Return
        End If



        m_sheet.Cells(row, col) = Value




        Try
            Dim rng As Object = m_sheet.Cells(row, col)
            rng.Select()
            rng.Style = StyleName
            rng.Columns.EntireColumn.AutoFit()
        Catch ex As Exception
            'non fa nulla
        End Try


        'rng.Borders.Weight = XlBorderWeight.xlThin
        'rng.Borders.LineStyle = XlLineStyle.xlContinuous
        'rng.Borders.ColorIndex = XlColorIndex.xlColorIndexAutomatic
    End Sub



    Public Overloads Sub FillExcelCell(ByVal worksheet As Object, ByVal row As Int32, ByVal col As Int32, ByVal Value As Object)
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Value Is Nothing Then
            Return
        End If

        worksheet.Cells(row, col) = Value



    End Sub



    Public Overloads Sub FillExcelCell(ByVal row As Int32, ByVal col As Int32, ByVal Value As Object)
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Value Is Nothing Then
            Return
        End If

        m_sheet.Cells(row, col) = Value



    End Sub

    Public Overloads Sub FillExcelRow(ByVal worksheet As Object, ByVal row As Int32, ByVal Values As Object(), ByVal styleName As String, Optional ByVal offSet As Int32 = 0)
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If


        If offSet < 0 Then
            offSet = 0
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False


        For i = 0 To Values.Length - 1
            FillExcelCell(worksheet, row, offSet + i + 1, Values(i), styleName)
            RaiseEvent RowInserted(row, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub



    Public Overloads Sub FillExcelRow(ByVal row As Int32, ByVal Values As Object(), ByVal styleName As String, Optional ByVal offSet As Int32 = 0)
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False

        If offSet < 0 Then
            offSet = 0
        End If

        For i = 0 To Values.Length - 1
            FillExcelCell(m_sheet, row, offSet + i + 1, Values(i), styleName)
            RaiseEvent RowInserted(row, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub

    Public Overloads Sub FillExcelRow(ByVal worksheet As Object, ByVal row As Int32, ByVal Values As Object(), Optional ByVal offSet As Int32 = 0)
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False

        If offSet < 0 Then
            offSet = 0
        End If

        For i = 0 To Values.Length - 1
            FillExcelCell(worksheet, row, offSet + i + 1, Values(i))
            RaiseEvent RowInserted(row, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub





    Public Overloads Sub FillExcelRow(ByVal row As Int32, ByVal Values As Object(), Optional ByVal offSet As Int32 = 0)
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False

        If offSet < 0 Then
            offSet = 0
        End If

        For i = 0 To Values.Length - 1
            FillExcelCell(m_sheet, row, offSet + i + 1, Values(i))
            RaiseEvent RowInserted(row, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub

    Public Overloads Sub FillExcelColumn(ByVal worksheet As Object, ByVal column As Int32, ByVal Values As Object(), ByVal styleName As String, Optional ByVal offSet As Int32 = 0)
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If


        If offSet < 0 Then
            offSet = 0
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False


        For i = 0 To Values.Length - 1
            FillExcelCell(worksheet, offSet + i + 1, column, Values(i), styleName)
            RaiseEvent RowInserted(column, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub

    Public Overloads Sub FillExcelColumn(ByVal column As Int32, ByVal Values As Object(), ByVal styleName As String, Optional ByVal offSet As Int32 = 0)
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False

        If offSet < 0 Then
            offSet = 0
        End If

        For i = 0 To Values.Length - 1
            FillExcelCell(m_sheet, offSet + i + 1, column, Values(i), styleName)
            RaiseEvent RowInserted(column, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub

    Public Overloads Sub FillExcelColumn(ByVal column As Int32, ByVal Values As Object(), Optional ByVal offSet As Int32 = 0)
        If m_sheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False

        If offSet < 0 Then
            offSet = 0
        End If

        For i = 0 To Values.Length - 1
            FillExcelCell(m_sheet, offSet + i + 1, column, Values(i))
            RaiseEvent RowInserted(column, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub

    Public Overloads Sub FillExcelColumn(ByVal worksheet As Object, ByVal column As Int32, ByVal Values As Object(), Optional ByVal offSet As Int32 = 0)
        If worksheet Is Nothing Then Throw New InvalidOperationException("Workshhet nullo!")
        If Values Is Nothing Then
            Return
        End If

        Dim i As Int32 = 0
        Dim cancel As Boolean = False

        If offSet < 0 Then
            offSet = 0
        End If

        For i = 0 To Values.Length - 1
            FillExcelCell(worksheet, offSet + i + 1, column, Values(i))
            RaiseEvent RowInserted(column, cancel)
            If cancel Then
                Exit Sub
            End If
        Next

    End Sub

    Private disposedValue As Boolean = False        ' Per rilevare chiamate ridondanti

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: liberare altro stato (oggetti gestiti).
                Me.CloseWorkbook()
                Me.QuitApplication()
                Thread.CurrentThread.CurrentCulture = OriginalCulture
            End If

            ' TODO: liberare lo stato personale (oggetti non gestiti).
            ' TODO: impostare campi di grandi dimensioni su null.
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



    Public Class CellCoordinate

        Public Sub New(ByVal Row As Int32, ByVal Column As Int32)
            m_Y = Column
            m_X = Row
        End Sub



        Private m_Y As Int32

        Public Property Column() As Int32
            Get
                Return m_Y
            End Get
            Set(ByVal value As Int32)
                m_Y = value
            End Set
        End Property


        Private m_X As Int32
        Public Property Row() As Int32
            Get
                Return m_X
            End Get
            Set(ByVal value As Int32)
                m_X = value
            End Set
        End Property

    End Class


    Public Enum FindMode
        Table
        LockColumn
        LockRow
    End Enum

End Class
