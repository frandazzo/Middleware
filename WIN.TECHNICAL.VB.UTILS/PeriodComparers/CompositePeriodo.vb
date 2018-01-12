Public Class CompositePeriodo
   Inherits AbstractPeriodo
   Protected m_ListOfPeriod As New ArrayList
   Private m_MeseIniziale As Integer
   Private m_EsercizioIniziale As Integer
   Private m_MeseFinale As Integer
   Private m_EsercizioFinale As Integer


   Public Sub New(ByVal MeseIniziale As Integer, ByVal EsercizioIniziale As Integer, ByVal MeseFinale As Integer, ByVal EsercizioFinale As Integer, ByVal StrategiaDiConfronto As IConfrontoPeriodi)
      If MeseIniziale < 1 Or MeseIniziale > 12 Then Throw New Exception("Impossibile creare un periodo non compreso tra 1 e 12")
      If MeseFinale < 1 Or MeseFinale > 12 Then Throw New Exception("Impossibile creare un periodo non compreso tra 1 e 12")
      If EsercizioIniziale < 0 Then Throw New Exception("Impossibile creare un periodo con un esercizio < 0")
      If EsercizioFinale < 0 Then Throw New Exception("Impossibile creare un periodo con un esercizio < 0")

      m_MeseIniziale = MeseIniziale
      m_EsercizioIniziale = EsercizioIniziale
      m_MeseFinale = MeseFinale
      m_EsercizioFinale = EsercizioFinale
      MyBase.m_DataRange = New DataRange(New DateTime(EsercizioIniziale, MeseIniziale, 1), CalcolaDataFinePeriodo)
      If MyBase.m_DataRange.IsEmpty Then Throw New Exception("Impossibile creare una lista di periodi se il DataRange è vuoto ")
      m_ConfrontoStrategy = StrategiaDiConfronto
      m_ListOfPeriod = m_ConfrontoStrategy.CreaListaPeriodi(MyBase.m_DataRange)
      MyBase.m_DataRange = m_ConfrontoStrategy.GetDataRange
   End Sub

   Public Sub New(ByVal MeseIniziale As Integer, ByVal EsercizioIniziale As Integer, ByVal MeseFinale As Integer, ByVal EsercizioFinale As Integer, ByVal StrategiaDiConfronto As IConfrontoPeriodi.UnitaDiCompetenza)

      If MeseIniziale < 1 Or MeseIniziale > 12 Then Throw New Exception("Impossibile creare un periodo non compreso tra 1 e 12")
      If MeseFinale < 1 Or MeseFinale > 12 Then Throw New Exception("Impossibile creare un periodo non compreso tra 1 e 12")
      If EsercizioIniziale < 0 Then Throw New Exception("Impossibile creare un periodo con un esercizio < 0")
      If EsercizioFinale < 0 Then Throw New Exception("Impossibile creare un periodo con un esercizio < 0")

      m_MeseIniziale = MeseIniziale
      m_EsercizioIniziale = EsercizioIniziale
      m_MeseFinale = MeseFinale
      m_EsercizioFinale = EsercizioFinale
      MyBase.m_DataRange = New DataRange(New DateTime(EsercizioIniziale, MeseIniziale, 1), CalcolaDataFinePeriodo)
      If MyBase.m_DataRange.IsEmpty Then Throw New Exception("Impossibile creare una lista di periodi se il DataRange è vuoto ")
      m_ConfrontoStrategy = IIf(StrategiaDiConfronto = IConfrontoPeriodi.UnitaDiCompetenza.Mensile, New ConfrontoMensile, New ConfrontoGiornaliero)
      m_ListOfPeriod = m_ConfrontoStrategy.CreaListaPeriodi(MyBase.m_DataRange)
      MyBase.m_DataRange = m_ConfrontoStrategy.GetDataRange
   End Sub
   Public Sub New(ByVal DataRange As DataRange, ByVal StrategiaDiConfronto As IConfrontoPeriodi.UnitaDiCompetenza)
      MyBase.m_DataRange = DataRange
      m_MeseIniziale = MyBase.m_DataRange.Start.Month
      m_EsercizioIniziale = MyBase.m_DataRange.Start.Year
      m_MeseFinale = MyBase.m_DataRange.Finish.Month
      m_EsercizioFinale = MyBase.m_DataRange.Finish.Year
      If MyBase.m_DataRange.IsEmpty Then Throw New Exception("Impossibile creare una lista di periodi se il DataRange è vuoto ")
      m_ConfrontoStrategy = IIf(StrategiaDiConfronto = IConfrontoPeriodi.UnitaDiCompetenza.Mensile, New ConfrontoMensile, New ConfrontoGiornaliero)
      m_ListOfPeriod = m_ConfrontoStrategy.CreaListaPeriodi(MyBase.m_DataRange)
      MyBase.m_DataRange = m_ConfrontoStrategy.GetDataRange
   End Sub

   Public Sub New(ByVal DataRange As DataRange, ByVal StrategiaDiConfronto As IConfrontoPeriodi)
      MyBase.m_DataRange = DataRange
      m_MeseIniziale = MyBase.m_DataRange.Start.Month
      m_EsercizioIniziale = MyBase.m_DataRange.Start.Year
      m_MeseFinale = MyBase.m_DataRange.Finish.Month
      m_EsercizioFinale = MyBase.m_DataRange.Finish.Year
      If MyBase.m_DataRange.IsEmpty Then Throw New Exception("Impossibile creare una lista di periodi se il DataRange è vuoto ")
      m_ConfrontoStrategy = StrategiaDiConfronto
      m_ListOfPeriod = m_ConfrontoStrategy.CreaListaPeriodi(MyBase.m_DataRange)
      MyBase.m_DataRange = m_ConfrontoStrategy.GetDataRange
   End Sub


   Protected Overrides Function CalcolaDataFinePeriodo() As DateTime

      Dim numGiorni As Integer = DateTime.DaysInMonth(m_EsercizioFinale, m_MeseFinale)
      Return New DateTime(m_EsercizioFinale, m_MeseFinale, numGiorni)

   End Function

   Public Overrides Function ToString() As String
      Return MyBase.m_DataRange.ToString()
   End Function
   Public Overrides Sub MergePeriod(ByVal Period As AbstractPeriodo)
      Try
         'i c riteri di merging possono essere affidati ad uno strategy
         Dim temp As DataRange = Period.GetDataRange
         If MyBase.m_DataRange.Includes(temp) Then Return
         MyBase.m_DataRange.Merge(temp)
         m_MeseIniziale = MyBase.m_DataRange.Start.Month
         m_EsercizioIniziale = MyBase.m_DataRange.Start.Year
         m_MeseFinale = MyBase.m_DataRange.Finish.Month
         m_EsercizioFinale = MyBase.m_DataRange.Finish.Year
         m_ListOfPeriod = m_ConfrontoStrategy.CreaListaPeriodi(MyBase.m_DataRange)
      Catch ex As Exception
         Throw New Exception(ex.Message)
      End Try
   End Sub


   Public Overrides Function GetNumberOfPeriods() As Integer
      Return m_ListOfPeriod.Count
   End Function
   Public Overrides Function Overlaps(ByVal Periodo As AbstractPeriodo) As Boolean
      For Each elem As AbstractPeriodo In m_ListOfPeriod
         If Periodo.Includes(elem.GetDataRange) Then Return True
      Next
      Return False
   End Function
   Public Overrides Function Includes(ByVal DataRange As DataRange) As Boolean
      For Each elem As AbstractPeriodo In m_ListOfPeriod
         If elem.GetDataRange.Includes(DataRange) Then Return True
      Next
      Return False
   End Function
   Public Overrides Function GetNumberOfCommonPeriods(ByVal periodo As AbstractPeriodo) As Integer
      Dim total As Integer = 0
      For Each elem As AbstractPeriodo In m_ListOfPeriod
         If periodo.Includes(elem.GetDataRange) Then
            total = total + 1
         End If

      Next
      Return total
   End Function
   Public Overrides Function GetNextPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo
      If NumberOfPeriodUnits <= 0 Then Throw New Exception("Non è possibile calcolare un nuovo periodo se il numerio di periodi di base è 0 oppure è minore di 0")
      Dim datarange As DataRange = m_ConfrontoStrategy.CreaPeriodoSuccessivo(NumberOfPeriodUnits, MyBase.m_DataRange)
      Return New CompositePeriodo(datarange, m_ConfrontoStrategy)
   End Function

   Public Overrides Function GetPreviousPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo
      If NumberOfPeriodUnits > 0 Then Throw New Exception("Non è possibile calcolare un nuovo periodo precredente se il numerio di periodi di base è 0 oppure è maggiore di 0")
      Dim datarange As DataRange = m_ConfrontoStrategy.CreaPeriodoPrecedente(NumberOfPeriodUnits, MyBase.m_DataRange)
      Return New CompositePeriodo(datarange, m_ConfrontoStrategy)
   End Function
End Class
