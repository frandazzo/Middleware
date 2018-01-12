Public Class PeriodoNullo
   Inherits PeriodoMensile
   Public Sub New()
      MyBase.New(1, 1800)
      MyBase.m_DataRange = DataRange.Empty
   End Sub
   Public Overrides Function ToString() As String
      Return "Periodo nullo"
   End Function
  
End Class
