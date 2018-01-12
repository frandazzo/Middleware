Public Class DBDeleteCommand
   Inherits AbstractDbCommand
   Public Sub New(ByVal Mapper As IMapper, ByVal Item As AbstractPersistenceObject)
      MyBase.New(Mapper, Item)
   End Sub
   Public Overrides Sub Execute()
      Try
         m_Mapper.Delete(m_Obj)
      Catch ex As Exception
         Throw New Exception(ex.Message)
      End Try
   End Sub

End Class
