Public Class CustomAssert
   Public Shared Sub Equal(ByVal ErrorMessage As String, ByVal arg1 As Object, ByVal arg2 As Object)
      If arg1.GetType Is arg2.GetType Then
         If Not arg1.Equals(arg2) Then
            Throw New Exception(ErrorMessage)
         End If
      Else
         Throw New Exception("Non è possibile effettuare un confronto tra oggetti diversi")
      End If
   End Sub
   Public Shared Sub IsTrue(ByVal ErrorMessage As String, ByVal arg1 As Boolean, ByVal arg2 As Boolean)

      If Not arg1 And arg2 Then
         Throw New Exception(ErrorMessage)
      End If

   End Sub
End Class
