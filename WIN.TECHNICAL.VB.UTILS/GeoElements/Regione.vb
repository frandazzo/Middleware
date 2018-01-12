Imports System.Xml.Serialization

Public Class Regione
    Inherits AbstractPersistenceObject
    Implements IComparable

    Protected m_Provincie As IList = New ArrayList
    Protected m_Comuni As IList = New ArrayList
    <XmlIgnore> _
    Public Property ListaProvincie() As IList
        Get
            Return m_Provincie
        End Get
        Set(ByVal value As IList)
            m_Provincie = value
        End Set
    End Property

    Friend Sub AddProvincia(ByVal Provincia As Provincia)
        If Provincia.IdRegione = Me.Id And Not m_Provincie.Contains(Provincia) Then
            m_Provincie.Add(Provincia)
        End If
    End Sub
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
        If Comune.IdRegione = Me.Id And Not m_Comuni.Contains(Comune) Then
            m_Comuni.Add(Comune)
            GetProvinciaById(Comune.IdProvincia).AddComune(Comune)
        End If
    End Sub
    Public Function GetProvinciaById(ByVal IdProvincia As Int32) As Provincia
        For Each elem As Provincia In m_Provincie
            If elem.Id = IdProvincia Then Return elem
        Next
        Throw New Exception("Impossibile ottenere la provincia richiesta. Provincia non appartenete alla Regione")
    End Function
    Public Function GetListaNomiComuni() As IList
        Dim lista As New ArrayList
        For Each elem As Comune In m_Comuni
            lista.Add(elem.Descrizione)
        Next
        Return lista
    End Function
    Public Function GetListaNomiProvincie() As IList
        Dim lista As New ArrayList
        For Each elem As Provincia In m_Provincie
            lista.Add(elem.Descrizione)
        Next
        Return lista
    End Function

    Public Function GetListaSigleProvincie() As IList
        Dim lista As New ArrayList
        For Each elem As Provincia In m_Provincie
            lista.Add(elem.Sigla)
        Next
        Return lista
    End Function
    Public Sub New(ByVal K As Key, ByVal Descr As String)
        MyBase.Key = K
        MyBase.Descrizione = Descr

    End Sub
    Public Sub New()

    End Sub

    Public Function CompareTo(obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim c As Regione = DirectCast(obj, Regione)
        If (c IsNot Nothing) Then
            Return String.Compare(Descrizione, c.Descrizione)
        Else
            Return String.Compare(Descrizione, "")
        End If
    End Function
End Class
