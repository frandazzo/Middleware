Public Class DateRangeContainedCriteria
   Inherits AbstractBoolCriteria
   Private criterion As AbstractBoolCriteria = Nothing

   Public Sub New(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime)
        'criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "#" & Format(Finish, "MM/dd/yyyy hh:mm:ss") & "#"), Criteria.LessEqualThan(ColumnNameStart, "#" & Format(Start, "MM/dd/yyyy hh:mm:ss") & "#"))
        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, DateString(Finish, False, DB.DBType.Access)), Criteria.LessEqualThan(ColumnNameStart, DateString(Start, False, DB.DBType.Access)))
   End Sub
   Public Sub New(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType)
        'Select Case DBType
        '   Case DB.DBType.Access
        '          criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "#" & Format(Finish, "MM/dd/yyyy hh:mm:ss") & "#"), Criteria.LessEqualThan(ColumnNameStart, "#" & Format(Start, "MM/dd/yyyy hh:mm:ss") & "#"))
        '   Case DB.DBType.SqlServer
        '          criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "'" & Format(Finish, "dd/MM/yyyy hh:mm:ss.fff") & "'"), Criteria.LessEqualThan(ColumnNameStart, "'" & Format(Start, "dd/MM/yyyy hh:mm:ss.fff") & "'"))


        '   Case DB.DBType.MySql
        '          criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, "'" & Format(Finish, "yyyy-MM-dd hh.mm.ss") & "'"), Criteria.LessEqualThan(ColumnNameStart, "'" & Format(Start, "yyyy-MM-dd hh.mm.ss") & "'"))




        'End Select

        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnNameFinish, DateString(Finish, False, DBType)), Criteria.LessEqualThan(ColumnNameStart, DateString(Start, False, DBType)))

   End Sub

   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      Throw New Exception("Not implemented method")
   End Sub

   Public Overrides Function GenerateSql() As String
      Return criterion.GenerateSql
   End Function
End Class
