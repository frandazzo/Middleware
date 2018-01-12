Public Class DBQueryExecuteCommand
    Inherits AbstractDbCommand


    Private m_command As IDbCommand

    Public Sub New(command As IDbCommand)
        MyBase.New(Nothing, Nothing)

        m_command = command

    End Sub
    Public Overrides Sub Execute()


        m_command.ExecuteNonQuery()

    End Sub
End Class
