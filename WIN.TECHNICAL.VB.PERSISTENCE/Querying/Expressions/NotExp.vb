Public Class NotExp
   Inherits AbstractBoolCriteria
   Protected m_Negato As AbstractBoolCriteria
   Public Sub New(ByVal Value As AbstractBoolCriteria)
      m_Negato = Value
   End Sub
   Public Overrides Function GenerateSql() As String
      Dim res As String = m_Negato.GenerateSql
      If res = " " Then Return " "
      Return "NOT " & res
   End Function

   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      Throw New Exception("Not implemented method")
   End Sub
End Class
