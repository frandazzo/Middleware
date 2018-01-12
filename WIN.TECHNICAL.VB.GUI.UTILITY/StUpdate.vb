Public Class StUpdate
   Inherits AbstractControlState
   Public Sub New()
      MyBase.m_StateName = "Aggiornamento"
   End Sub
   Public Shared Function Instance() As StUpdate
      Return New StUpdate
   End Function
    Public Overrides Sub UndoOperation(ByVal Control As AbstractCrudControl)

        Control.DoUndo()
        Control.State = StView.Instance


    End Sub
    Public Overrides Sub SaveOperation(ByVal Control As AbstractCrudControl)

        'Dim start As Int32 = Environment.TickCount

        Control.DoSave()
        'System.Diagnostics.Debug.WriteLine("salvataggio durato " & (Environment.TickCount - start).ToString)

        'start = Environment.TickCount
        'Control.Nested_ReLoadProperties()
        'System.Diagnostics.Debug.WriteLine("reloading delle proprietà " & (Environment.TickCount - start).ToString)

        Control.State = StView.Instance


    End Sub
End Class
