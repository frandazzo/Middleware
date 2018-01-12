Public Class StCreation
   Inherits AbstractControlState
   Public Sub New()
      MyBase.m_StateName = "Creazione"
   End Sub
   Public Shared Function Instance() As StCreation
      Return New StCreation
   End Function
    Public Overrides Sub SaveOperation(ByVal Control As AbstractCrudControl)

        Control.DoSave()
        'Control.Nested_ReLoadProperties()
        Control.State = StView.Instance

    End Sub

    Public Overrides Sub SearchOperation(ByVal Control As AbstractCrudControl)
        Control.DoSearch()
        Control.State = StSearch.Instance

    End Sub



End Class
