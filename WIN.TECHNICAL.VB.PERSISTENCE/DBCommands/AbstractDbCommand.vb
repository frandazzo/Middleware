Public MustInherit Class AbstractDbCommand
   Implements ICommand
   Protected m_Mapper As IMapper
   Protected m_Obj As AbstractPersistenceObject
   Public Sub New(ByVal Mapper As IMapper, ByVal Item As AbstractPersistenceObject)
        'If Mapper Is Nothing Then Throw New Exception("Non è possibile creare un comando per l'esecuzione di una transazione con un servizio di mapping nullo!")
        'If Item Is Nothing Then Throw New Exception("Non è possibile creare un comando per l'esecuzione di una transazione con un oggetto nullo!")
      m_Mapper = Mapper
      m_Obj = Item
   End Sub
   Public MustOverride Sub Execute() Implements ICommand.Execute
   Public Function GetObjectType() As Type
      Return m_Obj.GetType
   End Function
End Class
