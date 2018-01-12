Public Class PeriodoMensile
   Inherits AbstractPeriodo
   Private m_Mese As Integer
   Private m_Esercizio As Integer
   Public Sub New(ByVal Mese As Integer, ByVal Esercizio As Integer)
      If Mese < 1 Or Mese > 12 Then Throw New Exception("Impossibile creare un periodo non compreso tra 1 e 12")
      If Esercizio < 0 Then Throw New Exception("Impossibile creare un periodo con un esercizio < 0")
      m_Mese = Mese
      m_Esercizio = Esercizio
      MyBase.m_DataRange = New DataRange(New DateTime(Esercizio, Mese, 1), CalcolaDataFinePeriodo)
   End Sub

   Public Sub New(ByVal Data As DateTime)
      m_Mese = Data.Month
      m_Esercizio = Data.Year
      MyBase.m_DataRange = New DataRange(New DateTime(m_Esercizio, m_Mese, 1), CalcolaDataFinePeriodo)
   End Sub

   'Public ReadOnly Property DateRange() As DataRange
   '   Get
   '      Return m_DataRange
   '   End Get
   'End Property
   Protected Overrides Function CalcolaDataFinePeriodo() As DateTime

      Dim numGiorni As Integer = DateTime.DaysInMonth(m_Esercizio, m_Mese)
      Return New DateTime(m_Esercizio, m_Mese, numGiorni)

   End Function

   Public Overrides Function ToString() As String
      Return MyBase.m_DataRange.ToString
   End Function
   Public Overrides Sub MergePeriod(ByVal Period As AbstractPeriodo)
      'Metodo utile solamente alla classe composite periodo
      Throw New Exception("Metodo non implementato")
   End Sub


   Public Overrides Function GetNumberOfPeriods() As Integer
      Return 1
   End Function
   Public Overrides Function Overlaps(ByVal Periodo As AbstractPeriodo) As Boolean
      If Periodo.Includes(Me.GetDataRange) Then Return True
      Return False
   End Function
   Public Overrides Function Includes(ByVal Datarange As DataRange) As Boolean
      If Me.GetDataRange.Includes(Datarange) Then Return True
      Return False
   End Function
   Public Overrides Function GetNumberOfCommonPeriods(ByVal Periodo As AbstractPeriodo) As Integer
      If Periodo.Includes(Me.GetDataRange) Then Return 1
      Return 0
   End Function
   Public Overrides Function GetNextPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo
      If NumberOfPeriodUnits <= 0 Then Throw New Exception("Non è possibile calcolare un nuovo periodo se il numerio di periodi di base è 0 oppure è minore di 0")
      Dim confronto As New ConfrontoMensile
      Dim datarange As DataRange = confronto.CreaPeriodoSuccessivo(NumberOfPeriodUnits, MyBase.m_DataRange)
      Return New CompositePeriodo(datarange, New ConfrontoMensile)
   End Function

   Public Overrides Function GetPreviousPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo
      If NumberOfPeriodUnits >= 0 Then Throw New Exception("Non è possibile calcolare un nuovo periodo se il numerio di periodi di base è 0 oppure è maggiore di 0")
      Dim confronto As New ConfrontoMensile
      Dim datarange As DataRange = confronto.CreaPeriodoPrecedente(NumberOfPeriodUnits, MyBase.m_DataRange)
      Return New CompositePeriodo(datarange, New ConfrontoMensile)
   End Function
End Class
