Class MainWindowViewModel
    Inherits ViewModelBase

    Dim _MainTabItems As IEnumerable(Of PluginTabItem)
    Public ReadOnly Property MainTabItems As IEnumerable(Of PluginTabItem)
        Get
            Return _MainTabItems
        End Get
    End Property

    Dim _PluginViewModels As IEnumerable(Of PluginViewModel)
    Public ReadOnly Property PluginViewModels As IEnumerable(Of PluginViewModel)
        Get
            Return _PluginViewModels
        End Get
    End Property

    Dim _Plugins As IEnumerable(Of Plugin) = Nothing
    Public Property Plugins As IEnumerable(Of Plugin)
        Get
            Return _Plugins
        End Get
        Set(value As IEnumerable(Of Plugin))
            If ChangeIfDifferent(_Plugins, value, "Plugins") Then
                ActualizeMainTabItems()
                ActualizePluginViewModels()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New(True)
        If IsInDesignMode Then
            Plugins = _
            {
                New DesignerSupportPlugin("Tennis"), _
                New DesignerSupportPlugin("Fußball"), _
                New DesignerSupportPlugin("Anderer Stuff")
            }
        End If
    End Sub

    Private Sub ActualizePluginViewModels()
        If Not _PluginViewModels Is Nothing Then
            For Each i In _PluginViewModels
                i.Unload()
                RemoveHandler i.IsInUseChanged, AddressOf PluginViewModelIsInUseChanged
            Next
        End If
        Dim Temp As New List(Of PluginViewModel)
        For Each i In _Plugins
            Dim NewViewModel As New PluginViewModel(i)
            AddHandler NewViewModel.IsInUseChanged, AddressOf PluginViewModelIsInUseChanged
            Temp.Add(NewViewModel)
        Next
        _PluginViewModels = Temp
        OnPropertyChanged("PluginViewModels")
    End Sub

    Private Sub PluginViewModelIsInUseChanged()
        ActualizeMainTabItems()
        ActualizePluginViewModels()
    End Sub

    Private Sub ActualizeMainTabItems()
        If Not _MainTabItems Is Nothing Then
            For Each i In _MainTabItems
                i.UnloadPlugin()
            Next
        End If
        Dim Temp As New List(Of PluginTabItem)
        For Each i In _Plugins
            If PluginActiveStates.IsInUse(i.PluginGuid) Then
                Temp.Add(New PluginTabItem(i))
            End If
        Next
        _MainTabItems = Temp
        OnPropertyChanged("MainTabItems")
    End Sub

End Class
