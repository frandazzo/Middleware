Public Class VirtualLazyList
   Implements IList


   Protected Source As ArrayList
   Protected Overridable Function GetList() As ArrayList
      If Source Is Nothing Then Source = New ArrayList
      Return Source
   End Function

   Public Sub New()

   End Sub

   Public Sub New(ByVal CustomSource As ArrayList)
      Source = CustomSource
   End Sub
  

   Public Function Clone() As Object
      Return GetList.Clone()
   End Function

   Public Property Capacity() As Integer
      Get
         Return GetList.Capacity
      End Get
      Set(ByVal value As Integer)
         GetList.Capacity = value
      End Set
   End Property

   Public Overrides Function Equals(ByVal obj As Object) As Boolean
      Return GetList.Equals(obj)
   End Function


   Public Sub CopyTo(ByVal array As System.Array)
      GetList.CopyTo(array)
   End Sub


   Public Sub CopyTo(ByVal index As Integer, ByVal array As System.Array, ByVal arrayIndex As Integer, ByVal count As Integer)
      GetList.CopyTo(index, array, arrayIndex, count)
   End Sub


   Public Sub AddRange(ByVal c As System.Collections.ICollection)
      GetList.AddRange(c)
   End Sub



   Public Function LastIndexOf(ByVal value As Object) As Integer
      Return GetList.LastIndexOf(value)
   End Function

   Public Function BinarySearch(ByVal index As Integer, ByVal count As Integer, ByVal value As Object, ByVal comparer As System.Collections.IComparer) As Integer
      Return GetList.BinarySearch(index, count, value, comparer)
   End Function

   Public Function IndexOf(ByVal value As Object, ByVal startIndex As Integer) As Integer
      Return GetList.IndexOf(value, startIndex)
   End Function

   Public Function BinarySearch(ByVal value As Object) As Integer
      Return GetList.BinarySearch(value)
   End Function

   Public Function BinarySearch(ByVal value As Object, ByVal comparer As System.Collections.IComparer) As Integer
      Return GetList.BinarySearch(value, comparer)
   End Function

   Public Function GetEnumerator(ByVal index As Integer, ByVal count As Integer) As System.Collections.IEnumerator
      Return GetList.GetEnumerator(index, count)
   End Function

   Public Overrides Function GetHashCode() As Integer
      Return GetList.GetHashCode()
   End Function

   Public Function GetRange(ByVal index As Integer, ByVal count As Integer) As System.Collections.ArrayList
      Return GetList.GetRange(index, count)
   End Function

   Public Function IndexOf(ByVal value As Object, ByVal startIndex As Integer, ByVal count As Integer) As Integer
      Return GetList.IndexOf(value, startIndex, count)
   End Function


   Public Sub InsertRange(ByVal index As Integer, ByVal c As System.Collections.ICollection)
      GetList.InsertRange(index, c)
   End Sub

   Public Function LastIndexOf(ByVal value As Object, ByVal startIndex As Integer) As Integer
      Return GetList.LastIndexOf(value, startIndex)
   End Function

   Public Function LastIndexOf(ByVal value As Object, ByVal startIndex As Integer, ByVal count As Integer) As Integer
      Return GetList.LastIndexOf(value, startIndex, count)
   End Function



   Public Sub RemoveRange(ByVal index As Integer, ByVal count As Integer)
      GetList.RemoveRange(index, count)
   End Sub

   Public Sub Reverse()
      GetList.Reverse()
   End Sub

   Public Sub Reverse(ByVal index As Integer, ByVal count As Integer)
      GetList.Reverse(index, count)
   End Sub



   Public Sub SetRange(ByVal index As Integer, ByVal c As System.Collections.ICollection)
      GetList.SetRange(index, c)
   End Sub

   Public Sub Sort()
      GetList.Sort()
   End Sub

   Public Sub Sort(ByVal index As Integer, ByVal count As Integer, ByVal comparer As System.Collections.IComparer)
      GetList.Sort(index, count, comparer)
   End Sub

   Public Sub Sort(ByVal comparer As System.Collections.IComparer)
      GetList.Sort(comparer)
   End Sub

   Public Function ToArray() As Object()
      Return GetList.ToArray()
   End Function

   Public Function ToArray(ByVal type As System.Type) As System.Array
      Return GetList.ToArray(type)
   End Function

   Public Overrides Function ToString() As String
      Return GetList.ToString()
   End Function

   Public Sub TrimToSize()
      GetList.TrimToSize()
   End Sub

   Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
      GetList.CopyTo(array, index)
   End Sub

   Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
      Get
         Return GetList.Count
      End Get
   End Property

   Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
      Get
         Return GetList.IsSynchronized
      End Get
   End Property

   Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
      Get
         Return GetList.SyncRoot
      End Get
   End Property

   Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
      Return GetList.GetEnumerator()
   End Function

    Public Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
        GetList.Add(value)
    End Function

   Public Sub Clear() Implements System.Collections.IList.Clear
      GetList.Clear()
   End Sub

   Public Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
      Return GetList.Contains(value)
   End Function

   Public Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
      Return GetList.IndexOf(value)
   End Function

   Public Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
      GetList.Insert(index, value)
   End Sub

   Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
      Get
         Return GetList.IsFixedSize
      End Get
   End Property

   Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
      Get
         Return GetList.IsReadOnly
      End Get
   End Property

   Default Public Overloads Property Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
      Get
         Return GetList.Item(index)
      End Get
      Set(ByVal value As Object)
         GetList.Item(index) = value
      End Set
   End Property

   Public Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
      GetList.Remove(value)
   End Sub

   Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
      GetList.RemoveAt(index)
   End Sub
End Class
