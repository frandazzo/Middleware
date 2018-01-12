Friend Class SqlServerPaginationQuery
    Inherits Query


    Protected m_ExceedsMaxQueryRecords As Boolean = False





    Public Sub New(ByVal DomainClassName As String, ByVal MaxQueryRecords As Int32)
        MyBase.New(DomainClassName)

        m_MaxQueryRecords = MaxQueryRecords


    End Sub


    'Public Overrides Sub AddWhereClause(ByVal WhereClause As AbstractBoolCriteria)
    '    m_WhereClause = WhereClause

    'End Sub
    'Public Overrides Sub AddOrderByClause(ByVal OrderBy As AbstractBoolCriteria)
    '    m_OrderByClause = OrderBy

    'End Sub

    'Public Overrides Sub SetTable(ByVal Table As String)
    '    m_Table = Table

    'End Sub

    'Public Overrides Sub SetMaxNumberOfReturnedRecords(ByVal number As Int32)
    '    m_Top = number

    'End Sub


    'Public Overrides Sub SetListRequiredColumns(ByVal columns As ArrayList)
    '    m_Columns = columns

    'End Sub


    Public Overrides Sub SetDistinctClause(ByVal Distinct As Boolean)
        'clausola disabilitata per la presenza del rownumber nelle query paginate sqlserver
        ' m_Distinct = Distinct
        Throw New Exception("clausola disabilitata per la presenza del rownumber nelle query paginate sqlserver")

    End Sub



    'la funzione execute non può essere eseguita da un oggetto query paginata
    Public Overrides Function Execute(ByVal PersistenceManager As IPersistenceFacade) As System.Collections.IList
        Throw New NotImplementedException("Per una query semplice utilizzare un oggetto query semplice")
    End Function

    'informazione relativa al fatto che il  numero di oggetti restituiti dalla 
    'query è superiore al massimo consentito
    Public Overrides Function ExceedMaxQueryRecords() As Boolean
        Return m_ExceedsMaxQueryRecords
    End Function

    Protected Overrides Function ConstuctBaseStatement(ByVal PersistenceManager As IPersistenceFacade) As String
        If m_Table = "" Then Throw New Exception("Impostare una tabella da cui effettuare la query paginata")
        Dim temp As String = "Select"
       
        'qui devo inserire il costrutto che inserisce nella lista degli elementi da ricercare il rownumber
        'con le specifiche dell'ordinamento
        'di seguito il codice di test
        ''''''Select ROW_NUMBER()
        ''''''OVER (ORDER BY field1 asc, field2 asc) AS Row,  *
        ''''''FROM table where filed1 = .....

        'per prima cosa devo recuperare la clausola di ordinamento se esiste
        'se non esiste si farà riferimento al campo default di ogni tabella: il campo ID
        Dim rowFileddefinition As String = ""
        If m_OrderByClause Is Nothing Then
            rowFileddefinition = "ROW_NUMBER() OVER (ORDER BY ID asc) AS Row, "
        Else
            rowFileddefinition = String.Format("ROW_NUMBER() OVER ({0}) AS Row, ", m_OrderByClause.GenerateSql)
        End If



        temp = String.Format("Select * from ({0} {1} {2} from {3} {4}) a ", temp, rowFileddefinition, GenerateRequiredColumnString(), m_Table, GenerateInnerWhereClause())

        Return temp
    End Function


    Protected Function GenerateInnerWhereClause() As String
        Dim Where As String = ""

        If m_WhereClause IsNot Nothing Then
            Where = m_WhereClause.GenerateSql
        End If



       
        If Trim(Where) = "" Then

            Return ""

        End If

        Return " WHERE " & Where
    End Function

    Protected Function GenerateEndQueryClause(ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As String
        Dim Where As String = ""

       


        'la clausola order by è stata già presa in considerazione nella costruzione della clausola select
        ''''Dim orderBy As String = ""
        ''''If m_OrderByClause IsNot Nothing Then
        ''''    orderBy = m_OrderByClause.GenerateSql
        ''''End If

        'La clausola limit è sostituita in sqlserver dalle clausole sul rownumber
        Dim top As String = " Row >= " & (Offset + 1).ToString & " and Row <= " & (MaxRecordPerPage + Offset) & " "

           
        Return " WHERE " & top


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

        'se non ho ecceduto il massimo numero di elementi ammessi
        If Not m_ExceedsMaxQueryRecords Then
            m_TotalReturnedRecords = list.Count
        End If


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
        'il risultato della query sarà un numero oppure nothing
        Dim o As Object = PersistenceManager.ExecuteScalar(CreateSimpleLimitedTestQuery())

        'se è nothing vuol dire che ci sono meno elementi del massimo consentito per la query
        'e diventa necessario sapere quanti sono effettivamente
        If o Is Nothing Then
            ''non ricalcolo il numero di elementi che so essere sicuramente inferiore al maxElementiPerQuery
            'il numero di elementi lo calcolo durante la query reale con la direttiva select @@rowcount
            m_ExceedsMaxQueryRecords = False
        Else
            'se il risultato è un numero
            'il numero di elementi lo pongo uguale al numero di elementi massimo richiesto per l'intera query
            m_TotalReturnedRecords = m_MaxQueryRecords
            'confermo che il numero di elementi della query ha ecceduto il numero massimo richiesto
            m_ExceedsMaxQueryRecords = True
        End If




        'chiudo la connessione
        If connectionOpened Then
            PersistenceManager.CurrentConnection.Close()
        End If

        'm_preprocesExcuted = True
    End Sub


    'Protected Function CreateSimpleCountTestQuery(ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As String
    '    Dim Where As String = ""

    '    If m_WhereClause IsNot Nothing Then
    '        Where = m_WhereClause.GenerateSql
    '    End If

    '    If Trim(Where) <> "" Then

    '        Where = " WHERE " & Where

    '    End If

    '    Return "Select count(0) from " & m_Table & " " & Where
    'End Function

    Protected Function CreateSimpleLimitedTestQuery() As String

        Dim Where As String = ""

        If m_WhereClause IsNot Nothing Then
            Where = m_WhereClause.GenerateSql
        End If

        'vado a vedere all'offset con la posizione più alta ammessa e se trovo un un numero
        'ciò significa che ho più record del consentito

        Dim top As String = "where row = " & m_MaxQueryRecords

        If Trim(Where) = "" Then

            Where = ""
        Else
            Where = String.Format(" WHERE {0} ", Where)
        End If




        Return String.Format("select Row from (Select ROW_NUMBER() OVER (ORDER BY ID asc) AS Row FROM {0}  {1} ) a {2}", m_Table, Where, top)






    End Function

    Protected Function CreateRealQuery(ByVal PersistenceManager As IPersistenceFacade, ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As String
        Return ConstuctBaseStatement(PersistenceManager) & GenerateEndQueryClause(MaxRecordPerPage, Offset)
    End Function



 



End Class
