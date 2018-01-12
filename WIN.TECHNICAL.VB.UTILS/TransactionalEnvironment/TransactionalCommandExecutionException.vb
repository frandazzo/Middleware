Public Class TransactionalCommandExecutionException
    Inherits Exception

    Public Sub New(message As String, rollbackExecution As Boolean, commandName As String)

        MyBase.New(IIf(rollbackExecution, String.Format("An error occurend during the  rollback execution of the command {0}: /n{1}", commandName, message), String.Format("An error occurend during the execution of the command {0}: /n{1}", commandName, message)))

    End Sub



End Class
