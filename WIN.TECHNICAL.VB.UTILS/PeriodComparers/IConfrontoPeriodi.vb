Public Interface IConfrontoPeriodi
   Enum UnitaDiCompetenza
      Mensile
      Giornaliera
   End Enum
   Function CreaListaPeriodi(ByVal DataRange As DataRange) As ArrayList
   Function CreaPeriodoSuccessivo(ByVal NumberOfPeriodUnits As Integer, ByVal DataRange As DataRange) As DataRange
   Function GetDataRange() As DataRange
   Function CreaPeriodoPrecedente(ByVal NumberOfPeriodUnits As Integer, ByVal DataRange As DataRange) As DataRange
End Interface
