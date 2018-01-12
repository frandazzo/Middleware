Imports System.IO

Public Class ExportOption
    Private m_PathModello As String = ""
    Private m_NomeModello As String = ""
    Private m_NomeCompletoModello As String = ""



    Private m_wordType As WordTypeExt = WordTypeExt.Doc

    Public Property WordType() As WordTypeExt
        Get
            Return m_wordType
        End Get
        Set(ByVal value As WordTypeExt)
            m_wordType = value
        End Set
    End Property


    Public Enum WordTypeExt
        Doc
        Docx
    End Enum



    Friend ReadOnly Property NomeFileDaSalvare() As String
        Get


            Dim ext As String

            If m_wordType = WordTypeExt.Doc Then
                ext = ".doc"
            Else
                ext = ".docx"
            End If

            If m_NomeModello.ToLower.EndsWith("dotx") Then

                Return m_NomeModello.Substring(0, m_NomeModello.Length - 5) & ext

            End If

            Return m_NomeModello.Substring(0, m_NomeModello.Length - 4) & ext

        End Get

    End Property




    Private Sub New()

    End Sub


    Private Sub CheckModelFullName(ByVal nomeCompletModello As String)
        If String.IsNullOrEmpty(nomeCompletModello) Then
            Throw New ArgumentNullException("Nome completo modello")
        End If

        If Not (nomeCompletModello.EndsWith(".dot") Or nomeCompletModello.EndsWith(".dotx")) Then
            Throw New ArgumentException("Nome modello non corrispondente ad un modello Word!")
        End If


        If Not (My.Computer.FileSystem.FileExists(nomeCompletModello)) Then
            Throw New ArgumentException("Modello non esistente")
        End If
    End Sub



    Public Sub New(ByVal nomeCompletModello As String, ByVal pathSalvataggio As String)

        CheckSavePath(pathSalvataggio)

        CheckModelFullName(nomeCompletModello)




        m_PathSalvataggio = pathSalvataggio
        m_NomeCompletoModello = nomeCompletModello



        Dim f As New FileInfo(m_NomeCompletoModello)


        m_NomeModello = f.Name
        m_PathModello = f.DirectoryName

    End Sub



    Private Sub CheckSavePath(ByVal pathSalvataggio As String)
        If String.IsNullOrEmpty(pathSalvataggio) Then
            Throw New ArgumentNullException("Percorso di salvataggio")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(pathSalvataggio) Then
            Throw New ArgumentException("Percorso di salvataggio non valido!")
        End If
    End Sub


    Public Sub New(ByVal nomeModello As String, ByVal percorsoModello As String, ByVal pathSalvataggio As String)


        CheckSavePath(pathSalvataggio)



        m_NomeModello = nomeModello
        m_PathModello = percorsoModello


        CheckModelFullName(Me.NomeCompletoModello)



    End Sub


    Public ReadOnly Property NomeCompletoModello() As String
        Get
            If String.IsNullOrEmpty(m_NomeCompletoModello) Then

                If m_PathModello.EndsWith("\") Then
                    m_NomeCompletoModello = m_PathModello + m_NomeModello
                Else
                    m_NomeCompletoModello = m_PathModello + "\" + m_NomeModello
                End If
            End If

            Return m_NomeCompletoModello
        End Get
    End Property


    Private m_Ripetizioni As Int32 = 1

    Public Property Ripetizioni() As Int32
        Get
            Return m_Ripetizioni
        End Get
        Set(ByVal value As Int32)
            If value <= 0 Then
                m_Ripetizioni = 1
                Return
            End If
            m_Ripetizioni = value
        End Set
    End Property


    Private m_OffsetPartenza As Int32 = 1

    Public Property OffsetPartenza() As Int32
        Get
            Return m_OffsetPartenza
        End Get
        Set(ByVal value As Int32)
            If value <= 0 Then
                m_OffsetPartenza = 1
                Return
            End If
            m_OffsetPartenza = value
        End Set
    End Property


    Friend ReadOnly Property NomeCompletoFileDaSalvare() As String
        Get

            If m_PathSalvataggio.EndsWith("\") Then

                Return m_PathSalvataggio + Me.NomeFileDaSalvare
            Else
                Return m_PathSalvataggio + "\" + Me.NomeFileDaSalvare
            End If



        End Get
    End Property




    Private m_PathSalvataggio As String = ""


    Public ReadOnly Property PathSalvataggio() As String
        Get
            Return m_PathSalvataggio
        End Get
    End Property




    Private m_TagRicercaOccorrenze As String = "@Nome_Completo@"


    Public Property TagRicercaOccorrenze() As String
        Get
            Return m_TagRicercaOccorrenze
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentNullException("Tag di ricerca occorrenze")
            End If
            m_TagRicercaOccorrenze = value
        End Set
    End Property


    Private m_Metadata As IList(Of Hashtable) = New List(Of Hashtable)


    Public Property Metadata() As IList(Of Hashtable)
        Get
            Return m_Metadata
        End Get
        Set(ByVal value As IList(Of Hashtable))
            If value Is Nothing Then
                Throw New ArgumentNullException("Metadati")
            End If
            m_Metadata = value
        End Set
    End Property

End Class
