Imports System.Windows.Forms

Public Class UsabilityTabControl
   Inherits System.Windows.Forms.TabControl

   Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
      MyBase.OnPaint(e)

      'Inserire qui il codice personalizzato
   End Sub

   

   Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)

      'Dim tc As TabControl = DirectCast(sender, TabControl)
      Dim sf As New StringFormat
      sf.Alignment = StringAlignment.Center
      sf.LineAlignment = StringAlignment.Center
      sf.HotkeyPrefix = Drawing.Text.HotkeyPrefix.Show
      'If Me.SelectedIndex = e.Index Then
      '   e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds)
      'End If

      e.Graphics.DrawString(Me.TabPages(e.Index).Text, Me.Font, SystemBrushes.ControlText, RectangleF.op_Implicit(e.Bounds), sf)
      sf.Dispose()
      ''
   End Sub

   Protected Overrides Function ProcessMnemonic(ByVal charCode As Char) As Boolean
      For Each tp As TabPage In Me.TabPages
         If IsMnemonic(charCode, tp.Text) Then
            Me.SelectedTab = tp
            Return True
         End If
      Next
      Return MyBase.ProcessMnemonic(charCode)
   End Function


End Class
