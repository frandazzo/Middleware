Public Class DuplicatedIndexException
   Inherits Exception
   Public Overrides ReadOnly Property Message() As String
      Get
         Return "L'oggetto che si sta inserendo ha degli attributi identici ad altri oggetti gi� presenti. Controllare che l'oggetto non sia gi� presente."
      End Get
   End Property
End Class
