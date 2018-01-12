Imports System.Windows.Forms

Public Class AbstractSearchTextBox
   Public Event TextBoxChanged()
    Public Event SearchDataRequired()
    Public Event ReturnPressed()


   Public Sub New()

      ' Chiamata richiesta da Progettazione Windows Form.
      InitializeComponent()

      ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

   End Sub

   Private m_DescriptionTextWidth As Integer = 150

   Public Property DescriptionTextWidth() As Integer
      Get
         Return m_DescriptionTextWidth
      End Get
      Set(ByVal value As Integer)
         m_DescriptionTextWidth = value
      End Set
   End Property

   Private m_DescriptionTextXValue As Integer = 92

   Public Property DescriptionTextXValue() As Integer
      Get
         Return m_DescriptionTextXValue
      End Get
      Set(ByVal value As Integer)
         m_DescriptionTextXValue = value
      End Set
   End Property


   Public Property DescriptionOfText() As String
      Get
         Return DescriptionText.Text
      End Get
      Set(ByVal value As String)
         DescriptionText.Text = value
      End Set
   End Property


   Private m_IsDescriptionVisible As Boolean = False
   Public Property IsDescriptionVisible() As Boolean
      Get
         Return m_IsDescriptionVisible
      End Get
      Set(ByVal value As Boolean)
         m_IsDescriptionVisible = value

      End Set
   End Property

   Public Property Mandatory() As Boolean
      Get
         Return Me.IdTextBox.Mandatory
      End Get
      Set(ByVal value As Boolean)
         Me.IdTextBox.Mandatory = value
      End Set
   End Property

   'Public Property TextElementDock() As DockStyle
   '   Get
   '      Return IdTextBox.Dock
   '   End Get
   '   Set(ByVal value As DockStyle)
   '      IdTextBox.Dock = value
   '   End Set
   'End Property


   'Public Property ElementBoxWidth() As Integer
   '   Get
   '      Return IdTextBox.TextBox1.Width
   '   End Get
   '   Set(ByVal value As Integer)
   '      IdTextBox.TextBox1.Size = New Size(value, IdTextBox.TextBox1.Height)
   '      PictureBox1.Location = New Point(IdTextBox.TextBox1.Width - 21, PictureBox1.Location.Y)
   '   End Set
   'End Property

   Public Property EnableSerchTextBox() As Boolean
      Get
         Return Me.IdTextBox.OverridedEnable
      End Get
      Set(ByVal value As Boolean)
         Me.IdTextBox.OverridedEnable = value
      End Set
   End Property

   Private Sub IdTextBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
      DoLostFocus()
   End Sub
   Protected Overridable Sub DoLostFocus()
      '
   End Sub

   Protected Overridable Sub DoClick()
      '
   End Sub
   Public Property TextValue() As String
      Get
         Return IdTextBox.TextBox1.Text
      End Get
      Set(ByVal value As String)
         IdTextBox.TextBox1.Text = value
      End Set
   End Property




   Private Sub IdTextBox_KeyPressed(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles IdTextBox.KeyPressed
        If e.KeyChar = ControlChars.Cr Then
            If IsNumeric(Me.TextValue) Then
                RaiseEvent ReturnPressed()
            Else
                DoClick()
            End If

        End If
   End Sub


   Private Sub IdTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IdTextBox.TextChange
      RaiseEvent TextBoxChanged()
   End Sub



   Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
      DoClick()
   End Sub


   Public Sub SearchData()
      DoClick()
   End Sub

   Public ReadOnly Property InnerTextBox() As UsabilityTextBox
      Get
         Return IdTextBox
      End Get
   End Property

   Public Sub SetFocus()
      IdTextBox.SetFocus()
   End Sub

 
   Private Sub AbstractSearchTextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
      IdTextBox.TextBox1.Focus()
   End Sub

   Private Sub AbstractSearchTextBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      DescriptionText.Size = New Size(m_DescriptionTextWidth, DescriptionText.Height)
      DescriptionText.Location = New Point(m_DescriptionTextXValue, DescriptionText.Location.Y)
      DescriptionText.Visible = m_IsDescriptionVisible
   End Sub
End Class
