''' <summary>
''' Classe che contiene tutte le informazioni di base per un'entità giuridica
''' </summary>
''' <remarks></remarks>
Public Class AbstractAzienda
   Inherits AbstractPersistenceObject
   '''' <summary>
   '''' Metodo che verifica l'uguaglianza in base alla chiave identificativa dell'oggetto
   '''' </summary>
   '''' <param name="obj"></param>
   '''' <returns></returns>
   '''' <remarks></remarks>
   'Public Overrides Function Equals(ByVal obj As Object) As Boolean
   '   Dim azienda As AbstractAzienda
   '   Try
   '      azienda = DirectCast(obj, AbstractAzienda)
   '   Catch ex As Exception
   '      Return False
   '   End Try
   '   If MyBase.Key Is Nothing Then Return False
   '   Return MyBase.Key.EqualsTo(azienda.Key)
   'End Function
   Protected m_IndirizzoSedeLegale As Indirizzo = New Indirizzo
   Protected m_Responsable As String = ""
   Protected m_Comunicazione As Comunicazioni = New Comunicazioni
   Protected m_Note As String = ""


   Public Property Note() As String
      Get
         Return m_Note
      End Get
      Set(ByVal value As String)
         m_Note = value
      End Set
   End Property


   Public Property Responsable() As String
      Get
         Return m_Responsable
      End Get
      Set(ByVal value As String)
         m_Responsable = value
      End Set
   End Property

   Public Property Comunicazione() As Comunicazioni
      Get
         If m_Comunicazione Is Nothing Then
            m_Comunicazione = New Comunicazioni
         End If
         Return m_Comunicazione
      End Get
      Set(ByVal value As Comunicazioni)
         m_Comunicazione = value
      End Set
   End Property


   Public Property IndirizzoSedeLegale() As Indirizzo
      Get
         If m_IndirizzoSedeLegale Is Nothing Then
            m_IndirizzoSedeLegale = New Indirizzo
         End If
         Return m_IndirizzoSedeLegale
      End Get
      Set(ByVal value As Indirizzo)
         'If value Is Nothing Then Throw New NullReferenceException("Impossibile impostare un indirizzo legale nullo")
         m_IndirizzoSedeLegale = value
      End Set
   End Property
   Public Sub New()

   End Sub
   Protected Overrides Sub DoValidation()
      If MyBase.Descrizione = "" Then
         ValidationErrors.Add("Impossibile registrare i dati della segreteria con una ragione sociale nulla")
      End If
   End Sub
End Class
