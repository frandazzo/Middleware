Public Class MatchNoUpperCaseCriteria
    Inherits AbstractBoolCriteria
    Private m_dbtype As DB.DBType = DB.DBType.Access
    Public Sub New(ByVal ColumnName As String, ByVal Pattern As String, ByVal DBType As DB.DBType)
        MyBase.m_Column = ColumnName
        MyBase.m_Value = Replace(Pattern, "'", "''")
        m_dbtype = DBType
    End Sub

    Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
        Throw New Exception("Not implemented metod")
    End Sub

    Public Overrides Function GenerateSql() As String
        Select Case m_dbtype
            Case DB.DBType.Access
                Return "" & MyBase.m_Column & " LIKE '" & MyBase.m_Value & "%'"
            Case DB.DBType.SqlServer, DB.DBType.MySql
                Return "" & MyBase.m_Column & " LIKE '" & MyBase.m_Value & "%'"
            Case Else
                Throw New InvalidOperationException("Impossibile generare Sql per il criterio di matching. Tipo db sconosciuto")
        End Select

    End Function
End Class
