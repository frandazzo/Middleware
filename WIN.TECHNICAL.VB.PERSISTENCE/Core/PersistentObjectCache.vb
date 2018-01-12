Imports System.Runtime.InteropServices
Public Class PersistentObjectCache
   Private Cache As New Hashtable
   Private mru As LinkedList = Nothing ' most recently used
   Private lru As LinkedList = Nothing ' less recently used
   'se cambio il valore della cache ricordarsi di modificare i test effettuati nella classe TestCache
   Private MAX_CACHE_SIZE As Int32 = 5
    Private CurrentCacheSize As Int32 = 0


    Public ReadOnly Property MaxCacheSize As Int32
        Get
            Return MAX_CACHE_SIZE
        End Get
    End Property


   Public Sub New(ByVal NoCache As Boolean)
      If NoCache Then
         MAX_CACHE_SIZE = 0
      Else
            MAX_CACHE_SIZE = 1000
      End If
   End Sub


    Public Sub New(ByVal CacheSize As Int32)
        'inizializzo la dimensione della cache secondo quanto specificato nel file DB.Config
        MAX_CACHE_SIZE = CacheSize
        If MAX_CACHE_SIZE = 1 Then MAX_CACHE_SIZE = 2
    End Sub


   Public Sub AddToCache(ByVal Obj As AbstractPersistenceObject)
      Try
         Dim id As String = Obj.Key.ToString
         If MAX_CACHE_SIZE = 0 Then Exit Sub
         If Cache.Item(id) Is Nothing Then
            If CurrentCacheSize = 0 Then
               lru = New LinkedList
               mru = lru
               mru.Profile = Obj
            Else
               Dim newLink As LinkedList
               If CurrentCacheSize >= MAX_CACHE_SIZE Then
                  'rimuovo l'ultimo riferimento usato
                  newLink = lru
                  lru = newLink.PreviousLink
                  Cache.Remove(newLink.Profile.Key.ToString)
                  If Not lru Is Nothing Then lru.NextLink = Nothing
               Else
                  newLink = New LinkedList
               End If
               newLink.Profile = Obj
               newLink.PreviousLink = Nothing
               'Sto dicendo che New Link è il successivo di MRU
               newLink.NextLink = mru
               'MRU diventa il new link
               mru = newLink
               'aggiungo alla lista precedente l'informazione che essa è precedente a newlink
               newLink.NextLink.PreviousLink = mru
            End If
            Cache.Add(id, mru)
            CurrentCacheSize = Cache.Count
         Else
            'richiamo l'oggetto per riproporlo come l'ultimo recentemente usato
            GetObjectFromCache(id)
            'sono sicuro che adesso è all'ultima posizione
            'e quindi posso rimuoverlo e riaggiungerlo
            'in modo da rinfrescare il riferimento all'oggetto
            RemoveLastElement(id)
            AddToCache(Obj)
         End If
      Catch ex As Exception
         Throw New Exception("Impossibile aggiungere l'oggetto di tipo " & Obj.GetType.ToString & "  alla cache. " & vbCrLf & ex.Message)
      End Try
   End Sub
   Public Sub RemoveObjectFromCache(ByVal Obj As AbstractPersistenceObject)
      Try
         Dim id As String = Obj.Key.ToString
         If Cache.Item(id) Is Nothing Then Exit Sub
         GetObjectFromCache(id)
         RemoveLastElement(id)
      Catch ex As Exception
         Throw New Exception("Impossibile rimuovere l'oggetto dalla cache." & vbCrLf & ex.Message)
      End Try
   End Sub
   Public Sub RemoveObjectFromCache(ByVal Id As String)
      Try

         If Cache.Item(id) Is Nothing Then Exit Sub
         GetObjectFromCache(id)
         RemoveLastElement(id)
      Catch ex As Exception
         Throw New Exception("Impossibile rimuovere l'oggetto dalla cache." & vbCrLf & ex.Message)
      End Try
   End Sub
   Private Sub RemoveLastElement(ByVal id As String)
      Try
         If MAX_CACHE_SIZE = 0 Then Exit Sub
         If CurrentCacheSize = 0 Then
            Exit Sub
         Else
            'se c'è più di un elemento
            If Not Object.ReferenceEquals(lru, mru) Then
               Dim tempLink As LinkedList
               tempLink = mru
               tempLink.NextLink.PreviousLink = Nothing
               mru = tempLink.NextLink
               tempLink = Nothing
            End If
         End If
         Cache.Remove(id)
         CurrentCacheSize = Cache.Count
      Catch ex As Exception
         Throw New Exception("Impossibile rimuovere l'ultimo elemento dalla cache" & vbCrLf & ex.Message)
      End Try
   End Sub

   Public Function GetObjectFromCache(ByVal ObjId As String) As AbstractPersistenceObject
      Try
         Dim FoundLink As LinkedList = DirectCast(Cache.Item(ObjId), LinkedList)
         If FoundLink Is Nothing Then Return Nothing
         'se non sto cercando l'ultimo oggetto immesso
         If mru IsNot FoundLink Then
            If FoundLink.PreviousLink IsNot Nothing Then FoundLink.PreviousLink.NextLink = FoundLink.NextLink
            If FoundLink.NextLink IsNot Nothing Then
               FoundLink.NextLink.PreviousLink = FoundLink.PreviousLink
            Else
               If Not FoundLink.PreviousLink Is Nothing Then
                  lru = FoundLink.PreviousLink
                  FoundLink.PreviousLink.NextLink = Nothing
               End If

            End If
            If FoundLink.PreviousLink.PreviousLink Is Nothing Then
               FoundLink.PreviousLink.PreviousLink = FoundLink
            Else
               mru.PreviousLink = FoundLink
            End If

            FoundLink.PreviousLink = Nothing
            FoundLink.NextLink = mru
            mru = FoundLink
         End If
         Return FoundLink.Profile
      Catch ex As Exception
         Throw New Exception("Impossibile ottenere l'oggetto con Id " & ObjId & "  dalla cache. " & vbCrLf & ex.Message)
      End Try
   End Function






   ''' <summary>
   ''' Classe di supporto al servizio di caching
   ''' </summary>
   ''' <remarks></remarks>
   Private Class LinkedList
      Public Profile As AbstractPersistenceObject
      Public PreviousLink As LinkedList
      Public NextLink As LinkedList
   End Class
End Class
