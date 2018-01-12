Public MustInherit Class AbstractRDBMapper
    Inherits AbstractPersistentMapper


#Region "Funzioni per la query FindAllStatement"
    'Protected MustOverride Function FindAllStatement() As String
    ''' <summary>
    ''' Metodo pubblico per la ricerca di tutti gli oggetti provenienti dalla query FindAllStatement.
    ''' Esso crea il comando per la ricerca, e richiede al metodo LoadAll il caricamento di tutti i record presenti 
    ''' nel datareader. Restituisce la lista.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function FindAll() As System.Collections.IList
        Dim rs As IDataReader = Nothing
        Dim DataList As ArrayList
        Dim ObjectList As IList
        Try
            rs = GetDBRecordData()
            DataList = LoadDataMatrix(rs)
            rs.Close()
            ObjectList = Me.LoadAll(DataList)
            Return ObjectList
        Catch ex As Exception
            Throw New ApplicationException("Impossibile caricare la lista di tutti gli oggetti presenti nella tabella. " & vbCrLf & ex.Message)
        Finally
            ReleaseDBDatareader(rs)
            'libero la memoria
            DataList = New ArrayList
        End Try
    End Function
    ''' <summary>
    ''' Metodo che restituisce una lista con gli id di tutti i record presenti nel datareader. Di default
    ''' verrà inserito nella lista il campo Id di ogni record.
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function LoadIDList(ByVal rs As IDataReader) As ArrayList
        Dim arr As New ArrayList
        If rs.Read Then
            Do
                arr.Add(rs("Id"))
                If Not rs.Read Then Exit Do
            Loop
        End If
        Return arr
    End Function

    Protected Overridable Function LoadObjectInList(ByVal IDsList As ArrayList) As ArrayList

        Dim list As New ArrayList
        For i As Integer = 0 To IDsList.Count - 1
            list.Add(GetObjectFromStorage(New Key(DirectCast(IDsList.Item(i), Int32))))
        Next
        Return list

    End Function
    ''' <summary>
    ''' Metodo sovrascrivibile che carica il datareader completo con i dati di oggetti multipli
    ''' Esso crea il comando con lo statement FindAllStatement ed esegue il comando
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Overloads Function GetDBRecordData() As IDataReader

        Dim cmd As IDbCommand = GetCommand(FindAllStatement)
        Return cmd.ExecuteReader

    End Function
    Protected Overridable Overloads Function GetDBRecordData(ByVal Statement As String) As IDataReader

        Dim cmd As IDbCommand = GetCommand(Statement)
        cmd.CommandTimeout = 90
        Return cmd.ExecuteReader()
    End Function
    '''' <summary>
    '''' Obsoleto. Non usare
    '''' </summary>
    '''' <param name="rs"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Overloads Function LoadAll(ByVal rs As IDataReader) As ArrayList
    '   Dim List As New ArrayList
    '   Try
    '      While (rs.Read)
    '         List.Add(Load(rs))
    '      End While
    '      Return List
    '   Catch ex As Exception
    '      Throw New Exception(ex.Message)
    '   End Try
    'End Function
    ''' <summary>
    ''' Metodo che provvede al caricamento di oggetti multipli. Ogni hashtable nella lista contiene tutti gli elementi di un record e permette il caricamento di un oggetto.
    ''' Il metodo usa il metodo Load per caricare il singolo oggetto all'interno di un ciclo su tutti i record del datareader.
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function LoadAll(ByVal rs As ArrayList) As ArrayList
        Dim List As New ArrayList

        For Each elem As Hashtable In rs
            List.Add(Load(elem))
        Next
        Return List

    End Function

    ''' <summary>
    ''' Metodo che copia il contenuto di un datareader in una lista di hashtables
    ''' per permettere la chiusura immediata del datareader
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function LoadDataMatrix(ByVal rs As IDataReader) As ArrayList
        Dim List As New ArrayList
        While (rs.Read)
            List.Add(LoadHashTableDataFromDatareader(rs))
        End While
        Return List
    End Function
#End Region

#Region "Metodi per la ricerca di un' oggetto dalla base dati attraverso un la chiave identificativa"
    'Protected MustOverride Function FindNextKey() As Key
    ''' <summary>
    ''' Metodo hook per dipendente dal metodo FindByKey per recuperare l'oggetto dalla base dati e caricarlo nella cache.
    ''' Esso richiede il record dei dati per l'oggetto con id contenuto nell'oggetto Key e se il datareader
    ''' non è vuoto carica per mezzo del metodo Load l'oggetto contenuto nel record trovato e lo restituisce.
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function GetObjectFromStorage(ByVal Key As Key) As AbstractPersistenceObject
        Dim rs As IDataReader = Nothing
        Dim dataHash As Hashtable
        Try
            'qui devo leggere l'oggetto in un datareader e caricarlo

            If m_PersistentService.CurrentConnection.State = ConnectionState.Closed Then m_PersistentService.CurrentConnection.Open()
            rs = GetDBRecordData(Key)
            If rs.Read Then
                'questo passaggio è necessario poichè in tutti quegli oggetti i cui dati
                'fanno riferimento ad oggetti in altre tabelle, non è possibile caricare
                'l'oggetto mantenedo il datareader aperto. Una possibile soluzione sarebbe potuta essere
                'quella di aprire diverse connessioni ma non era decisamente il caso
                dataHash = LoadHashTableDataFromDatareader(rs)
                rs.Close()
                Dim obj As AbstractPersistenceObject = Load(dataHash)
                Return obj
            End If
            Return Nothing
        Finally
            ReleaseDBDatareader(rs)
            dataHash = New Hashtable
        End Try
    End Function
    ''' <summary>
    ''' Metodo che carica il contenuto di una riga di datareader in una hashtable. Di default i valori DBNULL 
    ''' diventeranno riferimenti nulli.
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' Metodo hook per la creazione del comando con il command text per la ricerca 
    ''' di un oggetto secondo la sua chiave identificativa definito dalla sottoclasse e 
    ''' l'oggetto connection definito da una classe singleton che fornisce le connessioni al database
    ''' relativo al sottosistema istanziato.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetCommand(ByVal CommandText As String) As IDbCommand
        Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, m_PersistentService.CurrentConnection)
    End Function
    ''' <summary>
    '''Metodo hook per il caricamento dei parametri dell'oggetto che deve essere cercato nell'oggetto command specificato Di default
    ''' è caricato un parametro di nome "Id" che ha come valore il valore long dell'oggetto Key.
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Command"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadFindByKeyCommandParameters(ByVal Key As Key, ByVal Command As IDbCommand)

        Dim param As IDbDataParameter = Command.CreateParameter
        param.ParameterName = "@Id"
        param.Value = Key.LongValue
        Command.Parameters.Add(param)

    End Sub
    ''' <summary>
    ''' Metodo sovrascrivibile che carica il datareader completo con i dati dell'oggetto richiesto. 
    ''' Esso crea il comando con lo statement FindByKeyStatement, carica i parametri del comando 
    ''' con l'identificativo dell'oggetto presente nell'oggetto Key  ed esegue il comando
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Overloads Function GetDBRecordData(ByVal Key As Key) As IDataReader

        Dim cmd As IDbCommand = GetCommand(FindByKeyStatement)
        LoadFindByKeyCommandParameters(Key, cmd)
        Dim rs As IDataReader = cmd.ExecuteReader
        Return rs

    End Function
    '''' <summary>
    '''' obsoleto
    '''' </summary>
    '''' <param name="rs"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Overridable Overloads Function Load(ByVal rs As IDataReader) As AbstractPersistenceObject
    '   Try
    '      Dim key As Key = CreateKey(rs)
    '      If Cache.GetObjectFromCache(key.ToString) IsNot Nothing Then Return DirectCast(Cache.GetObjectFromCache(key.ToString), AbstractPersistenceObject)
    '      Dim obj As AbstractPersistenceObject = DoLoad(key, rs)
    '      Cache.AddToCache(obj)
    '      Return obj
    '   Catch ex As Exception
    '      Throw New Exception(ex.Message)
    '   End Try
    'End Function
    ''' <summary>
    ''' Metodo sovrascrivibile per caricare un oggetto a partire dal record che ne contiene i dati. Il metodo verifica inoltre la presenza dell'oggetto nella cache.
    ''' Una volta creata la chiave per l'oggetto da caricare previa verifica dell'esistenza dell'oggetto nella cache, ne affida il caricamento al metodo hook DoLoad
    ''' che carica tutti i dati sensibili dell'oggetto. Al termine del caricamento l'oggetto viene aggiunto alla cache.
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function Load(ByVal rs As Hashtable) As AbstractPersistenceObject

        Dim key As Key = CreateKey(rs)
        If Cache.GetObjectFromCache(key.ToString) IsNot Nothing Then Return DirectCast(Cache.GetObjectFromCache(key.ToString), AbstractPersistenceObject)
        Dim obj As AbstractPersistenceObject = DoLoad(key, rs)
        Cache.AddToCache(obj)
        Return obj

    End Function
    '''' <summary>
    '''' Obsoleto
    '''' </summary>
    '''' <param name="rs"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected Overridable Overloads Function CreateKey(ByVal rs As IDataReader) As Key
    '   Try
    '      Dim id As Int32 = rs("Id")
    '      Dim k As Key = New Key(id)
    '      Return k

    '   Catch ex As Exception
    '      Throw New Exception("Impossibile creare una chiave dal record dei dati corrente." & vbCrLf & ex.Message)
    '   End Try
    'End Function
    ''' <summary>
    ''' Metodo sovrascrivibile per creare di default la chiave di un oggetto presente nella hashtable dei dati. Di default viene ricercato
    ''' nel record il campo con nome = "Id" e viene creato un oggetto Key col valore del campo citato.
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Overloads Function CreateKey(ByVal rs As Hashtable) As Key

        Dim id As Int32 = rs.Item("ID")
        Dim k As Key = New Key(id)
        Return k


    End Function
    '''' <summary>
    '''' Obsoleto
    '''' </summary>
    '''' <param name="Key"></param>
    '''' <param name="rs"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Protected MustOverride Function DoLoad(ByVal Key As Key, ByVal rs As IDataReader) As AbstractPersistenceObject
    ''' <summary>
    ''' Metodo hook per il caricamento di tutti i dati di un oggetto presenti nella hashtable
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="rs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function DoLoad(ByVal Key As Key, ByVal rs As Hashtable) As AbstractPersistenceObject


#End Region

    ''' <summary>
    ''' Rilascia le risorse di database
    ''' </summary>
    ''' <param name="rs"></param>
    ''' <remarks></remarks>
    Protected Sub ReleaseDBDatareader(ByVal rs As IDataReader)
        If Not rs Is Nothing Then
            If Not rs.IsClosed Then rs.Close()
            rs.Dispose()
        End If
    End Sub
    Public Overrides Function FindByCriteria(ByVal WhereClauseStatement As String) As System.Collections.IList
        Dim rs As IDataReader = Nothing
        Dim DataList As ArrayList
        Dim ObjectList As IList
        Try
            rs = GetDBRecordData(FindAllStatement() & WhereClauseStatement)
            DataList = LoadDataMatrix(rs)
            rs.Close()
            ObjectList = LoadAll(DataList)
            Return ObjectList
        Catch ex As Exception
            Throw New ApplicationException("Impossibile caricare la lista di tutti gli oggetti presenti nella tabella. " & vbCrLf & ex.Message)
        Finally
            ReleaseDBDatareader(rs)
            'libero la memoria
            DataList = New ArrayList
        End Try
    End Function
    Public Overridable Sub PostInsertAction(ByVal item As AbstractPersistenceObject)
        '
    End Sub
    Public Overridable Sub PostUpdateAction(ByVal item As AbstractPersistenceObject)
        '
    End Sub

    Public Overridable Sub PostDeleteAction(ByVal item As AbstractPersistenceObject)
        '
    End Sub

#Region "Funzioni di preparazioni all'inserimento"
    'Protected MustOverride Function FindByKeyStatement() As String
    ''' <summary>
    ''' Metodo hook per l'inserimento di un oggetto nella base dati. Esso prepara il comando,  carica il parametro per la chiave dell'oggetto, carica i parametri
    ''' per i dati dell'oggetto e infine esegue il comando.
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub DoInsert(ByVal item As AbstractPersistenceObject)
        Dim cmd As IDbCommand = Nothing
        Try
            Dim statement As String = InsertStatement()
            cmd = GetCommand(statement)

            If Not m_IsAutoIncrementID Then
                LoadInsertCommandKeyParameter(item, cmd)
            End If

            LoadInsertCommandParameters(item, cmd)
            cmd.ExecuteNonQuery()



            If m_IsAutoIncrementID Then
                cmd = GetCommand(SqlDialect.GetIdentityStatement(m_PersistentService.ServiceName))
                Dim m As Object = cmd.ExecuteScalar
                item.Key = New Key(Convert.ToInt32(m))
            End If


            PostInsertAction(item)
        Catch ex As Exception
            If ex.Message = "L'apporto modifiche non è riuscito perché si è cercato di duplicare i valori nell'indice, nella chiave primaria o nella relazione. Modificare i dati nel campo o nei campi che contengono dati duplicati, rimuovere l'indice o ridefinire l'indice per consentire l'inserimento di voci duplicate, quindi ritentare l'operazione." Then Throw New DuplicatedIndexException
            Throw New Exception("Impossibile inserire nella base dati l'oggetto di tipo " & item.GetType.ToString & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Metodo sovrascrivibile per il caricamento dei parametri dell'identificativo dell'oggetto che deve essere inserito nell'oggetto command specificato.
    ''' Di default è caricato un parametro di nome "Id" che ha come valore il valore long dell'oggetto Key dell'oggetto specificato.
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <param name="Cmd"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadInsertCommandKeyParameter(ByVal Item As AbstractPersistenceObject, ByVal Cmd As IDbCommand)
        Try
            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = Item.Key.LongValue
            Cmd.Parameters.Add(param)
        Catch ex As Exception
            Throw New Exception("Impossibile creare un parametro per la chiave identificativa nel comando per l'inserimento di un oggetto" & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Metodo hook per il caricamento dei parametri dell'oggetto che deve essere inserito nell'oggetto command specificato
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <param name="Cmd"></param>
    ''' <remarks></remarks>
    Protected MustOverride Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As IDbCommand)
    Protected Overrides Function FindNextKey() As Key
        Dim rs As IDataReader = Nothing
        Dim cmd As IDbCommand
        Try
            'qui devo leggere l'oggetto in un datareader e caricarlo
            cmd = Me.GetCommand(Me.FindNextKeyStatement)
            rs = cmd.ExecuteReader
            rs.Read()
            If IsDBNull(rs(0)) Then
                rs.Close()
                Return New Key(1)
            Else
                Dim Id As Int32 = rs(0) + 1
                rs.Close()
                Return New Key(Id)
            End If
        Catch ex As Exception
            Throw New Exception("Impossibile trovare una nuova chiave identificativa per l'oggetto. " & vbCrLf & ex.Message)
        Finally
            ReleaseDBDatareader(rs)
        End Try
    End Function
#End Region

#Region "Metodi per l'aggiornamento degli oggetti"


    ''' <summary>
    ''' Metodo per l'aggiornamento di un oggetto nella base dati. Esso prepara il comando, carica i parametri per il comando di aggiornamento ed esegue l'aggiornamento richiesto
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Update(ByVal item As AbstractPersistenceObject)
        Dim cmd As IDbCommand
        If item.IsValid Then
            cmd = GetCommand(UpdateStatement)
            LoadUpdateCommandParameters(item, cmd)
            cmd.ExecuteNonQuery()
            PostUpdateAction(item)
            'anche se già presente nella cache ne rinnovo il riferimento nel caso il riferimento
            'presente nella cache punti ad un'altra area di memoria
            Cache.AddToCache(item)
        Else
            Dim errors As String = ""
            For Each elem As String In item.ValidationErrors
                errors = errors & elem & vbCrLf
            Next
            Throw New Exception("L'oggetto aggiornato non è valido. Controllare il valore dei valori immessi" & vbCrLf & errors)
        End If

    End Sub
    ''' <summary>
    ''' Metodo hook per il caricamento dei parametri dell'oggetto che deve essere aggiornato nell'oggetto command specificato
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <param name="Cmd"></param>
    ''' <remarks></remarks>
    Protected MustOverride Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As IDbCommand)

#End Region

#Region "Metodi per la cancellazione degli oggetti"

    ''' <summary>
    ''' Metodo per l'aggiornamento di un oggetto nella base dati. Esso prepara il comando, carica i parametri per il comando di cancellazione ed esegue l'eliminazione  richiesta
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Delete(ByVal item As AbstractPersistenceObject)
        Dim cmd As IDbCommand
        Try
            cmd = GetCommand(DeleteStatement)
            LoadDeleteCommandParameters(item, cmd)
            cmd.ExecuteNonQuery()
            PostDeleteAction(item)
            Cache.RemoveObjectFromCache(item)
        Catch ex As Exception
            Throw New Exception("Impossibile eseguire la cancellazione dell'oggetto di tipo " & item.GetType.ToString & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Metodo sovrascrivibile per il caricamento dei parametri dell'oggetto che deve essere cancellato nell'oggetto command specificato
    ''' Di default è caricato un parametro di nome "Id" che ha come valore il valore long dell'oggetto Key dell'oggetto specificato.
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <param name="Cmd"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadDeleteCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As IDbCommand)
        Try
            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = Item.Key.LongValue
            Cmd.Parameters.Add(param)
        Catch ex As Exception
            Throw New Exception("Impossibile creare un parametro per la chiave identificativa nel comando per l'eliminazione di un oggetto" & vbCrLf & ex.Message)
        End Try
    End Sub

#End Region




    Public Overrides Function FindByQuery(ByVal Query As String) As System.Collections.IList
        Dim rs As IDataReader = Nothing
        Dim DataList As ArrayList
        Dim ObjectList As IList
        Try
            rs = GetDBRecordData(Query)
            DataList = LoadDataMatrix(rs)
            rs.Close()
            ObjectList = LoadAll(DataList)
            Return ObjectList
        Finally
            ReleaseDBDatareader(rs)
            'libero la memoria
            DataList = New ArrayList
        End Try
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
