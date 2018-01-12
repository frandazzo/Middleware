Public Class IndexedObject


    Private m_object As Object

    Public Property CurrentObject() As Object
        Get
            Return m_object
        End Get
        Set(ByVal value As Object)
            m_object = value
        End Set
    End Property




    Private m_index As Int32

    Public Property Index() As Int32
        Get
            Return m_index
        End Get
        Set(ByVal value As Int32)
            m_index = value
        End Set
    End Property


End Class
