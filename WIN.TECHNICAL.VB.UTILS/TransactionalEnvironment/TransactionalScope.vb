Imports System.Text

Public Class TransactionalScope

    Private m_executedCommands As IList(Of ITransactionalCommand) = New List(Of ITransactionalCommand)
    Private m_RolledBackCommands As IList(Of ITransactionalCommand) = New List(Of ITransactionalCommand)
    Private m_RolledBackErrors As IList(Of TransactionError) = New List(Of TransactionError)
    Private m_ContextParams As Dictionary(Of String, Object)
    Private m_error As TransactionError
    Private m_reverseRollback As Boolean
    Private m_numberOfCommandToExecute As Int32 = 0

    Friend ReadOnly Property ExecutedCommands As IList(Of ITransactionalCommand)
        Get
            Return m_executedCommands
        End Get
    End Property

    Friend ReadOnly Property RolledBackErrors() As IList(Of TransactionError)
        Get
            Return m_RolledBackErrors
        End Get
    End Property
    Friend ReadOnly Property TransactionError As TransactionError
        Get
            Return m_error
        End Get
    End Property

    Friend Sub New(numberOfCommandToExecute As Int32, contextParams As Dictionary(Of String, Object), Optional reverseRollBack As Boolean = False)
        m_reverseRollback = reverseRollBack
        m_numberOfCommandToExecute = numberOfCommandToExecute
        m_ContextParams = ContextParams
    End Sub

    Friend ReadOnly Property IsTransactionCommittable As Boolean
        Get
            Return m_RolledBackCommands.Count = 0 And m_executedCommands.Count = m_numberOfCommandToExecute
        End Get
    End Property

    Friend ReadOnly Property CommitReport As String
        Get
            If Not IsTransactionCommittable Then
                Throw New Exception("Transaction not commited")
            End If
            Dim sb As New StringBuilder

            For Each elem As ITransactionalCommand In m_executedCommands
                sb.AppendLine(String.Format("Command name: {0}; Command report:{1}", elem.CommandName, elem.ExecutionReport))
            Next


            Return sb.ToString
        End Get
    End Property


    Public Property ContextParams() As Dictionary(Of String, Object)
        Get
            Return m_ContextParams
        End Get
        Set(ByVal value As Dictionary(Of String, Object))
            m_ContextParams = value
        End Set
    End Property


    Friend ReadOnly Property RollBackReport As String
        Get
            If IsTransactionCommittable Then
                Return ""
            End If
            Dim sb As New StringBuilder

            For Each elem As ITransactionalCommand In m_RolledBackCommands
                sb.AppendLine(String.Format("Command name: {0}; Rollback command report:{1}", elem.CommandName, elem.ExecutionReport))
            Next


            Return sb.ToString
        End Get
    End Property


    Friend Sub AddExecutedCommand(command As ITransactionalCommand)
        m_executedCommands.Add(command)
    End Sub

    Friend Sub SetErrorCommand(command As ITransactionalCommand, ex As Exception)

        m_error = New TransactionError(ex, command, Me)
    End Sub

    Friend Sub AddRolledBackCommand(command As ITransactionalCommand)
        m_RolledBackCommands.Add(command)
    End Sub

    Friend Sub AddRolledBackError(command As ITransactionalCommand, ex As Exception)
        m_RolledBackErrors.Add(New TransactionError(ex, command, Me))
    End Sub



End Class
