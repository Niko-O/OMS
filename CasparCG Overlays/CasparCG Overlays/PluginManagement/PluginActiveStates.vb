
''' <summary>
''' Eine Liste von Zuständen, die angeben, ob Plugins aktiv sind. Die Plugins werden an ihren Guids erkannt.
''' </summary>
Public Class PluginActiveStates

    Public Shared Event IsInUseChanged As EventHandler(Of IsInUseChangedEventArgs)
    Private Shared Sub OnIsInUseChanged(PluginGuid As Guid, NewState As Boolean)
        RaiseEvent IsInUseChanged(Nothing, New IsInUseChangedEventArgs(PluginGuid, NewState))
    End Sub

    Public Shared Property DefaultState As Boolean = True

    Private Shared States As New Dictionary(Of Guid, Boolean)

    Public Shared Property IsInUse(PluginGuid As Guid) As Boolean
        Get
            IsInUse = DefaultState
            If Not States.TryGetValue(PluginGuid, IsInUse) Then
                States.Add(PluginGuid, DefaultState)
                IsInUse = DefaultState
            End If
        End Get
        Set(value As Boolean)
            Dim PreviousValue As Boolean = Nothing
            Dim Exists = States.TryGetValue(PluginGuid, PreviousValue)
            If Not Exists OrElse Not PreviousValue = value Then
                States(PluginGuid) = value
                OnIsInUseChanged(PluginGuid, value)
            End If
        End Set
    End Property

    Public Class IsInUseChangedEventArgs
        Inherits EventArgs

        Dim _PluginGuid As Guid
        Public ReadOnly Property PluginGuid As Guid
            Get
                Return _PluginGuid
            End Get
        End Property

        Dim _NewState As Boolean
        Public ReadOnly Property NewState As Boolean
            Get
                Return _NewState
            End Get
        End Property

        Public Sub New(NewPluginGuid As Guid, NewNewState As Boolean)
            _PluginGuid = NewPluginGuid
            _NewState = NewNewState
        End Sub

    End Class

End Class
