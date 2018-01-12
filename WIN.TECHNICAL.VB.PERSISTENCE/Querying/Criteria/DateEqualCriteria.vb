﻿Public Class DateEqualCriteria
    Inherits AbstractBoolCriteria
    Private criterion As AbstractBoolCriteria = Nothing

    'Public Sub New(ByVal ColumnName As String, ByVal Start As DateTime)
    '    MyBase.m_Column = ColumnName
    '    criterion = Criteria.Equal(ColumnName, "#" & Format(Start, "MM/dd/yyyy") & "#")
    '    'Select Case DbType
    '    '   Case DB.DBType.Access
    '    '      criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "#" & Format(Start, "MM/dd/yyyy") & "#"), Criteria.LessEqualThan(ColumnName, "#" & Format(Finish, "MM/dd/yyyy") & "#"))
    '    '   Case DB.DBType.SqlServer
    '    '      criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "'" & Format(Start, "dd/MM/yyyy") & "'"), Criteria.LessEqualThan(ColumnName, "'" & Format(Finish, "dd/MM/yyyy") & "'"))

    '    '   Case DB.DBType.MySql
    '    '      criterion = New AndExp(Criteria.GreaterEqualThan(ColumnName, "'" & Format(Start, "yyyy-MM-dd") & "'"), Criteria.LessEqualThan(ColumnName, "'" & Format(Finish, "yyyy-MM-dd") & "'"))

    '    'End Select
    'End Sub

    Public Sub New(ByVal ColumnName As String, ByVal Start As DateTime, ByVal DBType As DB.DBType)
        MyBase.m_Column = ColumnName
        'Select Case DBType
        '    Case DB.DBType.Access
        '        criterion = Criteria.Equal(ColumnName, "#" & Format(Start, "MM/dd/yyyy") & "#")
        '    Case DB.DBType.SqlServer
        '        criterion = Criteria.Equal(ColumnName, "'" & Format(Start, "dd/MM/yyyy") & "'")
        '    Case DB.DBType.MySql
        '        criterion = Criteria.Equal(ColumnName, "'" & Format(Start, "yyyy-MM-dd") & "'")

        'End Select

        criterion = Criteria.Equal(ColumnName, DateString(Start, True, DBType))


    End Sub
    Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
        Throw New Exception("Not implemented metod")
    End Sub
    Public Overrides Function GenerateSql() As String
        Return criterion.GenerateSql
    End Function


End Class
