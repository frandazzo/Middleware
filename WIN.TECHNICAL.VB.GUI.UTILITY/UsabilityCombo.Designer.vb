<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsabilityCombo
    Inherits System.Windows.Forms.UserControl

    'UserControl esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
      Me.ComboBox1 = New System.Windows.Forms.ComboBox
      Me.SuspendLayout()
      '
      'ComboBox1
      '
      Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.ComboBox1.FormattingEnabled = True
      Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
      Me.ComboBox1.Name = "ComboBox1"
      Me.ComboBox1.Size = New System.Drawing.Size(150, 21)
      Me.ComboBox1.TabIndex = 0
      '
      'UsabilityCombo
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.BackColor = System.Drawing.Color.Transparent
      Me.Controls.Add(Me.ComboBox1)
      Me.Name = "UsabilityCombo"
      Me.Size = New System.Drawing.Size(150, 25)
      Me.ResumeLayout(False)

   End Sub
   Public WithEvents ComboBox1 As System.Windows.Forms.ComboBox

End Class
