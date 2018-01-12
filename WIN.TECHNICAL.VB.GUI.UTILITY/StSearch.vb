Public Class StSearch
    Inherits AbstractControlState
    Public Sub New()
        MyBase.m_StateName = "Ricerca"
    End Sub
    Public Shared Function Instance() As StSearch
        Return New StSearch
    End Function

    Public Overrides Sub CreateOperation(ByVal Control As AbstractCrudControl)

        Control.DoCreation()
        Control.State = StCreation.Instance

    End Sub

    Public Overrides Sub ViewOperation(ByVal Control As AbstractCrudControl)
        Control.StartLoadOperation()
        Control.State = StView.Instance

    End Sub

    'Public Overrides Sub Change(ByVal Control As AbstractCrudControl)

    'End Sub

    'Public Overrides Sub Delete(ByVal Control As AbstractCrudControl)
 
    'End Sub

    'Public Overridable Sub Cancel(ByVal Control As AbstractCrudControl)

    'End Sub

    'Public Overridable Sub Delete(ByVal Control As AbstractCrudControl)

    'End Sub

    'Public Overridable Sub Insert(ByVal Control As AbstractCrudControl)

    'End Sub

    'Public Overridable Sub Search(ByVal Control As AbstractCrudControl)

    'End Sub
End Class
