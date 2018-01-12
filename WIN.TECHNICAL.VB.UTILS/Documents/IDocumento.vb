Public Interface IDocumento
   Property DataRegistrazione() As DateTime
   Property DataDocumento() As DateTime
   Property Note() As String
   Function GetId() As Int32
   ReadOnly Property TipoDocumento() As String
   Function ToString() As String

End Interface
