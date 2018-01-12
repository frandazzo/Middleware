Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Drawing2D.GraphicsPath


Public Class NiceGroupBox
    Inherits System.Windows.Forms.Panel

    Dim fromColor As Color
    Dim toColor As Color
    Dim topBar As Image
    Dim borderPen As Pen
    Dim _title As String
    Dim titleFont As Font
    Dim myRectangle As Rectangle
    Dim myTopRectangle As Rectangle
    Dim titleSize As SizeF
    Dim BoundsPath As GraphicsPath
    Dim myBrush As LinearGradientBrush
    Dim myTopBrush As LinearGradientBrush
    Dim BlurSize As Int32
    Dim bottomRectangle As RectangleF
    Dim TitleBarPath As GraphicsPath
    Dim backgroundColor As Color

    Public Sub New()

        'setting style variables
        BlurSize = 100
        backgroundColor = Color.FromArgb(245, 245, 245)
        fromColor = Color.FromArgb(237, 236, 237)
        toColor = backgroundColor
        borderPen = New Pen(Color.FromArgb(106, 145, 188))
        topBar = My.Resources.niceGroupBox_TopBar
        _title = "titolo di prova"
        titleFont = New Font("tahoma", 8.0F, FontStyle.Bold, GraphicsUnit.Point)
        TitleBarPath = New GraphicsPath
        titleSize = Nothing

        bottomRectangle = New RectangleF(0, 0, Me.Width, 20)
        myRectangle = New Rectangle(0, 0, Me.Width, Me.Height)
      myTopRectangle = New Rectangle(1, 0, Me.Width - 1, 16)
        myBrush = New LinearGradientBrush(myRectangle, fromColor, toColor, LinearGradientMode.Vertical)
      myTopBrush = New LinearGradientBrush(myTopRectangle, Color.Transparent, Color.FromArgb(135, 173, 228), LinearGradientMode.Vertical)
        BoundsPath = GetBoundsPath()

        SetStyle(Windows.Forms.ControlStyles.ResizeRedraw, True)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim g As Graphics = e.Graphics

        bottomRectangle = New RectangleF(0, 0, Me.Width, 20)
        myRectangle = New Rectangle(0, 0, Me.Width, Me.Height)
      myTopRectangle = New Rectangle(1, 0, Me.Width - 1, 16)
        myBrush = New LinearGradientBrush(myRectangle, fromColor, toColor, LinearGradientMode.Vertical)
      myTopBrush = New LinearGradientBrush(myTopRectangle, toColor, Color.FromArgb(135, 173, 228), LinearGradientMode.Vertical)

        BoundsPath = GetBoundsPath()

        g.Clear(backgroundColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.FillRectangle(myBrush, myRectangle)
        g.FillRectangle(myTopBrush, myTopRectangle)
        g.DrawPath(borderPen, BoundsPath)

        If (titleSize = Nothing) Then
            titleSize = g.MeasureString(_title, titleFont, Me.Width - 32)
        End If

        g.DrawString(_title, titleFont, New SolidBrush(Color.FromArgb(71, 97, 131)), Me.Width - titleSize.Width - 5, 2)
    End Sub

    Private Function GetBoundsPath() As GraphicsPath
        TitleBarPath = New GraphicsPath
        TitleBarPath.AddLine(0, 0, 0, Me.Height - bottomRectangle.Height)
        TitleBarPath.AddArc(0, Me.Height - bottomRectangle.Height, bottomRectangle.Height, bottomRectangle.Height - 1, 180.0F, -90.0F)
        TitleBarPath.AddLine(bottomRectangle.Height, Me.Height - 1, Me.Width - bottomRectangle.Height, Me.Height - 1)
        TitleBarPath.AddArc(Me.Width - bottomRectangle.Height - 1, Me.Height - bottomRectangle.Height, bottomRectangle.Height, bottomRectangle.Height - 1, 90.0F, -90.0F)
        TitleBarPath.AddLine(Me.Width - 1, Me.Height - bottomRectangle.Height, Me.Width - 1, 0)
        TitleBarPath.CloseFigure()

        Return TitleBarPath
    End Function

    Public Property Title() As String
        Get
            Return _title
        End Get

        Set(ByVal value As String)
            _title = value
        End Set
    End Property

End Class
