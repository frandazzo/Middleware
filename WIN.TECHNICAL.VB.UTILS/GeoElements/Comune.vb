Public Class Comune
    Inherits AbstractPersistenceObject
    Implements IComparable


    Protected m_IdProvincia As Int32
    Protected m_IdRegione As Int32
    Protected m_CAP As String = ""
    Protected m_CodiceFiscale As String = ""
    Protected m_codiceIstat As String = ""
    Public Sub New(ByVal K As Key, ByVal Descr As String, ByVal IdProv As Int32, ByVal IdReg As Int32, _
                   ByVal Cap As String, ByVal CodFisc As String, ByVal CodIst As String)
        MyBase.Key = K
        MyBase.Descrizione = Descr
        m_IdProvincia = IdProv
        m_IdRegione = IdReg
        m_CAP = Cap
        m_CodiceFiscale = CodFisc
        m_codiceIstat = CodIst
    End Sub
    Property IdProvincia() As Int32
        Get
            Return m_IdProvincia
        End Get
        Set(value As Int32)
            m_IdProvincia = value
        End Set
    End Property
    Property IdRegione() As Int32
        Get
            Return m_IdRegione
        End Get
        Set(value As Int32)
            m_IdRegione = value
        End Set
    End Property
    Property CAP() As String
        Get
            Return m_CAP
        End Get
        Set(value As String)
            m_CAP = value
        End Set
    End Property
    Property CodiceFiscale() As String
        Get
            Return m_CodiceFiscale
        End Get
        Set(value As String)
            m_CodiceFiscale = value
        End Set
    End Property
    Property CodiceIstat() As String
        Get
            Return m_codiceIstat
        End Get
        Set(value As String)
            m_codiceIstat = value
        End Set
    End Property
    Public Sub New()

    End Sub

    Public Function CompareTo(obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim c As Comune = DirectCast(obj, Comune)
        If (c IsNot Nothing) Then
            Return String.Compare(Descrizione, c.Descrizione)
        Else
            Return String.Compare(Descrizione, "")
        End If
    End Function
End Class
