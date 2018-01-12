Public Class CommandFactory
    Public Shared Function GetCommand(ByVal Type As DB.DBType) As IDbCommand
        Select Case Type
            Case DB.DBType.Access
                Return New OleDb.OleDbCommand
            Case DB.DBType.SqlServer
                Return New SqlClient.SqlCommand
            Case DB.DBType.MySql
                Return New MySql.Data.MySqlClient.MySqlCommand
                'Return New MySQLDriverCS.MySQLCommand
            Case Else
                Return New Odbc.OdbcCommand
        End Select
    End Function
End Class
