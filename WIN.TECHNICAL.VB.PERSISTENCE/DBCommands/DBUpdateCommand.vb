Public Class DBUpdateCommand
   Inherits AbstractDbCommand
   Public Sub New(ByVal Mapper As IMapper, ByVal Item As AbstractPersistenceObject)
      MyBase.New(Mapper, Item)
   End Sub
   Public Overrides Sub Execute()

      m_Mapper.Update(m_Obj)

   End Sub
End Class
