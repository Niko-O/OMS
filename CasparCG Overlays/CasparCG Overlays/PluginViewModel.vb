Public Class PluginViewModel
    Inherits ViewModelBase(Of PluginManagement.PluginWrapper)

    Public Event IsInUseChanged()
    Private Sub OnIsInUseChanged()
        RaiseEvent IsInUseChanged()
        OnPropertyChanged("IsInUse")
    End Sub

    Public Property IsInUse As Boolean
        Get
            Return PluginManagement.PluginActiveStates.IsInUse(Target.PluginGuid)
        End Get
        Set(value As Boolean)
            Dim CurrentState = IsInUse
            If Not CurrentState = value Then
                PluginManagement.PluginActiveStates.IsInUse(Target.PluginGuid) = value
                OnIsInUseChanged()
            End If
        End Set
    End Property

    Public Sub New(NewTarget As PluginManagement.PluginWrapper)
        MyBase.New(NewTarget)
        AddHandler PluginManagement.PluginActiveStates.IsInUseChanged, AddressOf CheckIsInUseChanged
    End Sub

    Private Sub CheckIsInUseChanged(sender As Object, e As PluginManagement.PluginActiveStates.IsInUseChangedEventArgs)
        If e.PluginGuid = Target.PluginGuid Then
            OnIsInUseChanged()
        End If
    End Sub

    Public Sub Unload()
        RemoveHandler PluginManagement.PluginActiveStates.IsInUseChanged, AddressOf CheckIsInUseChanged
    End Sub

End Class
