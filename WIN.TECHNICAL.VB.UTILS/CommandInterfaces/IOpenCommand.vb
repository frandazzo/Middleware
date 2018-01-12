Public Interface IOpenCommand
   Sub Execute()
   Sub Execute(ByVal WithParameter1 As Hashtable)
   Sub ExecuteInOtherWindow(ByVal Window As Object)
   Sub ExecuteInOtherWindow(ByVal Window As Object, ByVal WithParameter1 As Hashtable)
   Sub SetCommandParameters(ByVal WithParameter As Hashtable)
End Interface


