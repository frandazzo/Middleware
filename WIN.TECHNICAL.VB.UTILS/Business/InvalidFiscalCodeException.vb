Public Class InvalidFiscalCodeException
   Inherits Exception


   Private m_ragione As String = ""
   Public Property Ragione() As String
      Get
         Return m_ragione
      End Get
      Set(ByVal value As String)
         m_ragione = value
      End Set
   End Property


   Public Sub New(ByVal ragione As String)
      MyBase.New()
      m_ragione = ragione
   End Sub

   Public Sub New()
      '
   End Sub

   Public Overrides ReadOnly Property Message() As String
      Get
         Return "Codice fiscale inserito non corretto. " & m_ragione
      End Get
   End Property
End Class
