Public Class DBTypeUtils


   Public Shared Function GetDBTYpe(ByVal ServiceName As String) As PERSISTENCE.DB.DBType
      Select Case ServiceName
         Case "MsAccess"
            Return DB.DBType.Access
         Case "MsSqlServer2005"
            Return DB.DBType.SqlServer
         Case "MySql"
            Return DB.DBType.MySql
         Case Else

            Throw New InvalidProgramException("Tipo database non riconosciuto")
      End Select
   End Function


   Public Shared Function GetCommand(ByVal ServiceName As String) As IDbCommand
      Select Case ServiceName
         Case "MsAccess"
            Return New OleDb.OleDbCommand
         Case "MsSqlServer2005"
            Return New SqlClient.SqlCommand
            Case "MySql"
                Return New MySql.Data.MySqlClient.MySqlCommand
                '   Return New MySQLDriverCS.MySQLCommand
            Case Else
                Throw New InvalidProgramException("Tipo database non riconosciuto")
        End Select
   End Function


   Public Shared Function GetCommand(ByVal ServiceName As String, ByVal CommandText As String, ByVal Connection As IDbConnection) As IDbCommand
      Select Case ServiceName
         Case "MsAccess"
            Return New OleDb.OleDbCommand(CommandText, Connection)
         Case "MsSqlServer2005"
            Return New SqlClient.SqlCommand(CommandText, Connection)
            Case "MySql"
                Return New MySql.Data.MySqlClient.MySqlCommand(CommandText, Connection)
                ' Return New MySQLDriverCS.MySQLCommand(CommandText, Connection)
         Case Else
            Throw New InvalidProgramException("Tipo database non riconosciuto")
      End Select
   End Function


   Public Shared Function GetConnection(ByVal ServiceName As String, ByVal connectionString As String) As IDbConnection
        Select Case ServiceName
            Case "MsAccess"
                Return New OleDb.OleDbConnection(connectionString)
            Case "MsSqlServer2005"
                Return New SqlClient.SqlConnection(connectionString)
            Case "MySql"
                'Dim SERVER As String = ""
                'Dim DATABASE As String = ""
                'Dim UserName As String = ""
                'Dim Password As String = ""
                'Dim Port As String = "3306"
                'Try
                '    Dim arr As String() = Split(connectionString, ";")

                '    For Each elem As String In arr
                '        If elem.Trim().ToUpper.StartsWith(UCase("SERVER")) Then
                '            SERVER = elem.Substring(elem.LastIndexOf("=") + 1)
                '        End If
                '        If elem.Trim().ToUpper.StartsWith(UCase("DATABASE")) Then
                '            DATABASE = elem.Substring(elem.LastIndexOf("=") + 1)
                '        End If
                '        If elem.Trim().ToUpper.StartsWith(UCase("UserName")) Then
                '            UserName = elem.Substring(elem.LastIndexOf("=") + 1)
                '        End If
                '        If elem.Trim().ToUpper.StartsWith(UCase("Password")) Then
                '            Password = elem.Substring(elem.LastIndexOf("=") + 1)
                '        End If
                '        If elem.Trim().ToUpper.StartsWith(UCase("Port")) Then
                '            Port = elem.Substring(elem.LastIndexOf("=") + 1)
                '        End If
                '    Next
                'Catch ex As Exception
                '    Throw New Exception("Connessione non disponibile. Impossibile interpretare la stringa di connessione.")
                'End Try
                'Dim connst As New MySQLDriverCS.MySQLConnectionString(SERVER, DATABASE, UserName, Password, Port)


                'Dim conn As New MySQLDriverCS.MySQLConnection
                'Dim s As String = connst.AsString

                'conn.ConnectionString = "Server=localhost;protocol=pipe;Database=feneal;Uid=root;Pwd=RegUsr;" 'connst.AsString
                'Return conn
                Return New MySql.Data.MySqlClient.MySqlConnection(connectionString)
            Case Else
                Throw New InvalidProgramException("Tipo database non riconosciuto")
        End Select
    End Function



End Class
