Public Class AndExp
   Inherits CompositeCriteria

   Public Sub New(ByVal Exp1 As AbstractBoolCriteria, ByVal Exp2 As AbstractBoolCriteria)
      MyBase.new(Operatore.AndOp)
      MyBase.List.Add(Exp1)
      MyBase.List.Add(Exp2)
   End Sub

 
End Class
