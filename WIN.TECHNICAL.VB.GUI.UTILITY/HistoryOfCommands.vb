Public Class HistoryOfCommands
   Inherits System.Collections.Generic.List(Of IopenCommand)
   'Private m_Current As IopenCommand
   'Private m_CurrentIndex As Integer = -1
   Private m_MaxIndex As Integer
   Private m_MaxReached As Boolean = False
   Private Shared m_Instance As HistoryOfCommands

   Private browser As New Browser

   Public Shared Function Instance() As HistoryOfCommands
      If m_Instance Is Nothing Then
            m_Instance = New HistoryOfCommands
      End If
      Return m_Instance
   End Function




   ''' <summary>
   ''' Indica se si deve disabilitare la funzione GoNext poichè produrrebbe un errore di overflow
   ''' </summary>
   ''' <value></value>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public ReadOnly Property MaxReached() As Boolean
      Get
         Return m_MaxReached
      End Get
   End Property
   ''' <summary>
   ''' Questo metodo richiama il comando precedente e lo imposta come comando corrente
   ''' </summary>
   ''' <remarks></remarks>
   Public Sub GoBack()
      'Try
      '   If m_CurrentIndex = 0 Then Exit Sub
      '   If m_CurrentIndex > m_MaxIndex Then Throw New Exception("Indice comando corrente superiore al massimo consentito")
      '   m_CurrentIndex = m_CurrentIndex - 1
      '   SetCurrent(m_CurrentIndex)
      '   m_MaxReached = False
      'Catch ex As Exception
      '   Throw New Exception(ex.Message)
      'End Try
      browser.BrowseBackward()
   End Sub
   'Private Sub SetCurrent(ByVal NewIndex As Integer)
   '   'm_Current = Me.Item(NewIndex)
   'End Sub

   ''' <summary>
   ''' Questo metodo richiama il comando successivo e lo imposta come comando corrente
   ''' </summary>
   ''' <remarks></remarks>
   Public Sub GoNext()
      'Try
      '   If m_CurrentIndex < 0 Then Throw New Exception("Indice comando corrente inferiore a 0")
      '   m_CurrentIndex = m_CurrentIndex + 1
      '   SetCurrent(m_CurrentIndex)
      '   If m_CurrentIndex = Me.Count - 1 Then m_MaxReached = True
      'Catch ex As Exception
      '   Throw New Exception(ex.Message)
      'End Try
      browser.BrowseForward()
   End Sub
   ''' <summary>
   ''' Restituisce il comando corrente
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function GetCurrentCommand() As IOpenCommand
      'Return Me.Item(m_CurrentIndex)
      Return browser.GetCurrent()
   End Function

   ''' <summary>
   ''' Aggiunge il comando allo storico dei comandi
   ''' </summary>
   ''' <param name="Command"></param>
   ''' <remarks></remarks>
   Public Sub AddCommandToHistory(ByVal Command As IOpenCommand)
      ''rimuovo il primo intervallo
      'If Me.Count = m_MaxIndex Then
      '   Me.RemoveAt(0)
      '   'm_CurrentIndex = m_CurrentIndex - 1
      'End If

      'Me.Add(Command)
      'm_CurrentIndex = Me.Count - 1
      'm_MaxReached = True
      browser.AddCommand(Command)
   End Sub
   Public Sub New()
      m_MaxIndex = My.Settings.NumeroComandi
   End Sub

End Class

Public Class BrowserObject
   Public Sub New(ByVal Command As IOpenCommand)
      Elem = Command
   End Sub
   Public Succ As BrowserObject = Nothing
   Public Prev As BrowserObject = Nothing
   Public Elem As IOpenCommand = Nothing
End Class

Public Class Browser
   Private First As BrowserObject = Nothing
   Private Last As BrowserObject = Nothing
   Private m_noElem As Boolean = True
   Private m_IsDeeperElem As Boolean = False
   Public ReadOnly Property IsDeeperElem() As Boolean
      Get
         Return m_IsDeeperElem
      End Get
   End Property
   Public ReadOnly Property NoElem() As Boolean
      Get
         Return m_noElem
      End Get
   End Property
   Public Function GetCurrent() As IOpenCommand
      If NoElem Then Return Nothing
      Return Last.Elem
   End Function
   Private Sub AddFirst(ByVal Command As IOpenCommand)
      First = New BrowserObject(Command)
      Last = First
      m_noElem = False
      m_IsDeeperElem = True
   End Sub
   Public Sub AddCommand(ByVal Command As IOpenCommand)
      If First Is Nothing Then
         AddFirst(Command)
         Return
      End If
      Dim obj As New BrowserObject(Command)
      Last.Succ = obj
      obj.Prev = Last
      obj.Succ = Nothing
      Last = obj
      m_noElem = False
      m_IsDeeperElem = True
   End Sub
   Public Sub BrowseBackward()
      If First Is Nothing Then m_noElem = True
      If Last Is First Then
         m_IsDeeperElem = True
         Return
      End If
      Last = Last.Prev
      m_IsDeeperElem = False
   End Sub
   Public Sub BrowseForward()
      If First Is Nothing Then m_noElem = True
      If Last.Succ Is Nothing Then
         m_IsDeeperElem = True
         Return
      End If
      Last = Last.Succ
      If Last.Succ Is Nothing Then
         m_IsDeeperElem = True
      Else
         m_IsDeeperElem = False
      End If
   End Sub
   Public Function Count() As Int32
      If m_noElem Then Return 0
      Dim temp As BrowserObject = First
      Dim i As Int32 = 1
      Do While temp.Succ IsNot Nothing
         i = i + 1
         temp = temp.Succ
      Loop
      Return i
   End Function

End Class