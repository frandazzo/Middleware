Public Interface IMapperFinder
   Function GetMapperByObjectTypeName(ByVal ObjectTypeName As String) As IMapper
   Function GetMapperByName(ByVal MapperName As String) As IMapper
    Sub SetPersistentService(ByVal PersistentService As IPersistenceFacade)
    ReadOnly Property DBMappers As IEnumerable
End Interface
