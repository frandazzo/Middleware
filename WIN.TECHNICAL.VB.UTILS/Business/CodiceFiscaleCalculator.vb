Public Class CodiceFiscaleCalculator
   Public Overloads Shared Function CalcolaCodiceFiscale(ByVal Persona As AbstractPersona) As String

      Try
         Dim Codice As String = ""
         Dim data As Date = Format(Persona.DataNascita, "dd/MM/yyyy")
         Dim sex As Int32 = 0
         If Persona.Sesso = AbstractPersona.Sex.Maschio Then
            sex = 1
         Else
            sex = 2
         End If
         If sex = 1 Or sex = 2 Then
         Else
            Throw New Exception("Valore non valido per il sesso")
         End If
         With Persona
            If Persona.Nazionalita.Descrizione = "Italia" Then 'se si tratta di codice italiano
               'per la composizione del codice fiscale uso il comune
               Codice = CodiceFiscale(.Cognome, .Nome, data, sex, .ComuneNascita.CodiceFiscale)
            Else
               'altrimenti uso la nazionalità
               Codice = CodiceFiscale(.Cognome, .Nome, data, sex, .Nazionalita.CodiceFiscale)
            End If
         End With
         Return Codice
      Catch ex As Exception
         Throw (New Exception)
      End Try
   End Function
   Public Overloads Shared Function CalcolaCodiceFiscale(ByVal nome As String, ByVal cognome As String, _
                                               ByVal dataNascita As Date, ByVal sex As Int32, ByVal nazionalita As String, _
                                               ByVal comuneNascita As String) As String


        Dim Codice As String = ""
        'Dim data As Date = DateTime.Parse(Format(dataNascita, "dd/MM/yyyy")).Date
        If sex = 1 Or sex = 2 Then
        Else
            Throw New Exception("Valore non valido per il sesso")
        End If
        If nazionalita = "" Then 'se si tratta di codice italiano
            'per la composizione del codice fiscale uso il comune
            Codice = CodiceFiscale(cognome, nome, dataNascita, sex, comuneNascita)
        Else
            'altrimenti uso la nazionalità
            Codice = CodiceFiscale(cognome, nome, dataNascita, sex, nazionalita)
        End If
        Return Codice

    End Function
   Private Shared Function CodiceFiscale(ByVal Cognome As String, ByVal Nome As String, ByVal dtmDataNascita As Date, ByVal Sesso As Int32, ByVal CodiceComune As String) As String
      '*** Modificata da Michele il 4/6/98

      ' Cognome e Nome in maiuscolo
        ' Sesso  = 1 per maschile   = 2 per femminile


        Dim DataNascita As String
        Dim Cognome_valido As String, Cogn_Consonanti As String = "", Cogn_Vocali As String = ""
        Dim Nome_valido As String = "", Nome_Consonanti As String = "", Nome_Vocali As String = ""
        Dim ms As Int32, somma As Int32

        Cognome_valido = EliminaCaratteriNonValidi(Cognome.ToUpper)
        Nome_valido = EliminaCaratteriNonValidi(Nome.ToUpper)
        SeparaVocaliEConsonanti(Cognome_valido, Cogn_Vocali, Cogn_Consonanti)
        SeparaVocaliEConsonanti(Nome_valido, Nome_Vocali, Nome_Consonanti)
        '  DataNascita = Trim$(DataNascita)
        DataNascita = Format(dtmDataNascita, "dd/MM/yy")

        '-- cognome
        Select Case Len(Cogn_Consonanti)
            Case 0
                CodiceFiscale = Left$(Cogn_Vocali, 2)
                If Len(CodiceFiscale) = 2 Then CodiceFiscale = CodiceFiscale + "X"
            Case 1
                CodiceFiscale = Cogn_Consonanti & Left$(Cogn_Vocali, 2)
                If Len(CodiceFiscale) = 2 Then CodiceFiscale = CodiceFiscale + "X"
            Case 2
                CodiceFiscale = Cogn_Consonanti & Left$(Cogn_Vocali, 1)
            Case Else
                CodiceFiscale = Left$(Cogn_Consonanti, 3)
        End Select
        '-- nome
        Select Case Len(Nome_Consonanti)
            Case 0
                CodiceFiscale = CodiceFiscale & Left$(Nome_Vocali, 2)
                If Len(CodiceFiscale) = 2 Then CodiceFiscale = CodiceFiscale + "X"
            Case 1
                CodiceFiscale = CodiceFiscale & Nome_Consonanti & Left$(Nome_Vocali, 2)
                If Len(CodiceFiscale) = 2 Then CodiceFiscale = CodiceFiscale + "X"
            Case 2
                CodiceFiscale = CodiceFiscale & Nome_Consonanti & Left$(Nome_Vocali, 1)
            Case 3
                CodiceFiscale = CodiceFiscale & Left$(Nome_Consonanti, 3)
            Case Else
                CodiceFiscale = CodiceFiscale & Left$(Nome_Consonanti, 1) + Mid$(Nome_Consonanti, 3, 2)
        End Select
        '-- data di nascita
        CodiceFiscale = CodiceFiscale + Right$(DataNascita, 2)
        ms = Val(Mid$(DataNascita, 4, 2))
        Select Case ms
            Case 1 To 5
                CodiceFiscale = CodiceFiscale + Chr(ms + 64)
            Case 6
                CodiceFiscale = CodiceFiscale + "H"
            Case 7
                CodiceFiscale = CodiceFiscale + "L"
            Case 8
                CodiceFiscale = CodiceFiscale + "M"
            Case 9
                CodiceFiscale = CodiceFiscale + "P"
            Case 10 To 12
                CodiceFiscale = CodiceFiscale + Chr(ms + 72)
        End Select
        If Sesso = 1 Then
            CodiceFiscale = CodiceFiscale + Left$(DataNascita, 2)
        Else
            CodiceFiscale = CodiceFiscale & (Val(Left$(DataNascita, 2)) + 40)
        End If
        '-- codice comune
        CodiceFiscale = CodiceFiscale & CodiceComune
        '-- carattere di controllo
        If Len(CodiceFiscale) >= 15 Then
            For ms = 1 To 15 Step 2
                somma = somma + ConversioneCharPosizDispari(Mid$(CodiceFiscale, ms, 1))
            Next
            For ms = 2 To 14 Step 2
                somma = somma + ConversioneCharPosizPari(Mid$(CodiceFiscale, ms, 1))
            Next
            CodiceFiscale = CodiceFiscale + Chr(65 + (somma Mod 26))
        Else
            CodiceFiscale = ""
        End If




    End Function
   Private Shared Function EliminaCaratteriNonValidi(ByRef Stringa As String) As String

      Dim l As Int32, i As Int32, Vl As Int32
      'Nuovo aggiunto per togliere il warning
      '**************************************
      EliminaCaratteriNonValidi = ""
      '**************************************
      l = Len(Stringa)
      For i = 1 To l
         Vl = Asc(Mid(Stringa, i, 1))
         If Vl > 64 And Vl < 91 Then EliminaCaratteriNonValidi = EliminaCaratteriNonValidi + Chr(Vl)
      Next

   End Function
   Private Shared Sub SeparaVocaliEConsonanti(ByRef Stringa As String, ByRef Vocali As String, ByRef Consonanti As String)

      Dim l As Int32, i As Int32, Vl As String '* 1

      Vocali = ""
      Consonanti = ""
      l = Len(Stringa)
      For i = 1 To l
         Vl = UCase(Mid(Stringa, i, 1))
         If Vl = "A" Or Vl = "E" Or Vl = "I" Or Vl = "O" Or Vl = "U" Then
            Vocali = Vocali + Mid(Stringa, i, 1)
         Else
            Consonanti = Consonanti + Mid(Stringa, i, 1)
         End If
      Next

   End Sub
   Private Shared Function ConversioneCharPosizPari(ByVal Carattere As String) As Int32

      Select Case Asc(Carattere)
         Case 48 To 57
            ConversioneCharPosizPari = Asc(Carattere) - 48
         Case 65 To 90
            ConversioneCharPosizPari = Asc(Carattere) - 65
      End Select

   End Function
   Private Shared Function ConversioneCharPosizDispari(ByVal Carattere As String) As Int32

      Select Case Carattere
         Case "A", "0"
            ConversioneCharPosizDispari = 1
         Case "B", "1"
            ConversioneCharPosizDispari = 0
         Case "C", "2"
            ConversioneCharPosizDispari = 5
         Case "D", "3"
            ConversioneCharPosizDispari = 7
         Case "E", "4"
            ConversioneCharPosizDispari = 9
         Case "F", "5"
            ConversioneCharPosizDispari = 13
         Case "G", "6"
            ConversioneCharPosizDispari = 15
         Case "H", "7"
            ConversioneCharPosizDispari = 17
         Case "I", "8"
            ConversioneCharPosizDispari = 19
         Case "J", "9"
            ConversioneCharPosizDispari = 21
         Case "K"
            ConversioneCharPosizDispari = 2
         Case "L"
            ConversioneCharPosizDispari = 4
         Case "M"
            ConversioneCharPosizDispari = 18
         Case "N"
            ConversioneCharPosizDispari = 20
         Case "O"
            ConversioneCharPosizDispari = 11
         Case "P"
            ConversioneCharPosizDispari = 3
         Case "Q"
            ConversioneCharPosizDispari = 6
         Case "R"
            ConversioneCharPosizDispari = 8
         Case "S"
            ConversioneCharPosizDispari = 12
         Case "T"
            ConversioneCharPosizDispari = 14
         Case "U"
            ConversioneCharPosizDispari = 16
         Case "V"
            ConversioneCharPosizDispari = 10
         Case "W"
            ConversioneCharPosizDispari = 22
         Case "X"
            ConversioneCharPosizDispari = 25
         Case "Y"
            ConversioneCharPosizDispari = 24
         Case "Z"
            ConversioneCharPosizDispari = 23
      End Select

   End Function



   Public Shared Function GetDatiFiscali(ByRef codiceFiscale As String, ByVal comuniLoader As IGeoLocationLoader)

      If codiceFiscale.Length <> 16 Then Throw New InvalidFiscalCodeException("Lunghezza del codice fiscale errata")

      codiceFiscale = codiceFiscale.ToUpper

      Dim errore As String = ""

      'Calcolo l'anno
      Dim anno As String = Mid(codiceFiscale, 7, 2)
      If Not IsNumeric(anno) Then errore += "L'anno non ha un formato numerico (" & anno & ") " & Environment.NewLine
      'Calcolo il mese
      Dim mese As String = CalculateMese(Mid(codiceFiscale, 9, 1))
      If Not IsNumeric(mese) Then errore += mese & Environment.NewLine
      'Calcolo il giorno
      Dim giorno As String = CalculateGiorno(Mid(codiceFiscale, 10, 2))
      If Not IsNumeric(giorno) Then errore += giorno & Environment.NewLine


      'Calcolo il sesso
      Dim sex As DatiFiscali.Sesso = DatiFiscali.Sesso.Maschio
      If IsNumeric(giorno) Then
         If giorno > 40 Then
            sex = DatiFiscali.Sesso.Femmina
            giorno = (Val(giorno) - 40).ToString
         End If
      End If

      If errore <> "" Then Throw New InvalidFiscalCodeException(errore)


      'assegno un valore all'anno scegliendo tra 1900 0 2000
      anno = CostruisciAnno(anno)



      'costruisco la data di nascita
      'proteggo con una struttura try catch poichè il giorno e il mese potrebbero non
      'essere compatibili

      Dim data As DateTime

      Try
         data = New DateTime(Val(anno), Val(mese), Val(giorno))
      Catch ex As Exception
         Throw New InvalidFiscalCodeException("Errore nel formato della data. Controllare che i valori immessi di anno( " & anno & "), mese(" & mese & ") e giorno(" & giorno & ") siano corretti" & Environment.NewLine & ex.Message)
      End Try



      'Calcolo la nazione
      'Calcolo la provincia
      'Calcolo il comune
      Dim cod As String = Mid(codiceFiscale, 12, 4)

      Dim comune As Comune = New ComuneNullo
      Dim provincia As Provincia = New ProvinciaNulla
      Dim nazione As Nazione = New NazioneNulla


      If cod.StartsWith("Z") Then
         nazione = comuniLoader.GetNazionByFiscalCode(cod)
      Else
         nazione = comuniLoader.GetNazionByFiscalCode("A000")
         comune = CalcolaComune(cod, comuniLoader)
         provincia = comuniLoader.GetProvinciaById(comune.IdProvincia)
      End If

      'Costruisco la struttura DATI FISCALI

      Dim d = New DatiFiscali(data, sex, comune, provincia, nazione)

      Return d
   End Function



   Private Shared Function CostruisciAnno(ByVal anno As String) As String

      Dim a As Int32 = Val(anno)

      Dim duemila As Boolean = False
      Dim diff As Int32 = 0

      If Date.Now.Year > 2000 Then
         duemila = True
      End If

      If duemila Then
         diff = Date.Now.Year - 2000
      Else
         diff = Date.Now.Year - 1900
      End If

      If a > diff Then
         Return (1900 + a).ToString
      Else
         Return (2000 + a).ToString
      End If


   End Function



   Private Shared Function CalcolaComune(ByVal CodiceFiscale As String, ByVal loader As IGeoLocationLoader) As Comune

      Dim list As IList = loader.GetComuneByFiscalCode(CodiceFiscale)

      If list.Count = 0 Then Return New ComuneNullo


      'nel caso ci siano comuni multipli (nuove aggiunte) prendo sempre l'ultimo
      Return list(list.Count - 1)



   End Function




   Private Shared Function CalculateGiorno(ByVal CodiceFiscale As String) As String

      If Not IsNumeric(CodiceFiscale) Then Return "Il codice giorno (" & CodiceFiscale & ") non è numerico"

      Dim giorno As Int32 = Val(CodiceFiscale)


      Return giorno.ToString
   End Function



   Private Shared Function CalculateMese(ByVal CodiceFiscale As String) As String

      Select Case CodiceFiscale

         Case "A"
            Return "01"
         Case "B"
            Return "02"
         Case "C"
            Return "03"
         Case "D"
            Return "04"
         Case "E"
            Return "05"
         Case "H"
            Return "06"
         Case "L"
            Return "07"
         Case "M"
            Return "08"
         Case "P"
            Return "09"
         Case "R"
            Return "10"
         Case "S"
            Return "11"
         Case "T"
            Return "12"
         Case Else
            Return "Il codice mese (" & CodiceFiscale & ") non è stato identificato!"




      End Select


   End Function


End Class
