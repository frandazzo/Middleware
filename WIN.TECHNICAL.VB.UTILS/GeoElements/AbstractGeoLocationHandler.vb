Public Class GeoLocationHandler
   Protected m_Comuni As Hashtable
   Protected m_Provincie As Hashtable
   Protected m_Regioni As Hashtable
   Protected m_Nazioni As Hashtable



   Protected m_ListaProvincie As IList
   Protected m_ListaRegioni As IList
   Protected m_ListaNazioni As IList

   Protected m_IsInitialized As Boolean = False

   Public ReadOnly Property IsInitialized() As Boolean
      Get
         Return m_IsInitialized
      End Get
   End Property

   'Public Function GetComuni() As IList
   '   Return New ArrayList(m_Comuni.Values)
   'End Function

#Region "Metodi per ottenere un oggetto nazione, regione, provincia o comune specifico"

   'Public Function GetComuneById(ByVal IdComune As String) As Comune
   '   Dim i As IDictionaryEnumerator
   '   i = m_Comuni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.Id = IdComune Then
   '         Return i.Value
   '         Exit Do
   '      End If
   '   Loop
   '   Return New ComuneNullo
   'End Function
   'Public Function GetComuneByName(ByVal NomeComune As String) As Comune
   '   If m_Comuni.ContainsKey(UCase(NomeComune)) Then
   '      Return m_Comuni.Item(UCase(NomeComune))
   '   End If
   '   Return New ComuneNullo
   'End Function

  
   'Public Function GetNazioneById(ByVal IdNazione As String) As Nazione
   '   Dim i As IDictionaryEnumerator
   '   i = m_Nazioni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.Id = IdNazione Then
   '         Return i.Value
   '         Exit Do
   '      End If
   '   Loop
   '   Return New NazioneNulla
   'End Function
   'Public Function GetNazioneByName(ByVal NomeNazione As String) As Nazione
   '   If m_Nazioni.ContainsKey(UCase(NomeNazione)) Then
   '      Return m_Nazioni.Item(UCase(NomeNazione))
   '   End If
   '   Return New NazioneNulla

   'End Function
   'Public Function GetProvinciaByName(ByVal NomeProvincia As String) As Provincia
   '   If m_Provincie.ContainsKey(UCase(NomeProvincia)) Then
   '      Return m_Provincie.Item(UCase(NomeProvincia))
   '   End If
   '   Return New ProvinciaNulla
   'End Function
   'Public Function GetProvinciaById(ByVal IdProvincia As String) As Provincia
   '   Dim i As IDictionaryEnumerator
   '   i = m_Provincie.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.id = IdProvincia Then
   '         Return i.Value
   '         Exit Do
   '      End If
   '   Loop
   '   Return New ProvinciaNulla
   'End Function
   'Public Function GetRegioneByName(ByVal NomeRegione As String) As Regione
   '   If m_Regioni.ContainsKey(NomeRegione) Then
   '      Return m_Regioni.Item(NomeRegione)
   '   End If
   '   Return New RegioneNulla
   'End Function
   'Public Function GetRegioneById(ByVal IdRegione As String) As Regione
   '   Dim i As IDictionaryEnumerator
   '   i = m_Regioni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.Id = IdRegione Then
   '         Return i.Value
   '         Exit Do
   '      End If
   '   Loop
   '   Return New RegioneNulla
   'End Function
   'Public Function GetProviciaBySigla(ByVal Sigla As String) As Provincia
   '   Dim i As IDictionaryEnumerator
   '   i = m_Provincie.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.Sigla = Sigla Then
   '         Return i.Value
   '      End If
   '   Loop
   '   Return New ProvinciaNulla
   'End Function


#End Region



   Public Overloads Function GetNomiProviciePerRegione(ByVal NomeRegione As String) As IList
      Dim reg As Regione

      reg = Me.GetRegioneByName(NomeRegione)
      Return reg.GetListaNomiProvincie

   End Function
   Public Function GetSigleProviciePerRegione(ByVal NomeRegione As String) As IList
      Dim reg As Regione

      reg = Me.GetRegioneByName(NomeRegione)
      Return reg.GetListaSigleProvincie

   End Function
   Public Function GetNomiComuniPerRegione(ByVal NomeRegione As String) As IList
      Dim reg As Regione

      reg = Me.GetRegioneByName(NomeRegione)
      Return reg.GetListaNomiComuni

   End Function
   Public Function GetNomiComuniPerProvincia(ByVal NomeProvincia As String) As IList
      Dim Prov As Provincia

      Prov = Me.GetProvinciaByName(NomeProvincia)
      Return Prov.GetListaNomiComuni

   End Function
   'Public Function GetListaNomiComuni() As IList
   '   Dim comuni As New ArrayList
   '   Dim i As IDictionaryEnumerator
   '   i = m_Comuni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      comuni.Add(i.Value.descrizione)
   '   Loop
   '   Return comuni
   'End Function
   'Public Function GetListaNomiNazioni() As IList
   '   Dim Nazioni As New ArrayList
   '   Dim i As IDictionaryEnumerator
   '   i = m_Nazioni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      Nazioni.Add(i.Value.descrizione)
   '   Loop
   '   Return Nazioni
   'End Function
   'Public Function GetListaSigleProvincie() As IList
   '   Dim provincie As New ArrayList
   '   Dim i As IDictionaryEnumerator
   '   i = m_Provincie.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      provincie.Add(i.Value.Sigla)
   '   Loop
   '   Return provincie
   'End Function
   'Public Function GetListaNomiProvincie() As IList
   '   Dim provincie As New ArrayList
   '   Dim i As IDictionaryEnumerator
   '   i = m_Provincie.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      provincie.Add(i.Value.Descrizione)
   '   Loop
   '   Return provincie
   'End Function
   'Public Function GetListaNomiRegioni() As IList
   '   Dim regioni As New ArrayList
   '   Dim i As IDictionaryEnumerator
   '   i = m_Regioni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      regioni.Add(i.Value.Descrizione)
   '   Loop
   '   Return regioni
   'End Function






#Region "Metodi per il caricamento iniziale degli oggetti"

   'Public Sub LoadComuniPerRegioni()

   '   ' Dim time As DateTime = Now
   '   Dim i As IDictionaryEnumerator = m_Regioni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      Dim reg As Regione = i.Value
   '      Me.LoadListaComuniByRegione(reg)
   '   Loop
   '   ' Debug.WriteLine(Now.Subtract(time))

   'End Sub
   'Public Sub LoadProvinciePerRegioni()

   '   ' Dim time As DateTime = Now
   '   Dim i As IDictionaryEnumerator = m_Regioni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      Dim reg As Regione = i.Value
   '      Me.LoadListaProvincieByRegione(reg)
   '   Loop
   '   ' Debug.WriteLine(Now.Subtract(time))

   'End Sub
   'Protected Sub LoadListaProvincieByRegione(ByVal Regione As Regione)
   '   Dim provincie As New ArrayList
   '   Dim i As IDictionaryEnumerator
   '   i = m_Provincie.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.IdRegione = Regione.Id Then
   '         Regione.AddProvincia(i.Value)
   '      End If
   '   Loop
   'End Sub
   'Protected Sub LoadListaComuniByRegione(ByVal Regione As Regione)
   '   Dim i As IDictionaryEnumerator
   '   i = m_Comuni.GetEnumerator
   '   i.Reset()
   '   Do While i.MoveNext
   '      If i.Value.IdRegione = Regione.Id Then
   '         Regione.AddComune(i.Value)
   '      End If
   '   Loop

   'End Sub

   'Private Sub Initialize()
   '   '  Dim t As Integer = Environment.TickCount
   '   '  Debug.WriteLine(t)

   '   LoadProvinciePerRegioni()
   '   LoadComuniPerRegioni()

   '   '  Debug.WriteLine((Environment.TickCount - t).ToString)
   'End Sub


   'Public Sub New(ByVal Nazioni As Hashtable, ByVal Regioni As Hashtable, ByVal Provincie As Hashtable, ByVal Comuni As Hashtable)
   '   m_Nazioni = Nazioni
   '   m_Regioni = Regioni
   '   m_Provincie = Provincie
   '   m_Comuni = Comuni
   '   Initialize()
   '   Me.m_IsInitialized = True
   'End Sub

   Private m_loader As IGeoLocationLoader

   Public Sub New(ByVal loader As IGeoLocationLoader)
      m_ListaNazioni = loader.GetNazioni()
      m_ListaRegioni = loader.GetRegioni
      m_ListaProvincie = loader.GetProvincie
      m_loader = loader
      Me.m_IsInitialized = True
   End Sub



#End Region

#Region "Metodi di supporto nella ricerca"
   Public Function GetNazioneById(ByVal Id As String) As Nazione
      For Each elem As Nazione In m_ListaNazioni
         If elem.Id = Id Then
            Return elem
         End If
      Next
      Return New NazioneNulla
   End Function

   Public Function GetListaNomiNazioni() As IList
      Dim Nazioni As New ArrayList
      For Each elem As Nazione In m_ListaNazioni
         Nazioni.Add(elem.Descrizione)
      Next
      Return Nazioni
   End Function

   Public Function GetNazioneByName(ByVal Name As String) As Nazione
      For Each elem As Nazione In m_ListaNazioni
            If elem.Descrizione.ToUpper = Name.ToUpper Then
                Return elem
            End If
      Next
      Return New NazioneNulla
   End Function

   Public Function GetComuneById(ByVal IdComune As String) As Comune
      Dim id As Int32 = TryCastId(IdComune)
      Dim com As Comune = m_loader.GetComuneById(id)
      If com Is Nothing Then
         Return New ComuneNullo
      End If
      Return com
   End Function


   Private Function TryCastId(ByVal Id As String) As Int32
      Try
         Return Convert.ToInt32(Id)
      Catch ex As Exception
         Return 0
      End Try

   End Function

   Public Function GetComuneByName(ByVal NomeComune As String) As Comune
      Dim com As Comune = m_loader.GetComuneByName(NomeComune)
      If com Is Nothing Then
         Return New ComuneNullo
      End If
      Return com
   End Function



   Public Function GetProvinciaByName(ByVal NomeProvincia As String) As Provincia
      For Each elem As Provincia In m_ListaProvincie
            If elem.Descrizione.ToUpper = NomeProvincia.ToUpper Then
                Return elem
            End If
      Next
      Return New ProvinciaNulla
   End Function
   Public Function GetListaSigleProvincie() As IList
      Dim provincie As New ArrayList
      For Each elem As Provincia In m_ListaProvincie
         provincie.Add(elem.Sigla)
      Next
      Return provincie
   End Function


   Public Function GetProviciaBySigla(ByVal Sigla As String) As Provincia
      For Each elem As Provincia In m_ListaProvincie
            If elem.Sigla.ToUpper = Sigla.ToUpper Then
                Return elem
            End If
      Next
      Return New ProvinciaNulla
   End Function
   Public Function GetProvinciaById(ByVal IdProvincia As String) As Provincia
      For Each elem As Provincia In m_ListaProvincie
         If elem.Id = IdProvincia Then
            Return elem
         End If
      Next
      Return New ProvinciaNulla
   End Function

   Public Function GetRegioneByName(ByVal NomeRegione As String) As Regione
      For Each elem As Regione In m_ListaRegioni
            If elem.Descrizione.ToUpper = NomeRegione.ToUpper Then
                Return elem
            End If
      Next
      Return New RegioneNulla
   End Function
   Public Function GetRegioneById(ByVal IdRegione As String) As Regione
      For Each elem As Regione In m_ListaRegioni
         If elem.Id = IdRegione Then
            Return elem
         End If
      Next
      Return New RegioneNulla
   End Function

   Public Function GetListaNomiProvincie() As IList
      Dim provincie As New ArrayList
      For Each elem As Provincia In m_ListaProvincie
         provincie.Add(elem.Descrizione)
      Next
      Return provincie
   End Function
   Public Function GetListaNomiRegioni() As IList
      Dim regioni As New ArrayList
      For Each elem As Regione In m_ListaRegioni
         regioni.Add(elem.Descrizione)
      Next
      Return regioni
   End Function



#End Region

End Class