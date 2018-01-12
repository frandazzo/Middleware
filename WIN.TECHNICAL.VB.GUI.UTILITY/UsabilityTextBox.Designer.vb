<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsabilityTextBox
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
      Me.TextBox1 = New System.Windows.Forms.TextBox
      Me.SuspendLayout()
      '
      'TextBox1
      '
      Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.TextBox1.Location = New System.Drawing.Point(0, 0)
      Me.TextBox1.Name = "TextBox1"
      Me.TextBox1.Size = New System.Drawing.Size(217, 20)
      Me.TextBox1.TabIndex = 1
      Me.TextBox1.TabStop = False
      '
      'UsabilityTextBox
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.BackColor = System.Drawing.Color.Transparent
      Me.Controls.Add(Me.TextBox1)
      Me.Name = "UsabilityTextBox"
      Me.Size = New System.Drawing.Size(218, 23)
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Public WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
