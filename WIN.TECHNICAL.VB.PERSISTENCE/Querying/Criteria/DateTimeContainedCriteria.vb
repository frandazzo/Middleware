Public Class DateTimeContainedCriteria
    Inherits AbstractBoolCriteria
    Private criterion As AbstractBoolCriteria = Nothing

    Public Sub New(ByVal ColumnName As String, ByVal Start As DateTime, ByVal Finish As DateTime)
        MyBase.m_Column = ColumnName
        'criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "#" & Format(Start, "MM/dd/yyyy hh:mm:ss") & "#"), Criteria.LessEqualThan(ColumnName, "#" & Format(Finish, "MM/dd/yyyy hh:mm:ss") & "#"))
        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, DateString(Start, False, DB.DBType.Access)), Criteria.LessEqualThan(ColumnName, DateString(Finish, False, DB.DBType.Access)))

    End Sub

    Public Sub New(ByVal ColumnName As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType)
        MyBase.m_Column = ColumnName
        'Select Case DBType
        '    Case DB.DBType.Access
        '        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "#" & Format(Start, "MM/dd/yyyy hh:mm:ss") & "#"), Criteria.LessEqualThan(ColumnName, "#" & Format(Finish, "MM/dd/yyyy hh:mm:ss") & "#"))
        '    Case DB.DBType.SqlServer
        '        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "'" & Format(Start, "dd/MM/yyyy hh:mm:ss.fff") & "'"), Criteria.LessThan(ColumnName, "'" & Format(Finish, "dd/MM/yyyy hh:mm:ss.fff") & "'"))

        '    Case DB.DBType.MySql
        '        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "'" & Format(Start, "yyyy-MM-dd hh.mm.ss") & "'"), Criteria.LessEqualThan(ColumnName, "'" & Format(Finish, "yyyy-MM-dd hh.mm.ss") & "'"))

        'End Select
        criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, DateString(Start, False, DBType)), Criteria.LessEqualThan(ColumnName, DateString(Finish, False, DBType)))


    End Sub
    Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
        Throw New Exception("Not implemented metod")
    End Sub
    Public Overrides Function GenerateSql() As String
        Return criterion.GenerateSql
    End Function


End Class
