Public Class Helper
   Private Shared waitingForm As WaitingForm = Nothing

   Public Shared Sub ShowWaitBox(ByVal message As String, ByVal Image As Image)
      If (waitingForm IsNot Nothing) Then
         Return
      End If


      waitingForm = New WaitingForm()
      waitingForm.BackgroundImage = Image
      If (message IsNot Nothing) Then
         waitingForm.StatusText = message
      End If


        waitingForm.Show()
        System.Windows.Forms.Application.DoEvents()
   End Sub



   Public Shared Sub HideWaitBox()
      If (waitingForm Is Nothing) Then
         Return
      End If
      waitingForm.Close()
      waitingForm = Nothing
   End Sub

End Class