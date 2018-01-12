Public Class OrExp
   Inherits CompositeCriteria
   Public Sub New(ByVal Exp1 As AbstractBoolCriteria, ByVal Exp2 As AbstractBoolCriteria)
      MyBase.new(Operatore.OrOp)
      MyBase.List.Add(Exp1)
      MyBase.List.Add(Exp2)
   End Sub
End Class
