Public Class TransactionError
    Private ReadOnly _commandError As Exception
    Private ReadOnly _command As ITransactionalCommand
    Private ReadOnly _scope As TransactionalScope
    Public Sub New(CommandError As Exception, command As ITransactionalCommand, scope As TransactionalScope)
        _scope = scope
        _command = command
        _commandError = CommandError

    End Sub
    Public ReadOnly Property Command() As ITransactionalCommand
        Get
            Return _command
        End Get
    End Property
    Public ReadOnly Property CommandError() As Exception
        Get
            Return _commandError
        End Get
    End Property
    Public ReadOnly Property Scope() As TransactionalScope
        Get
            Return _scope
        End Get
    End Property

End Class
