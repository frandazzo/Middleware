''' <summary>
''' Classe singleton per l'accesso ai servizi di persistenza. 
''' Legge da un file di configurazione come inizializzarsi e a quale tipo di DB puntare. 
''' </summary>
''' <remarks></remarks>
Public Class DataAccessServices
   Private Shared m_Instance As DataAccessServices
    'Private Shared ActiveServiceAssembly As System.Reflection.Assembly
    'Private Shared ActiveService As IPersistenceFacade
    ' Private Shared m_ConfigurationFile As String = ""


    Private ActiveServiceAssembly1 As System.Reflection.Assembly
    Private ActiveService1 As IPersistenceFacade
    Private m_configurationSettings As DBConfigSettings


    Private Sub New()
        m_configurationSettings = New DBConfigSettings()
        Dim par() As Object = {m_configurationSettings.ServiceName, m_configurationSettings.ConnectionString, m_configurationSettings.MaxCacheSize}
        ActiveServiceAssembly1 = System.Reflection.Assembly.LoadFrom(m_configurationSettings.ActiveServiceAssemblyName)
        ActiveService1 = ActiveServiceAssembly1.CreateInstance(m_configurationSettings.ActiveServiceName, False, Reflection.BindingFlags.CreateInstance, Nothing, par, New System.Globalization.CultureInfo("it-IT"), Nothing)
    End Sub


    Private Sub New(ByVal ConfigurationFile As String)
        m_configurationSettings = New DBConfigSettings(ConfigurationFile)
        Dim par() As Object = {m_configurationSettings.ServiceName, m_configurationSettings.ConnectionString, m_configurationSettings.MaxCacheSize}
        ActiveServiceAssembly1 = System.Reflection.Assembly.LoadFrom(m_configurationSettings.ActiveServiceAssemblyName)
        ActiveService1 = ActiveServiceAssembly1.CreateInstance(m_configurationSettings.ActiveServiceName, False, Reflection.BindingFlags.CreateInstance, Nothing, par, New System.Globalization.CultureInfo("it-IT"), Nothing)
    End Sub



    Public Shared Function Instance(ByVal ConfigurationFile As String) As DataAccessServices
        If m_Instance Is Nothing Then
            m_Instance = New DataAccessServices(ConfigurationFile)
        End If
        Return m_Instance
    End Function


    Public Shared Function Instance() As DataAccessServices
        If m_Instance Is Nothing Then
            m_Instance = New DataAccessServices
        End If
        Return m_Instance
    End Function

    'Private Shared Sub OpenDBSettingsInstance(ByVal ConfigurationFile As String)
    '    If String.IsNullOrEmpty(ConfigurationFile) Then
    '        'avvio l'istanza normalmente
    '        DBConfigSettings.Instance()
    '    Else
    '        'la avvio con il file di configurazione designato
    '        DBConfigSettings.Instance(ConfigurationFile)
    '    End If
    'End Sub
    '''' <summary>
    '''' Metodo che inizializza i servizi ella base dati. Esso carica l'assembly specificato
    '''' nel file di configurazione, ne carica la classe di accesso e la classe che mappa gli oggetti di dominio
    '''' ai relativi servizi concreti di persistenza.
    '''' </summary>
    '''' <remarks></remarks>
    'Private Shared Sub Initialize()
    '    Try
    '        OpenDBSettingsInstance(m_ConfigurationFile)

    '        Dim par() As Object = {DBConfigSettings.Instance.ServiceName, DBConfigSettings.Instance.ConnectionString, DBConfigSettings.Instance.MaxCacheSize}
    '        ActiveServiceAssembly = System.Reflection.Assembly.LoadFrom(DBConfigSettings.Instance.ActiveServiceAssemblyName)
    '        ActiveService = ActiveServiceAssembly.CreateInstance(DBConfigSettings.Instance.ActiveServiceName, False, Reflection.BindingFlags.CreateInstance, Nothing, par, New System.Globalization.CultureInfo("it-IT"), Nothing)
    '        'ActiveService.ServiceName = DBConfigSettings.Instance.ServiceName

    '        'MapperFinder = ActiveServiceAssembly.CreateInstance(DBConfigSettings.Instance.MapperFinderName)
    '        If ActiveService Is Nothing Then Throw New Exception("Impossibile trovare la classe dei servizi di persistenza. Controllare il nome del servizio attivo nel file DB.Config.")
    '        'If MapperFinder Is Nothing Then Throw New Exception("Impossibile trovare la classe di mappatura dei servizi di persistenza che implementa l'interfaccia IMapperFinder. Controllare il nome del MapperFinder nel file DB.Config.")
    '        'ActiveService.SetMapperFinder(MapperFinder)
    '    Catch ex As Exception
    '        Throw New Exception("Impossibile inizializzare i servizi di accesso alla base dati. " & vbCrLf & ex.Message)
    '    End Try
    'End Sub


    Public Shared Function SimplePersistenceFacadeInstance(ByVal ConfigurationFile As String) As IPersistenceFacade
        Return New DataAccessServices(ConfigurationFile).PersistenceFacade
    End Function

    Public Shared Function SimplePersistenceFacadeInstance() As IPersistenceFacade
        Return New DataAccessServices().PersistenceFacade
    End Function


    Public ReadOnly Property PersistenceFacade() As IPersistenceFacade
        Get
            Return ActiveService1
        End Get
    End Property


    Public ReadOnly Property CustomPersistentServiceAssembly() As String
        Get
            Return m_configurationSettings.ActiveServiceAssemblyName
        End Get
    End Property

    Public ReadOnly Property MaxCacheSize() As String
        Get
            Return m_configurationSettings.MaxCacheSize
        End Get
    End Property

    Public ReadOnly Property ServiceName() As String
        Get
            Return m_configurationSettings.ServiceName
        End Get
    End Property


    Public ReadOnly Property ConnectionString() As String
        Get
            Return m_configurationSettings.ConnectionString
        End Get
    End Property


End Class
