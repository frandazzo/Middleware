Public Class GeoLocationFacade
    Protected Shared m_Instance As GeoLocationFacade
    Protected m_GeoLocationHandler As GeoLocationHandler

    Public Shared Function Instance() As GeoLocationFacade
        If m_Instance Is Nothing Then
            Throw New InvalidOperationException("Oggetto non inizializzato: utilizzare prima il metodo 'InitializeInstance'")
        End If
        Return m_Instance
    End Function



    Public Shared Sub InitializeInstance(ByVal loader As IGeoLocationLoader)

        If m_Instance Is Nothing Then
            m_Instance = New GeoLocationFacade(loader)
        End If

    End Sub


    Protected Sub New(ByVal loader As IGeoLocationLoader)
        If m_GeoLocationHandler Is Nothing Then
            m_GeoLocationHandler = New GeoLocationHandler(loader)
        End If
    End Sub


















    Public Function GetGeoHandler() As GeoLocationHandler
        Return Me.m_GeoLocationHandler
    End Function
#Region "Metodi per il calcolo del CF"
    ''' <summary>
    ''' Metodo che calcola il codice fiscale
    ''' </summary>
    ''' <param name="nome"></param>
    ''' <param name="cognome"></param>
    ''' <param name="Sesso">Parametri ammessi: "MASCHIO" , "FEMMINA"</param>
    ''' <param name="DataNascita"></param>
    ''' <param name="NomeComuneNascita"></param>
    ''' <param name="NomeNazionalita"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalcolaCodiceFiscale(ByVal Nome As String, ByVal Cognome As String, ByVal Sesso As String, _
                                          ByVal DataNascita As Date, ByVal NomeComuneNascita As String, ByVal NomeNazionalita As String) As String

        Dim nazione As Nazione = New NazioneNulla
        Dim comune As Comune = New ComuneNullo
        Dim sex As Int32 = 1
        If Sesso = "MASCHIO" Then
            sex = 1
        ElseIf Sesso = "FEMMINA" Then
            sex = 2
        Else
            Throw New Exception("Valore non valido per il sesso")
        End If

        If NomeNazionalita <> "ITALIA" Then
            nazione = Me.m_GeoLocationHandler.GetNazioneByName(NomeNazionalita)
            Return CodiceFiscaleCalculator.CalcolaCodiceFiscale(Nome, Cognome, DataNascita, sex, nazione.CodiceFiscale, "")
        Else
            comune = Me.m_GeoLocationHandler.GetComuneByName(NomeComuneNascita)
            Return CodiceFiscaleCalculator.CalcolaCodiceFiscale(Nome, Cognome, DataNascita, sex, "", comune.CodiceFiscale)
        End If

    End Function

    Public Function CalcolaDatiFiscali(ByVal codiceFiscale As String) As DatiFiscali
        Return CodiceFiscaleCalculator.GetDatiFiscali(codiceFiscale, m_GeoLocationHandler.Loader)
    End Function


#End Region



    Public Function GetListaNazioni() As IList

        Return m_GeoLocationHandler.GetListaNomiNazioni
    End Function
    Public Function GetListaOggettiNazioni() As IList

        Return m_GeoLocationHandler.ListaNazioni
    End Function
    Public Function GetListaProvincie() As IList

        Return m_GeoLocationHandler.GetListaNomiProvincie
    End Function

    Public Function GetListaOggettiProvincie() As IList

        Return m_GeoLocationHandler.ListaProvince
    End Function


    Public Function GetListaRegioni() As IList

        Return m_GeoLocationHandler.GetListaNomiRegioni
    End Function

    Public Function GetListaOggettiRegioni() As IList

        Return m_GeoLocationHandler.ListaRegioni
    End Function


    Public Function GetSiglaProvinciaByNomeProvincia(ByVal NomeProvincia As String) As String
        Dim prov As Provincia = m_GeoLocationHandler.GetProvinciaByName(NomeProvincia)
        Return prov.Sigla
    End Function


    Public Function GetNomeProvinciaByNomeComune(ByVal NomeComune As String) As String
        Dim com As Comune = m_GeoLocationHandler.GetComuneByName(NomeComune)
        Return m_GeoLocationHandler.GetProvinciaById(com.IdProvincia).Descrizione
    End Function

    Public Function GetListaOggettiComuniPerProvincia(ByVal NomeProvincia As String) As IList
        Return m_GeoLocationHandler.GetComuniPerProvincia(NomeProvincia)
    End Function
    Public Function GetListaComuniPerProvincia(ByVal NomeProvincia As String) As IList
        Return m_GeoLocationHandler.GetNomiComuniPerProvincia(NomeProvincia)
    End Function
    Public Function GetCapForComune(ByVal NomeComune As String)
        Dim com As Comune = m_GeoLocationHandler.GetComuneByName(NomeComune)
        Return com.CAP
    End Function
    Public Function ExistComune(ByVal NomeComune As String) As Boolean
        Dim com As Comune = m_GeoLocationHandler.GetComuneByName(NomeComune)
        If com.Id = -1 Then
            Return False
        End If
        Return True
    End Function

    Public Function ExistNazione(ByVal NomeNazione As String) As Boolean
        Dim com As Nazione = m_GeoLocationHandler.GetNazioneByName(NomeNazione)
        If com.Id = -1 Then
            Return False
        End If
        Return True
    End Function









End Class
