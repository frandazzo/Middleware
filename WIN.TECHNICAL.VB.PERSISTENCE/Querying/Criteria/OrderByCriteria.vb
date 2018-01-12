Public Class OrderByCriteria
   Inherits AbstractBoolCriteria
   Protected m_OrderList As New ArrayList


   Public Overrides Sub AddCriteria(ByVal Criteria As AbstractBoolCriteria)
      m_OrderList.Add(Criteria)
   End Sub

   Public Overrides Function GenerateSql() As String
      Dim orderClause As String = " ORDER BY "
      If m_OrderList.Count = 0 Then Return ""
      If m_OrderList.Count = 1 Then Return orderClause & m_OrderList.Item(0).GenerateSql
      Dim orderColumns As String = ""
      Dim i As Integer = 0
      For i = 0 To m_OrderList.Count - 1
         Dim cr As AbstractBoolCriteria = m_OrderList(i)
         orderColumns = orderColumns & cr.GenerateSql & IIf(i = m_OrderList.Count - 1, "", ",")
      Next
      Return orderClause & orderColumns
   End Function
End Class
