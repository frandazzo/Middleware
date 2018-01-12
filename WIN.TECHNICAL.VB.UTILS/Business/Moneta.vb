Public Class Moneta
    Implements IComparable
    Private m_Importo As Double
    Private m_Valuta As Valuta
   Public Overrides Function ToString() As String
      If Me.Importo >= 0 Then
         Return Me.Importo & " Euro"
      Else
         Return Me.Negate.Importo & " Euro"
      End If
   End Function

    Public Enum Valuta
        Euro = 0
        Dollaro = 1
    End Enum
   Public Shared Function GetTipoValuta(ByVal Tipo As String) As Moneta.Valuta
      If Tipo = "Euro" Then
         Return Moneta.Valuta.Euro
      ElseIf Tipo = "Dollaro" Then
         Return Moneta.Valuta.Dollaro
      Else
         Throw New Exception("Impostare un tipo valuta corretto!")
      End If
   End Function
   Public Shared Function GetTipoValutaToString(ByVal Tipo As Moneta.Valuta) As String
      If Tipo = Moneta.Valuta.Euro Then
         Return "Euro"
      ElseIf Tipo = Moneta.Valuta.Dollaro Then
         Return "Dollaro"
      Else
         Throw New Exception("Tipo valuta sconosciuto!")
      End If
   End Function


    Public Sub New(ByVal Importo As Double, ByVal Valuta As Valuta)
        m_Importo = CDbl(Format(Importo, "###0.00"))
        m_Valuta = Valuta
    End Sub
    Public ReadOnly Property Importo() As Double
        Get
            Return CDbl(Format(m_Importo, "###0.00"))
        End Get
    End Property
    Public ReadOnly Property GetValuta() As Valuta
        Get
            Return m_Valuta
        End Get
    End Property
    Public Sub AssertSameValuta(ByVal arg As Moneta)
      If Not m_Valuta.Equals(arg.GetValuta) Then Throw New Exception("Valuta diversa")
   End Sub
   Public Function Add(ByVal Moneta As Moneta) As Moneta
      AssertSameValuta(Moneta)
      Return New Moneta(m_Importo + Moneta.Importo, m_Valuta)
   End Function
   Public Function Subtract(ByVal Moneta As Moneta) As Moneta
      AssertSameValuta(Moneta)
      Return New Moneta(m_Importo - Moneta.Importo, m_Valuta)
   End Function
    Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
        Return Compareto(DirectCast(obj, Moneta))
    End Function

   Public Shared Function GetPercOf(ByVal Importo As Moneta, ByVal Perc As Double) As Moneta
      If Not IsNumeric(Perc) Then Throw New Exception("Impossibile calcolare la percentuale di un importo. Tipo percentuale non numerico.")
      If Not IsPerCent(Perc) Then Throw New Exception("Impossibile calcolare la percentuale di un importo. Tipo percentuale non compreso tra 0 e 100.")
      If Importo Is Nothing Then Throw New Exception("Impossibile calcolare la percentuale di un importo. Importo nullo.")
      Dim Val As Double = CDbl(Format(Importo.Importo / 100 * Perc, "##0.00"))
      Return New Moneta(Val, Importo.GetValuta)
   End Function

   Private Shared Function IsPerCent(ByVal Perc As Double) As Boolean
      If Perc < 0 Or Perc > 100 Then Return False
      Return True
   End Function

    Public Function CompareTo(ByVal Moneta As Moneta) As Integer
        AssertSameValuta(Moneta)
        If Me.Importo < Moneta.Importo Then
            Return (-1)
        ElseIf Me.Importo = Moneta.Importo Then
            Return 0
        Else
            Return 1
        End If
    End Function
   Public Overloads Function Equals(ByVal obj As Object) As Boolean
      Try
         Dim mon As Moneta = DirectCast(obj, Moneta)
         If m_Importo = mon.Importo And m_Valuta = mon.GetValuta Then
            Return True
         Else
            Return False
         End If
      Catch ex As Exception
         Throw New Exception(ex.Message)
      End Try
   End Function
   Public Function IsGreaterThan(ByVal Moneta As Moneta) As Boolean
      AssertSameValuta(Moneta)
      Return Me.CompareTo(Moneta) > 0
   End Function
   Public Function IsSmallerThan(ByVal Moneta As Moneta) As Boolean
      AssertSameValuta(Moneta)
      Return Me.CompareTo(Moneta) < 0
   End Function
   Public Function IsGreaterEqualThan(ByVal Moneta As Moneta) As Boolean
      AssertSameValuta(Moneta)
      Return Me.CompareTo(Moneta) >= 0
   End Function
   Public Function IsSmallerEqualThan(ByVal Moneta As Moneta) As Boolean
      AssertSameValuta(Moneta)
      Return Me.CompareTo(Moneta) <= 0
   End Function



   Public Function IsIncludedBetween(ByVal Lower As Moneta, ByVal Upper As Moneta) As Boolean
      If Me.IsSmallerThan(Lower) Or Me.IsGreaterThan(Upper) Then
         Return False
      Else
         Return True
      End If
   End Function
    Public Function Negate() As Moneta
        Return New Moneta(m_Importo * -1, m_Valuta)
    End Function
End Class
