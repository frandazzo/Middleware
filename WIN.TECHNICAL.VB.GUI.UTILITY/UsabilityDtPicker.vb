Public Class UsabilityDtPicker
   Private m_overriredEnable As Boolean = True
   'Public Event GotTextFocus(ByVal e As EventArgs)
   'Public Event LostTextFocus(ByVal e As EventArgs)
   Public Event ValueChange(ByVal sender As System.Object, ByVal e As EventArgs)
   Public Event KeyPressed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
   Private m_isLeaving As Boolean = False
   Private m_mandatory As Boolean

   Private m_required As Boolean

   Public Property Required() As Boolean
      Get
         Return m_required
      End Get
      Set(ByVal value As Boolean)
         m_required = value
         Me.Invalidate()
      End Set
   End Property

   Public Property Mandatory() As Boolean
      Get
         Return m_mandatory
      End Get
      Set(ByVal value As Boolean)
         m_mandatory = value
         Me.Invalidate()
      End Set
   End Property

   Public Sub New()

      ' Chiamata richiesta da Progettazione Windows Form.
      InitializeComponent()

      'Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

   End Sub


   Public Sub SetFocus()
      DateTimePicker1.Focus()
   End Sub


   Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
      MyBase.OnPaint(e)
      Dim color As Pen = Pens.Black

      If Not m_overriredEnable Then
         color = Pens.Black
      ElseIf m_mandatory Then
         color = Pens.Red
      ElseIf m_required Then
         color = Pens.SkyBlue
      Else
         color = Pens.Transparent

      End If

      e.Graphics.DrawLine(color, 0, Me.DateTimePicker1.Height + 1, Me.Width, Me.DateTimePicker1.Height + 1)
   End Sub

   Public Property OverridedEnable() As Boolean
      Get
         Return m_overriredEnable
      End Get
      Set(ByVal value As Boolean)
         m_overriredEnable = value
         If value Then
            DateTimePicker1.BackColor = Color.White
         Else
            DateTimePicker1.BackColor = Color.Silver
         End If
         Me.Invalidate()
      End Set
   End Property


   Private Sub UsabilityTextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
      DateTimePicker1.Focus()
   End Sub




   Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
      If Not m_overriredEnable Then
         e.Handled = True
         Return
      End If
      RaiseEvent KeyPressed(sender, e)
   End Sub

   Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Enter
      'RaiseEvent GotTextFocus(e)
      If m_isLeaving Then Return
      If m_overriredEnable Then
         'If m_mandatory Then
         '   TextBox1.TextAlign = Windows.Forms.HorizontalAlignment.Right
         'End If

         DateTimePicker1.CalendarTitleBackColor = Color.FromArgb(205, 226, 252)
         DateTimePicker1.Font = New Font(DateTimePicker1.Font, FontStyle.Bold)
      End If

   End Sub


   Private Sub TextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
      'RaiseEvent LostTextFocus(e)
      If m_overriredEnable Then
         m_isLeaving = True
         'If m_mandatory Then
         '   'TextBox1.TextAlign = Windows.Forms.HorizontalAlignment.Left
         'End If
         DateTimePicker1.Font = New Font(DateTimePicker1.Font, FontStyle.Regular)
         'TextBox1.Select(TextBox1.TextLength, 0)

         m_isLeaving = False

      End If
   End Sub

   Public Property DataValue() As DateTime
      Get
         Return DateTimePicker1.Value
      End Get
      Set(ByVal value As DateTime)
         DateTimePicker1.Value = value
      End Set
   End Property


   Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
      If m_overriredEnable Then
         RaiseEvent ValueChange(Me, e)
      End If
   End Sub

   Private Sub UsabilityTextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Enter
      DateTimePicker1.Focus()
   End Sub
End Class
