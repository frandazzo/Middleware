Imports System.Text

Public Class Comunicazioni
    Protected m_TelCasa As String = ""
    Protected m_TelUff As String = ""
    Protected m_Fax As String = ""
    Protected m_Cell1 As String = ""
    Protected m_Cell2 As String = ""
    Protected m_Mail As String = ""

    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder


        Dim cell1 As String = String.Format("Cellulare1: {0}", m_Cell1)
        Dim cell2 As String = String.Format("Cellulare2: {0}", m_Cell2)

        Dim telc As String = String.Format("Telefono casa: {0}", m_TelCasa)
        Dim telu As String = String.Format("Telefono ufficio: {0}", m_TelUff)
        Dim fax As String = String.Format("Fax: {0}", m_Fax)

        Dim mail As String = String.Format("Mail: {0}", m_Mail)

        If Not String.IsNullOrEmpty(m_Cell1) Then
            sb.AppendLine(cell1)
        End If

        If Not String.IsNullOrEmpty(m_Cell2) Then
            sb.AppendLine(cell2)
        End If

        If Not String.IsNullOrEmpty(m_TelCasa) Then
            sb.AppendLine(telc)
        End If

        If Not String.IsNullOrEmpty(m_TelUff) Then
            sb.AppendLine(telu)
        End If

        If Not String.IsNullOrEmpty(m_Fax) Then
            sb.AppendLine(fax)
        End If

        If Not String.IsNullOrEmpty(m_Mail) Then
            sb.AppendLine(mail)
        End If

        Return sb.ToString
    End Function

    Public Property Cellulare1() As String
        Get
            Return m_Cell1
        End Get
        Set(ByVal value As String)
            m_Cell1 = value
        End Set
    End Property
    Public Property Cellulare2() As String
        Get
            Return m_Cell2
        End Get
        Set(ByVal value As String)
            m_Cell2 = value
        End Set
    End Property
    Public Property Mail() As String
        Get
            Return m_Mail
        End Get
        Set(ByVal value As String)
            m_Mail = value
        End Set
    End Property
    Public Property TelefonoCasa() As String
        Get
            Return m_TelCasa
        End Get
        Set(ByVal value As String)
            m_TelCasa = value
        End Set
    End Property
    Public Property TelefonoUfficio() As String
        Get
            Return m_TelUff
        End Get
        Set(ByVal value As String)
            m_TelUff = value
        End Set
    End Property
    Public Property Fax() As String
        Get
            Return m_Fax
        End Get
        Set(ByVal value As String)
            m_Fax = value
        End Set
    End Property

End Class
