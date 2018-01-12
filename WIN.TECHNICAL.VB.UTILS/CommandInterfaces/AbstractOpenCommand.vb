Public MustInherit Class AbstractOpenCommand
   Implements IOpenCommand

   Protected m_IdObjectToOpen As Int32 = -1
   Public MustOverride Sub Execute(ByVal WitParameter1 As Hashtable) Implements IOpenCommand.Execute
   Public MustOverride Sub Execute() Implements IOpenCommand.Execute
   Protected MustOverride Sub ExecuteInOtherWindow(ByVal Window As Object, ByVal WithParameter1 As Hashtable) Implements IOpenCommand.ExecuteInOtherWindow
   Protected MustOverride Sub ExecuteInOtherWindow(ByVal Window As Object) Implements IOpenCommand.ExecuteInOtherWindow
   Public MustOverride Sub SetCommandParameters(ByVal WitParameter1 As Hashtable) Implements IOpenCommand.SetCommandParameters


End Class
