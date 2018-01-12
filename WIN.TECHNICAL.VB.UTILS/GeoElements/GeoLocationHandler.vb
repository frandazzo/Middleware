Public Class GeoLocationHandler
   Protected m_Comuni As Hashtable
   Protected m_Provincie As Hashtable
   Protected m_Regioni As Hashtable
   Protected m_Nazioni As Hashtable



   Protected m_ListaProvincie As IList
   Protected m_ListaRegioni As IList
    Protected m_ListaNazioni As IList

    Private m_cittaBolzanoTradotto As String() = New String() {"Avelengo * Hafling",
"Badia * Abtei",
"Barbiano * Barbian",
"Bolzano * Bozen",
"Braies * Prags",
"Brennero * Brenner",
"Bressanone * Brixen",
"Bronzolo * Branzoll",
"Brunico * Bruneck",
"Caines * Kuens",
"Caldaro Sulla Strada Del Vino * Kaltern",
"Campo Di Trens * Freienfeld",
"Campo Tures * Sand In Taufers",
"Castelbello Ciardes * Kastelbell Tschars",
"Castelrotto * Kastelruth",
"Cermes * Tscherms",
"Chienes * Kiens",
"Chiusa * Klausen",
"Cornedo All'Isarco * Karneid",
"Cortaccia Sulla Strada Del Vino * Kurtatsch",
"Cortina Sulla Strada Del Vino * Kurtinig",
"Corvara In Badia * Kurfar",
"Curon Venosta * Graun In Vinschgau",
"Dobbiaco * Toblach",
"Egna * Neumarkt",
"Falzes * Pfalzen",
"Fie' Allo Sciliar * Vols Am Schlern",
"Fortezza * Franzensfesten",
"Funes * Villnoss",
"Gais * Gais",
"Gargazzone * Gargazon",
"Glorenza * Glurns",
"La Valle * Wengen",
"Laces * Latsch",
"Lagundo * Algund",
"Laion * Lajen",
"Laives * Leifers",
"Lana * Lana",
"Lasa * Laas",
"Lauregno * Laurein",
"Luson * Lusen",
"Magre' Sulla Strada Del Vino * Margreid",
"Malles Venosta * Mals",
"Marebbe * Enneberg",
"Martello * Martell",
"Meltina * Molten",
"Merano * Meran",
"Monguelfo * Welsberg",
"Montagna * Montan",
"Moso In Passiria * Moos In Passeier",
"Nalles * Nals",
"Naturno * Naturns",
"Naz Sciaves * Natz Schabs",
"Nova Levante * Welschnofen",
"Nova Ponente * Deutschnofen",
"Ora * Auer",
"Ortisei * St Ulrich",
"Parcines * Partschins",
"Perca * Percha",
"Plaus * Plaus",
"Ponte Gardena * Waidbruck",
"Postal * Burgstall",
"Prato Allo Stelvio * Prad Am Stilfserioch",
"Predoi * Prettau",
"Proves * Proveis",
"Racines * Ratschings",
"Rasun Anterselva * Rasen Antholz",
"Renon * Ritten",
"Rifiano * Riffian",
"Rio Di Pusteria * Muhlbach",
"Rodengo * Rodeneck",
"Salorno * Salurn",
"San Candido * Innichen",
"San Genesio Atesino * Jenesien",
"San Leonardo In Passiria * St Leonhard In Passeier",
"San Lorenzo Di Sebato * St Lorenzen",
"San Pancrazio * St Pankraz",
"Santa Cristina Valgardena * St Christina In Groden",
"Sarentino * Sarntal",
"Scena * Schonna",
"Selva Dei Molini * Muhlwald",
"Selva Di Val Gardena * Wolkenstein In Groden",
"Senale-san Felice * Unsere Liebe Frau Im Walde-st Felix",
"Senales * Schnals",
"Sesto * Sexten",
"Silandro * Schlanders",
"Sluderno * Schluderns",
"Stelvio * Stilfs",
"Terento * Terenten",
"Terlano * Terlan",
"Termeno Sulla Strada Del Vino * Tramin",
"Tesimo * Tisens",
"Tires * Tiers",
"Tirolo * Tirol",
"Trodena * Truden",
"Tubre * Taufers In Munsterthal",
"Ultimo * Ulten",
"Vadena * Pfatten",
"Val Di Vizze * Pfitsch",
"Valdaora * Olang",
"Valle Aurina * Ahrntal",
"Valle Di Casies * Gsies",
"Vandoies * Vintl",
"Varna * Vahrn",
"Velturno * Feldthurns",
"Verano * Voran",
"Villabassa",
"Villandro * Villanders",
"Vipiteno * Sterzing",
"Acereto * Ahornach",
"Albes * Albeins",
"Alliz * Allitz",
"Anterselva * Antholz",
"Burgusio * Burgeis",
"Caminata In Tures * Kematen",
"Castelbello * Kastelbell",
"Casteldarne * Ehrenburg",
"Cauria * Gfrill",
"Cengles * Tschengls",
"Ceves * Tschofs",
"Ciardes * Tschars",
"Clusio * Schleis",
"Coldrano * Goldrain",
"Colfosco * Colfuschg",
"Colle In Casies * Pichl In Gsies",
"Colli In Pusteria * Pichlern",
"Colsano * Galsaun",
"Corti In Pusteria * Hofern",
"Corvara In Passiria * Rabenstein",
"Corzes * Kortsch",
"Covelano * Goflan",
"Curon * Graun",
"Elle * Ellen",
"Eores * Afers",
"Faogna Di Sotto * Unterpfennberg",
"Fleres * Pflersch",
"Foiana * Vollan",
"Fundres * Pfunders",
"Gries * Gries",
"Grimaldo * Greinwalden",
"Gudon * Gufidaun",
"Issengo * Issing",
"Lacinigo * Latschinig",
"Lappago * Lappach",
"Laudes * Laatsch",
"Lazfons * Latzfons",
"Longiaru' * Campill",
"Lutago * Luttach",
"Magre' * Margreid",
"Maia Alta * Obermais",
"Maia Bassa * Untermais",
"Malles * Mals",
"Mantana * Monthal",
"Maranza * Maransen",
"Mareta * Mareit",
"Mazia * Matsch",
"Millan-sarness * Milland-sarns",
"Molini Di Tures * Muhlen",
"Monghezzo Di Fuori * Getzenberg",
"Montassilone * Tesselberg",
"Monte Di Mezzodi' * Sonnenberg",
"Monte Di Tramontana * Nordersberg",
"Monte San Candido * Innichberg",
"Montechiaro * Lichtenberg",
"Montefontana * Tomberg",
"Monteponente * Pfeffersberg",
"Morter",
"Mules * Mauls",
"Naz * Natz",
"Novacella * Neustift",
"Onies * Onach",
"Oris * Eyrs",
"Planol * Planeil",
"Plata * Platt",
"Prati * Wiesen",
"Prato Alla Drava * Winnbach",
"Quarazze * Gratsch",
"Rasun Di Sopra * Oberrasen",
"Rasun Di Sotto * Niederrasen",
"Resia * Reschen",
"Ridanna * Riednaun",
"Rina * Welschellen",
"Riomolino * Muhlbach",
"Riscone * Reischach",
"Riva Di Tures * Rain Taufers",
"San Felice * St Felix",
"San Giacomo * St Jakob In Ahrn",
"San Giorgio * St Georgen",
"San Giovanni * St Johann In Ahrn",
"San Leonardo * St Leonhard",
"San Martino Al Monte * St Martin Am Vorberg",
"San Martino In Badia * St Martin In Thurn",
"San Martino In Casies * St Martin In Gsies",
"San Martino In Passiria * St Martin In Passeier",
"San Pietro * St Peter In Ahrn",
"San Sigismondo * St Sigmund",
"San Valentino Al Brennero * Brenner",
"San Valentino Alla Mutta * St Valentin Auf Der Haide",
"Sant'Andrea In Monte * St Andra",
"Santa Maddalena In Casies * St Magdalena In Gsies",
"Scaleres * Schalder",
"Sciaves * Schabs",
"Slingia * Schlinig",
"Spinga * Spinges",
"Stava * Staben",
"Stilves * Stilfes",
"Tabla * Tabland",
"Tanas * Tannas",
"Tarces * Tartsch",
"Tarres * Tarsch",
"Telves * Telfes",
"Teodone * Dietenheim",
"Tesido * Taisten",
"Tiso * Theis",
"Trens * Trens",
"Tunes * Thuins",
"Valas * Flaas",
"Valgiovo * Jaufenthal",
"Vallarga * Weitental",
"Valle San Silvestro * Vahlen",
"Vallelunga * Langtaufers",
"Valles * Vals",
"Vandoies Di Sopra * Obervintl",
"Vandoies Di Sotto * Niedervintl",
"Vanga * Wangen",
"Versciago * Vierschach",
"Vezzano * Vezzan",
"Villa Ottone * Uttenheim",
"Villa Santa Caterina * Aufhofen",
"Villabassa * Niederdorf",
"Vizze * Pfitsch",
"Marlengo * marling",
"Morter * morter",
"Prato in venosta * prad",
"Rasun valdaora * rasen olang",
"Senale * unsere liebe frau im walde"}

    Private m_cittaBolzano As String() = New String() {"Avelengo ",
"Badia ",
"Barbiano ",
"Bolzano ",
"Braies ",
"Brennero ",
"Bressanone ",
"Bronzolo ",
"Brunico ",
"Caines ",
"Caldaro Sulla Strada Del Vino ",
"Campo Di Trens ",
"Campo Tures ",
"Castelbello Ciardes ",
"Castelrotto ",
"Cermes ",
"Chienes ",
"Chiusa ",
"Cornedo All'Isarco ",
"Cortaccia Sulla Strada Del Vino ",
"Cortina Sulla Strada Del Vino ",
"Corvara In Badia ",
"Curon Venosta ",
"Dobbiaco ",
"Egna ",
"Falzes ",
"Fie' Allo Sciliar ",
"Fortezza ",
"Funes ",
"Gais ",
"Gargazzone ",
"Glorenza ",
"La Valle ",
"Laces ",
"Lagundo ",
"Laion ",
"Laives ",
"Lana ",
"Lasa ",
"Lauregno ",
"Luson ",
"Magre' Sulla Strada Del Vino ",
"Malles Venosta ",
"Marebbe ",
"Martello ",
"Meltina ",
"Merano ",
"Monguelfo ",
"Montagna ",
"Moso In Passiria ",
"Nalles ",
"Naturno ",
"Naz Sciaves ",
"Nova Levante ",
"Nova Ponente ",
"Ora ",
"Ortisei ",
"Parcines ",
"Perca ",
"Plaus ",
"Ponte Gardena ",
"Postal ",
"Prato Allo Stelvio ",
"Predoi ",
"Proves ",
"Racines ",
"Rasun Anterselva ",
"Renon ",
"Rifiano ",
"Rio Di Pusteria ",
"Rodengo ",
"Salorno ",
"San Candido ",
"San Genesio Atesino ",
"San Leonardo In Passiria ",
"San Lorenzo Di Sebato ",
"San Pancrazio ",
"Santa Cristina Valgardena ",
"Sarentino ",
"Scena ",
"Selva Dei Molini ",
"Selva Di Val Gardena ",
"Senale-san Felice ",
"Senales ",
"Sesto ",
"Silandro ",
"Sluderno ",
"Stelvio ",
"Terento ",
"Terlano ",
"Termeno Sulla Strada Del Vino ",
"Tesimo ",
"Tires ",
"Tirolo ",
"Trodena ",
"Tubre ",
"Ultimo ",
"Vadena ",
"Val Di Vizze ",
"Valdaora ",
"Valle Aurina ",
"Valle Di Casies ",
"Vandoies ",
"Varna ",
"Velturno ",
"Verano ",
"Villabassa",
"Villandro ",
"Vipiteno ",
"Acereto ",
"Albes ",
"Alliz ",
"Anterselva ",
"Burgusio ",
"Caminata In Tures ",
"Castelbello ",
"Casteldarne ",
"Cauria ",
"Cengles ",
"Ceves ",
"Ciardes ",
"Clusio ",
"Coldrano ",
"Colfosco ",
"Colle In Casies ",
"Colli In Pusteria ",
"Colsano ",
"Corti In Pusteria ",
"Corvara In Passiria ",
"Corzes ",
"Covelano ",
"Curon ",
"Elle ",
"Eores ",
"Faogna Di Sotto ",
"Fleres ",
"Foiana ",
"Fundres ",
"Gries ",
"Grimaldo ",
"Gudon ",
"Issengo ",
"Lacinigo ",
"Lappago ",
"Laudes ",
"Lazfons ",
"Longiaru' ",
"Lutago ",
"Magre' ",
"Maia Alta ",
"Maia Bassa ",
"Malles ",
"Mantana ",
"Maranza ",
"Mareta ",
"Mazia ",
"Millan-sarness ",
"Molini Di Tures ",
"Monghezzo Di Fuori ",
"Montassilone ",
"Monte Di Mezzodi' ",
"Monte Di Tramontana ",
"Monte San Candido ",
"Montechiaro ",
"Montefontana ",
"Monteponente ",
"Morter",
"Mules ",
"Naz ",
"Novacella ",
"Onies ",
"Oris ",
"Planol ",
"Plata ",
"Prati ",
"Prato Alla Drava ",
"Quarazze ",
"Rasun Di Sopra ",
"Rasun Di Sotto ",
"Resia ",
"Ridanna ",
"Rina ",
"Riomolino ",
"Riscone ",
"Riva Di Tures ",
"San Felice ",
"San Giacomo ",
"San Giorgio ",
"San Giovanni ",
"San Leonardo ",
"San Martino Al Monte ",
"San Martino In Badia ",
"San Martino In Casies ",
"San Martino In Passiria ",
"San Pietro ",
"San Sigismondo ",
"San Valentino Al Brennero ",
"San Valentino Alla Mutta ",
"Sant'Andrea In Monte ",
"Santa Maddalena In Casies ",
"Scaleres ",
"Sciaves ",
"Slingia ",
"Spinga ",
"Stava ",
"Stilves ",
"Tabla ",
"Tanas ",
"Tarces ",
"Tarres ",
"Telves ",
"Teodone ",
"Tesido ",
"Tiso ",
"Trens ",
"Tunes ",
"Valas ",
"Valgiovo ",
"Vallarga ",
"Valle San Silvestro ",
"Vallelunga ",
"Valles ",
"Vandoies Di Sopra ",
"Vandoies Di Sotto ",
"Vanga ",
"Versciago ",
"Vezzano ",
"Villa Ottone ",
"Villa Santa Caterina ",
"Villabassa ",
"Vizze ",
"Marlengo ",
"Morter ",
"Prato in venosta ",
"Rasun valdaora ",
"Senale "
}


    Public ReadOnly Property ListaRegioni() As IList
        Get
            Return m_ListaRegioni
        End Get
    End Property

    Public ReadOnly Property ListaProvince() As IList
        Get
            Return m_ListaProvincie
        End Get
    End Property

    Public ReadOnly Property ListaNazioni() As IList
        Get
            Return m_ListaNazioni
        End Get
    End Property

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
    Public Overloads Function GetProviciePerRegione(ByVal NomeRegione As String) As IList
        Dim reg As Regione

        reg = Me.GetRegioneByName(NomeRegione)
        Return reg.ListaProvincie

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
    Public Function GetComuniPerRegione(ByVal NomeRegione As String) As IList
        Dim reg As Regione

        reg = Me.GetRegioneByName(NomeRegione)
        Return reg.ListaComuni

    End Function
    Public Function GetNomiComuniPerProvincia(ByVal NomeProvincia As String) As IList
        Dim Prov As Provincia

        Prov = Me.GetProvinciaByName(NomeProvincia)
        Return Prov.GetListaNomiComuni

    End Function

    Public Function GetComuniPerProvincia(ByVal NomeProvincia As String) As IList
        Dim Prov As Provincia

        Prov = Me.GetProvinciaByName(NomeProvincia)
        Return Prov.ListaComuni
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

    Public ReadOnly Property Loader() As IGeoLocationLoader
        Get
            Return m_loader
        End Get
    End Property

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


        NomeComune = CheckIfComuneProvinciaBolzano(NomeComune)


        Dim com As Comune = m_loader.GetComuneByName(NomeComune)
        If com Is Nothing Then
            Return New ComuneNullo
        End If
        Return com
    End Function

    Private Function CheckIfComuneProvinciaBolzano(comune As String) As String
        If String.IsNullOrEmpty(comune) Then
            Return comune
        End If

        Return GetCittaDiBolzanoTradotta(comune)



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

    Private Function IsInCittaDiBolzano(p1 As String) As Boolean
        For Each elem As String In m_cittaBolzano

            If elem.Trim.ToLower.Equals(p1.ToLower) Then
                Return True
            End If


        Next
        Return False
    End Function
    Private Function GetCittaDiBolzanoTradotta(p1 As String) As String
        Dim i As Int32 = 0
        For Each elem As String In m_cittaBolzano

            If elem.Trim.ToLower.Equals(p1.ToLower) Then
                Return m_cittaBolzanoTradotto(i)
            End If

            i = i + 1
        Next
        Return p1
    End Function
End Class