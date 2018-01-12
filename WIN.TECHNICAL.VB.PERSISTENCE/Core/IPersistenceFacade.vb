''' <summary>
''' Offre tutti i servizi di persistenza. E' implementata dai vari sottosistemi di persistenza.
''' </summary>
''' <remarks></remarks>
Public Interface IPersistenceFacade
    ReadOnly Property CacheSize() As Int32
    ReadOnly Property ServiceName() As String
    ReadOnly Property DBType() As DB.DBType

    Sub EmptyChache(ByVal ObjectTypeName As String)
    Sub EmptyCacheAll()

    Function GetObject(ByVal ObjectTypeName As String, ByVal IdObject As Int32) As AbstractPersistenceObject
    Function GetObjectReloadingCache(ByVal ObjectTypeName As String, ByVal IdObject As Int32) As AbstractPersistenceObject
    Sub InsertObject(ByVal ObjectTypeName As String, ByVal Item As AbstractPersistenceObject)
    Sub UpdateObject(ByVal ObjectTypeName As String, ByVal Item As AbstractPersistenceObject)
    Sub DeleteObject(ByVal ObjectTypeName As String, ByVal Item As AbstractPersistenceObject)
    Function GetAllObjects(ByVal ObjectTypeName As String) As IList
    Sub BeginTransaction()

    Sub BeginTransaction(ByVal SortService As ISortStrategy)

    Sub CommitTransaction()
    Sub RollBackTRansaction()
    Sub MarkDirty(ByVal Item As AbstractPersistenceObject)
    Sub MarkNew(ByVal Item As AbstractPersistenceObject)
    Sub MarkRemoved(ByVal Item As AbstractPersistenceObject)
    Sub SetMapperFinder(ByVal MapperFinder As IMapperFinder)
    Function GetMapperByName(ByVal MapperName As Object)
    'Function GetMapperForObject(ByVal PersistentObject As AbstractPersistenceObject) As IMapper
    Function CreateNewQuery(ByVal DomainClassName As String) As Query
    Function ExecuteNonQuery(ByVal SQLQuery As String) As Int32
    Function ExecuteScalar(ByVal SQLQuery As String) As Object
    Function ExecuteSqlQuery(ByVal SQLQuery As String) As IList
    Function CreateNewPaginationQuery(ByVal DomainClassName As String, ByVal MaxQueryResult As Int32, ByVal MaxResultPerPage As Int32) As PaginationQueryHandler

    Sub ExecuteQueryList(ByVal QueryCommands As IList(Of String))

    Property CurrentConnection() As IDbConnection


End Interface
