Imports WIN.SECURITY.Core

Public Class Profile
   Inherits AbstractPersistenceObject
   Implements IProfile



   Private _roles As IList(Of IRole) '= New List(Of IRole)
   Private _rolesProxy As IList

   Private _permissions As IList(Of IPermission) '=New List(Of IPermission)

   Private _permissionsProxy As IList

   Public Sub SetRolesProxy(ByVal proxy As IList)
      _rolesProxy = proxy
   End Sub
   Public Sub SetPermissionsProxy(ByVal proxy As IList)
      _permissionsProxy = proxy
   End Sub

   Public Overrides Property Descrizione() As String Implements IProfile.Description
      Get
         Return MyBase.Descrizione
      End Get
      Set(ByVal value As String)
         MyBase.Descrizione = value
      End Set
   End Property

   Public Property IDProfile() As Integer Implements IProfile.ID
      Get
         If MyBase.Key IsNot Nothing Then
            Return MyBase.Key.Value(0)
         End If
         Return -1
      End Get
      Set(ByVal value As Integer)
         Throw New NotImplementedException
      End Set
   End Property




   Public Property Permissions() As System.Collections.Generic.IList(Of SECURITY.Core.IPermission) Implements SECURITY.Core.IProfile.Permissions
      Get
         If _permissions Is Nothing Then
            _permissions = New List(Of IPermission)
            If _permissionsProxy IsNot Nothing Then
               Dim a As Int32 = _permissionsProxy.Count
               For Each pr As IPermission In _permissionsProxy
                  _permissions.Add(pr)
               Next
            End If
         End If
         Return _permissions
      End Get
      Set(ByVal value As System.Collections.Generic.IList(Of IPermission))
         _permissions = value
      End Set
   End Property

   Public Property Roles() As System.Collections.Generic.IList(Of SECURITY.Core.IRole) Implements SECURITY.Core.IProfile.Roles
      Get
         If _roles Is Nothing Then
            _roles = New List(Of IRole)
            If _rolesProxy IsNot Nothing Then
               Dim a As Int32 = _rolesProxy.Count
               For Each pr As IRole In _rolesProxy
                  _roles.Add(pr)
               Next
            End If
         End If
         Return _roles
      End Get
      Set(ByVal value As System.Collections.Generic.IList(Of IRole))
         _roles = value
      End Set
   End Property
End Class
