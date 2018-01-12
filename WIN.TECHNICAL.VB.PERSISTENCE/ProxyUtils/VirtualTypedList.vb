Public Class VirtualTypedList(Of T)
   Implements Generic.IList(Of T)





   Protected Source As IList(Of T)
   Protected Overridable Function GetList() As Generic.IList(Of T)
      If Source Is Nothing Then Source = New Generic.List(Of T)
      Return Source
   End Function

   Public Sub New()

   End Sub

   Public Sub New(ByVal CustomSource As Generic.IList(Of T))
      Source = CustomSource
   End Sub

   Public Function Contains(ByVal item As T) As Boolean Implements Generic.IList(Of T).Contains
      Return GetList.Contains(item)
   End Function


   Public Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
      GetList.Add(item)
   End Sub

   Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
      GetList.Clear()
   End Sub

   Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
      GetList.CopyTo(array, arrayIndex)
   End Sub

   Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count
      Get
         Return GetList.Count
      End Get
   End Property

   Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
      Get
         Return GetList.IsReadOnly
      End Get
   End Property

   Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
      Return GetList.Remove(item)
   End Function

   Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
      Return GetList.GetEnumerator
   End Function

   Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
      Return GetList.IndexOf(item)
   End Function

   Public Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
      GetList.Insert(index, item)
   End Sub

   Default Public Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
      Get
         Return GetList.Item(index)
      End Get
      Set(ByVal value As T)
         GetList.Item(index) = value
      End Set
   End Property

   Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
      GetList.RemoveAt(index)
   End Sub

   Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
      Return GetList.GetEnumerator
   End Function
End Class
