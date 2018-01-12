Imports System.Text

Public Class Indirizzo
    Protected m_Via As String = ""
    Private m_Frazione As String = ""
    Private m_Civico As String = ""
    'Private m_Tel As String = ""
    Protected m_Cap As String = ""
    'Private m_Fax As String = ""
    Protected m_Nazione As Nazione = New NazioneNulla
    Protected m_Comune As Comune = New ComuneNullo
    Protected m_Provincia As Provincia = New ProvinciaNulla
    Private Dim m_LuogoResidenza As String = ""
    Protected m_IndirizzoWeb As String = ""





    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder


        Dim provincia As String = ""
        Dim via As String = ""

        If Not String.IsNullOrEmpty(m_Via) Then
            via = String.Format("{0} {1} {2}", m_Via, m_Civico, m_Frazione).Trim()
            sb.AppendLine(via)
        End If

        If m_Provincia.Id > -1 Then
            provincia = String.Format("{0} {1} ({2})", m_Cap, m_Comune.Descrizione, m_Provincia.Sigla).Trim()
            sb.AppendLine(provincia)
        End If

        Return sb.ToString

    End Function

    Public Function IsIndirizzoValido() As Boolean

        If Not String.IsNullOrEmpty(m_Via) And Not String.IsNullOrEmpty(m_Cap) And Not String.IsNullOrEmpty(m_Comune.Descrizione) Then
            Return True
        End If

        Return False

    End Function

    Property IndirizzoWeb() As String
        Get
            Return m_IndirizzoWeb
        End Get
        Set(ByVal value As String)
            m_IndirizzoWeb = value
        End Set
    End Property

    Property Cap() As String
        Get
            Return m_Cap
        End Get
        Set(ByVal value As String)
            m_Cap = value
        End Set
    End Property
    Public Property Frazione() As String
        Get
            Return m_Frazione
        End Get
        Set(ByVal value As String)
            m_Frazione = value
        End Set
    End Property
    Public Property Civico() As String
        Get
            Return m_Civico
        End Get
        Set(ByVal value As String)
            m_Civico = value
        End Set
    End Property
    Public Property LuogoResidenza() As String
        Get
            Return m_LuogoResidenza
        End Get
        Set(ByVal value As String)
            m_LuogoResidenza = value
        End Set
    End Property
    Property Via() As String
        Get
            Return m_Via
        End Get
        Set(ByVal value As String)
            m_Via = value
        End Set
    End Property
    Public Property Nazione() As Nazione
        Get
            Return m_Nazione
        End Get
        Set(ByVal value As Nazione)
            If value Is Nothing Then Throw New Exception("Impossibile impostare la nazione. Valore nullo")
            m_Nazione = value
        End Set
    End Property
    Public Property Provincia() As Provincia
        Get
            Return m_Provincia
        End Get
        Set(ByVal value As Provincia)
            If value Is Nothing Then Throw New Exception("Impossibile impostare la provincia. Valore nullo")
            m_Provincia = value
        End Set
    End Property
    Public Property Comune() As Comune
        Get
            Return m_Comune
        End Get
        Set(ByVal value As Comune)
            If value Is Nothing Then Throw New Exception("Impossibile impostare il comune. Valore nullo")
            m_Comune = value
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf (obj) Is DBNull Then Return False
        If TypeOf (obj) Is Indirizzo Then
            Dim ind As Indirizzo = DirectCast(obj, Indirizzo)
            If ind.Cap = m_Cap And ind.Via = m_Via And ind.Comune.Id = m_Comune.Id And ind.Provincia.Id = m_Provincia.Id Then
                Return True
            End If
            Return False
        End If
        Return False
    End Function

    'Property Fax() As String
    '   Get
    '      Return m_Fax
    '   End Get
    '   Set(ByVal value As String)
    '      m_Fax = value
    '   End Set
    'End Property

    'Property Tel() As String
    '   Get
    '      Return m_Tel
    '   End Get
    '   Set(ByVal value As String)
    '      m_Tel = value
    '   End Set
    'End Property

    Public Sub New()

    End Sub
    'Public Sub New(ByVal Ab As String, ByVal telAb As String, ByVal Cp As String, ByVal fax As String)
    '   m_Fax = fax
    '   m_Indirizzo = Ab
    '   m_Tel = telAb
    '   m_Cap = Cp
    'End Sub

End Class
