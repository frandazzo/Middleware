Public Class Nazione
    Inherits AbstractPersistenceObject
    Implements IComparable

    Protected m_IdRazza As Int32
    Protected m_CodiceFiscale As String
    Protected m_codiceISS As String
    Public Sub New(ByVal K As Key, ByVal Descr As String, ByVal IdRazza As Int32, _
                    ByVal CodFisc As String, ByVal CodIss As String)
        MyBase.Key = K
        MyBase.Descrizione = Descr
        m_IdRazza = IdRazza
        m_CodiceFiscale = CodFisc
        m_codiceISS = CodIss
    End Sub
    Public Sub New()

    End Sub
    ReadOnly Property IdRazza() As Int32
        Get
            Return m_IdRazza
        End Get
    End Property

    ReadOnly Property CodiceFiscale() As String
        Get
            Return m_CodiceFiscale
        End Get
    End Property
    ReadOnly Property CodiceIss() As String
        Get
            Return m_codiceISS
        End Get
    End Property

    Public Function CompareTo(obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim c As Nazione = DirectCast(obj, Nazione)
        If (c IsNot Nothing) Then
            Return String.Compare(Descrizione, c.Descrizione)
        Else
            Return String.Compare(Descrizione, "")
        End If
    End Function
End Class

