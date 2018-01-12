Public Class AbstractControlState
   Protected m_StateName As String
    Public Overridable Sub ChangeOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub
    Public Overridable Sub CreateOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub
    Public Overridable Sub UndoOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub
    Public Overridable Sub DeleteOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub
    Public Overridable Sub SaveOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub
    Public Overridable Sub SearchOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub
    Public Overridable Sub ViewOperation(ByVal Control As AbstractCrudControl)
        '
    End Sub

   Public ReadOnly Property StateName() As String
      Get
         Return m_StateName
      End Get
   End Property
End Class
