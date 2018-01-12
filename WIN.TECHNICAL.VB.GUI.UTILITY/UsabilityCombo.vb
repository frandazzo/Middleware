Imports System.Windows.Forms

Public Class UsabilityCombo
   Private m_overriredEnable As Boolean = True
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

   Private m_ComboBoxStyle As ComboBoxStyle = Windows.Forms.ComboBoxStyle.DropDown
   Public Property ComboBoxStyle() As ComboBoxStyle
      Get
         Return m_ComboBoxStyle
      End Get
      Set(ByVal value As ComboBoxStyle)
         m_ComboBoxStyle = value
         ComboBox1.DropDownStyle = m_ComboBoxStyle
      End Set
   End Property


   Public Property TextValue() As String
      Get
         Return ComboBox1.Text
      End Get
      Set(ByVal value As String)
         ComboBox1.Text = value
      End Set
   End Property



   Public Event SelectedIndexChange(ByVal e As EventArgs)
   Private m_isLeaving As Boolean = False
   Private m_mandatory As Boolean


   Public Property Mandatory() As Boolean
      Get
         Return m_mandatory
      End Get
      Set(ByVal value As Boolean)
         m_mandatory = value
         Me.Invalidate()
      End Set
   End Property


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

      e.Graphics.DrawLine(color, 0, Me.ComboBox1.Height + 1, Me.Width, Me.ComboBox1.Height + 1)

   End Sub
   Public Property OverridedEnable() As Boolean
      Get
         Return m_overriredEnable
      End Get
      Set(ByVal value As Boolean)
         m_overriredEnable = value
         If value Then
            ComboBox1.BackColor = Color.White
            ComboBox1.DropDownStyle = m_ComboBoxStyle
         Else
            ComboBox1.BackColor = Color.Silver
            ComboBox1.DropDownStyle = ComboBoxStyle.Simple
         End If
         Me.Invalidate()
      End Set
   End Property




   Private Sub UsabilityCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
      ComboBox1.Focus()
   End Sub

   Private Sub ComboBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.Enter
      If m_isLeaving Then Return
      If m_overriredEnable Then
         'If m_mandatory Then
         '   TextBox1.TextAlign = Windows.Forms.HorizontalAlignment.Right
         'End If

         ComboBox1.BackColor = Color.FromArgb(205, 226, 252)

      End If
   End Sub

   Private Sub ComboBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.Leave
      If m_overriredEnable Then
         m_isLeaving = True
         'If m_mandatory Then
         '   'TextBox1.TextAlign = Windows.Forms.HorizontalAlignment.Left
         'End If
         ComboBox1.BackColor = Color.White
         'TextBox1.Select(TextBox1.TextLength, 0)

         m_isLeaving = False

      End If
   End Sub

   Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
      If m_overriredEnable Then
         RaiseEvent SelectedIndexChange(e)
      End If
   End Sub

   Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
      If Not m_overriredEnable Then
         e.Handled = True
      End If
   End Sub

   'Private Sub ComboBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
   '   If Not m_overriredEnable Then
   '      ComboBox1.DropDownStyle = ComboBoxStyle.Simple
   '   End If
   'End Sub

   Private Sub UsabilityCombo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
      ComboBox1.Focus()
   End Sub

   Private Sub UsabilityCombo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      ComboBox1.DropDownStyle = m_ComboBoxStyle
   End Sub
End Class
