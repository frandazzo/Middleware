Imports WIN.SECURITY.Core

Public Class RoleProfile
   Inherits AbstractPersistenceObject
   Private m_Role As IRole
   Private m_Profile As IProfile


   Public Sub New(ByVal Role As IRole, ByVal Profile As IProfile)
      m_Role = Role
      m_Profile = Profile
   End Sub


   Public Overrides Function Equals(ByVal obj As Object) As Boolean
      If obj Is Nothing Then Return False

      If Not TypeOf (obj) Is RoleProfile Then Return False
      Dim rp As RoleProfile = DirectCast(obj, RoleProfile)

      If m_Role Is Nothing Then Return False
      If m_Profile Is Nothing Then Return False

      If rp.Role Is Nothing Then Return False
      If rp.Profile Is Nothing Then Return False


      If m_Role.ID <> rp.Role.ID Then Return False
      If m_Profile.ID <> rp.Profile.ID Then Return False

      Return True
   End Function

   Public Overrides Function GetHashCode() As Integer
      Return MyBase.GetHashCode()
   End Function


   Public Property Profile() As IProfile
      Get
         Return m_Profile
      End Get
      Set(ByVal value As IProfile)
         m_Profile = value
      End Set
   End Property


   Public Property Role() As IRole
      Get
         Return m_Role
      End Get
      Set(ByVal value As IRole)
         m_Role = value
      End Set
   End Property



End Class
