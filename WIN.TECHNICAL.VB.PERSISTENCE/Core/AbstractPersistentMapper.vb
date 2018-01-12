Public MustInherit Class AbstractPersistentMapper
   Implements IMapper
    Protected Cache As PersistentObjectCache
    Protected UseDefaultCacheMechanism As Boolean = True
   Public MustOverride Sub Delete(ByVal item As AbstractPersistenceObject) Implements IMapper.Delete
   'Public MustOverride Function Exist(ByVal item As AbstractPersistenceObject) As Boolean Implements IMapper.Exist
   Public MustOverride Function FindAll() As System.Collections.IList Implements IMapper.FindAll
   Public MustOverride Sub Update(ByVal item As AbstractPersistenceObject) Implements IMapper.Update
   Public MustOverride Function FindByCriteria(ByVal FindByCriteriaStatement As String) As System.Collections.IList Implements IMapper.FindByCriteria
   Public MustOverride Function FindByQuery(ByVal Query As String) As System.Collections.IList Implements IMapper.FindByQuery
    Protected m_PersistentService As IPersistenceFacade


    Public Sub EmptyChache() Implements IMapper.EmptyCache
        Dim size As Int32 = Cache.MaxCacheSize


        Cache = New PersistentObjectCache(size)


    End Sub



    Protected m_IsAutoIncrementID As Boolean = False

   Public Sub SetPersistentService(ByVal PersistentService As IPersistenceFacade)
        m_PersistentService = PersistentService
        If UseDefaultCacheMechanism Then
            Cache = New PersistentObjectCache(PersistentService.CacheSize)
        End If
   End Sub

#Region "Testo per i  comandi CRUD da eseguire sui dati dello storage"



    Public Function PublicFindAllStatement() As String
        Return FindAllStatement()
    End Function


   ''' <summary>
   ''' Metodo che restituisce il testo della query fondamentale che ricerca un oggetto secondo il proprio Id dallo storage
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function FindByKeyStatement() As String
   ''' <summary>
   ''' Metodo che restituisce il testo della query fondamentale che ricerca un insieme di oggetti appartenenti allo storage
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function FindAllStatement() As String
   ''' <summary>
   ''' Metodo che restituisce il testo del comando per inserire un oggetto nella base dati
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
    Protected MustOverride Function InsertStatement() As String

   ''' <summary>
   ''' Metodo che restituisce il testo del comando per aggiornare un oggetto nella base dati
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function UpdateStatement() As String
   ''' <summary>
   ''' Metodo che restituisce il testo del comando per cancellare un oggetto nella base dati
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function DeleteStatement() As String
   ''' <summary>
   ''' Metodo che restituisce la query per cercare il nuovo id dell'oggetto all'interno dello storage
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function FindNextKeyStatement() As String
#End Region

#Region "Metodi per la ricerca secondo una chiave specificata"
   ''' <summary>
   ''' Metodo che deve essere sovrascritto dalle sottoclassi per recuperare l'oggetto in base al suo identificativo. 
   ''' Il metodo eseguirà il cast dell'oggetto nel suo tipo concreto e creerà un oggetto Key inizializzandolo
   ''' con il parametro inserito
   ''' </summary>
   ''' <param name="Id"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public MustOverride Function FindObjectById(ByVal Id As Int32) As AbstractPersistenceObject Implements IMapper.FindById


   Public Overridable Function FindObjectByIdReloadingCache(ByVal Id As Int32) As AbstractPersistenceObject Implements IMapper.FindByIdReloadingCache
      Throw New NotImplementedException
   End Function
   ''' <summary>
   ''' Metodo per la ricerca di un elemento nello Storage attraverso la sua chiave identificativa. Risponde alla query
   ''' FindByKeyStatement. Esso verifica la presenza dell'oggetto nella cache. se lo trova lo restituisce altrimenti
   ''' richiede l'ausilio del metodo GetObjectFromStorage. Può restituire l'oggetto trovato altrimenti il metodo
   ''' restituirà un oggetto nullo.
   ''' </summary>
   ''' <param name="key"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected Overridable Function FindByKey(ByVal key As Key) As AbstractPersistenceObject
      Try
         Dim obj As AbstractPersistenceObject = Cache.GetObjectFromCache(Key.ToString)
         If obj Is Nothing Then 'recupero l'oggetto dalla base dati
            obj = GetObjectFromStorage(Key)
         End If
         Return obj
      Catch ex As Exception
         Throw New Exception("Impossibile trovare e recuperare l'oggetto con id = " & Key.ToString & vbCrLf & ex.Message)
      End Try
   End Function


   Protected Overridable Function FindByKeyReloadingCache(ByVal key As Key) As AbstractPersistenceObject
      Try
         Dim obj As AbstractPersistenceObject
         Cache.RemoveObjectFromCache(key.ToString)
         obj = GetObjectFromStorage(key)
         Return obj
      Catch ex As Exception
         Throw New Exception("Impossibile trovare e recuperare l'oggetto con id = " & key.ToString & vbCrLf & ex.Message)
      End Try
   End Function



   ''' <summary>
   ''' Metodo hook per dipendente dal metodo FindByKey per recuperare l'oggetto dallo storage e caricarlo nella cache
   ''' </summary>
   ''' <param name="Key"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function GetObjectFromStorage(ByVal Key As Key) As AbstractPersistenceObject

#End Region

#Region "Metodi per l'inserimento"

   ''' <summary>
   ''' Metodo sovrascivibile per l'inserimento dell'oggetto nello storage indicato dalla sottoclasse.
   ''' </summary>
   ''' <param name="item"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Overridable Function Insert(ByVal item As AbstractPersistenceObject) As Key Implements IMapper.Insert

        If item.IsValid Then
            If m_IsAutoIncrementID Then
                Return PerformInsert(item)
            Else
                Return PerformInsert(item, FindNextKey())
            End If
        Else
            Dim errors As String = ""
            For Each elem As String In item.ValidationErrors
                errors = errors & elem & vbCrLf
            Next
            Throw New Exception("L'oggetto inserito non è valido. Controllare il valore dei valori immessi" & vbCrLf & errors)
        End If

   End Function
   ''' <summary>
   ''' Metodo hook dipendente da Insert che prepara l'inserimento attraverso la ricerca di un identificativo
   ''' appropriato. Esso assegna l'identificativo trovato all'oggetto da inserire, inserisce l'oggetto tramite il metodo DoInsert, lo aggiunge alla cache
   ''' e ne restituisce l'identificativo
   ''' </summary>
   ''' <param name="item"></param>
   ''' <param name="key"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected Overridable Function PerformInsert(ByVal item As AbstractPersistenceObject, ByVal key As Key) As Key
      Try
         item.Key = key
         DoInsert(item)
         Cache.AddToCache(item)
         Return key
      Catch ex As Exception
         item.SetKeyToNothing()
         Throw
      End Try
    End Function



    Protected Overridable Function PerformInsert(ByVal item As AbstractPersistenceObject) As Key
        Try
            DoInsert(item)
            Cache.AddToCache(item)
            Return item.Key
        Catch ex As Exception
            item.SetKeyToNothing()
            Throw
        End Try
    End Function
   ''' <summary>
   ''' Metodo hook per inserire l'oggetto nello storage. Esso dipende direttamente dal metodo Perform insert.
   ''' </summary>
   ''' <param name="item"></param>
   ''' <remarks></remarks>
   Protected MustOverride Sub DoInsert(ByVal item As AbstractPersistenceObject)
   ''' <summary>
   ''' Metodo che deve essere sovrascritto dalla classe concreta per la generazione di un identificativo appropriato
   ''' per l'oggetto da inserire nello storage.
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected MustOverride Function FindNextKey() As Key

#End Region

End Class
