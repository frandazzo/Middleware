Imports WIN.SECURITY.Core

Public Class Role
   Inherits AbstractPersistenceObject
   Implements IRole

   Private _usersProxy As IList
   Private _profilesProxy As IList
   'Private _roleProfiles As IList
   Private _users As IList(Of IUser) '= New List(Of IUser)
   Private _profiles As IList(Of IProfile) '= New List(Of IProfile)

   Public Sub SetUserProxy(ByVal proxy As IList)
      _usersProxy = proxy
   End Sub
   Public Sub SetProfileProxy(ByVal proxy As IList)
      _profilesProxy = proxy
   End Sub



   'Public ReadOnly Property RoleProfiles() As IList
   '   Get
   '      If _roleProfiles Is Nothing Then
   '         Return New ArrayList
   '      Else
   '         Return _roleProfiles
   '      End If
   '   End Get
   'End Property




   Public Overrides Property Descrizione() As String Implements SECURITY.Core.IRole.Description
      Get
         Return MyBase.Descrizione
      End Get
      Set(ByVal value As String)
         MyBase.Descrizione = value
      End Set
   End Property
   Public Overrides Function ToString() As String
      Return MyBase.Descrizione
   End Function

   Public Property IDRole() As Integer Implements SECURITY.Core.IRole.ID
      Get
         Return MyBase.Id
      End Get
      Set(ByVal value As Integer)
         MyBase.Key = New Key(value)
      End Set
   End Property


   'Public Sub AddProfile(ByVal Profile As IProfile)
   '   Profiles.Add(Profile)
   '   _roleProfiles.Add(New RoleProfile(Me, Profile))
   'End Sub

   'Public Sub RemoveProfile(ByVal Profile As IProfile)
   '   For Each elem As IProfile In _profiles
   '      If elem.Description = Profile.Description Then
   '         _profiles.Remove(elem)
   '      End If
   '   Next
   '   For Each elem As RoleProfile In _roleProfiles
   '      If elem.Profile.Description = Profile.Description Then
   '         _roleProfiles.Remove(elem)
   '      End If
   '   Next
   'End Sub


   'Public Function GetRoleProfile(ByVal Profile As IProfile) As RoleProfile
   '   For Each elem As RoleProfile In _roleProfiles
   '      If elem.Profile.Description = Profile.Description Then
   '         Return elem
   '      End If
   '   Next
   '   Return Nothing
   'End Function


   Public Property Profiles() As System.Collections.Generic.IList(Of IProfile) Implements SECURITY.Core.IRole.Profiles
      Get
         If _profiles Is Nothing Then
            _profiles = New List(Of IProfile)
            If _profilesProxy IsNot Nothing Then
               '_roleProfiles = New ArrayList
               Dim a As Int32 = _profilesProxy.Count
               For Each pr As IProfile In _profilesProxy
                  _profiles.Add(pr)
                  ' _roleProfiles.Add(New RoleProfile(Me, pr))
               Next
            End If
         End If
         Return _profiles
      End Get
      Set(ByVal value As System.Collections.Generic.IList(Of SECURITY.Core.IProfile))
         _profiles = value
      End Set
   End Property

   Public Property Users() As System.Collections.Generic.IList(Of SECURITY.Core.IUser) Implements SECURITY.Core.IRole.Users
      Get
         If _users Is Nothing Then
            _users = New List(Of IUser)
            If _usersProxy IsNot Nothing Then
               Dim a As Int32 = _usersProxy.Count
               For Each us As IUser In _usersProxy
                  _users.Add(us)
               Next
            End If
         End If
         Return _users
      End Get
      Set(ByVal value As System.Collections.Generic.IList(Of SECURITY.Core.IUser))
         _users = value
      End Set
   End Property
End Class
