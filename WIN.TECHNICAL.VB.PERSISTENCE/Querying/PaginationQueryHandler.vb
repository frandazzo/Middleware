Public Class PaginationQueryHandler

    Private m_Query As Query
    Private m_MaxElementPerPage As Int32 = 20
    Private m_Offset As Int32 = 0
    Private m_CurrentQueryResult = 0

    Public Sub New(ByVal Query As Query, ByVal MaxElementPerPage As Int32)
        m_Query = Query
        If MaxElementPerPage < 1 Then
            Return
        End If
        m_MaxElementPerPage = MaxElementPerPage
    End Sub


    Public Sub SetTable(ByVal Table As String)
        m_Query.SetTable(Table)
    End Sub

    Public Sub SetListRequiredColumns(ByVal columns As ArrayList)
        m_Query.SetListRequiredColumns(columns)
    End Sub

    Public Sub AddWhereClause(ByVal WhereClause As AbstractBoolCriteria)
        m_Query.AddWhereClause(WhereClause)
    End Sub
    Public Sub AddOrderByClause(ByVal OrderBy As AbstractBoolCriteria)
        m_Query.AddOrderByClause(OrderBy)
    End Sub

    Public Sub SetDistinctClause(ByVal Distinct As Boolean)
        m_Query.SetDistinctClause(Distinct)
    End Sub

    Public ReadOnly Property RecordFound() As Int32
        Get
            Return m_Query.TotalReturnedRecords
        End Get
    End Property

    Public ReadOnly Property CurrentPage() As Int32
        Get
            If m_MaxElementPerPage <= 0 Then
                Return 0
            End If
            Return Math.Ceiling(FirstViewedRecord / m_MaxElementPerPage)

        End Get
    End Property
    Public ReadOnly Property TotalPages() As Int32
        Get
            If m_MaxElementPerPage <= 0 Then
                Return 0
            End If
            Return Math.Ceiling(RecordFound / m_MaxElementPerPage)

        End Get
    End Property

    Public ReadOnly Property FoundElementMessage() As String
        Get
            If m_Query.ExceedMaxQueryRecords Then
                Return "Il numero di elementi trovati è superiore al massimo consentito di " + m_Query.MaxQueryRecords.ToString + " record. Verranno recuperati solo i primi " + m_Query.TotalReturnedRecords.ToString() + " record! Per una ricerca più precisa specificare ulteriori parametri di ricerca."
            Else
                Return "Sono stati trovati " + m_Query.TotalReturnedRecords.ToString() + " record!"
            End If
        End Get
    End Property


    Private Function CalculateOffsetFromPageNumber(pageNumber As Int32) As Int32
        If pageNumber < 1 Then
            Return 0
        End If
        Return (pageNumber - 1) * m_MaxElementPerPage
    End Function

    Public Function ExecuteQuery(ByVal PersistenceManager As IPersistenceFacade, offsetOrpageNumber As Int32, Optional isOffset As Boolean = True) As IList
        If Not isOffset Then 'se sto passando un numero di pagina ne ricavo l'offset
            m_Offset = CalculateOffsetFromPageNumber(offsetOrpageNumber)
        End If
        Dim l As IList = m_Query.Execute(PersistenceManager, m_MaxElementPerPage, m_Offset)
        m_CurrentQueryResult = l.Count
        Return l
    End Function


    Public Function ExecuteFirstQuery(ByVal PersistenceManager As IPersistenceFacade) As IList
        Dim l As IList = m_Query.Execute(PersistenceManager, m_MaxElementPerPage, 0)
        m_CurrentQueryResult = l.Count
        Return l
    End Function


    Public Function NextPageQuery(ByVal PersistenceManager As IPersistenceFacade) As IList
        If IsNextQueryEnabled Then
            m_Offset = m_Offset + m_MaxElementPerPage

            Dim l As IList = m_Query.Execute(PersistenceManager, m_MaxElementPerPage, m_Offset)
            m_CurrentQueryResult = l.Count
            Return l

        End If
        Return New ArrayList
    End Function


    Public ReadOnly Property IsNextQueryEnabled() As Boolean
        Get
            If m_Offset + m_MaxElementPerPage >= RecordFound Then
                Return False
            End If
            Return True
        End Get
    End Property
    Public ReadOnly Property FirstViewedRecord() As Int32
        Get
            Return m_Offset + 1
        End Get
    End Property


    Public ReadOnly Property ViewedRecordMessage() As String
        Get
            Return "Record visualizzati: dal record " + (m_Offset + 1).ToString() + " al record " + LastViewedrecord.ToString + "."
        End Get
    End Property


    Public Function PreviousPageQuery(ByVal PersistenceManager As IPersistenceFacade) As IList
        If IsPreviousQueryEnabled Then
            m_Offset = m_Offset - m_MaxElementPerPage
            Dim l As IList = m_Query.Execute(PersistenceManager, m_MaxElementPerPage, m_Offset)
            m_CurrentQueryResult = l.Count
            Return l
        End If
        Return New ArrayList
    End Function


    Public ReadOnly Property LastViewedRecord As Int32
        Get
            Return m_Offset + m_CurrentQueryResult
        End Get
    End Property




    Public ReadOnly Property IsPreviousQueryEnabled() As Boolean
        Get
            If m_Offset + m_CurrentQueryResult - m_MaxElementPerPage <= 0 Then
                Return False
            End If
            Return True
        End Get
    End Property



End Class
