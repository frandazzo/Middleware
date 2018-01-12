Public Class SortCriteria
   Inherits AbstractBoolCriteria
   Private m_Ascending As Boolean = True
   Public Sub New(ByVal ColumnName As String, ByVal Ascending As Boolean)
      MyBase.m_Column = ColumnName
      m_Ascending = Ascending
   End Sub

   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      Throw New InvalidOperationException("Not implemented method")
   End Sub

   Public Overrides Function GenerateSql() As String
      Return " " & MyBase.m_Column & IIf(m_Ascending, " ", " DESC ")
   End Function
End Class
