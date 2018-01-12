Imports WIN.SECURITY.Core

Public Class Permission
   Inherits AbstractPersistenceObject
   Implements IPermission

   Private _profile As IProfile
   Private _fullName As String = ""
   Private _alias As String = ""
   Private _macro As String = ""

   Public Property FullMethodName() As String Implements IPermission.FullMethodName
      Get
         Return _fullName
      End Get
      Set(ByVal value As String)
         _fullName = value
      End Set
   End Property

   Public Property IDPermission() As Integer Implements IPermission.ID
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

   

   Public Property [Alias]() As String Implements SECURITY.Core.IPermission.Alias
      Get
         Return _alias
      End Get
      Set(ByVal value As String)
         _alias = value
      End Set
   End Property

   Public Property Macroarea() As String Implements SECURITY.Core.IPermission.Macroarea
      Get
         Return (_macro)
      End Get
      Set(ByVal value As String)
         _macro = value
      End Set
   End Property

   Public Property Profile() As SECURITY.Core.IProfile Implements SECURITY.Core.IPermission.Profile
      Get
         Return _profile
      End Get
      Set(ByVal value As SECURITY.Core.IProfile)
         _profile = value
      End Set
   End Property
End Class
