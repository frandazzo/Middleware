Public Class AbstractBrowserControl





    Protected m_LinkTo As IOpenCommand
    Protected m_ListOfLinkCommands As New System.Collections.Generic.Dictionary(Of String, IOpenCommand)
    Public Sub AddLinkCommand(ByVal Link As String, ByVal LinkCommand As IOpenCommand)
        If Not m_ListOfLinkCommands.ContainsKey(Link) Then
            m_ListOfLinkCommands.Add(Link, LinkCommand)
        End If
    End Sub
    Public Overloads Sub NavigateTo(ByVal Link As String)

        If TypeOf (MyBase.State) Is StView Then
            SetLinkCommand(Link)
            m_LinkTo.Execute()
            HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
        End If

    End Sub
    Public Overloads Sub NavigateTo(ByVal Link As String, ByVal NoMatterState As Boolean)

        If NoMatterState Then
            SetLinkCommand(Link)
            m_LinkTo.Execute()
            HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
        End If

    End Sub
    Public Overridable Sub NavigateWithoutHistoryTo(ByVal Link As String)

        If TypeOf (MyBase.State) Is StView Then
            SetLinkCommand(Link)
            m_LinkTo.Execute()
        End If

    End Sub
    Public Overridable Sub NavigateWithoutHistoryTo(ByVal Link As String, ByVal NoMatterState As Boolean)
        If NoMatterState Then
            SetLinkCommand(Link)
            m_LinkTo.Execute()
        End If
        

    End Sub

    Public Overridable Sub NavigateWithoutHistoryTo(ByVal Link As String, ByVal WitParameter1 As Hashtable)

        If TypeOf (MyBase.State) Is StView Then
            SetLinkCommand(Link)
            m_LinkTo.SetCommandParameters(WitParameter1)
            m_LinkTo.Execute(WitParameter1)
        End If

    End Sub

    Public Overridable Sub AddCommandToHistory(ByVal Link As String, ByVal WitParameter1 As Hashtable)
        SetLinkCommand(Link)
        m_LinkTo.SetCommandParameters(WitParameter1)
        HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
    End Sub

    ''' <summary>
    ''' l'argomento noMatterState specifica l'esecuzione della navigazione indipendentemente dallo stato del controllo
    ''' </summary>
    ''' <param name="Link"></param>
    ''' <param name="WitParameter1"></param>
    ''' <param name="NoMatterState"></param>
    ''' <remarks></remarks>
    Public Overridable Sub NavigateWithoutHistoryTo(ByVal Link As String, ByVal WitParameter1 As Hashtable, ByVal NoMatterState As Boolean)

        If NoMatterState Then
            SetLinkCommand(Link)
            m_LinkTo.SetCommandParameters(WitParameter1)
            m_LinkTo.Execute(WitParameter1)
        End If

    End Sub
    ''' <summary>
    ''' L'argomento noMatterState specifica l'esecuzione della navigazione indipendentemente dallo stato del controllo
    ''' </summary>
    ''' <param name="Link"></param>
    ''' <param name="WitParameter1"></param>
    ''' <param name="NoMatterState"></param>
    ''' <remarks></remarks>
    Public Overloads Sub NavigateTo(ByVal Link As String, ByVal WitParameter1 As Hashtable, ByVal NoMatterState As Boolean)
        If NoMatterState Then
            SetLinkCommand(Link)
            m_LinkTo.SetCommandParameters(WitParameter1)
            m_LinkTo.Execute(WitParameter1)
            HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
        End If


    End Sub
    Public Overloads Sub NavigateTo(ByVal Link As String, ByVal WitParameter1 As Hashtable)

        If TypeOf (MyBase.State) Is StView Then
            SetLinkCommand(Link)
            m_LinkTo.SetCommandParameters(WitParameter1)
            m_LinkTo.Execute(WitParameter1)
            HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
        End If


    End Sub
    Protected Sub SetLinkCommand(ByVal LinkName As String)
        If Me.m_ListOfLinkCommands.ContainsKey(LinkName) Then
            m_LinkTo = Me.m_ListOfLinkCommands.Item(LinkName)
        Else
            Throw New Exception("Impossibile visualizzare la maschera desiderata. Il controllo non possiede il link selezionato. Contattare l'amministratore del sistema per la correzione dell'errore.")
        End If
    End Sub
    Public Overloads Sub NavigateInOtherWindow(ByVal Link As String)

        If TypeOf (MyBase.State) Is StView Then
            SetLinkCommand(Link)
            Dim Form As Windows.Forms.Form = New Windows.Forms.Form
            m_LinkTo.ExecuteInOtherWindow(Form)
            HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
            'Form.Text = 1
            'Form.Show()
        End If

    End Sub
    Public Overloads Sub NavigateInOtherWindow(ByVal Link As String, ByVal WitParameter1 As Hashtable)

        If TypeOf (MyBase.State) Is StView Then
            SetLinkCommand(Link)
            m_LinkTo.SetCommandParameters(WitParameter1)
            m_LinkTo.Execute(WitParameter1)
            HistoryOfCommands.Instance.AddCommandToHistory(m_LinkTo)
        End If

    End Sub



    Protected Overridable Sub Navigate()

    End Sub

End Class
