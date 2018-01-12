Public Class Query




    Protected m_DomainClassName As String
    Protected m_WhereClause As AbstractBoolCriteria = Nothing
    Protected m_OrderByClause As AbstractBoolCriteria = Nothing
    Protected m_Table As String = ""
    Protected m_Top As Int32 = -1
    Protected m_Distinct As Boolean
    Protected m_Columns As New ArrayList
    Protected m_TotalReturnedRecords As Int32 = -1
    Protected m_MaxQueryRecords As Int32 = 500

    Public Sub New()

    End Sub

    Public Sub New(ByVal MaxQueryRecords As Int32)
        m_MaxQueryRecords = MaxQueryRecords
    End Sub


    Public ReadOnly Property MaxQueryRecords() As Int32
        Get
            Return m_MaxQueryRecords
        End Get
    End Property


    Public ReadOnly Property TotalReturnedRecords() As Int32
        Get
            Return m_TotalReturnedRecords
        End Get
    End Property

    Public Sub New(ByVal DomainClassName As String)
        m_DomainClassName = DomainClassName
    End Sub

    Public Overridable Sub SetTable(ByVal Table As String)
        m_Table = Table
    End Sub

    Public Overridable Sub SetMaxNumberOfReturnedRecords(ByVal number As Int32)
        m_Top = number
    End Sub


    Public Overridable Sub SetListRequiredColumns(ByVal columns As ArrayList)
        m_Columns = columns
    End Sub


    Public Overridable Sub SetDistinctClause(ByVal Distinct As Boolean)
        m_Distinct = Distinct
    End Sub

    Protected Overridable Function ConstuctBaseStatement(ByVal PersistenceManager As IPersistenceFacade) As String
        If m_Table = "" Then Return ""
        Dim temp As String = "Select "
        If m_Distinct Then
            temp = temp & "distinct "
        End If


        If PersistenceManager.DBType <> DB.DBType.MySql Then
            If m_Top > 0 Then
                temp = temp & " top " & m_Top.ToString & " "
            End If
        End If


        temp = temp & GenerateRequiredColumnString() & " from " & m_Table & " "
        Return temp
    End Function

    Public Overridable Sub AddWhereClause(ByVal WhereClause As AbstractBoolCriteria)
        m_WhereClause = WhereClause
    End Sub
    Public Overridable Sub AddOrderByClause(ByVal OrderBy As AbstractBoolCriteria)
        m_OrderByClause = OrderBy
    End Sub


    Public Function CreateQuery(ByVal PersistenceManager As IPersistenceFacade) As String
        If m_Table = "" Then
            Dim mapper As AbstractPersistentMapper = PersistenceManager.GetMapperByName(m_DomainClassName)
            Return String.Format("{0} {1}", mapper.PublicFindAllStatement, GenerateWhereClause(PersistenceManager))

        Else
            Return GenerateQuery(PersistenceManager)
        End If
    End Function


    Public Overridable Overloads Function Execute(ByVal PersistenceManager As IPersistenceFacade) As IList
        Dim connectionOpened As Boolean = False

        If PersistenceManager.CurrentConnection.State = ConnectionState.Closed Then
            PersistenceManager.CurrentConnection.Open()
            connectionOpened = True
        End If

        Dim list As IList
        If m_Table = "" Then
            list = PersistenceManager.GetMapperByName(m_DomainClassName).FindByCriteria(GenerateWhereClause(PersistenceManager))
        Else
            list = PersistenceManager.GetMapperByName(m_DomainClassName).FindByQuery(GenerateQuery(PersistenceManager))
        End If

        If connectionOpened Then
            PersistenceManager.CurrentConnection.Close()
        End If


        m_TotalReturnedRecords = list.Count

        Return list
    End Function
    Protected Overridable Function GenerateWhereClause(ByVal PersistenceManager As IPersistenceFacade) As String


        Dim Where As String = ""
        If m_WhereClause IsNot Nothing Then
            Where = m_WhereClause.GenerateSql
        End If
        Dim orderBy As String = ""
        If m_OrderByClause IsNot Nothing Then
            orderBy = m_OrderByClause.GenerateSql
        End If

        Dim top As String = " "
        If PersistenceManager.DBType = DB.DBType.MySql Then
            If m_Top > 0 Then
                top = top & " limit " & m_Top.ToString & " "
            End If
        End If




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



    Protected Function GenerateRequiredColumnString()
        If m_Columns.Count = 0 Then
            Return " * "
        End If

        If m_Columns.Count = 1 Then
            Return " " + m_Columns.Item(0).ToString + " "
        End If

        Dim s As String = " "
        Dim i As Integer = 0

        For Each elem As String In m_Columns
            s += m_Columns.Item(i) + ","
            i = i + 1
        Next

        'tolgo l'ultima virgola
        s = s.Substring(0, s.Length - 1)

        'aggiungo uno spazio
        Return s + " "

    End Function


    Protected Overridable Function GenerateQuery(ByVal PersistenceManager As IPersistenceFacade) As String

        Return ConstuctBaseStatement(PersistenceManager) & GenerateWhereClause(PersistenceManager)

    End Function





    Public Overridable Overloads Function Execute(ByVal PersistenceManager As IPersistenceFacade, ByVal MaxRecordPerPage As Integer, ByVal Offset As Integer) As System.Collections.IList
        Throw New NotImplementedException("Per utilizzare la paginazione creare una query Paginata")
    End Function

    Public Overridable Function ExceedMaxQueryRecords() As Boolean
        Return False
    End Function
End Class
