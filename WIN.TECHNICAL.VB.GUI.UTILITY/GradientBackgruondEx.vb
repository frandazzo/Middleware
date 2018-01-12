Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design
Imports System.Windows.Forms
' An improved version of the GradientBackground control
' with a new RotateAngle property

Public Class GradientBackgroundEx
   Inherits GradientBackground

   Dim m_RotateAngle As Single

   <Description("The rotation angle for the gradient brush"), DefaultValue(0), _
    Editor(GetType(RotateAngleEditor), GetType(UITypeEditor))> _
   Property RotateAngle() As Single
      Get
         Return m_RotateAngle
      End Get
      Set(ByVal Value As Single)
         m_RotateAngle = Value
         Me.Invalidate()
      End Set
   End Property

   ' redefine the OnPaint event to account for the new property.

   Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
      ' Create a gradient brush as large as the client area, with specified
      ' start/end color and gradient mode
      Dim br As New Drawing2D.LinearGradientBrush(Me.ClientRectangle, Me.StartColor, Me.EndColor, Me.GradientMode)
      ' apply the rotation angle
      br.RotateTransform(Me.RotateAngle)
      ' paint the background.
      e.Graphics.FillRectangle(br, Me.ClientRectangle)
      ' orderly destroy the brush.
      br.Dispose()
   End Sub

   '------------------------------------------------------------------------
   ' The Editor for the RotateAngle property
   '------------------------------------------------------------------------

   Class RotateAngleEditor
      Inherits UITypeEditor

      ' Override the GetEditStyle to tell that this editor supports DropDown style.
      ' The context object passed to this function provides information about the
      ' context, and context.Instance is the control being editor.

      Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
         If Not (context Is Nothing) AndAlso Not (context.Instance Is Nothing) Then
            Return UITypeEditorEditStyle.DropDown
         End If
         Return MyBase.GetEditStyle(context)
      End Function

      ' This is the TrackBar control that will be displayed in the editor
      Dim WithEvents tb As TrackBar
      ' this the the object that represent the service in the editor that
      ' creates the dropdown portion or shows a dialog.
      Dim wfes As IWindowsFormsEditorService

      ' Override the EditValue function.
      ' The Provider argument lets you query for an IWindowsFormsEditorService object, which in
      ' turn lets you access the Properties window, and do things such as opening/closing the
      ' dropdown area,or display a modal form
      ' The Value argument is the current value for the property.
      ' The return value must be the new value of the property.

      Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
         ' Exit without changing the value if no context, instance, or provider is provided
         If (context Is Nothing) OrElse (context.Instance Is Nothing) OrElse (provider Is Nothing) Then
            Return value
         End If
         ' get the Editor Service object, exit if not there.
         wfes = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
         If (wfes Is Nothing) Then
            Return value
         End If

         ' create the TrackBar control, set its properties
         tb = New TrackBar()
         tb.Size = New Size(50, 150)
         tb.TickStyle = TickStyle.TopLeft
         tb.TickFrequency = 45
         tb.SetRange(0, 360)
         tb.Orientation = Orientation.Vertical
         ' initalize Value
         tb.Value = CInt(value)

         ' show the control (this call won't call until the dropdown area is closed
         wfes.DropDownControl(tb)

         ' the return value must be of the correct type
         EditValue = CSng(tb.Value)
         ' destroy the trackbar control
         tb.Dispose()
         tb = Nothing
      End Function

      ' Close the dropdown area when the mouse button is released
      Private Sub TB_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles tb.MouseUp
         If Not (wfes Is Nothing) Then
            wfes.CloseDropDown()
         End If
      End Sub

      ' Let the property editor know that we want to paint the value.
      Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
         ' In this demo we return True regardless of the actual editor
         Return True
      End Function

      ' Display a yellow circle to the left of the property value in the Properties window
      ' with a black line forming the same angle as the value of the RotateAngle property.
      Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
         ' Get the angle in radians.
         Dim a As Single = CSng(e.Value) * CSng(Math.PI) / 180.0!
         ' Get the rectangle in which we can draw.
         Dim rect As Rectangle = e.Bounds
         ' Evaluate the radius of the circle
         Dim r As Single = Math.Min(rect.Width, rect.Height) / 2.0!
         ' Get the center point.
         Dim p1 As New PointF(rect.Width / 2.0!, rect.Height / 2.0!)
         ' Calculate where the line should end.
         Dim p2 As New PointF(CSng(p1.X + Math.Cos(a) * r), CSng(p1.Y + Math.Sin(a) * r))
         ' Draw the yellow filled circle.
         e.Graphics.FillEllipse(Brushes.Yellow, rect.Width / 2.0! - r, rect.Height / 2.0! - r, r * 2, r * 2)
         ' Draw the line.
         e.Graphics.DrawLine(Pens.Black, p1, p2)
      End Sub

   End Class

End Class


