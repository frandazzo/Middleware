Public Class AbstractCrudControl


    Protected m_State As AbstractControlState = StView.Instance
    Protected m_IdShowedObject As Int32 = -1
    Protected m_IsLoading As Boolean = False
    Public Event StateChanged(ByVal State As AbstractControlState)
    Protected m_ChangeStateEnabled As Boolean = True


    Public Property StateChangeEnable() As Boolean
        Get
            Return m_ChangeStateEnabled
        End Get
        Set(ByVal value As Boolean)
            m_ChangeStateEnabled = value
        End Set
    End Property

    Public Sub New()

        ' Chiamata richiesta da Progettazione Windows Form.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

    Public ReadOnly Property IsLoading() As Boolean
        Get
            Return m_IsLoading
        End Get
    End Property

    Public Property IdShowedObject() As String
        Get
            Return m_IdShowedObject
        End Get
        Set(ByVal value As String)
            m_IdShowedObject = value
        End Set
    End Property

#Region "Operazioni di cancellazione"

    Public Overridable Sub StartDeleteOperation()
        If m_ChangeStateEnabled Then
            m_State.DeleteOperation(Me)
        End If
    End Sub


    Public Overridable Sub DoDelete()
        '
    End Sub

#End Region

#Region "Metodo per la ricerca"
    Public Sub StartSearchOperation()
        If m_ChangeStateEnabled Then

            m_State.SearchOperation(Me)

        End If

    End Sub


    Public Overridable Sub DoSearch()
        '
    End Sub
#End Region




#Region "Metodo Load"
    Public Sub StartLoadOperation()
        If m_ChangeStateEnabled Then
            DoLoad()
        End If
    End Sub


    Public Overridable Sub DoLoad()
        Try
            If Me.m_IdShowedObject = -1 Then
                StartSearchOperation()
            Else
                m_IsLoading = True
                Nested_PrepareLoading()
                Nested_ClearWindowEditors()
                Nested_LoadDataFromDataSource()
                Nested_LoadEditorsProperties()
                Nested_PostLoadingActions()
                Nested_NotifyParent()
                m_IsLoading = False
            End If
        Catch ex As Exception
            m_IsLoading = False
            Throw
        End Try

    End Sub
    Protected Overridable Sub Nested_PrepareLoading()
        '
    End Sub
    Protected Overridable Sub Nested_ClearWindowEditors()

    End Sub
    Protected Overridable Sub Nested_LoadDataFromDataSource()
        '
    End Sub
    Protected Overridable Sub Nested_LoadEditorsProperties()
        '
    End Sub
    Protected Overridable Sub Nested_PostLoadingActions()

    End Sub

#End Region

#Region "Metodo Create"
    Public Overridable Sub StartCreateOperation()
        If m_ChangeStateEnabled Then
            m_State.CreateOperation(Me)
        End If
    End Sub


    Public Overridable Sub GetInfo()

    End Sub



    Public Overridable Sub DoCreation()
        Try
            m_IsLoading = True
            Nested_CheckSecurityForCreation()
            Nested_ClearWindowEditors()
            Nested_PrepareForCreation()
            m_IsLoading = False
        Catch ex As Exception
            m_IsLoading = False
            Throw
        End Try

        'NotifyParent()
    End Sub

    Protected Overridable Sub Nested_CheckSecurityForCreation()
        '
    End Sub
    Protected Overridable Sub Nested_CheckSecurityForDeletion()
        '
    End Sub

    Protected Overridable Sub Nested_PrepareForCreation()
        '
    End Sub


    Public Overridable Sub Nested_NotifyParent()
        '
    End Sub
#End Region

#Region "Metodo Change"
    Public Overridable Sub StartChangeOperation()
        If m_ChangeStateEnabled Then
            m_State.ChangeOperation(Me)
        End If
    End Sub
    Protected Overridable Sub Nested_PostChangeActions()
        '
    End Sub

    Public Overridable Sub DoChange()
        Nested_CheckSecurityForChanging()
        Nested_PrepareForUpdate()
        Nested_NotifyParent()
        Nested_PostChangeActions()
    End Sub

    Protected Overridable Sub Nested_CheckSecurityForChanging()

    End Sub


    Public Overridable Sub Nested_PrepareForUpdate()
        '
    End Sub
#End Region

#Region "Metodo Annulla"
    Public Overridable Sub StartUndoOperation()
        If m_ChangeStateEnabled Then
            m_State.UndoOperation(Me)
        End If
    End Sub

    Public Overridable Sub DoUndo()

        Nested_ReLoadProperties()

    End Sub


#End Region


#Region "Metodo Inserisci"
    Public Overridable Sub StartSaveOperation()
        If m_ChangeStateEnabled Then
            m_State.SaveOperation(Me)
        End If
    End Sub
    Public Overridable Sub DoSave()
        Nested_PrepareSaving()
        If Me.State.StateName = "Creazione" Then
            Nested_InsertData()
            Nested_PostCreationActions()
        ElseIf Me.State.StateName = "Aggiornamento" Then
            Nested_UpdateData()
            Nested_PostUpdateActions()
        End If
        Nested_PostSaveActions()
    End Sub


    Public Overridable Sub Nested_ReLoadProperties()
        Nested_LoadEditorsProperties()
    End Sub
    Protected Overridable Sub Nested_PostUpdateActions()

    End Sub
    Protected Overridable Sub Nested_PostCreationActions()

    End Sub
    Protected Overridable Sub Nested_PostSaveActions()

    End Sub
    Public Overridable Sub Nested_PrepareSaving()
        '
    End Sub
    Public Overridable Sub Nested_UpdateData()
        '
    End Sub
    Public Overridable Sub Nested_InsertData()
        '
    End Sub
#End Region



    Public Property State() As AbstractControlState
        Get
            Return m_State
        End Get
        Set(ByVal value As AbstractControlState)
            m_State = value
            Nested_NotifyParent()
            RaiseEvent StateChanged(m_State)
        End Set
    End Property

    Private Sub AbstractCrudControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
