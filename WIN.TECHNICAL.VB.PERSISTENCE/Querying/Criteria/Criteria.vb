Public Class Criteria
   Inherits AbstractBoolCriteria
   Protected m_SqlOperator As String
   

   Public Sub New(ByVal SqlOperator As String, ByVal ColumnName As String, ByVal Value As String)
      m_SqlOperator = SqlOperator
      m_Column = ColumnName
      m_Value = Value
   End Sub
   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      Throw New Exception("Not implemented method")
   End Sub

   Public Overrides Function GenerateSql() As String
      Return m_Column & m_SqlOperator & m_Value
   End Function

   Public Shared Function GreaterThan(ByVal ColumnName As String, ByVal Value As String) As Criteria
      Return New Criteria(" > ", ColumnName, Value)
   End Function
   Public Shared Function LessThan(ByVal ColumnName As String, ByVal Value As String) As Criteria
      Return New Criteria(" < ", ColumnName, Value)
   End Function
   Public Shared Function GreaterEqualThan(ByVal ColumnName As String, ByVal Value As String) As Criteria
      Return New Criteria(" >= ", ColumnName, Value)
   End Function
   Public Shared Function LessEqualThan(ByVal ColumnName As String, ByVal Value As String) As Criteria
      Return New Criteria(" <= ", ColumnName, Value)
   End Function
   Public Shared Function Equal(ByVal ColumnName As String, ByVal Value As String) As Criteria
      Return New Criteria(" = ", ColumnName, Value)
   End Function
   Public Shared Function Matches(ByVal ColumnName As String, ByVal Pattern As String, ByVal DbType As DB.DBType) As MatchingCriteria
      Return New MatchingCriteria(ColumnName, Pattern, DbType)
    End Function
    Public Shared Function MatchesNoUpperCase(ByVal ColumnName As String, ByVal Pattern As String, ByVal DbType As DB.DBType) As MatchNoUpperCaseCriteria
        Return New MatchNoUpperCaseCriteria(ColumnName, Pattern, DbType)
    End Function
    Public Shared Function MatchesEqual(ByVal ColumnName As String, ByVal Pattern As String, ByVal DbType As DB.DBType) As MatchEqualCriteria
        Return New MatchEqualCriteria(ColumnName, Pattern, DbType)
    End Function
    Public Shared Function MatchesEqualNoUpperCase(ByVal ColumnName As String, ByVal Pattern As String, ByVal DbType As DB.DBType) As MatchEqualNoUpperCaseCriteria
        Return New MatchEqualNoUpperCaseCriteria(ColumnName, Pattern, DbType)
    End Function
   Public Shared Function IsNull(ByVal ColumnName As String) As Criteria
      Return New Criteria(" IS ", ColumnName, "NULL")
   End Function
   Public Shared Function SortCriteria(ByVal ColumnName As String, ByVal Ascending As Boolean) As AbstractBoolCriteria
      Return New SortCriteria(ColumnName, Ascending)
   End Function
   Public Shared Function NotIsNull(ByVal ColumnName As String) As Criteria
      Return New Criteria(" IS NOT ", ColumnName, "NULL")
   End Function
   Public Overloads Shared Function INClause(ByVal ColumnName As String, ByVal Pattern As String) As Criteria
      Return New Criteria(" IN ", ColumnName, "(" & Pattern & ")")
   End Function
   Public Overloads Shared Function INClause(ByVal ColumnName As String, ByVal SubQuery As SubQueryCriteria) As AbstractBoolCriteria
      Return New Criteria(" IN ", ColumnName, "(" & SubQuery.GenerateSql & ")")
   End Function
   Public Overloads Shared Function DateContained(ByVal ColumnName As String, ByVal Start As DateTime, ByVal Finish As DateTime) As DateContainedCriteria
      Return New DateContainedCriteria(ColumnName, Start, Finish)
   End Function
   Public Overloads Shared Function DateRangeIntersects(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime) As DateRangeIntersectsCriteria
      Return New DateRangeIntersectsCriteria(ColumnNameStart, ColumnNameFinish, Start, Finish)
    End Function
    Public Overloads Shared Function DateRangeStrictlyIntersects(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType) As DataRangeStrictlyIntersectsCriteria
        Return New DataRangeStrictlyIntersectsCriteria(ColumnNameStart, ColumnNameFinish, Start, Finish, DBType)
    End Function
   Public Overloads Shared Function DateRangeContained(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime) As DateRangeContainedCriteria
      Return New DateRangeContainedCriteria(ColumnNameStart, ColumnNameFinish, Start, Finish)
    End Function

    Public Shared Function DateEqual(ByVal ColumnNameStart As String, ByVal Start As DateTime, ByVal DBType As DB.DBType) As DateEqualCriteria
        Return New DateEqualCriteria(ColumnNameStart, Start, DBType)
    End Function
    Public Overloads Shared Function DateTimeContained(ByVal ColumnName As String, ByVal Start As DateTime, ByVal Finish As DateTime) As DateTimeContainedCriteria
        Return New DateTimeContainedCriteria(ColumnName, Start, Finish)
    End Function
    Public Overloads Shared Function DateTimeContained(ByVal ColumnName As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType) As DateTimeContainedCriteria
        Return New DateTimeContainedCriteria(ColumnName, Start, Finish, DBType)
    End Function
   Public Overloads Shared Function DateContained(ByVal ColumnName As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType) As DateContainedCriteria
      Return New DateContainedCriteria(ColumnName, Start, Finish, DBType)
   End Function
   Public Overloads Shared Function DateRangeIntersects(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType) As DateRangeIntersectsCriteria
      Return New DateRangeIntersectsCriteria(ColumnNameStart, ColumnNameFinish, Start, Finish, DBType)
   End Function
   Public Overloads Shared Function DateRangeContained(ByVal ColumnNameStart As String, ByVal ColumnNameFinish As String, ByVal Start As DateTime, ByVal Finish As DateTime, ByVal DBType As DB.DBType) As DateRangeContainedCriteria
      Return New DateRangeContainedCriteria(ColumnNameStart, ColumnNameFinish, Start, Finish, DBType)
   End Function
End Class
