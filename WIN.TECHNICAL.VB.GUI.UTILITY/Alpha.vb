Public Class Alpha
   Private _duration As Integer
   Private _start As Integer
   Private _started As Boolean
   Private _goAndGoBack As Boolean
   Private _loops As Integer
   Private _complete As Boolean
   Private _isGoingBack As Boolean
   Private _executedLoops As Integer


   ''' <summary>
   ''' The loop number. 0 to set infinite
   ''' </summary>
   ''' <remarks></remarks>
   Public Property Loops() As Integer
      Get
         Return _loops
      End Get
      Set(ByVal value As Integer)
         _loops = value
      End Set
   End Property


   Public Property GoAndGoBack() As Boolean
      Get
         Return _goAndGoBack
      End Get
      Set(ByVal value As Boolean)
         _goAndGoBack = value
      End Set
   End Property

   Public Property Started() As Boolean
      Get
         Return _started
      End Get
      Set(ByVal value As Boolean)
         _started = value
      End Set
   End Property

   Public Property Complete() As Boolean
      Get
         Return _complete
      End Get
      Set(ByVal value As Boolean)
         _complete = value
      End Set
   End Property




   Public Sub New(ByVal duration As Integer)
      _duration = duration
      _complete = False
      _loops = 1
      _goAndGoBack = False
      _executedLoops = 0
   End Sub


   Public Sub Start()
      _complete = False
      _start = Environment.TickCount
      _started = True
      _isGoingBack = False
   End Sub


   Public Sub Reset()
      _complete = False
      _started = False
      _isGoingBack = False
      _executedLoops = 0
   End Sub


   Public Sub Update()
      Dim elapsed As Integer = Environment.TickCount - _start

      If (elapsed >= _duration) Then
         Me._complete = True
      End If
   End Sub


   Public ReadOnly Property Value() As Double
      Get
         If (_complete) Then
            If (_goAndGoBack) Then
               Return 0.0
            Else
               Return 1.0
            End If
         End If

         Dim val As Double = 0.0
         Dim elapsed As Integer = Environment.TickCount - _start

         If (elapsed >= _duration) Then

            If (_goAndGoBack And Not _isGoingBack) Then

               Start()
               _isGoingBack = True
               val = 1.0

            Else

               _executedLoops = _executedLoops + 1

               If (_isGoingBack) Then
                  val = 0.0
               Else
                  val = 1.0
               End If

               If (_executedLoops < _loops Or _loops = 0) Then
                  Start()
               Else
                  _complete = True
                  _executedLoops = 0
               End If
            End If

            Return val
         End If

         If (_isGoingBack) Then
            val = (1.0) - (Convert.ToDouble(elapsed) / Convert.ToDouble(_duration))
         Else
            val = Convert.ToDouble(elapsed) / Convert.ToDouble(_duration)
         End If
         Return val
      End Get

   End Property



   Public ReadOnly Property InverseValue() As Double
      Get
         Return (1) - (Value)
      End Get
   End Property
End Class
