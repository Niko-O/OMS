Public Class CallCommand
    Implements ICasparServerCommand

    Dim _ChannelId As Integer
    Public ReadOnly Property ChannelId As Integer
        Get
            Return _ChannelId
        End Get
    End Property

    Dim _Layer As Integer?
    Public ReadOnly Property Layer As Integer?
        Get
            Return _Layer
        End Get
    End Property

    Dim _MethodName As String
    Public ReadOnly Property MethodName As String
        Get
            Return _MethodName
        End Get
    End Property

    Dim _Parameters As Object()
    Public ReadOnly Property Parameters As Object()
        Get
            Return _Parameters
        End Get
    End Property

    Public Sub New(NewChannelId As Integer, NewLayer As Integer?, NewMethodName As String, ParamArray NewParameters As Object())
        If NewChannelId < 0 Then
            Throw New ArgumentOutOfRangeException("NewChannelId", "NewChannelId darf nicht negativ sein.")
        End If
        If NewLayer.HasValue AndAlso NewLayer < 0 Then
            Throw New ArgumentOutOfRangeException("NewLayer", "NewLayer darf nicht negativ sein.")
        End If
        If String.IsNullOrWhiteSpace(NewMethodName) Then
            Throw New ArgumentNullException("NewMethodName")
        End If
        If NewParameters Is Nothing Then
            Throw New ArgumentNullException("NewParameters")
        End If
        For Each i In NewParameters
            If i Is Nothing Then
                Throw New ArgumentNullException("NewParameters", "Ein Element in NewParameters ist null.")
            End If
        Next
        _ChannelId = NewChannelId
        _Layer = NewLayer
        _MethodName = NewMethodName
        _Parameters = NewParameters
    End Sub

    Public Function GetCommandString() As String Implements ICasparServerCommand.GetCommandString
        Return String.Format("CALL {0} INVOKE ""{1}({2})""", _ChannelId.ToString & If(_Layer.HasValue, "-" & _Layer.Value.ToString, ""), _MethodName, String.Join(", ", _Parameters))
    End Function

End Class
