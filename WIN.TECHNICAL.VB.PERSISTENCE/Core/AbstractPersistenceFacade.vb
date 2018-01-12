Public MustInherit Class AbstractPersistenceFacade
    Implements IPersistenceFacade









    Protected m_DbType As DB.DBType
    Protected m_ServiceName As String
    Protected m_CacheSize As Int32
    Protected m_UnitOfWork As UnitOfWork
    Protected m_CurrentConnection As IDbConnection

    'Protected m_MapperFinder As IMapperFinder

    Protected m_MapperFinder As IMapperFinder
#Region "Metodi di inizializzazione del servizio"
    Public Sub New(ByVal ServiceName As String, ByVal ConnectionString As String, ByVal Cachesize As Int32)
        m_ServiceName = ServiceName
        m_CurrentConnection = DBTypeUtils.GetConnection(m_ServiceName, ConnectionString)
        m_CacheSize = Cachesize
        Initialize()
    End Sub


    Public Overridable Function ExecuteSqlQuery(ByVal SQLQuery As String) As IList Implements IPersistenceFacade.ExecuteSqlQuery
        Dim rs As IDataReader = Nothing
        Dim DataList As ArrayList

        Try
            Dim cmd As IDbCommand = GetCommand(SQLQuery)
            rs = cmd.ExecuteReader
            DataList = LoadDataMatrix(rs)
            rs.Close()
            Return DataList
        Finally
            If Not rs Is Nothing Then
                If Not rs.IsClosed Then rs.Close()
                rs.Dispose()
            End If
        End Try
    End Function

    Protected Function GetCommand(ByVal CommandText As String) As IDbCommand
        Return DBTypeUtils.GetCommand(Me.ServiceName, CommandText, Me.CurrentConnection)
    End Function

    Protected Function LoadDataMatrix(ByVal rs As IDataReader) As ArrayList
        Dim List As New ArrayList
        While (rs.Read)
            List.Add(LoadHashTableDataFromDatareader(rs))
        End While
        Return List
    End Function

    Protected Overridable Function LoadHashTableDataFromDatareader(ByVal rs As IDataReader) As Hashtable

        Dim DataHash As New Hashtable
        For I As Int32 = 0 To rs.FieldCount - 1
            Dim name As String = rs.GetName(I)
            Dim Value As Object = IIf(IsDBNull(rs(name)), Nothing, rs(name))
            DataHash.Add(name, Value)
        Next
        Return DataHash

    End Function
    ''' <summary>
    ''' Esegue una non query del tipo insert, update, delete o anche begin transaction ecc.
    ''' Nel caso di begin transaction dare sempre un nome alla transazione per non confliggere
    ''' con la transazione interna
    ''' </summary>
    ''' <param name="SQLQuery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function ExecuteNonQuery(ByVal SQLQuery As String) As Int32 Implements IPersistenceFacade.ExecuteNonQuery
        Dim command As IDbCommand = CommandFactory.GetCommand(Me.DBType)
        command.CommandText = SQLQuery
        command.Connection = m_CurrentConnection 'DBConnectionManager.Instance.GetCurrentConnection
        Dim i As Int32 = 0

        If command.Connection.State = ConnectionState.Open Then
            Return command.ExecuteNonQuery
        Else
            Try
                command.Connection.Open()
                i = command.ExecuteNonQuery
                command.Connection.Close()
                Return i
            Catch ex As Exception
                If command.Connection.State = ConnectionState.Open Then
                    command.Connection.Close()
                End If
                Throw
            End Try


        End If



    End Function




    Public Overridable Function ExecuteScalar(ByVal SQLQuery As String) As Object Implements IPersistenceFacade.ExecuteScalar
        Dim command As IDbCommand = CommandFactory.GetCommand(Me.DBType)
        command.CommandText = SQLQuery
        command.Connection = m_CurrentConnection 'DBConnectionManager.Instance.GetCurrentConnection
        Dim i As Object = Nothing

        If command.Connection.State = ConnectionState.Open Then
            Return command.ExecuteScalar
        Else
            Try
                command.Connection.Open()
                i = command.ExecuteScalar
                command.Connection.Close()
                Return i
            Catch ex As Exception
                If command.Connection.State = ConnectionState.Open Then
                    command.Connection.Close()
                End If
                Throw
            End Try


        End If



    End Function








    ''' <summary>
    ''' Metodo hook che restituisce il nome del tipo di servizio di persistenza
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ServiceName() As String Implements IPersistenceFacade.ServiceName
        Get
            Return m_ServiceName
        End Get
    End Property


    ''' <summary>
    ''' Metodo hook usato per l'inizializzazione del servizio di persistenza. Vengono create le connessioni nel
    ''' ConnectionDBManager. Vengono inizializzati tutti i mapper del sottosistema.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Initialize()
#End Region

    Friend Sub SetMapperFinder(ByVal MapperFinder As IMapperFinder) Implements IPersistenceFacade.SetMapperFinder
        m_MapperFinder = MapperFinder
    End Sub

#Region "Metodi di accesso diretto alla base dati"

    ''' <summary>
    ''' Metodo  per il recupero del mapper che gestisce il tipo dell'oggetto di dominio selezionato
    ''' </summary>
    ''' <param name="ObjectTypeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetMapper(ByVal ObjectTypeName As String) As IMapper
        Return DirectCast(m_MapperFinder.GetMapperByObjectTypeName(ObjectTypeName), IMapper)
    End Function

    Public Sub DeleteObject(ByVal ObjectTypeName As String, ByVal Item As AbstractPersistenceObject) Implements IPersistenceFacade.DeleteObject
        Try
            If Item.NonCancellabile = True Then
                Throw New ObjectNotDeletableException
            End If
            Dim mapper As IMapper = DirectCast(GetMapper(ObjectTypeName), IMapper)
            BeginAtomicTransaction()
            mapper.Delete(Item)
            CommitAtomicTransaction()
        Catch ex As ObjectNotDeletableException
            Throw
        Catch ex As Exception
            RollBackAtomicTransaction()
            Throw
        End Try
    End Sub
    Public Function GetAllObjects(ByVal ObjectTypeName As String) As System.Collections.IList Implements IPersistenceFacade.GetAllObjects
        Try
            Dim list As New ArrayList
            Dim mapper As IMapper = DirectCast(GetMapper(ObjectTypeName), IMapper)
            BeginAtomicTransaction()
            list = mapper.FindAll
            CommitAtomicTransaction()
            Return list
        Catch ex As Exception
            RollBackAtomicTransaction()
            Throw
        End Try
    End Function

    Public Function GetObjectReloadingCache(ByVal ObjectTypeName As String, ByVal IdObject As Integer) As AbstractPersistenceObject Implements IPersistenceFacade.GetObjectReloadingCache
        Try
            Dim obj As AbstractPersistenceObject
            Dim mapper As IMapper = DirectCast(GetMapper(ObjectTypeName), IMapper)
            BeginAtomicTransaction()
            obj = mapper.FindByIdReloadingCache(IdObject)
            CommitAtomicTransaction()
            Return obj
        Catch ex As Exception
            RollBackAtomicTransaction()
            Throw
        End Try
    End Function




    Public Function GetObject(ByVal ObjectTypeName As String, ByVal IdObject As Integer) As AbstractPersistenceObject Implements IPersistenceFacade.GetObject
        Try
            Dim obj As AbstractPersistenceObject
            Dim mapper As IMapper = DirectCast(GetMapper(ObjectTypeName), IMapper)
            BeginAtomicTransaction()
            obj = mapper.FindById(IdObject)
            CommitAtomicTransaction()
            Return obj
        Catch ex As Exception
            RollBackAtomicTransaction()
            Throw
        End Try
    End Function
    Public Sub InsertObject(ByVal ObjectTypeName As String, ByVal Item As AbstractPersistenceObject) Implements IPersistenceFacade.InsertObject
        Try
            Dim mapper As IMapper = DirectCast(GetMapper(ObjectTypeName), IMapper)
            BeginAtomicTransaction()
            mapper.Insert(Item)
            CommitAtomicTransaction()
        Catch ex As Exception
            RollBackAtomicTransaction()
            Throw
        End Try
    End Sub
    Public Sub UpdateObject(ByVal ObjectTypeName As String, ByVal Item As AbstractPersistenceObject) Implements IPersistenceFacade.UpdateObject
        Try
            Dim mapper As IMapper = DirectCast(GetMapper(ObjectTypeName), IMapper)
            BeginAtomicTransaction()
            mapper.Update(Item)
            CommitAtomicTransaction()
        Catch ex As Exception
            RollBackAtomicTransaction()
            Throw
        End Try
    End Sub

#End Region

#Region "Metodi per la gestione delle transazioni atomiche (Senza l'uso della unit of work)"
    Protected Sub RollBackAtomicTransaction()
        Dim cnn As IDbConnection = Nothing
        Try
            cnn = m_CurrentConnection 'DBConnectionManager.Instance.GetCurrentConnection()
            DBTransaction.RollBackTransaction(cnn, m_ServiceName)
            cnn.Close()
        Catch ex As Exception
            CloseConnection(cnn)
        End Try
    End Sub
    Protected Sub CommitAtomicTransaction()
        Dim cnn As IDbConnection = Nothing
        Try
            cnn = m_CurrentConnection 'DBConnectionManager.Instance.GetCurrentConnection()
            DBTransaction.CommitTransaction(cnn, m_ServiceName)
            cnn.Close()
        Catch ex As Exception
            CloseConnection(cnn)
            Throw New Exception("Impossibile eseguire il commit di una transazione per un'operazione atomica" & vbCrLf & ex.Message)
        End Try
    End Sub
    Protected Sub CloseConnection(ByVal Connection As IDbConnection)
        Try
            If Not Connection Is Nothing Then
                Connection.Close()
            End If
        Catch ex1 As Exception
            '
        End Try
    End Sub
    Protected Sub BeginAtomicTransaction()
        Dim cnn As IDbConnection
        Try
            cnn = m_CurrentConnection ' DBConnectionManager.Instance.GetCurrentConnection()
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Open()
            DBTransaction.BeginTransaction(cnn, m_ServiceName)
        Catch ex As Exception
            Throw New Exception("Impossibile aprire una transazione per un'operazione atomica" & vbCrLf & ex.Message)
        End Try
    End Sub
#End Region




#Region "Metodi per la gestione delle transazioni"


    ''' <summary>
    ''' Metodo che marca come modificato l'oggetto selezionato
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <remarks></remarks>
    Public Sub MarkDirty(ByVal Item As AbstractPersistenceObject) Implements TECHNICAL.PERSISTENCE.IPersistenceFacade.MarkDirty
        If Not m_UnitOfWork Is Nothing Then
            m_UnitOfWork.RegisterDirty(Item)
        Else
            Throw New Exception("Impossibile marcare come modificato l'oggetto selezionato poichè nessuna transazione è attiva. Iniziare una nuova transazione")
        End If
    End Sub
    ''' <summary>
    ''' Metodo che marca come nuovo l'oggetto selezionato
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <remarks></remarks>
    Public Sub MarkNew(ByVal Item As AbstractPersistenceObject) Implements TECHNICAL.PERSISTENCE.IPersistenceFacade.MarkNew
        If Not m_UnitOfWork Is Nothing Then
            m_UnitOfWork.RegisterNew(Item)
        Else
            Throw New Exception("Impossibile marcare come nuovo l'oggetto selezionato poichè nessuna transazione è attiva. Iniziare una nuova transazione")
        End If
    End Sub
    ''' <summary>
    ''' Metodo che marca come rimosso l'oggetto selezionato
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <remarks></remarks>
    Public Sub MarkRemoved(ByVal Item As AbstractPersistenceObject) Implements TECHNICAL.PERSISTENCE.IPersistenceFacade.MarkRemoved
        If Not m_UnitOfWork Is Nothing Then
            m_UnitOfWork.RegisterRemoved(Item)
        Else
            Throw New Exception("Impossibile marcare come rimosso l'oggetto selezionato poichè nessuna transazione è attiva. Iniziare una nuova transazione")
        End If
    End Sub


    ''' <summary>
    ''' Metodo per eseguire il rollback della transazione
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RollBackTRansaction() Implements IPersistenceFacade.RollBackTRansaction
        ClearUnitOfWork()
    End Sub
    ''' <summary>
    ''' Metodo per eseguire il commit della transazione
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CommitTransaction() Implements IPersistenceFacade.CommitTransaction

        If Not m_UnitOfWork Is Nothing Then
            m_UnitOfWork.Commit()
            ClearUnitOfWork()
        Else
            Throw New Exception("Impossibile eseguire il commit di una transazione non ancora iniziata. Iniziare una nuova transazione")
        End If

    End Sub



    ''' <summary>
    ''' Metodo  per incominciare una qualunque transazione. Viene inizializzata la unit of work
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BeginTransaction() Implements IPersistenceFacade.BeginTransaction
        m_UnitOfWork = New UnitOfWork(m_MapperFinder, Me)
    End Sub
    ''' <summary>
    ''' Metodo  per incominciare una qualunque transazione. Viene fornito un algoritmo di ordinamento per
    ''' i comandi transazionali che verranno eseguiti
    ''' </summary>
    ''' <param name="SortService"></param>
    ''' <remarks></remarks>
    Public Sub BeginTransaction(ByVal SortService As ISortStrategy) Implements IPersistenceFacade.BeginTransaction
        m_UnitOfWork = New UnitOfWork(m_MapperFinder, SortService, Me)
    End Sub

    Protected Sub ClearUnitOfWork()
        m_UnitOfWork = New UnitOfWork(m_MapperFinder, Me)
        m_UnitOfWork = Nothing
    End Sub

#End Region


    Public Function GetMapperByName(ByVal MapperName As Object) As Object Implements IPersistenceFacade.GetMapperByName
        Return m_MapperFinder.GetMapperByName(MapperName)
    End Function

    'Public Function GetMapperForObject(ByVal PersistentObject As AbstractPersistenceObject) As IMapper Implements IPersistenceFacade.GetMapperForObject
    '   Return m_MapperFinder.GetMapperByObjectTypeName(PersistentObject.GetType.Name)
    'End Function

    Public Function CreateNewQuery(ByVal DomainClassName As String) As Query Implements IPersistenceFacade.CreateNewQuery
        Return New Query(DomainClassName)
    End Function

    Public ReadOnly Property DBType() As DB.DBType Implements IPersistenceFacade.DBType
        Get
            Return DBTypeUtils.GetDBTYpe(m_ServiceName)
        End Get
    End Property

    Public Function CreateNewPaginationQuery(ByVal DomainClassName As String, ByVal MaxQueryResult As Int32, ByVal MaxResultPerPage As Int32) As PaginationQueryHandler Implements IPersistenceFacade.CreateNewPaginationQuery
        Return QueryFactory.GetPaginationQuery(Me, DomainClassName, MaxQueryResult, MaxResultPerPage)
    End Function

    Public Sub ExecuteQueryList(ByVal QueryCommands As IList(Of String)) Implements IPersistenceFacade.ExecuteQueryList

        Dim commands As IList(Of DBQueryExecuteCommand) = New List(Of DBQueryExecuteCommand)

        For Each query As String In QueryCommands
            Dim command As IDbCommand = CommandFactory.GetCommand(Me.DBType)
            command.CommandText = query
            command.Connection = m_CurrentConnection
            commands.Add(New DBQueryExecuteCommand(command))

        Next

        m_UnitOfWork = New UnitOfWork(m_MapperFinder, Me)
        m_UnitOfWork.Commit(commands)



    End Sub

    Public Property CurrentConnection() As System.Data.IDbConnection Implements IPersistenceFacade.CurrentConnection
        Get
            Return m_CurrentConnection
        End Get
        Set(ByVal value As System.Data.IDbConnection)
            m_CurrentConnection = value
        End Set
    End Property

    Public ReadOnly Property CacheSize() As Integer Implements IPersistenceFacade.CacheSize
        Get
            Return m_CacheSize
        End Get
    End Property

    Public Sub EmptyCacheAll() Implements IPersistenceFacade.EmptyCacheAll
        Dim maps As IEnumerator = m_MapperFinder.DBMappers.GetEnumerator
        While maps.MoveNext
            Dim map As IMapper = DirectCast(maps.Current, IMapper)
            map.EmptyCache()
        End While

    End Sub

    Public Sub EmptyChache(ObjectTypeName As String) Implements IPersistenceFacade.EmptyChache
        Dim mapper As IMapper = GetMapper(ObjectTypeName)
        If Not mapper Is Nothing Then
            mapper.EmptyCache()
        End If
    End Sub
End Class
