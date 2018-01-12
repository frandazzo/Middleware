<System.Serializable()> Public Class AbstractPersistenceObject
    Private m_Key As Key
    Private m_Descrizione = ""
    Private m_versione As Int32 = -1
    Private m_ModificatoDa As String = ""
    Private m_CreatoDa As String = ""
    Private m_DataCreazione As DateTime = DateTime.MinValue
    Private m_DataModifica As DateTime = DateTime.MinValue
    Protected m_ValidationErrors As ArrayList = New ArrayList
    Protected m_NonCancellabile As Boolean = False

    Public Property NonCancellabile() As Boolean
        Get
            Return m_NonCancellabile
        End Get
        Set(ByVal value As Boolean)
            m_NonCancellabile = value
        End Set
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then
            Return False
        End If

        Dim other As AbstractPersistenceObject


        Try
            other = DirectCast(obj, AbstractPersistenceObject)
        Catch ex As Exception
            Return False
        End Try



        If other.Key Is Nothing Then
            'se le chiavi sono entrambe nulle verifico l'hash
            If Me.Key Is Nothing Then
                Return MyBase.Equals(obj)
            Else ' altrimenti non sono uguali
                Return False
            End If

        Else
            'se le chiavi non sono entrambe valide restituisco false
            If Me.Key Is Nothing Then
                Return False
            Else ' altrimenti verifico le chiavi
                Return Me.Key.ToString.Equals(other.Key.ToString)

            End If

        End If

    End Function


    Protected Overridable Sub DoValidation()

    End Sub
    'Public Enum StatoOggetto
    '   Nuovo
    '   Dirty
    '   Clean
    '   Deleted
    'End Enum
    'Private m_stato As OggettoDominio.StatoOggetto = StatoOggetto.Clean
    'Property Stato() As OggettoDominio.StatoOggetto
    '   Get
    '      Return m_stato
    '   End Get
    '   Set(ByVal value As OggettoDominio.StatoOggetto)
    '      m_stato = value
    '   End Set
    'End Property
    Property DataCreazione() As DateTime
        Get
            Return m_DataCreazione
        End Get
        Set(ByVal value As DateTime)
            m_DataCreazione = value
        End Set
    End Property
    Property DataModifica() As DateTime
        Get
            Return m_DataModifica
        End Get
        Set(ByVal value As DateTime)
            m_DataModifica = value
        End Set
    End Property
    Property CreatoDa() As String
        Get
            Return m_CreatoDa
        End Get
        Set(ByVal value As String)
            m_CreatoDa = value
        End Set
    End Property
    Property ModificatoDa() As String
        Get
            Return m_ModificatoDa
        End Get
        Set(ByVal value As String)
            m_ModificatoDa = value
        End Set
    End Property
    Property Versione() As Int32
        Get
            Return (m_versione)
        End Get
        Set(ByVal value As Int32)
            m_versione = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return m_Descrizione
    End Function


    Public Overridable Property Id() As Int32
        Get
            If m_Key.IsSimple Then
                Return m_Key.ToString
            Else
                Throw New Exception("Impossibile ottenere un id numerico da una chiave composta!")
            End If
        End Get
        Set(value As Int32)

        End Set
    End Property
    Property Key() As Key
        Get
            Return m_Key
        End Get
        Set(ByVal value As Key)
            ''Dim obj As Object = value
            'If obj Is Nothing Then
            '    m_Key = Nothing
            'Else
            '    Try
            m_Key = value
            '    Catch ex As Exception
            '        Throw New Exception("Impossibile settare la chiave di un oggetto con un riferimento di tipo diverso")
            '    End Try

            'End If


        End Set
    End Property
    Public Sub SetKeyToNothing()
        If Not m_Key Is Nothing Then
            m_Key = Nothing
        End If
    End Sub

    Public ReadOnly Property ValidationErrors() As ArrayList
        Get
            Return m_ValidationErrors
        End Get
    End Property

    Overridable Property Descrizione() As String
        Get
            Return m_Descrizione
        End Get
        Set(ByVal value As String)
            'If value = "" Then Throw New Exception("Il nome dell'oggetto non è stato assegnato!")
            m_Descrizione = UCase(value)
        End Set
    End Property
    Public Function IsValid() As Boolean
        m_ValidationErrors.Clear()
        DoValidation()
        If m_ValidationErrors.Count = 0 Then
            Return True
        End If
        Return False
    End Function
End Class
