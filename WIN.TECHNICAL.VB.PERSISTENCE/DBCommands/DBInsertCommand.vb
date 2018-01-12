Public Class DBInsertCommand
   Inherits AbstractDbCommand
   Public Sub New(ByVal Mapper As IMapper, ByVal Item As AbstractPersistenceObject)
      MyBase.New(Mapper, Item)
   End Sub
   Public Overrides Sub Execute()
      Try
         m_Mapper.Insert(m_Obj)
      Catch ex As Exception
         Throw New Exception(ex.Message)
      End Try
   End Sub
End Class
