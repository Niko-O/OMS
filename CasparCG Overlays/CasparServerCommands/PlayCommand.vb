Public Class PlayCommand
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

    Dim _Clip As String
    Public ReadOnly Property Clip As String
        Get
            Return _Clip
        End Get
    End Property

    Dim _AdditionalParameters As String
    Public ReadOnly Property AdditionalParameters As String
        Get
            Return _AdditionalParameters
        End Get
    End Property

    Public Sub New(NewChannelId As Integer, NewLayer As Integer?, NewClip As String, NewAdditionalParameters As String)
        If NewChannelId < 0 Then
            Throw New ArgumentOutOfRangeException("NewChannelId", "NewChannelId darf nicht negativ sein.")
        End If
        If NewLayer.HasValue AndAlso NewLayer.Value < 0 Then
            Throw New ArgumentOutOfRangeException("NewLayer", "NewLayer darf nicht negativ sein.")
        End If
        If String.IsNullOrWhiteSpace(NewClip) Then
            Throw New ArgumentNullException("NewClip")
        End If
        _ChannelId = NewChannelId
        _Layer = NewLayer
        _Clip = NewClip
        _AdditionalParameters = NewAdditionalParameters
    End Sub

    Public Function GetCommandString() As String Implements ICasparServerCommand.GetCommandString
        Dim Parts As New List(Of String)
        Parts.Add("PLAY")
        Parts.Add(_ChannelId.ToString & If(_Layer.HasValue, "-" & _Layer.Value.ToString, ""))
        For Each i In _AdditionalParameters
            Parts.Add(i)
        Next
        Return String.Join(" ", Parts)
    End Function

End Class
