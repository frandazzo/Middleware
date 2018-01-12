Public Class StView
   Inherits AbstractControlState
   Public Sub New()
      MyBase.m_StateName = "Visualizzazione"
   End Sub
   Public Shared Function Instance() As StView
      Return New StView
   End Function
    Public Overrides Sub ChangeOperation(ByVal Control As AbstractCrudControl)

        If Control.IdShowedObject <> -1 And Control.IsLoading = False Then
            Control.DoChange()
            Control.State = StUpdate.Instance
            Control.Refresh()
            '
        End If


    End Sub
    Public Overrides Sub CreateOperation(ByVal Control As AbstractCrudControl)

        Control.DoCreation()
        Control.State = StCreation.Instance

    End Sub

    Public Overrides Sub DeleteOperation(ByVal Control As AbstractCrudControl)
        If Control.IdShowedObject <> -1 Then
            Control.DoDelete()
        End If
    End Sub

    Public Overrides Sub SearchOperation(ByVal Control As AbstractCrudControl)
        Control.DoSearch()
        Control.State = StSearch.Instance
    End Sub

End Class
