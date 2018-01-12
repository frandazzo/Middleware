Public Class Field
   Private m_FileField As String = ""
   Private m_KeyField As String = ""
   Private m_number As Int16 = 0


   Public Property Number() As Int16
      Get
         Return m_number
      End Get
      Set(ByVal value As Int16)
         m_number = value
      End Set
   End Property

   Public Property FileField() As String
      Get
         Return m_FileField
      End Get
      Set(ByVal value As String)
         m_FileField = value
      End Set
   End Property


   Public Property KeyField() As String
      Get
         Return m_KeyField
      End Get
      Set(ByVal value As String)
         m_KeyField = value
      End Set
   End Property
End Class
