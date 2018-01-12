Public Class DatiFiscali

   Public Enum Sesso
      Maschio
      Femmina
   End Enum




   Private m_Sesso As String
   Private m_Comune As Comune
   Private m_data As DateTime
   Private m_Provincia As Provincia


   Private m_Nazione As Nazione
   Public Property Nazione() As Nazione
      Get
         Return m_Nazione
      End Get
      Set(ByVal value As Nazione)
         m_Nazione = value
      End Set
   End Property


   Public ReadOnly Property Provincia() As Provincia
      Get
         Return m_Provincia
      End Get

   End Property




   Public ReadOnly Property DataNascita() As DateTime
      Get
         Return m_data
      End Get
     
   End Property






   Public ReadOnly Property Comune() As Comune
      Get
         Return m_Comune
      End Get
      
   End Property





   Public ReadOnly Property SessoPersona() As String
      Get
         Return m_Sesso
      End Get

   End Property



   Public Sub New(ByVal data As DateTime, ByVal sesso As Sesso, ByVal comune As Comune, ByVal Provincia As Provincia, ByVal Nazione As Nazione)
      m_data = data
      m_Sesso = sesso
      m_Comune = comune
      m_Provincia = Provincia
      m_Nazione = Nazione
   End Sub



End Class
