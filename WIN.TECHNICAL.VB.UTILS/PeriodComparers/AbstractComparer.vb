Public MustInherit Class AbstractComparer
   Implements IComparer
   Protected MustOverride Function DoCompare(ByRef x As Object, ByRef y As Object) As Integer
   Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
      If x Is Nothing And y Is Nothing Then Return 0
      If x Is Nothing Then Return 1
      If y Is Nothing Then Return -1
      DoCompare(x, y)
   End Function

End Class
