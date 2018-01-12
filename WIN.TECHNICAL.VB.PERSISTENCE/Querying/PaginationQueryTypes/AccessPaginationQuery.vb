Friend Class AccessPaginationQuery
    Inherits Query



    Public Sub New(ByVal DomainClassName As String, ByVal MaxQueryRecords As Int32)
        MyBase.New(DomainClassName)

        m_MaxQueryRecords = MaxQueryRecords


    End Sub

    Protected Overrides Function GenerateQuery(ByVal PersistenceManager As IPersistenceFacade) As String
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function Execute(ByVal PersistenceManager As IPersistenceFacade) As System.Collections.IList
        Throw New NotImplementedException
    End Function

    Public Overridable Overloads Function Execute(ByVal PersistenceManager As IPersistenceFacade, ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As System.Collections.IList
        Throw New NotImplementedException
    End Function

End Class
