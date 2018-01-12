<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AbstractSearchTextBox
    Inherits System.Windows.Forms.UserControl

    'UserControl esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
      Me.PictureBox1 = New System.Windows.Forms.PictureBox
      Me.IdTextBox = New WIN.GUI.UTILITY.UsabilityTextBox
      Me.DescriptionText = New System.Windows.Forms.TextBox
      CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'PictureBox1
      '
      Me.PictureBox1.ErrorImage = Global.WIN.GUI.UTILITY.My.Resources.Resources.SearchDir
      Me.PictureBox1.Image = Global.WIN.GUI.UTILITY.My.Resources.Resources.find
      Me.PictureBox1.InitialImage = Global.WIN.GUI.UTILITY.My.Resources.Resources.SearchDir
      Me.PictureBox1.Location = New System.Drawing.Point(68, 2)
      Me.PictureBox1.Name = "PictureBox1"
      Me.PictureBox1.Size = New System.Drawing.Size(18, 16)
      Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
      Me.PictureBox1.TabIndex = 1
      Me.PictureBox1.TabStop = False
      '
      'IdTextBox
      '
      Me.IdTextBox.BackColor = System.Drawing.Color.Transparent
      Me.IdTextBox.Location = New System.Drawing.Point(0, 0)
      Me.IdTextBox.Mandatory = False
      Me.IdTextBox.Name = "IdTextBox"
      Me.IdTextBox.OverridedEnable = True
      Me.IdTextBox.Required = False
      Me.IdTextBox.Size = New System.Drawing.Size(90, 23)
      Me.IdTextBox.TabIndex = 1
      Me.IdTextBox.TextValue = ""
      '
      'DescriptionText
      '
      Me.DescriptionText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.DescriptionText.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
      Me.DescriptionText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.DescriptionText.Location = New System.Drawing.Point(92, 0)
      Me.DescriptionText.Name = "DescriptionText"
      Me.DescriptionText.Size = New System.Drawing.Size(150, 20)
      Me.DescriptionText.TabIndex = 2
      Me.DescriptionText.TabStop = False
      '
      'AbstractSearchTextBox
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.BackColor = System.Drawing.Color.Transparent
      Me.Controls.Add(Me.DescriptionText)
      Me.Controls.Add(Me.PictureBox1)
      Me.Controls.Add(Me.IdTextBox)
      Me.Margin = New System.Windows.Forms.Padding(0)
      Me.Name = "AbstractSearchTextBox"
      Me.Size = New System.Drawing.Size(93, 23)
      CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Public WithEvents IdTextBox As WIN.GUI.UTILITY.UsabilityTextBox
   Public WithEvents PictureBox1 As System.Windows.Forms.PictureBox
   Public WithEvents DescriptionText As System.Windows.Forms.TextBox

End Class
