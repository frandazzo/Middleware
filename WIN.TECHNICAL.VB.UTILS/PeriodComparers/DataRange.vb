Public Class DataRange
    Implements IComparable
    Private m_start As DateTime
    Private m_finish As DateTime

   Public Sub New(ByVal start As DateTime, ByVal finish As DateTime)
      'If start <> Date.MinValue And finish <> Date.MinValue Then
      m_start = start
      m_finish = finish
      'Else
      ''Throw New Exception("Impossibile creare un range di date con data inizio o data fine nulli!")
      'End If

   End Sub
    Public ReadOnly Property Start() As DateTime
        Get
            Return m_start
        End Get

    End Property
    Public ReadOnly Property Finish() As DateTime
        Get
            Return m_finish
        End Get

    End Property


   Public Overrides Function ToString() As String
      If IsEmpty() Then Return "Data range vuoto"
      Return Format(m_start, "dd/MM/yyyy") & " - " & Format(m_finish, "dd/MM/yyyy")
   End Function
   Public Overrides Function Equals(ByVal obj As Object) As Boolean
      Try
         If TypeOf (obj) Is System.DBNull Then Return False
         Dim temp As DataRange = DirectCast(obj, DataRange)
         Return m_start.Equals(temp.Start) And m_finish.Equals(temp.Finish)
      Catch ex As Exception
         Throw New Exception("Non è possibile verificare l'uguaglianza tra i datarange poichè l'oggetto passato non è un datarange")
      End Try


   End Function

    Public Function IsEmpty() As Boolean
      Return m_start.Subtract(m_finish).TotalSeconds > 0
    End Function

    'Uno dei metodi fondamentali è l'Include Method
   Public Function Includes(ByVal Data As DateTime) As Boolean
      Dim start As DateTime = New DateTime(m_start.Year, m_start.Month, m_start.Day, 0, 0, 0)
      Dim fin As DateTime = New DateTime(m_finish.Year, m_finish.Month, m_finish.Day, 23, 59, 59)
      Return Not Data.Subtract(start).TotalSeconds < 0 And Not Data.Subtract(fin).TotalSeconds > 0
   End Function
    'Ecco i costruttori per i range senza un fine

   Public Shared Function CreateOpen(ByVal StartingOn As DateTime) As DataRange
      Return New DataRange(StartingOn, DateTime.MaxValue)
   End Function
   Public Shared Function CreateOpenEnded(ByVal UpTo As DateTime)
      Return New DataRange("01/01/1980", UpTo)
   End Function




    Public Shared Function Empty() As DataRange
      Return New DataRange("01/01/1881", "01/01/1880")
    End Function

    'Ecco delle operazioni per comparare i ranges

    Public Function Overlaps(ByVal DataRange As DataRange) As Boolean
      Return Includes(DataRange.Start) Or Includes(DataRange.Finish) Or Me.Includes(DataRange) Or DataRange.Includes(Me)
    End Function

    Public Function Includes(ByVal DataRange As DataRange) As Boolean
        Return Me.Includes(DataRange.Start) And Me.Includes(DataRange.Finish)
    End Function


    'Per la maggior parte delle applicazione queste funzionalità possono già bastare ma comunque può essere
    'utile scoprire quale gap esiste tra due ranges

    Public Function Gap(ByVal DataRange As DataRange) As DataRange
        If Me.Overlaps(DataRange) Then Return BASEREUSE.DataRange.Empty
        Dim higher As DataRange
        Dim lower As DataRange
        If Me.CompareTo(DataRange) < 0 Then
            lower = Me
            higher = DataRange
        Else
            lower = DataRange
            higher = Me
        End If
        Return New DataRange(lower.m_finish.AddDays(1), higher.Start.AddDays(-1))
    End Function


    Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
        Dim nuovo As DataRange = DirectCast(obj, DataRange)
        'vedo quale range inizia prima
        If (Not m_start.Equals(nuovo.Start)) Then Return m_start.CompareTo(nuovo.Start)
        'oppure quale finisce prima se gli inizi sono uguali
        Return m_finish.CompareTo(nuovo.Finish)
    End Function
    'Un altro metodo per verificare se due range sono contigui
    Public Function Bounds(ByVal DataRange As DataRange) As Boolean
        Return Not Me.Overlaps(DataRange) And Me.Gap(DataRange).IsEmpty
    End Function


   Public Sub Merge(ByVal DataRange As DataRange)
      Dim min As DateTime = IIf(m_start >= DataRange.Start, DataRange.Start, m_start)
      Dim max As DateTime = IIf(m_finish < DataRange.Finish, DataRange.Finish, m_finish)
      m_start = min
      m_finish = max
   End Sub

   'Public Sub MergeAndFill(ByVal DataRange As DataRange)
   '   If Not Me.Overlaps(DataRange) And Not Me.Gap(DataRange).IsEmpty Then

   '      Dim higher As DataRange
   '      Dim lower As DataRange
   '      If Me.CompareTo(DataRange) < 0 Then
   '         lower = Me
   '         higher = DataRange
   '         m_finish = DataRange.Finish
   '      Else
   '         lower = DataRange
   '         higher = Me
   '         m_start = DataRange.Start
   '      End If
   '   Else
   '      Throw New Exception("Non è possibile eseguire il MergeAndFill di due datarange che si sovrappongono")
   '   End If


   'End Sub
End Class
