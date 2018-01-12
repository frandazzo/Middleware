Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class GradientBackground
   Inherits System.Windows.Forms.Control

   '--------------------------------------------------------------
   ' The StartColor property
   '--------------------------------------------------------------

   Dim m_StartColor As Color = Color.Lavender

   <Description("The start color for the gradient")> _
   Property StartColor() As Color
      Get
         Return m_StartColor
      End Get
      Set(ByVal Value As Color)
         m_StartColor = Value
         ' Redraw the control when this property changes.
         Me.Invalidate()
      End Set
   End Property

   Sub ResetStartColor()
      m_StartColor = Color.Lavender
   End Sub

   Function ShouldSerializeStartColor() As Boolean
      Return Not m_StartColor.Equals(Color.Blue)
   End Function

   '--------------------------------------------------------------
   ' The EndColor property
   '--------------------------------------------------------------

   Dim m_EndColor As Color = Color.CornflowerBlue

   <Description("The end color for the gradient")> _
   Property EndColor() As Color
      Get
         Return m_EndColor
      End Get
      Set(ByVal Value As Color)
         m_EndColor = Value
         ' Redraw the control when this property changes.
         Me.Invalidate()
      End Set
   End Property

   Sub ResetEndColor()
      m_EndColor = Color.Lavender
   End Sub

   Function ShouldSerializeEndColor() As Boolean
      Return Not m_EndColor.Equals(Color.Black)
   End Function

   '--------------------------------------------------------------
   ' The GradientMode property
   '--------------------------------------------------------------

   Dim m_GradientMode As Drawing2D.LinearGradientMode = Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal

   <Description("The gradient mode"), DefaultValue(Drawing2D.LinearGradientMode.ForwardDiagonal)> _
   Overridable Property GradientMode() As Drawing2D.LinearGradientMode
      Get
         Return m_GradientMode
      End Get
      Set(ByVal Value As Drawing2D.LinearGradientMode)
         m_GradientMode = Value
         ' Redraw the control when this property changes.
         Me.Invalidate()
      End Set
   End Property

   ' render the control backgound

   Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
      ' let the base control do its stuff.
      MyBase.OnPaint(e)
      ' Create a gradient brush as large as the client area, with specified
      ' start/end color and gradient mode
      Dim br As New Drawing2D.LinearGradientBrush(Me.ClientRectangle, m_StartColor, m_EndColor, m_GradientMode)

      ' paint the background.
      e.Graphics.FillRectangle(br, Me.ClientRectangle)
      ' orderly destroy the brush.
      br.Dispose()
   End Sub

   Private Sub GradientBackground_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
      Me.Invalidate()
   End Sub
End Class


