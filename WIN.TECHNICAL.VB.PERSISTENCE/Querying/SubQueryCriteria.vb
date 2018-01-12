Public Class SubQueryCriteria
   Inherits AbstractBoolCriteria
   Private m_TableName As String = ""
   Protected m_TopClauseNumber As Int32 = -1
   Protected m_DistinctClause As Boolean = False
   Private CompositeCriteria As AbstractBoolCriteria = New CompositeCriteria(Operatore.AndOp)
   Public Sub New(ByVal TableName As String, ByVal ColumnName As String)
      m_TableName = TableName
      MyBase.m_Column = ColumnName
   End Sub
   Public Sub New(ByVal TableName As String, ByVal ColumnName As String, ByVal EnableDistinctClause As Boolean)
      m_TableName = TableName
      MyBase.m_Column = ColumnName
      m_DistinctClause = EnableDistinctClause
   End Sub
   Public Sub New(ByVal TableName As String, ByVal ColumnName As String, ByVal TopClauseNumber As Int32)
      m_TableName = TableName
      MyBase.m_Column = ColumnName
      m_TopClauseNumber = TopClauseNumber
   End Sub
   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      CompositeCriteria.AddCriteria(Criteria)
   End Sub

   Public Overrides Function GenerateSql() As String
      Dim WhereClause As String = Trim(CompositeCriteria.GenerateSql)
      If WhereClause = "" Then
         If m_TopClauseNumber <= 0 Then
            If m_DistinctClause Then
               Return "Select distinct" & MyBase.m_Column & " from " & m_TableName
            End If
            Return "Select " & MyBase.m_Column & " from " & m_TableName
         Else
            Return "Select top " & m_TopClauseNumber.ToString & " " & MyBase.m_Column & " from " & m_TableName
         End If
      Else
         If m_TopClauseNumber <= 0 Then
            If m_DistinctClause Then
               Return "Select distinct " & MyBase.m_Column & " from " & m_TableName & " WHERE " & WhereClause
            End If
            Return "Select " & MyBase.m_Column & " from " & m_TableName & " WHERE " & WhereClause
         Else
            Return "Select top " & m_TopClauseNumber.ToString & " " & MyBase.m_Column & " from " & m_TableName & " WHERE " & WhereClause
         End If

      End If
   End Function
End Class
