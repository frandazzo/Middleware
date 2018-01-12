Imports System.Windows.Forms
Public Class WaitingForm
   Dim _positions As Integer = 2
   Dim _position As Integer = 0
   Dim _deltaX As Integer = 104
   Dim _deltaY As Integer = 66
   Dim _offsetX As Integer = 50
   Dim _size As Integer = 14




   Private _statusText As String

   Public Property StatusText() As String
      Get
            Return Label1.Text
      End Get
      Set(ByVal value As String)
            Label1.Text = value
      End Set
   End Property


   Dim _font As Font
   Dim _alpha As Alpha
   Dim _fontBrush As Brush

   Public Sub New()

      ' Chiamata richiesta da Progettazione Windows Form.
      InitializeComponent()

      ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
      SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Label1.Text = "Attendere..."
      _font = New Font(New FontFamily("Tahoma"), 9.0)

      _alpha = New Alpha(500)
      _alpha.Start()
      _fontBrush = New SolidBrush(Color.Black)
   End Sub




   Dim g As Graphics = Nothing
   Dim _textSize As SizeF = New SizeF()
   Dim _fillBrush As SolidBrush = New SolidBrush(Color.Black)

   Private Sub Painter()
      If (Not Me.Created) Then
         Return
      End If


      Try


         If (g Is Nothing) Then
            g = Me.CreateGraphics()
         End If


         Dim alpha As Double = 255 * _alpha.Value
         _fillBrush.Color = Color.FromArgb(Convert.ToInt32(alpha), Color.Black)

         If (_position > _positions) Then
            _position = 0
         End If


         Dim x As Integer = _deltaX + (_offsetX * _position)

         g.FillRectangle(Brushes.White, x, _deltaY, _size, _size)
         g.FillRectangle(_fillBrush, x, _deltaY, _size, _size)

            'If (_textSize.IsEmpty) Then
            '   _textSize = g.MeasureString(_statusText, _font)
            'End If


            'g.FillRectangle(Brushes.White, 1, Height - 20, Width - 2, 19)
            'g.DrawString(_statusText, _font, _fontBrush, Width - 5 - _textSize.Width, Height - 18)

         If (_alpha.Complete) Then
            g.FillRectangle(Brushes.White, x, _deltaY, _size, _size)
            _position = _position + 1
            _alpha.Start()
         End If


         System.Threading.Thread.Sleep(100)
         Painter()

      Catch ex As Exception

      End Try
   End Sub


   Private Sub WaitingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      Dim t As System.Threading.Thread = New System.Threading.Thread(AddressOf Painter)
      t.Start()
   End Sub
End Class