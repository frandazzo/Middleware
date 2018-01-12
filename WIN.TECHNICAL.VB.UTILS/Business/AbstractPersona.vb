''' <summary>
''' Classe che contiene tutte le informazioni base per l'anagrafica di una persona fisica
''' </summary>
''' <remarks></remarks>
Public Class AbstractPersona
    Inherits AbstractPersistenceObject
    Implements IPersona
    Protected m_Nome As String = ""
    Protected m_Cognome As String = ""
    Protected m_Sesso As Sex = Sex.Maschio
    Protected m_DataNascita As Date = New DateTime(1800, 1, 1)
    Protected m_Nazionalita As Nazione = New NazioneNulla
    Private Dim m_LuogoNascita As String = ""
    Protected m_ComuneNascita As Comune = New ComuneNullo
    Protected m_ProvinciaNascita As Provincia = New ProvinciaNulla
    Protected m_CodiceFiscale As String = ""
    Protected m_Residenza As Indirizzo = New Indirizzo
    Protected m_Comunicazione As Comunicazioni = New Comunicazioni
    Protected m_Note As String = ""


    Public Property LuogoNascita() As String
        Get
            Return m_LuogoNascita
        End Get
        Set(ByVal value As String)
            m_LuogoNascita = value
        End Set
    End Property
    Public Property Note() As String
        Get
            Return m_Note
        End Get
        Set(ByVal value As String)
            m_Note = value
        End Set
    End Property

#Region "Proprietà "
    Public Sub New()

    End Sub
    Public Sub New(ByVal Nome As String, ByVal Cognome As String)
        If Nome = "" Then Throw New Exception("Non è possibile creare un oggetto persona con un nome nullo. Inserire un nome corretto.")
        If Cognome = "" Then Throw New Exception("Non è possibile creare un oggetto persona con un cognome nullo. Inserire un cognome corretto.")
        m_Nome = UCase(Nome)
        m_Cognome = UCase(Cognome)
        MyBase.Descrizione = m_Nome & " " & m_Cognome
    End Sub
    Public ReadOnly Property CompleteName() As String Implements IPersona.CompleteName
        Get
            Return m_Cognome & " " & m_Nome
        End Get
    End Property
    Public Property Nome() As String Implements IPersona.Nome
        Get
            Return m_Nome
        End Get
        Set(ByVal value As String)
            '  If value = "" Then Throw New Exception("Non è possibile creare un oggetto persona con un nome nullo. Inserire un nome corretto.")
            m_Nome = UCase(value)
        End Set
    End Property
    Public Property Cognome() As String Implements IPersona.Cognome
        Get
            Return m_Cognome
        End Get
        Set(ByVal value As String)
            ' If value = "" Then Throw New Exception("Non è possibile creare un oggetto persona con un cognome nullo. Inserire un cognome corretto.")
            m_Cognome = UCase(value)
        End Set
    End Property

    Public Property Nazionalita() As Nazione
        Get
            Return m_Nazionalita
        End Get
        Set(ByVal value As Nazione)
            ' If value Is Nothing Then Throw New Exception("Impossibile impostare la nazionalità. Valore nullo")
            m_Nazionalita = value
        End Set
    End Property
    Public Property ProvinciaNascita() As Provincia
        Get
            Return m_ProvinciaNascita
        End Get
        Set(ByVal value As Provincia)
            ' If value Is Nothing Then Throw New Exception("Impossibile impostare la provincia di nascita. Valore nullo")
            m_ProvinciaNascita = value
        End Set
    End Property
    Public Property ComuneNascita() As Comune
        Get
            Return m_ComuneNascita
        End Get
        Set(ByVal value As Comune)
            ' If value Is Nothing Then Throw New Exception("Impossibile impostare il comune di nascita. Valore nullo")
            m_ComuneNascita = value
        End Set
    End Property
    Property DataNascita() As Date
        Get
            Return m_DataNascita
        End Get
        Set(ByVal value As Date)
            m_DataNascita = value
        End Set
    End Property
    Enum Sex
        Maschio = 0
        Femmina = 1
    End Enum
    Property Sesso() As Sex
        Get
            Return m_Sesso
        End Get
        Set(ByVal value As Sex)
            m_Sesso = value
        End Set
    End Property

    Public ReadOnly Property Genere() As String
        Get
            If Sesso = Sex.Maschio Then
                Return "M"
            End If
            Return "F"
        End Get
    End Property

    Public Property CodiceFiscale() As String
        Get
            Return m_CodiceFiscale
        End Get
        Set(ByVal value As String)
            m_CodiceFiscale = value
        End Set
    End Property
    Public Property Residenza() As Indirizzo
        Get
            Return m_Residenza
        End Get
        Set(ByVal value As Indirizzo)
            m_Residenza = value
        End Set
    End Property
    Public Property Comunicazione() As Comunicazioni
        Get
            Return m_Comunicazione
        End Get
        Set(ByVal value As Comunicazioni)
            m_Comunicazione = value
        End Set
    End Property

#End Region






End Class
