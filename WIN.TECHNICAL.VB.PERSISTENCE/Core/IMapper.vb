Public Interface IMapper
   Function Insert(ByVal item As AbstractPersistenceObject) As Key
   Sub Update(ByVal item As AbstractPersistenceObject)
   Sub Delete(ByVal item As AbstractPersistenceObject)
   Function FindAll() As IList
   Function FindById(ByVal Id As Int32) As AbstractPersistenceObject
   Function FindByCriteria(ByVal FindByCriteriaStatement As String) As IList
   Function FindByQuery(ByVal FindByCriteriaStatement As String) As IList
   'Function Exist(ByVal item As AbstractPersistenceObject) As Boolean
   Function FindByIdReloadingCache(ByVal Id As Int32) As AbstractPersistenceObject
    Sub EmptyCache()
End Interface
