Public Class DataRangeStrictlyIntersectsCriteria
    Inherits AbstractBoolCriteria
    Private criterion As AbstractBoolCriteria = Nothing

    Public Sub New(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime)
        criterion = New AndExp(Criteria.GreaterThan(ColumnNameFinish, DateString(Start, False, DB.DBType.Access)), Criteria.LessThan(ColumnNameStart, DateString(Finish, False, DB.DBType.Access)))
    End Sub
    Public Sub New(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType)
        criterion = New AndExp(Criteria.GreaterThan(ColumnNameFinish, DateString(Start, False, DBType)), Criteria.LessThan(ColumnNameStart, DateString(Finish, False, DBType)))
    End Sub

    Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
        Throw New Exception("Not implemented method")
    End Sub

    Public Overrides Function GenerateSql() As String
        Return criterion.GenerateSql
    End Function
End Class
