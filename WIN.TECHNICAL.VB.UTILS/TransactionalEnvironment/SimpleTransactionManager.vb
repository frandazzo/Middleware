Public Class SimpleTransactionManager

    Private m_commands As IList(Of ITransactionalCommand)
    Private m_reverseRollback As Boolean
    Private m_Commitexecuted As Boolean = False
    Private m_CommittReport As String = ""
    Private m_RollBackReport As String = ""
    Private m_rollbackExecuted As Boolean = False
    Private m_RolledBackErrors As IList(Of TransactionError) = New List(Of TransactionError)
    Private m_TransactionError As TransactionError

    Public Function GetCommandByName(commandName As String) As ITransactionalCommand

        For Each elem As ITransactionalCommand In m_commands
            If (elem.CommandName = commandName) Then
                Return elem
            End If
        Next

        Return Nothing
    End Function

    Public ReadOnly Property IsRollBackExecuted As Boolean
        Get
            Return m_rollbackExecuted
        End Get
    End Property
    Public ReadOnly Property IsExecutionCommitted As Boolean
        Get
            Return m_Commitexecuted
        End Get
    End Property
    Public ReadOnly Property CommittReport As String
        Get
            Return m_Commitexecuted
        End Get
    End Property

    Public Sub New(commands As IList(Of ITransactionalCommand), Optional reverseRollback As Boolean = False)

        m_commands = commands
        m_reverseRollback = reverseRollback


    End Sub

    Public Sub ExecuteTransaction(contextParams As Dictionary(Of String, Object))
        Dim a As TransactionalScope = Nothing
        Dim err As Boolean = False
        Try
            a = BeginTransaction(contextParams)


            For Each elem As ITransactionalCommand In m_commands
                'se non viene gestita l'eccezione nel comando provvedo a gestire l'errore
                Try
                    elem.Execute(a)
                Catch ex As Exception
                    a.SetErrorCommand(elem, ex)
                End Try

                'verifico se c'è un errore
                If Not a.TransactionError Is Nothing Then
                    err = True
                    Exit For
                End If
            Next

            If err Then
                'nel caso ci sia un errore so chi lo ha commesso e conosco i comandi da un-eseguire 
                RollBackTransaction(a)
                Return
            End If

            CommitTransaction(a)

        Catch ex As Exception
            RollBackTransaction(a)
        End Try
    End Sub


    Protected Function BeginTransaction(contextParams As Dictionary(Of String, Object)) As TransactionalScope
        Return New TransactionalScope(m_commands.Count, contextParams, m_reverseRollback)
    End Function

  

    Protected Sub CommitTransaction(scope As TransactionalScope)
        m_Commitexecuted = scope.IsTransactionCommittable
        m_CommittReport = scope.CommitReport
    End Sub

    Private Shared Sub UnexecuteCommand(ByVal scope As TransactionalScope, ByVal i As Int32)
        Dim cmd As ITransactionalCommand = scope.ExecutedCommands(i)
        'nel caso non fosse gestito nel comando gestisco un eventuale errore nella rollback del comando
        Try
            cmd.Unexecute(scope)
        Catch ex As Exception
            scope.AddRolledBackError(cmd, ex)
        End Try

    End Sub
    Protected Sub RollBackTransaction(scope As TransactionalScope)
        'la finalità della rollback è quella di annullare i comandi eseguiti in precedenza e notificare il comando che ha
        'incontrato un errore

        'imposto l'errore che ha generato il rollback
        m_TransactionError = scope.TransactionError

        'per prima cosa devo verficare che ci siano comandi da annullare
        If scope.ExecutedCommands.Count = 0 Then 'nulla da annullare
            m_rollbackExecuted = True
            m_RollBackReport = m_TransactionError.CommandError.Message
            Return
        End If

        'a questo punto posso avviare la procedura per l'annullamento dei comandi

        If Not m_reverseRollback Then
            Dim i As Int32 = 0
            Do While (i < scope.ExecutedCommands.Count)
                UnexecuteCommand(scope, i)
                i = i + 1
            Loop
        Else
            Dim i As Int32 = scope.ExecutedCommands.Count - 1
            Do While (i >= 0)
                UnexecuteCommand(scope, i)
                i = i - 1
            Loop
        End If

        'a questo punto devo verificare se la rollback è andata a buon fine dallo scope
        'altrimenti non mi resta che enumerare i comandi che non sono andati a buon fine
        If scope.RolledBackErrors.Count = 0 Then
            m_rollbackExecuted = True
            m_RollBackReport = scope.RollBackReport + m_TransactionError.CommandError.Message
            Return
        End If


        m_RolledBackErrors = scope.RolledBackErrors
        m_RollBackReport = scope.RollBackReport + m_TransactionError.CommandError.Message


    End Sub

    Public ReadOnly Property RolledBackErrors() As IList(Of TransactionError)
        Get
            Return m_RolledBackErrors
        End Get
    End Property

    Public ReadOnly Property RollBackReport As String
        Get
            Return m_RollBackReport
        End Get
    End Property

End Class
