Public Class UnitOfWork
    Protected NewObjects As New ArrayList
    Protected DirtyObjects As New ArrayList
    Protected RemovedObjects As New ArrayList
    Protected m_persistentService As IPersistenceFacade


    Protected ListOfCommands As New ArrayList
    Protected ISort As ISortStrategy
    Protected MappingService As IMapperFinder
    Public Sub New(ByVal DataMappingService As IMapperFinder, ByVal persistentService As IPersistenceFacade)
        MappingService = DataMappingService
        m_persistentService = persistentService
    End Sub
    Public Sub New(ByVal DataMappingService As IMapperFinder, ByVal SortStrategy As ISortStrategy, ByVal persistentService As IPersistenceFacade)
        ISort = SortStrategy
        MappingService = DataMappingService
        m_persistentService = persistentService
    End Sub
    Public Sub RegisterNew(ByVal Obj As AbstractPersistenceObject)
        Try
            Assert.Condition(Not DirtyObjects.Contains(Obj), "L'oggetto non è stato modificato")
            Assert.Condition(Not RemovedObjects.Contains(Obj), "L'oggetto non è stato rimosso")
            Assert.Condition(Not NewObjects.Contains(Obj), "L'oggetto non è stato registrato come nuovo")
            NewObjects.Add(Obj)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Public Sub RegisterDirty(ByVal Obj As AbstractPersistenceObject)
        Try
            'Assert.Condition((Obj.Key IsNot Nothing), "L'oggetto non ha un id nullo")
            'Assert.Condition((Obj.Key.ToString <> ""), "L'oggetto non ha un id nullo")
            Assert.Condition(Not RemovedObjects.Contains(Obj), "L'oggetto non è stato rimosso")
            If (Not DirtyObjects.Contains(Obj)) And (Not NewObjects.Contains(Obj)) Then
                DirtyObjects.Add(Obj)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Public Sub RegisterRemoved(ByVal Obj As AbstractPersistenceObject)
        Try
            'Assert.Condition((Obj.Key IsNot Nothing), "L'oggetto non ha un id nullo")
            'Assert.Condition((Obj.Key.ToString <> ""), "L'oggetto non ha un id nullo")
            If NewObjects.Contains(Obj) Then
                NewObjects.Remove(Obj)
                Return
            End If
            DirtyObjects.Remove(Obj)
            If Not RemovedObjects.Contains(Obj) Then
                RemovedObjects.Add(Obj)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    'Public Sub RegisterClean(ByVal Obj As AbstractPersistenceObject)
    '   Try
    '      Assert.Condition((Obj.Key IsNot Nothing), "L'oggetto non ha un id nullo")
    '      Assert.Condition((Obj.Key.ToString <> ""), "L'oggetto non ha un id nullo")
    '   Catch ex As Exception
    '      Exit Sub
    '   End Try
    'End Sub
    Protected Class Assert
        Public Shared Sub Condition(ByVal Condition As Boolean, ByVal Message As String)
            If Not Condition Then Throw New Exception(Message)
        End Sub
    End Class
    Protected Sub CreateListOfCommands()
        Try
            ListOfCommands = New ArrayList
            CreateInsertCommands()
            CreateUpdateCommands()
            CreateDeleteCommands()
        Catch ex As Exception
            Throw New Exception("Impossibile creare una lista di comandi per la transazione richiesta." & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Function GetMappingService() As IMapperFinder
        Return MappingService
    End Function
    Protected Sub CreateInsertCommands()
        Try
            For Each obj As AbstractPersistenceObject In NewObjects
                Dim IComm As ICommand = New DBInsertCommand(GetMapperForObject(obj), obj)
                ListOfCommands.Add(IComm)
            Next
        Catch ex As Exception
            Throw New Exception("Impossibile creare una lista di comandi di inserimento per la transazione richiesta." & vbCrLf & ex.Message)
        End Try
    End Sub
    Protected Sub CreateUpdateCommands()
        Try
            For Each obj As AbstractPersistenceObject In DirtyObjects
                Dim IComm As ICommand = New DBUpdateCommand(GetMapperForObject(obj), obj)
                ListOfCommands.Add(IComm)
            Next
        Catch ex As Exception
            Throw New Exception("Impossibile creare una lista di comandi di aggiornamento per la transazione richiesta." & vbCrLf & ex.Message)
        End Try
    End Sub
    Protected Sub CreateDeleteCommands()
        Try
            For Each obj As AbstractPersistenceObject In RemovedObjects
                Dim IComm As ICommand = New DBDeleteCommand(GetMapperForObject(obj), obj)
                ListOfCommands.Add(IComm)
            Next
        Catch ex As Exception
            Throw New Exception("Impossibile creare una lista di comandi di cancellazione per la transazione richiesta." & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Metodo hook dipendente dal metodo CreateListOfCommands che restituisce il mapper associato all'oggetto
    ''' di dominio specificato. Il metodo deve necessariamente essere sovrascritto poichè 
    ''' la classe astratta non conosce il meccanismo di mappatura tra gli oggetti di dominio
    ''' e i relativi mapper
    ''' </summary>
    ''' <param name="Obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetMapperForObject(ByVal Obj As AbstractPersistenceObject) As IMapper
        Return MappingService.GetMapperByObjectTypeName(Obj.GetType.Name)
    End Function
    ''' <summary>
    ''' Metodo per l'ordinamento dei comandi di inserimento, modifica, e cancellazione degli oggetti. Se in fase di creazione di 
    ''' un nuovo UnitOfWork non sarà specificato nessuna sottoclasse di ordinamento non verrà
    ''' eseguito alcun ordinamento sui comandi.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Sort()
        If Not ISort Is Nothing Then
            ListOfCommands = ISort.SortList(ListOfCommands)
        End If
    End Sub
    ''' <summary>
    ''' Metodo per preparare al commit dell'intera transazione. Esso Crea la lista dei comandi, la ordina 
    '''  ed esegue tutti i comandi. Al termine dell'esecuzione prepara la unit of work ad una nuova transazione
    ''' </summary>
    ''' <remarks></remarks>
    Public Overloads Sub Commit()
        Dim cnn As IDbConnection = GetConnection()

        CreateListOfCommands()
        Sort()
        If ListOfCommands.Count > 0 Then
            ExecuteCommit(cnn)
        End If
        CleanUnitOfWork()

    End Sub


    Public Overloads Sub Commit(ByVal GenericCommands As IList)
        Dim cnn As IDbConnection = GetConnection()
        Try

            cnn.Open()
            OpenDBTransaction(cnn)
            For Each IComm As ICommand In GenericCommands
                IComm.Execute()
            Next
            CommitDBTransaction(cnn)
            cnn.Close()
        Catch ex As Exception
            RollBack(cnn)
            Throw New Exception("Impossibile eseguire il commit della transazione" & vbCrLf & ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' Metodo che esegue il commit sul database. Esso apre la connessione e la transazione, esegue tutti i comandi schedulati
    ''' esegue il commit e chide la transazione. Se qualcosa non va bene invoca il rollback della transazione
    ''' </summary>
    ''' <param name="cnn"></param>
    ''' <remarks></remarks>
    Protected Sub ExecuteCommit(ByVal cnn As IDbConnection)
        Try
            cnn.Open()
            OpenDBTransaction(cnn)
            For Each IComm As ICommand In ListOfCommands
                IComm.Execute()
            Next
            CommitDBTransaction(cnn)
            cnn.Close()
        Catch ex As Exception
            RollBack(cnn)
            Throw New Exception("Impossibile eseguire il commit della transazione" & vbCrLf & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo che prepara la unit of work ad un'altra transazione pulendo tutte le variabili di stato
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CleanUnitOfWork()
        NewObjects = New ArrayList
        DirtyObjects = New ArrayList
        RemovedObjects = New ArrayList
        ListOfCommands = New ArrayList
    End Sub
    ''' <summary>
    ''' Metodo che restituisce la connessione corrente chiusa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetConnection() As IDbConnection
        Try
            Dim cnn As IDbConnection
            cnn = m_persistentService.CurrentConnection
            If cnn.State = ConnectionState.Open Then cnn.Close()
            Return cnn
        Catch ex As Exception
            Throw New Exception("Impossibile ottenere la connessione corrente." & vbCrLf & ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Metodo che  apre una transazione al tipo di database corrente 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub OpenDBTransaction(ByVal Connection As IDbConnection)
        Try
            DBTransaction.BeginTransaction(Connection, m_persistentService.ServiceName)
        Catch ex As Exception
            Throw New Exception("Impossibile aprire la transazione." & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Metodo  che esegue il commit di una transazione sul tipo di database corrente 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CommitDBTransaction(ByVal Connection As IDbConnection)
        Try
            DBTransaction.CommitTransaction(Connection, m_persistentService.ServiceName)
            If Connection.State = ConnectionState.Open Then Connection.Close()
        Catch ex As Exception
            If Connection.State = ConnectionState.Open Then Connection.Close()
            Throw New Exception("Impossibile eseguire il commit della transazione." & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Metodo  che esegue il rollback di una transazione sul tipo di database corrente 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub RollbackDBTransaction(ByVal Connection As IDbConnection)
        Try
            DBTransaction.RollBackTransaction(Connection, m_persistentService.ServiceName)
            If Connection.State = ConnectionState.Open Then Connection.Close()
        Catch ex As Exception
            If Connection.State = ConnectionState.Open Then Connection.Close()
        End Try
    End Sub
    Public Sub RollBack(ByVal Connection As IDbConnection)

        RollbackDBTransaction(Connection)
        For Each elem As AbstractPersistenceObject In NewObjects
            elem.SetKeyToNothing()
        Next
    End Sub
End Class
