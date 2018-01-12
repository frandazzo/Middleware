Public Interface IBaseExcelReaderFactory
    Function GetReader(readerName As String, filename As String) As BaseExcelReader
End Interface
