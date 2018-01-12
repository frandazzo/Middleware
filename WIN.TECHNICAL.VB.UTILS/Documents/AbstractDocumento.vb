Public Class AbstractDocumento
   Inherits AbstractPersistenceObject
   Implements IDocumento

   Protected m_dataRegistrazione As DateTime = Date.MinValue
   Protected m_dataDocumento As DateTime = Date.MinValue
   Protected m_note As String = ""
   Protected m_TipoDocumento As String = ""
   Protected m_Path_Documento As String


   'Public Property Path_Documento() As String
   '   Get
   '      Return m_Path_Documento
   '   End Get
   '   Set(ByVal value As String)
   '      m_Path_Documento = value
   '   End Set
   'End Property

   Public Sub New()

   End Sub
   Public Sub New(ByVal DataDocumento As DateTime, ByVal DataRegistrazione As DateTime, ByVal Note As String)
      'If DataDocumento > DataRegistrazione Then Throw New Exception("Impossibile creare un documento dova la data di registrazione è precedente la data del documento")
      m_dataDocumento = DataDocumento
      m_dataRegistrazione = DataRegistrazione
      m_note = Note
   End Sub
   ''' <summary>
   ''' Data registrazione del documento
   ''' </summary>
   ''' <value></value>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Overridable Property DataRegistrazione() As DateTime Implements IDocumento.DataRegistrazione
      Get
         Return m_dataRegistrazione
      End Get
      Set(ByVal value As DateTime)
         m_dataRegistrazione = value
      End Set
   End Property
   ''' <summary>
   ''' Data effettiva del documento
   ''' </summary>
   ''' <value></value>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Overridable Property DataDocumento() As DateTime Implements IDocumento.DataDocumento
      Get
         Return m_dataDocumento
      End Get
      Set(ByVal value As DateTime)
         m_dataDocumento = value
      End Set
   End Property
   ''' <summary>
   ''' Note del documento
   ''' </summary>
   ''' <value></value>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Overridable Property Note() As String Implements IDocumento.Note
      Get
         Return m_note
      End Get
      Set(ByVal value As String)
         m_note = value
      End Set
   End Property
   ''' <summary>
   ''' Restituisce il tipo di ogni documento. Variabile inizializzata da ogni costruttore concreto
   ''' </summary>
   ''' <value></value>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public ReadOnly Property TipoDocumento() As String Implements IDocumento.TipoDocumento
      Get
         Return m_TipoDocumento
      End Get
   End Property
   Protected Overrides Sub DoValidation()
        'If DataDocumento > DataRegistrazione Then
        '   ValidationErrors.Add("Impossibile creare un documento con data di registrazione è precedente la data del documento")
        'End If
      If m_TipoDocumento = "" Then
         ValidationErrors.Add("Impossibile creare un documento con tipo documento non specificato")
      End If
   End Sub
   ''' <summary>
   ''' Metodo per la registrazione di un documento
   ''' </summary>
   ''' <remarks></remarks>
   Public Overridable Sub Post()
      Try
         If Not IsValid() Then Throw New Exception("Documento non valido")
      Catch ex As Exception
         Throw New Exception("Impossibilire registrare il documento." & vbCrLf & ex.Message)
      End Try
   End Sub
   ''' <summary>
   ''' Metodo che restituisce l'id del documento specifico. Il metodo solleva un'eccezione se 
   ''' la chiave identificativa del documento è composta
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function GetId() As Int32 Implements IDocumento.GetId
      Return MyBase.Id
   End Function

   Public Overrides Function ToString() As String Implements IDocumento.ToString
      Return "AA"
   End Function
End Class
