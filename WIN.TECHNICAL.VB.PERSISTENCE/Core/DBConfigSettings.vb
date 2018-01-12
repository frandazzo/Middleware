Imports System.IO
Imports System.Reflection

Public Class DBConfigSettings

    'Private Shared m_Instance As DBConfigSettings
    'Private Shared m_ActiveServiceAssemblyName As String
    'Private Shared m_ActiveServiceName As String
    'Private Shared m_MaxCacheSize As String = "3" ' 3 è il valore di default per la cache
    'Private Shared m_PathFile As String
    'Private Shared m_ConnectionString As String = ""
    'Private Shared m_MapperFinderName As String = ""
    'Private Shared m_ServiceName As String = ""
    'Private Shared m_ConfigurationFile As String = ""


    Private m_ActiveServiceAssemblyName1 As String
    Private m_ActiveServiceName1 As String
    Private m_MaxCacheSize1 As String = "3" ' 3 è il valore di default per la cache
    Private m_PathFile1 As String
    Private m_ConnectionString1 As String = ""
    Private m_MapperFinderName1 As String = ""
    Private m_ServiceName1 As String = ""
    Private m_ConfigurationFile1 As String = ""

    Private Const default_password As String = "Password=RegUsr"


    Private Function GetExecutingAssemplyPath() As String
        Dim path As String = Assembly.GetExecutingAssembly.CodeBase.Replace("file:///", "")
        Dim FileInfo As New FileInfo(path)
        Return FileInfo.DirectoryName
    End Function


    Private Function GetInstanceFilePath() As String

       
        Dim AppPath As String = GetExecutingAssemplyPath()
        m_PathFile1 = AppPath & "\" & "DB.Config"

        Return m_PathFile1

    End Function

    Public Sub New(ByVal ConfigurationFile As String)

        If String.IsNullOrEmpty(ConfigurationFile) Then
            GetInstanceFilePath()
        Else
            m_PathFile1 = ConfigurationFile
        End If

        InitializeInstance()

    End Sub

    Private Sub InitializeInstance()
        If System.IO.File.Exists(m_PathFile1) Then
            InitializeInstanceFromFile(m_PathFile1)
        Else
            Throw New Exception("File di configurazione per l'accesso alla base dati non corretto!")
        End If
    End Sub


    Private Sub InitializeInstanceFromFile(ByVal PathFile As String)
        m_ActiveServiceAssemblyName1 = SearchServiceInstanceFromFile("PersistenceService")
        m_ActiveServiceName1 = SearchServiceInstanceFromFile("PersistenceFacadeService")
        m_MaxCacheSize1 = SearchServiceInstanceFromFile("MaxCacheSize")
        m_ServiceName1 = SearchServiceInstanceFromFile("ServiceName")
        If m_ServiceName1 = "MsAccess" Then
            Dim ss As String = SearchServiceInstanceFromFile("ConnectionString")
            If ss.ToLower.Contains("password") Then
                m_ConnectionString1 = String.Format("{0} Jet OLEDB:Database", SearchServiceInstanceFromFile("ConnectionString"))
            Else
                m_ConnectionString1 = String.Format("{0} Jet OLEDB:Database {1}", SearchServiceInstanceFromFile("ConnectionString"), default_password)
            End If

            'recuperata la stringa di connessione
            'eseguo una replace sulla possibile directory del file access
            'Per convenzione nella stringa di connessione per access dal file db.config
            'posso avere un segnaposto cosi: |DirPath|/Access.mdb
            'se esiste un simile path e non un percorso reale la replace andrà a buon fine;
            Dim s As String = GetExecutingAssemplyPath()
            m_ConnectionString1 = m_ConnectionString1.Replace("|DirPath|", s)

        Else
            Dim ss As String = SearchServiceInstanceFromFile("ConnectionString")
            If ss.ToLower.Contains("password") Then
                m_ConnectionString1 = ss
            Else
                m_ConnectionString1 = String.Format("{0} {1}", ss, default_password)
            End If

        End If
    End Sub

    Private Function SearchServiceInstanceFromFile(ByVal ServiceName As String) As String
        Dim s As String
        If ServiceName = "PersistenceService" Then
            s = GetType(DBConfigSettings).Assembly.CodeBase.Replace("file:///", "")
            Dim info As FileInfo = My.Computer.FileSystem.GetFileInfo(s)
            s = info.DirectoryName
        Else
            s = ""
        End If

        Dim stream As System.IO.StreamReader = New System.IO.StreamReader(m_PathFile1)
        Try
            Do Until stream.EndOfStream
                If stream.ReadLine = ServiceName Then
                    If ServiceName = "PersistenceService" Then
                        s = s & "\" & stream.ReadLine
                    Else
                        s = stream.ReadLine
                    End If
                    Exit Do
                End If
            Loop
        Catch ex As Exception
            Throw
        Finally
            stream.Close()
        End Try

        Return s
    End Function

    Public Sub New()
        GetInstanceFilePath()
        InitializeInstance()
    End Sub

    'Public Shared Function Instance(ByVal configuratonFile As String) As DBConfigSettings
    '    If m_Instance Is Nothing Then
    '        m_Instance = New DBConfigSettings(configuratonFile)
    '    End If
    '    Return m_Instance
    'End Function

    'Public Shared Function Instance() As DBConfigSettings
    '    If m_Instance Is Nothing Then
    '        m_Instance = New DBConfigSettings
    '    End If
    '    Return m_Instance
    'End Function




    'Private Shared Function GetFilePath() As String

    '    Dim path As String = Assembly.GetExecutingAssembly.CodeBase.Replace("file:///", "")
    '    Dim FileInfo As New FileInfo(path)
    '    Dim AppPath As String = FileInfo.DirectoryName


    '    m_PathFile = AppPath & "\" & "DB.Config"
    '    Return m_PathFile
    'End Function
    'Private Shared Sub Initialize()
    '    Try

    '        Dim pathFile As String

    '        If String.IsNullOrEmpty(m_ConfigurationFile) Then
    '            pathFile = GetFilePath()
    '        Else
    '            pathFile = m_ConfigurationFile
    '        End If

    '        If System.IO.File.Exists(pathFile) Then
    '            InitializeFromFile(pathFile)
    '        Else
    '            InitializeWithDefaultValues()
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception("Impossibile inizializzare i servizi di accesso alla base dati. " & vbCrLf & ex.Message)
    '    End Try
    'End Sub

    'Private Shared Sub InitializeWithDefaultValues()

    '    m_ActiveServiceAssemblyName = "WIN.PERSISTENCE.ACCESS_SERVICES.dll"
    '    m_ActiveServiceName = "WIN.PERSISTENCE.ACCESS_SERVICES.PersistenceFacade"
    '    m_MaxCacheSize = "0"
    '    m_ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\GeSin.mdb "
    '    m_ServiceName = "MsAccess"

    'End Sub
    'Private Shared Sub InitializeFromFile(ByVal PathFile As String)
    '    m_ActiveServiceAssemblyName = SearchServiceFromFile("PersistenceService")
    '    m_ActiveServiceName = SearchServiceFromFile("PersistenceFacadeService")
    '    m_MaxCacheSize = SearchServiceFromFile("MaxCacheSize")
    '    m_ConnectionString = SearchServiceFromFile("ConnectionString") & " Password=RegUsr"
    '    m_ServiceName = SearchServiceFromFile("ServiceName")
    'End Sub

    'Private Shared Function SearchServiceFromFile(ByVal ServiceName As String) As String
    '    Dim s As String
    '    If ServiceName = "PersistenceService" Then
    '        s = GetType(DBConfigSettings).Assembly.CodeBase.Replace("file:///", "")
    '        Dim info As FileInfo = My.Computer.FileSystem.GetFileInfo(s)
    '        s = info.DirectoryName
    '    Else
    '        s = ""
    '    End If

    '    Dim stream As System.IO.StreamReader = New System.IO.StreamReader(m_PathFile)
    '    Do Until stream.EndOfStream
    '        If stream.ReadLine = ServiceName Then
    '            If ServiceName = "PersistenceService" Then
    '                s = s & "\" & stream.ReadLine
    '            Else
    '                s = stream.ReadLine
    '            End If
    '            Exit Do
    '        End If
    '    Loop
    '    stream.Close()
    '    Return s
    'End Function
    Public ReadOnly Property ActiveServiceAssemblyName() As String
        Get
            Return m_ActiveServiceAssemblyName1
        End Get
    End Property
    Public ReadOnly Property ActiveServiceName() As String
        Get
            Return m_ActiveServiceName1
        End Get
    End Property
    Public ReadOnly Property MaxCacheSize() As Int32
        Get
            Return m_MaxCacheSize1
        End Get
    End Property
    Public ReadOnly Property ConnectionString() As String
        Get
            Return m_ConnectionString1
        End Get
    End Property
    Public ReadOnly Property MapperFinderName() As String
        Get
            Return m_MapperFinderName1
        End Get
    End Property


    Public ReadOnly Property ServiceName() As String
        Get
            Return m_ServiceName1
        End Get
    End Property
End Class
