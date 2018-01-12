<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AbstractBrowserControl
   Inherits WIN.GUI.UTILITY.AbstractCrudControl

   'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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
      Me.SuspendLayout()
      '
      'AbstractBrowserControl
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScroll = True
      Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
      Me.Name = "AbstractBrowserControl"
      Me.Size = New System.Drawing.Size(237, 195)
      Me.ResumeLayout(False)

   End Sub

End Class
