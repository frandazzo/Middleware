Friend Class MySqlPaginationQuery
    Inherits Query


    Protected m_ExceedsMaxQueryRecords As Boolean = False

    Protected m_preprocesExcuted As Boolean = False



    Public Sub New(ByVal DomainClassName As String, ByVal MaxQueryRecords As Int32)
        MyBase.New(DomainClassName)

        m_MaxQueryRecords = MaxQueryRecords


    End Sub


    Public Overrides Sub AddWhereClause(ByVal WhereClause As AbstractBoolCriteria)
        m_WhereClause = WhereClause
        m_preprocesExcuted = False
    End Sub
    Public Overrides Sub AddOrderByClause(ByVal OrderBy As AbstractBoolCriteria)
        m_OrderByClause = OrderBy
        m_preprocesExcuted = False
    End Sub

    Public Overrides Sub SetTable(ByVal Table As String)
        m_Table = Table
        m_preprocesExcuted = False
    End Sub

    Public Overrides Sub SetMaxNumberOfReturnedRecords(ByVal number As Int32)
        m_Top = number
        m_preprocesExcuted = False
    End Sub


    Public Overrides Sub SetListRequiredColumns(ByVal columns As ArrayList)
        m_Columns = columns
        m_preprocesExcuted = False
    End Sub


    Public Overrides Sub SetDistinctClause(ByVal Distinct As Boolean)
        m_Distinct = Distinct
        m_preprocesExcuted = False
    End Sub




    Public Overrides Function Execute(ByVal PersistenceManager As IPersistenceFacade) As System.Collections.IList
        Throw New NotImplementedException("Per una query semplice utilizzare un oggetto query semplice")
    End Function

    Public Overrides Function ExceedMaxQueryRecords() As Boolean
        Return m_ExceedsMaxQueryRecords
    End Function

    Protected Overrides Function ConstuctBaseStatement(ByVal PersistenceManager As IPersistenceFacade) As String
        If m_Table = "" Then Throw New Exception("Impostare una tabella da cui effettuare la query paginata")
        Dim temp As String = "Select "
        If m_Distinct Then
            temp = temp & "distinct "
        End If

        temp = temp & GenerateRequiredColumnString() & " from " & m_Table & " "

        Return temp
    End Function

    Protected Function GenerateEndQueryClause(ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As String
        Dim Where As String = ""
        If m_WhereClause IsNot Nothing Then
            Where = m_WhereClause.GenerateSql
        End If
        Dim orderBy As String = ""
        If m_OrderByClause IsNot Nothing Then
            orderBy = m_OrderByClause.GenerateSql
        End If

        Dim top As String = " limit " & Offset.ToString & "," & MaxRecordPerPage & " "

        If Trim(Where) = "" Then
            If Trim(orderBy) = "" Then
                If Trim(top) = "" Then
                    Return ""
                Else
                    Return top
                End If
            Else
                Return " " & orderBy & top
            End If

        End If

        Return " WHERE " & Where & orderBy & top
    End Function




    Public Overloads Overrides Function Execute(ByVal PersistenceManager As IPersistenceFacade, ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As System.Collections.IList
        'qui eseguo le query per verificare il numero di elementi
        Dim connectionOpened As Boolean = False
        If PersistenceManager.CurrentConnection.State = ConnectionState.Closed Then
            PersistenceManager.CurrentConnection.Open()
            connectionOpened = True
        End If


        'preprocesso la query per verificare il numero di elementi
        PreprocessQuery(PersistenceManager)


        'eseguo finalmente la query paginata
        Dim list As IList = PersistenceManager.GetMapperByName(m_DomainClassName).FindByQuery(CreateRealQuery(PersistenceManager, MaxRecordPerPage, Offset))


        'chiudo la connessione
        If connectionOpened Then
            PersistenceManager.CurrentConnection.Close()
        End If

        Return list

    End Function


    Protected Sub PreprocessQuery(ByVal PersistenceManager As IPersistenceFacade)
        'tolgo il chek sul campo m_preprocessed per rendere l'oggetto anche stateless
        'If m_preprocesExcuted Then
        '    Return
        'End If

        If m_Table = "" Then
            Throw New Exception("Tabella non specificata")
        End If

        'qui eseguo le query per verificare il numero di elementi
        Dim connectionOpened As Boolean = False
        If PersistenceManager.CurrentConnection.State = ConnectionState.Closed Then
            PersistenceManager.CurrentConnection.Open()
            connectionOpened = True
        End If

        'verifico se ci sono più record del massimo ammesso
        'il risultato della query sarà 1 oppure nothing
        Dim o As Object = PersistenceManager.ExecuteScalar(CreateSimpleLimitedTestQuery())

        'se è nothing vuol dire che ci sono meno elementi del massimo consentito per la query
        'e diventa necessario sapere quanti sono effettivamente
        If o Is Nothing Then
            'ricalcolo il numero di elementi che so essere sicuramente inferiore al maxElementiPerQuery
            Dim o1 As Object = PersistenceManager.ExecuteScalar(CreateSimpleCountTestQuery(m_MaxQueryRecords, 0))
            If o1 Is Nothing Then
                o1 = 0
            End If
            m_TotalReturnedRecords = Convert.ToInt32(o1)
        Else
            'se il risultato è "1"
            'il numero di elementi lo pongo uguale al numero di elementi massimo richiesto per l'intera query
            m_TotalReturnedRecords = m_MaxQueryRecords
            'confermo che il numero di elementi della query ha ecceduto il numero massimo richiesto
            m_ExceedsMaxQueryRecords = True
        End If




        'chiudo la connessione
        If connectionOpened Then
            PersistenceManager.CurrentConnection.Close()
        End If

        m_preprocesExcuted = True
    End Sub


    Protected Function CreateSimpleCountTestQuery(ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As String
        Return "Select count(0) from " & m_Table & " " & GenerateEndQueryClause(MaxRecordPerPage, Offset)
    End Function

    Protected Function CreateSimpleLimitedTestQuery() As String
        'vado a vedere all'offset 400(la 401-esima posizione ) se trovo un uno
        'ciò significa che ho più record del consentito
        Return "Select 1 from " & m_Table & " " & GenerateEndQueryClause(1, m_MaxQueryRecords)
        'sqlserver test
        'SELECT rows FROM sysindexes WHERE id = OBJECT_ID('DB_item_documento_contabile') AND indid < 2
        'last statement affected rows nuimber
        'select @@rowcount 
    End Function

    Protected Function CreateRealQuery(ByVal PersistenceManager As IPersistenceFacade, ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As String
        Return ConstuctBaseStatement(PersistenceManager) & GenerateEndQueryClause(MaxRecordPerPage, Offset)
    End Function
End Class
