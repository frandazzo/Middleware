Public Class SqlDialect

    Public Shared Function GetIdentityStatement(ByVal ServiceName As String) As String
        Select Case ServiceName
            Case "MsAccess"
                Return " Select @@IDENTITY"
            Case "MsSqlServer2005"
                Return " Select @@IDENTITY"
            Case "MySql"
                Return "Select LAST_INSERT_ID()"
            Case Else
                Throw New InvalidProgramException("Tipo database non riconosciuto")
        End Select
    End Function


    Public Shared Function GetBeginTransactionStatement(ByVal ServiceName As String) As String
        Select Case ServiceName
            Case "MsAccess"
                Return "BEGIN TRANSACTION"
            Case "MsSqlServer2005"
                Return "BEGIN TRANSACTION"
            Case "MySql"
                Return "START TRANSACTION"
            Case Else
                Throw New InvalidProgramException("Tipo database non riconosciuto")
        End Select
    End Function


   Public Shared Function GetCommitTransactionStatement(ByVal ServiceName As String) As String
      Select Case ServiceName
         Case "MsAccess"
            Return "COMMIT TRANSACTION"
         Case "MsSqlServer2005"
            Return "COMMIT TRANSACTION"
         Case "MySql"
            Return "COMMIT"
         Case Else
            Throw New InvalidProgramException("Tipo database non riconosciuto")
      End Select
   End Function


   Public Shared Function GetRollbackTransactionStatement(ByVal ServiceName As String) As String
      Select Case ServiceName
         Case "MsAccess"
            Return "ROLLBACK TRANSACTION"
         Case "MsSqlServer2005"
            Return "ROLLBACK TRANSACTION"
         Case "MySql"
            Return "ROLLBACK"
         Case Else
            Throw New InvalidProgramException("Tipo database non riconosciuto")
      End Select
   End Function



   

End Class
