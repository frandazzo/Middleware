Public Class DBTransaction

    '   Private Shared Begin As String = SqlDialect.GetBeginTransactionStatement(DataAccessServices.Instance.ServiceName)
    '   Private Shared Commit As String = SqlDialect.GetCommitTransactionStatement(DataAccessServices.Instance.ServiceName)
    '   Private Shared RollBack As String = SqlDialect.GetRollbackTransactionStatement(DataAccessServices.Instance.ServiceName)
    Shared Sub BeginTransaction(ByVal conn As IDbConnection, ByVal ServiceName As String)
        Try
            Dim Begin1 As String = SqlDialect.GetBeginTransactionStatement(ServiceName)
            Dim myCom As IDbCommand = CreateCommand(Begin1, conn)
            myCom.ExecuteNonQuery()
            myCom.Dispose()
            myCom = Nothing
        Catch ex As Exception
            Throw New Exception("Impossibile cominciare la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Shared Sub BeginTransaction(ByVal nomeTransazione As String, ByVal conn As IDbConnection, ByVal ServiceName As String)
        Try
            Dim Begin1 As String = SqlDialect.GetBeginTransactionStatement(ServiceName)
            Dim myCom As IDbCommand = CreateCommand(Begin1 & " " & nomeTransazione, conn)
            myCom.ExecuteNonQuery()
            myCom.Dispose()
            myCom = Nothing
        Catch ex As Exception
            Throw New Exception("Impossibile cominciare la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub
    Shared Sub CommitTransaction(ByVal conn As IDbConnection, ByVal ServiceName As String)
        Try
            Dim Commit1 As String = SqlDialect.GetCommitTransactionStatement(ServiceName)
            Dim myCom As IDbCommand = CreateCommand(Commit1, conn)
            myCom.ExecuteNonQuery()
            myCom.Dispose()
            myCom = Nothing
        Catch ex As Exception
            Throw New Exception("Impossibile eseguire il commit per la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Shared Sub CommitTransaction(ByVal nomeTransazione As String, ByVal conn As IDbConnection, ByVal ServiceName As String)
        Try
            Dim Commit1 As String = SqlDialect.GetCommitTransactionStatement(ServiceName)
            Dim myCom As IDbCommand = CreateCommand(Commit1 & " " & nomeTransazione, conn)
            myCom.ExecuteNonQuery()
            myCom.Dispose()
            myCom = Nothing
        Catch ex As Exception
            Throw New Exception("Impossibile eseguire il commit per la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Shared Sub RollBackTransaction(ByVal conn As IDbConnection, ByVal ServiceName As String)
        Try
            Dim RollBack1 As String = SqlDialect.GetRollbackTransactionStatement(ServiceName)
            Dim myCom As IDbCommand = CreateCommand(RollBack1, conn)
            myCom.ExecuteNonQuery()
            myCom.Dispose()
            myCom = Nothing
        Catch ex As Exception
            Throw New Exception("Impossibile eseguire il rollback per la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Shared Sub RollBackTransaction(ByVal nomeTransazione As String, ByVal conn As IDbConnection, ByVal ServiceName As String)
        Try
            Dim RollBack1 As String = SqlDialect.GetRollbackTransactionStatement(ServiceName)
            Dim myCom As IDbCommand = CreateCommand(RollBack1 & " " & nomeTransazione, conn)
            myCom.ExecuteNonQuery()
            myCom.Dispose()
            myCom = Nothing
        Catch ex As Exception
            Throw New Exception("Impossibile eseguire il rollback per la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Shared Function CreateCommand(ByVal CommandText As String, ByVal Conn As IDbConnection) As IDbCommand
        Dim cmd As IDbCommand
        If TypeOf (Conn) Is OleDb.OleDbConnection Then
            cmd = New OleDb.OleDbCommand(CommandText, Conn)
        ElseIf TypeOf (Conn) Is SqlClient.SqlConnection Then
            cmd = New SqlClient.SqlCommand(CommandText, Conn)
        ElseIf TypeOf (Conn) Is MySql.Data.MySqlClient.MySqlConnection Then 'MySQLDriverCS.MySQLConnection Then
            cmd = New MySql.Data.MySqlClient.MySqlCommand(CommandText, Conn) 'New MySQLDriverCS.MySQLCommand(CommandText, Conn)
        Else
            Throw New Exception("Impossibile un comando DBTransaction. Tipo di connessione sconosciuto.")
        End If
        Return (cmd)
    End Function

End Class
