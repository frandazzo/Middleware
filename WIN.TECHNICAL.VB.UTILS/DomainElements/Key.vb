<System.Serializable()> Public Class Key
    Private m_oggetti As ArrayList
    Private m_id As String
    Public Property Id() As Int32
        Get
            Return m_id
        End Get
        Set(ByVal value As Int32)
            m_oggetti = New ArrayList
            m_oggetti.Add(value)
            m_id = value
        End Set
    End Property


   ''' <summary>
   ''' Metodo che verifica l'uguaglianza tra due chiavi identificative
   ''' </summary>
   ''' <param name="oggetto"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function EqualsTo(ByVal oggetto As Object) As Boolean
      If Not (TypeOf (oggetto) Is Key) Then Return False
      Dim Chiave As Key = DirectCast(oggetto, Key)
      If m_oggetti.Count <> Chiave.m_oggetti.Count Then Return False
      Dim i As Int32
      For i = 0 To m_oggetti.Count - 1
         If Not m_oggetti.Item(i).Equals(Chiave.m_oggetti.Item(i)) Then Return False
      Next
      Return True
   End Function
   Public Sub New(ByVal l As ArrayList)
      VerificaValoriNonNulli(l)
      m_oggetti = l
   End Sub
   Private Sub VerificaValoriNonNulli(ByVal l As ArrayList)
      If l Is Nothing Then Throw New NullReferenceException("Lista valori chiave nulla!")
      Dim i As Int32
      For i = 0 To m_oggetti.Count - 1
         If m_oggetti.Item(i) Is Nothing Then Throw New ArgumentException("Non si può avere un parametro nullo")
      Next
   End Sub
   Public Sub New(ByVal arg As Int32)
      m_oggetti = New ArrayList
      m_oggetti.Add(arg)
   End Sub
   Public Sub New(ByVal arg As Int32, ByVal arg1 As Int32)
      m_oggetti = New ArrayList
      m_oggetti.Add(arg)
      m_oggetti.Add(arg1)
    End Sub

    

    Public Sub New()

    End Sub
   ''' <summary>
   ''' Metodo che restituisce un oggetto identificativo all'indice specificato
   ''' </summary>
   ''' <param name="i"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function Value(ByVal i As Int32) As Object
      Return m_oggetti.Item(i)
   End Function
   ''' <summary>
   ''' Metodo che restituisce un oggetto identificativo 
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function Value() As Object
      ControllaValoreSingolo()
      Return m_oggetti.Item(1)
   End Function
   Private Sub ControllaValoreSingolo()
      If m_oggetti.Count <> 1 Then Throw New ArgumentException("E' una chiave composta!")
   End Sub
   ''' <summary>
   ''' Metodo che restituisce il valore Long all'indice specificato
   ''' </summary>
   ''' <param name="i"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function LongValue(ByVal i As Int32) As Int32
      Return m_oggetti.Item(i)
   End Function
   ''' <summary>
   ''' Metodo che restituisce il valore Long di una chiave semplice
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function LongValue() As Int32

      ControllaValoreSingolo()
      Return m_oggetti.Item(0)


   End Function
   ''' <summary>
   ''' Metodo che restituisce un valore che indica se la chiave è semplice o composta
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
    Public Function IsSimple() As Boolean
       
        If m_oggetti.Count = 1 Then Return True
        Return False
    End Function
   ''' <summary>
   ''' Metodo che restituisce una stringa con la concatenazione degli identificativi di un oggetto
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
    Public Overrides Function ToString() As String
      

        Dim K As String = ""
        Dim obj As String
        If m_oggetti.Count > 1 Then
            K = m_oggetti.Item(0)
            For i As Int32 = 0 To m_oggetti.Count - 1
                If i > 0 Then
                    obj = m_oggetti.Item(i)
                    K = K & "-" & obj.ToString
                End If
            Next
            Return K
        End If

        If m_oggetti.Count = 1 Then
            Return m_oggetti.Item(0).ToString
        End If

        Return ""
    End Function
End Class
