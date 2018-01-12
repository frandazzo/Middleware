Imports WIN.SECURITY.Core

Public Class User
   Inherits AbstractPersistenceObject
   Implements IUser



   Private _pwd As String = ""
   Private _mail As String = ""
   Private _usn As String = ""
   Private _role As IRole
   Private _name As String = ""
   Private _surName As String = ""


   Public Sub New()

   End Sub
   Public Sub New(ByVal role As IRole)
      _role = role
      _role.Users.Add(Me)
   End Sub

   Public Property SurName() As String
      Get
         Return _surName
      End Get
      Set(ByVal value As String)
         _surName = value
      End Set
   End Property
   Public Property Name() As String
      Get
         Return _name
      End Get
      Set(ByVal value As String)
         _name = value
      End Set
   End Property

   Public Property IDUser() As Integer Implements SECURITY.Core.IUser.ID
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

   Public Property Password() As String Implements SECURITY.Core.IUser.Password
      Get
         Return _pwd
      End Get
      Set(ByVal value As String)
         _pwd = value
      End Set
   End Property

   Public Property Role() As SECURITY.Core.IRole Implements SECURITY.Core.IUser.Role
      Get
         Return _role
      End Get
      Set(ByVal value As IRole)
         _role = value
      End Set
   End Property

   Public Property Username() As String Implements SECURITY.Core.IUser.Username
      Get
         Return _usn
      End Get
      Set(ByVal value As String)
         _usn = value
      End Set
   End Property

   Public Property Mail() As String Implements SECURITY.Core.IUser.Mail
      Get
         Return _mail
      End Get
      Set(ByVal value As String)
         _mail = Mail
      End Set
   End Property
End Class
