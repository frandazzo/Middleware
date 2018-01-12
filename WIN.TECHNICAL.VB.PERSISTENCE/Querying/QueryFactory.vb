Public Class QueryFactory

    Public Shared Function GetPaginationQuery(ByVal PersistenceManager As IPersistenceFacade, ByVal DomainClassName As String, ByVal MaxQueryRecords As Int32, ByVal MaxElementPerPage As Int32) As PaginationQueryHandler


        Select Case PersistenceManager.DBType
            Case DB.DBType.Access
                Return New PaginationQueryHandler(New AccessPaginationQuery(DomainClassName, MaxQueryRecords), MaxElementPerPage)
            Case DB.DBType.MySql
                Return New PaginationQueryHandler(New MySqlPaginationQuery(DomainClassName, MaxQueryRecords), MaxElementPerPage)
            Case DB.DBType.SqlServer
                Return New PaginationQueryHandler(New SqlServerPaginationQuery(DomainClassName, MaxQueryRecords), MaxElementPerPage)
            Case Else
                Throw New Exception("Tipo di queri di paginazione sconosciuto")
        End Select



    End Function
End Class
