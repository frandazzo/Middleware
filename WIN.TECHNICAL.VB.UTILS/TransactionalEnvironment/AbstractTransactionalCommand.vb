Public MustInherit Class AbstractTransactionalCommand
    Implements ITransactionalCommand

    Protected m_IsRollBackCorrectlyExecuted As Boolean = True
    Protected m_IsCorrectlyExecuted As Boolean
    Protected m_ExecutionReport As String = ""


    Public MustOverride ReadOnly Property CommandName As String Implements ITransactionalCommand.CommandName
    Protected MustOverride Sub DoExecution(scope As TransactionalScope)
    Protected MustOverride Sub DoUnexecution(scope As TransactionalScope)

    Public Overridable Sub Execute(scope As TransactionalScope) Implements ITransactionalCommand.Execute
        Try
            DoExecution(scope)
            'una volta eseguito il comando, e non avendo avuto alcuna eccezione
            'posso aggiungere il comand stesso alla lista dei comandi eseguiti nello scope
            If m_IsCorrectlyExecuted Then
                scope.AddExecutedCommand(Me)
                Return
            End If
            scope.SetErrorCommand(Me, New TransactionalCommandExecutionException(m_ExecutionReport, False, CommandName))
        Catch ex As Exception
            scope.SetErrorCommand(Me, ex)
        End Try
    End Sub

    Public Overridable ReadOnly Property ExecutionReport As String Implements ITransactionalCommand.ExecutionReport
        Get
            Return m_ExecutionReport
        End Get
    End Property

    Public Overridable ReadOnly Property IsCorrectlyExecuted As Boolean Implements ITransactionalCommand.IsCorrectlyExecuted
        Get
            Return m_IsCorrectlyExecuted
        End Get
    End Property

    Public Overridable Sub Unexecute(scope As TransactionalScope) Implements ITransactionalCommand.Unexecute
        Try
            DoUnexecution(scope)
            'una volta eseguito il comando, e non avendo avuto alcuna eccezione
            'posso aggiungere il comand stesso alla lista dei comandi eseguiti nello scope
            If m_IsRollBackCorrectlyExecuted Then
                scope.AddRolledBackCommand(Me)
                Return
            End If
            scope.AddRolledBackError(Me, New TransactionalCommandExecutionException(m_ExecutionReport, True, CommandName))
        Catch ex As Exception
            scope.AddRolledBackError(Me, ex)
        End Try
    End Sub



    Public Overridable ReadOnly Property IsRollBackCorrectlyExecuted As Boolean Implements ITransactionalCommand.IsRollBackCorrectlyExecuted
        Get
            Return m_IsRollBackCorrectlyExecuted
        End Get
    End Property
End Class
