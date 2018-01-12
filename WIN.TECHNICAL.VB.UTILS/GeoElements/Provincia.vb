Imports System.Xml.Serialization

Public Class Provincia
    Inherits AbstractPersistenceObject
    Implements IComparable

    Protected m_IdRegione As Int32
    Protected m_Sigla As String = ""
    Protected m_Comuni As IList = New ArrayList
    <XmlIgnore> _
    Public Property ListaComuni() As IList
        Get
            Return m_Comuni
        End Get
        Set(ByVal value As IList)
            m_Comuni = value
        End Set
    End Property
    Friend Sub AddComune(ByVal Comune As Comune)
        If Comune.IdProvincia = Me.Id And Not m_Comuni.Contains(Comune) Then
            m_Comuni.Add(Comune)
        End If
    End Sub
    Public Function GetListaNomiComuni() As IList
        Dim lista As New ArrayList
        For Each elem As Comune In m_Comuni
            lista.Add(elem.Descrizione)
        Next
        Return lista
    End Function


    Public Sub New(ByVal K As Key, ByVal Descr As String, ByVal IdReg As Int32, _
                   ByVal Sigla As String)
        MyBase.Key = K
        MyBase.Descrizione = Descr
        m_IdRegione = IdReg
        m_Sigla = Sigla
    End Sub

    Property IdRegione() As Int32
        Get
            Return m_IdRegione
        End Get
        Set(value As Int32)
            m_IdRegione = value
        End Set
    End Property
    Property Sigla() As String
        Get
            Return m_Sigla
        End Get
        Set(value As String)
            m_Sigla = value
        End Set
    End Property
    Public Sub New()

    End Sub
    Public Function CompareTo(obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim c As Provincia = DirectCast(obj, Provincia)
        If (c IsNot Nothing) Then
            Return String.Compare(Descrizione, c.Descrizione)
        Else
            Return String.Compare(Descrizione, "")
        End If
    End Function
End Class
