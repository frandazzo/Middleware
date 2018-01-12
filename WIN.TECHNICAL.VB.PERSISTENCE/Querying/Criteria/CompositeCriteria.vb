Public Class CompositeCriteria
   Inherits AbstractBoolCriteria
   Protected List As New ArrayList
   Protected m_TipoOperatoreBool As String

   Public Sub New(ByVal TipoOperatore As AbstractBoolCriteria.Operatore)
      Select Case TipoOperatore
         Case Operatore.AndOp
            m_TipoOperatoreBool = " AND "
         Case Operatore.OrOp
            m_TipoOperatoreBool = " OR "
         Case Else
            Throw New Exception("Operatore sconosciuto")
      End Select

   End Sub
   Public Overrides Function GenerateSql() As String
      Dim sql As String = " ( "
      If List.Count = 0 Then Return " "
      For i As Int32 = 0 To List.Count - 1
         If i = 0 Then
            sql = sql & List.Item(0).GenerateSql
         Else
            sql = sql & m_TipoOperatoreBool & List.Item(i).GenerateSql
         End If
      Next
      sql = sql & " ) "
      Return sql
   End Function



   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      List.Add(Criteria)
   End Sub
End Class
