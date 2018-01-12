<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsabilityDtPicker
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
      Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
      Me.SuspendLayout()
      '
      'DateTimePicker1
      '
      Me.DateTimePicker1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.DateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.CadetBlue
      Me.DateTimePicker1.CalendarTrailingForeColor = System.Drawing.Color.DarkOrange
      Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
      Me.DateTimePicker1.Location = New System.Drawing.Point(0, 0)
      Me.DateTimePicker1.Name = "DateTimePicker1"
      Me.DateTimePicker1.Size = New System.Drawing.Size(123, 20)
      Me.DateTimePicker1.TabIndex = 0
      '
      'UsabilityDtPicker
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.BackColor = System.Drawing.Color.Transparent
      Me.Controls.Add(Me.DateTimePicker1)
      Me.Name = "UsabilityDtPicker"
      Me.Size = New System.Drawing.Size(125, 26)
      Me.ResumeLayout(False)

   End Sub
   Public WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker

End Class
