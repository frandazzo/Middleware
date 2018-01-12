Public Interface ITransactionalCommand
    Sub Execute(scope As TransactionalScope)
    Sub Unexecute(scope As TransactionalScope)

    ReadOnly Property IsCorrectlyExecuted As Boolean
    ReadOnly Property IsRollBackCorrectlyExecuted As Boolean

    ReadOnly Property ExecutionReport As String

    ReadOnly Property CommandName As String

End Interface
