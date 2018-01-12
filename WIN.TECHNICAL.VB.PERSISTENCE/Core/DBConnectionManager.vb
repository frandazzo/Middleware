Imports WIN.TECHNICAL.PERSISTENCE

''' <summary>
''' Classe che consente la gestione delle connessioni a vari database fornendo interfacce utili
''' all'aggiunta, recupero, rimozione delle connessioni ai Database. Fornisce un'interfaccia per il recupero e l'impostazione della 
''' connessione corrente.
''' </summary>
''' <remarks></remarks>
Public Class DBConnectionManager
   'all'inizio dell'applicazione si devono fornire al manager 
   'delle coonessioni tutte le connessioni che l'applicazione utilizza
    'Private ConnectionList As New Hashtable
    'Private CurrentConnection As IDbConnection
    'Private Shared m_Instance As DBConnectionManager



    'Public Overloads Sub AddNewDBConnection(ByVal ConnectionName As String, ByVal ConnectionString As String, ByVal ps As IPersistenceFacade)
    '   Dim connection As IDbConnection = DBTypeUtils.GetConnection(ps.ServiceName, ConnectionString)

    '   'connection.ConnectionString = ConnectionString
    '   Try
    '      ConnectionList.Add(ConnectionName, connection)
    '      CurrentConnection = connection
    '   Catch ex As Exception
    '      Throw New Exception("Non è possibile aggiungere una nuova connessione al Connection Manager poichè esiste già un'altra connessione con lo stesso nome!" & vbCrLf & ex.Message)
    '   End Try
    'End Sub







    '''' <summary>
    '''' Restituisce un'istanza vuota del connection manager
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Shared Function Instance() As DBConnectionManager
    '   If m_Instance Is Nothing Then
    '      m_Instance = New DBConnectionManager
    '   End If
    '   Return m_Instance
    'End Function
    ''''' <summary>
    ''''' Metodo che aggiunge nella lista delle connessioni una nuova connessione al DB Access col nome e con la stringa di connessione specificati.
    ''''' Imposta la connessione immessa come connessione corrente.
    ''''' </summary>
    ''''' <param name="ConnectionName"></param>
    ''''' <param name="ConnectionString"></param>
    ''''' <remarks></remarks>
    ''Public Overloads Sub AddNewDBAccessConnection(ByVal ConnectionName As String, ByVal ConnectionString As String)
    ''   Dim connection As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConnectionString)
    ''   Try
    ''      ConnectionList.Add(ConnectionName, Connection)
    ''      CurrentConnection = Connection
    ''   Catch ex As Exception
    ''      Throw New Exception("Non è possibile aggiungere una nuova connessione Access al Connection Manager poichè esiste già un'altra connessione con lo stesso nome!" & vbCrLf & ex.Message)
    ''   End Try
    ''End Sub
    ''''' <summary>
    ''''' Metodo che aggiunge nella lista delle connessioni una nuova connessione al DB SqlServer col nome e con la stringa di connessione specificati.
    ''''' Imposta la connessione immessa come connessione corrente.
    ''''' </summary>
    ''''' <param name="ConnectionName"></param>
    ''''' <param name="ConnectionString"></param>
    ''''' <remarks></remarks>
    ''Public Overloads Sub AddNewDBSqlServerConnection(ByVal ConnectionName As String, ByVal ConnectionString As String)
    ''   Dim connection As SqlClient.SqlConnection = New SqlClient.SqlConnection(ConnectionString)
    ''   Try
    ''      ConnectionList.Add(ConnectionName, connection)
    ''      CurrentConnection = connection
    ''   Catch ex As Exception
    ''      Throw New Exception("Non è possibile aggiungere una nuova connessione SqlServer al Connection Manager poichè esiste già un'altra connessione con lo stesso nome!" & vbCrLf & ex.Message)
    ''   End Try
    ''End Sub
    ''''' <summary>
    ''''' Metodo che aggiunge  nella lista delle connessioni una nuova connessione al DB Access, col nome specificato.
    ''''' Imposta la connessione immessa come connessione corrente.
    ''''' </summary>
    ''''' <param name="ConnectionName"></param>
    ''''' <param name="Connection"></param>
    ''''' <remarks></remarks>
    ''Public Overloads Sub AddNewDBAccessConnection(ByVal ConnectionName As String, ByVal Connection As OleDb.OleDbConnection)
    ''   Try
    ''      ConnectionList.Add(ConnectionName, Connection)
    ''      CurrentConnection = Connection
    ''   Catch ex As Exception
    ''      Throw New Exception("Non è possibile aggiungere una nuova connessione Access al Connection Manager poichè esiste già un'altra connessione con lo stesso nome!" & vbCrLf & ex.Message)
    ''   End Try
    ''End Sub
    ''''' <summary>
    ''''' Metodo che aggiunge  nella lista delle connessioni una nuova connessione al DB SqlServer, col nome specificato.
    ''''' Imposta la connessione immessa come connessione corrente.
    ''''' </summary>
    ''''' <param name="ConnectionName"></param>
    ''''' <param name="Connection"></param>
    ''''' <remarks></remarks>
    ''Public Overloads Sub AddNewDBSqlServerConnection(ByVal ConnectionName As String, ByVal Connection As SqlClient.SqlConnection)
    ''   Try
    ''      ConnectionList.Add(ConnectionName, Connection)
    ''      CurrentConnection = Connection
    ''   Catch ex As Exception
    ''      Throw New Exception("Non è possibile aggiungere una nuova connessione SqlServer al Connection Manager poichè esiste già un'altra connessione con lo stesso nome!" & vbCrLf & ex.Message)
    ''   End Try
    ''End Sub
    '''' <summary>
    '''' Recupera la connessione corrente se c'è altrimenti solleva una eccezione.
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetCurrentConnection() As IDbConnection
    '   If CurrentConnection Is Nothing Then Throw New Exception("Non è stata selezionata o non è presente alcuna connessione corrente")
    '   Return CurrentConnection
    'End Function
    '''' <summary>
    '''' Metodo che rimuove la connessione col nome specificato dalla lista delle connessioni.
    '''' </summary>
    '''' <param name="ConnectionName"></param>
    '''' <remarks></remarks>
    'Public Sub RemoveConnection(ByVal ConnectionName As String)
    '   Try
    '      ConnectionList.Remove(ConnectionName)
    '      'questa funzione elimina la connessione col nome specificato
    '      'e setta l'ultima nella lista come connessione corrente
    '      Dim i As IDictionaryEnumerator
    '      i = ConnectionList.GetEnumerator
    '      i.Reset()
    '      i.MoveNext()
    '      For j As Int32 = 0 To ConnectionList.Count - 1
    '         If j = ConnectionList.Count - 1 Then
    '            CurrentConnection = i.Value
    '         End If
    '         i.MoveNext()
    '      Next
    '   Catch ex As Exception
    '      Throw New Exception("Non è possibile rimuovere la connessione " & ConnectionName & " dal Connection Manager!", ex)
    '   End Try
    'End Sub
    '''' <summary>
    '''' Metodo che imposta la connessione corrente.
    '''' </summary>
    '''' <param name="ConnectionName"></param>
    '''' <remarks></remarks>
    'Public Sub SetCurrentConnection(ByVal ConnectionName As String)
    '   Try
    '      If ConnectionList.ContainsKey(ConnectionName) Then
    '         CurrentConnection = ConnectionList.Item(ConnectionName)
    '      Else
    '         Throw New Exception("Non è possibilie impostare la connessione " & ConnectionName & " come connessione corrente poichè la lista delle connessioni non la contiene")
    '      End If
    '   Catch ex As Exception
    '      Throw New Exception(ex.Message)
    '   End Try
    'End Sub

End Class
