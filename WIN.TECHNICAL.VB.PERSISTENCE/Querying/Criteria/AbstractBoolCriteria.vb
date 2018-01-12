Public MustInherit Class AbstractBoolCriteria
   Protected m_Column As String
   Protected m_Value As String
   Public MustOverride Function GenerateSql() As String
   Public Enum Operatore
      AndOp
      OrOp
      NotOp
   End Enum
   Public MustOverride Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)

    Protected Function DateString(ByVal dateVal As DateTime, ByVal dateOnly As Boolean, ByVal type As DB.DBType) As String
        Return DateUtils.OverlapWithDBCharacter(DateUtils.FormatItalianDateString(dateVal, dateOnly, type), type)
    End Function
End Class
