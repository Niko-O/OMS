Public Class StopCommand
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

    Public Sub New(NewChannelId As Integer, NewLayer As Integer?)
        If NewChannelId < 0 Then
            Throw New ArgumentOutOfRangeException("NewChannelId", "NewChannelId darf nicht negativ sein.")
        End If
        If NewLayer.HasValue AndAlso NewLayer.Value < 0 Then
            Throw New ArgumentOutOfRangeException("NewLayer", "NewLayer darf nicht negativ sein.")
        End If
        _ChannelId = NewChannelId
        _Layer = NewLayer
    End Sub

    Public Function GetCommandString() As String Implements ICasparServerCommand.GetCommandString
        Dim Parts As New List(Of String)
        Parts.Add("STOP")
        Parts.Add(_ChannelId.ToString & If(_Layer.HasValue, "-" & _Layer.Value.ToString, ""))
        Return String.Join(" ", Parts)
    End Function
End Class
