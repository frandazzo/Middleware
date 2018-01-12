Public MustInherit Class AbstractPeriodo
   Protected m_DataRange As DataRange
   Protected m_ConfrontoStrategy As IConfrontoPeriodi
   Public Overridable Function GetDataRange() As DataRange
      Return m_DataRange
   End Function
   Public MustOverride Overrides Function ToString() As String
   Public Overrides Function Equals(ByVal obj As Object) As Boolean
      Try
         If obj Is System.DBNull.Value Then Return False
         Dim temp As AbstractPeriodo = DirectCast(obj, AbstractPeriodo)
         Return m_DataRange.Equals(temp.GetDataRange)
      Catch ex As Exception
         Throw New Exception("Non è possibile verificare l'uguaglianza con il DataRange poichè l'oggetto di confronto non è un DataRange ")
      End Try
   End Function
   Public MustOverride Sub MergePeriod(ByVal Period As AbstractPeriodo)
   Public MustOverride Function GetNumberOfPeriods() As Integer
   Protected MustOverride Function CalcolaDataFinePeriodo() As DateTime
   Public MustOverride Function Overlaps(ByVal Periodo As AbstractPeriodo) As Boolean
   Public MustOverride Function Includes(ByVal DataRange As DataRange) As Boolean
   Public MustOverride Function GetNumberOfCommonPeriods(ByVal Periodo As AbstractPeriodo) As Integer
   Public MustOverride Function GetNextPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo
   Public MustOverride Function GetPreviousPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo




End Class
