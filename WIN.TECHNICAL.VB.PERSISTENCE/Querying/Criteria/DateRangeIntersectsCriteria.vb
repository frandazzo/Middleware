Public Class DateRangeIntersectsCriteria
   Inherits AbstractBoolCriteria
   Private criterion As AbstractBoolCriteria = Nothing

   Public Sub New(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime)
        'criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "#" & Format(Start, "MM/dd/yyyy hh:mm:ss") & "#"), Criteria.LessEqualThan(ColumnNameStart, "#" & Format(Finish, "MM/dd/yyyy hh:mm:ss") & "#"))
        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, DateString(Start, False, DB.DBType.Access)), Criteria.LessEqualThan(ColumnNameStart, DateString(Finish, False, DB.DBType.Access)))
   End Sub
   Public Sub New(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType)
        'Select Case DBType
        '   Case DB.DBType.Access
        '          criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "#" & Format(Start, "MM/dd/yyyy hh:mm:ss") & "#"), Criteria.LessEqualThan(ColumnNameStart, "#" & Format(Finish, "MM/dd/yyyy hh:mm:ss") & "#"))
        '   Case DB.DBType.SqlServer
        '          criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "'" & Format(Start, "dd/MM/yyyy hh:mm:ss.fff") & "'"), Criteria.LessEqualThan(ColumnNameStart, "'" & Format(Finish, "dd/MM/yyyy") & "'"))
        '   Case DB.DBType.MySql
        '          criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "'" & Format(Start, "yyyy-MM-dd hh.mm.ss") & "'"), Criteria.LessEqualThan(ColumnNameStart, "'" & Format(Finish, "yyyy-MM-dd hh.mm.ss") & "'"))
        '  End Select


        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, DateString(Start, False, DBType)), Criteria.LessEqualThan(ColumnNameStart, DateString(Finish, False, DBType)))
   End Sub

   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      Throw New Exception("Not implemented method")
   End Sub

   Public Overrides Function GenerateSql() As String
      Return criterion.GenerateSql
   End Function
End Class
