Public Class ObjectNotDeletableException
    Inherits Exception


    Public Overrides ReadOnly Property Message() As String
        Get
            Return "Oggento non eliminabile per impostazione predefinita. Contattare l'amminstratore del sistema per ulteriori informazioni"
        End Get
    End Property

End Class
