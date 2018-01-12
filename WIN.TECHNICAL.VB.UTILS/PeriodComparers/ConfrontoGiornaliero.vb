Public Class ConfrontoGiornaliero
   Implements IConfrontoPeriodi

   Private m_Range As DataRange

   Public Overloads Function CreaListaPeriodi(ByVal DataRange As DataRange) As ArrayList Implements IConfrontoPeriodi.CreaListaPeriodi
      Dim datai As DateTime = DataRange.Start
      Dim dataf As DateTime = DataRange.Finish.AddDays(1)
      Dim ListOfPeriod As New ArrayList
      m_Range = New DataRange(datai, dataf.AddDays(-1))
      Do While datai <> dataf
         ListOfPeriod.Add(New PeriodoGiornaliero(datai))
         datai = datai.AddDays(1)
      Loop

      Return ListOfPeriod
   End Function
   Public Function CreaPeriodoSuccessivo(ByVal NumberOfPeriodUnits As Integer, ByVal DataRange As DataRange) As DataRange Implements IConfrontoPeriodi.CreaPeriodoSuccessivo
      Dim start As DateTime = DataRange.Finish
      Dim finish As DateTime = start.AddDays(NumberOfPeriodUnits)
      Return New DataRange(start.AddDays(1), finish)
   End Function
   Public Function CreaPeriodoPrecedente(ByVal NumberOfPeriodUnits As Integer, ByVal DataRange As DataRange) As DataRange Implements IConfrontoPeriodi.CreaPeriodoPrecedente

      Dim start As DateTime = DataRange.Start.AddDays(NumberOfPeriodUnits)
      Dim finish As DateTime = DataRange.Start.AddDays(-1)

      Return New DataRange(start, finish)
   End Function

   Public Function GetDataRange() As DataRange Implements IConfrontoPeriodi.GetDataRange
      Return m_Range
   End Function


End Class
