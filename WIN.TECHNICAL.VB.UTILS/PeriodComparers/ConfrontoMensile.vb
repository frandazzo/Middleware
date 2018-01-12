Public Class ConfrontoMensile
   Implements IConfrontoPeriodi

   Private m_Range As DataRange
   Public Function CreaListaPeriodi(ByVal DataRange As DataRange) As ArrayList Implements IConfrontoPeriodi.CreaListaPeriodi
      Dim mesei As Integer = DataRange.Start.Month
      Dim esei As Integer = DataRange.Start.Year
      Dim mesef As Integer = DataRange.Finish.Month
      Dim esef As Integer = DataRange.Finish.Year
      Dim TempMesef As Integer
      m_Range = New DataRange(New DateTime(esei, mesei, 1), New DateTime(esef, mesef, Date.DaysInMonth(esef, mesef)))
      Dim ListOfPeriod As New ArrayList
      If esei = esef Then
         TempMesef = mesef
      Else
         TempMesef = 12
      End If
      For j As Integer = esei To esef
         If j = esef Then
            TempMesef = mesef
         Else
            TempMesef = 12
         End If
         For i As Integer = mesei To TempMesef
            ListOfPeriod.Add(New PeriodoMensile(i, j))
         Next
         mesei = 1

      Next
      Return ListOfPeriod
   End Function
   Public Function CreaPeriodoSuccessivo(ByVal NumberOfPeriodUnits As Integer, ByVal DataRange As DataRange) As DataRange Implements IConfrontoPeriodi.CreaPeriodoSuccessivo
      Dim start As DateTime = DataRange.Finish
      Dim tempData As DateTime = start.AddMonths(NumberOfPeriodUnits)
      Dim finish As DateTime = tempData
      Return New DataRange(start.AddDays(1), finish)
   End Function
   Public Function CreaPeriodoPrecedente(ByVal NumberOfPeriodUnits As Integer, ByVal DataRange As DataRange) As DataRange Implements IConfrontoPeriodi.CreaPeriodoPrecedente
      Dim start As DateTime = DataRange.Start.AddMonths(NumberOfPeriodUnits)
      Dim finish As DateTime = DataRange.Start.AddDays(-1)
      Return New DataRange(start, finish)
   End Function

   Public Function GetDataRange() As DataRange Implements IConfrontoPeriodi.GetDataRange
      Return m_Range
   End Function

 
End Class
