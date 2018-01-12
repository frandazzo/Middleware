Public Interface IGeoLocationLoader
   Function GetComuneById(ByVal id As Int32) As Comune
   Function GetComuneByName(ByVal name As String) As Comune
   Function GetComuneByFiscalCode(ByVal code As String) As IList
   Function GetProvincie() As IList

   Function GetProvinciaById(ByVal id As Integer) As Provincia
   Function GetRegioni() As IList
   Function GetNazionByFiscalCode(ByVal code As String) As Nazione
   Function GetNazioni() As IList
End Interface
