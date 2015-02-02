Public Class PluginViewModel
    Inherits ViewModelBase(Of Plugin)

    Public Event IsInUseChanged()
    Private Sub OnIsInUseChanged()
        RaiseEvent IsInUseChanged()
        OnPropertyChanged("IsInUse")
    End Sub

    Public Property IsInUse As Boolean
        Get
            Return PluginActiveStates.IsInUse(Target.PluginGuid)
        End Get
        Set(value As Boolean)
            Dim CurrentState = IsInUse
            If Not CurrentState = value Then
                PluginActiveStates.IsInUse(Target.PluginGuid) = value
                OnIsInUseChanged()
            End If
        End Set
    End Property

    Public Sub New(NewTarget As Plugin)
        MyBase.New(NewTarget)
        AddHandler PluginActiveStates.IsInUseChanged, AddressOf CheckIsInUseChanged
    End Sub

    Private Sub CheckIsInUseChanged(sender As Object, e As PluginActiveStates.IsInUseChangedEventArgs)
        If e.PluginGuid = Target.PluginGuid Then
            OnIsInUseChanged()
        End If
    End Sub

    Public Sub Unload()
        RemoveHandler PluginActiveStates.IsInUseChanged, AddressOf CheckIsInUseChanged
    End Sub

End Class
