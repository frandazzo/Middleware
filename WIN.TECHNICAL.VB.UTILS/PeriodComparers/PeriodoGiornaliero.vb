Public Class PeriodoGiornaliero
   Inherits AbstractPeriodo


   Private m_data As DateTime

   Public Sub New(ByVal Data As DateTime)
      m_data = Data
      MyBase.m_DataRange = New DataRange(Data, Data)
   End Sub


   Protected Overrides Function CalcolaDataFinePeriodo() As DateTime
      '
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
      Dim confronto As New ConfrontoGiornaliero
      Dim datarange As DataRange = confronto.CreaPeriodoSuccessivo(NumberOfPeriodUnits, MyBase.m_DataRange)
      Return New CompositePeriodo(datarange, New ConfrontoGiornaliero)
   End Function

   Public Overrides Function GetPreviousPeriod(ByVal NumberOfPeriodUnits As Integer) As AbstractPeriodo
      If NumberOfPeriodUnits >= 0 Then Throw New Exception("Non è possibile calcolare un nuovo periodo se il numerio di periodi di base è 0 oppure è maggiore di 0")
      Dim confronto As New ConfrontoGiornaliero
      Dim datarange As DataRange = confronto.CreaPeriodoPrecedente(NumberOfPeriodUnits, MyBase.m_DataRange)
      Return New CompositePeriodo(datarange, New ConfrontoGiornaliero)
   End Function
End Class
