Class MainWindowViewModel
    Inherits ViewModelBase

    Dim _MainTabItems As IEnumerable(Of PluginManagement.TabControl.PluginTabItem)
    Public ReadOnly Property MainTabItems As IEnumerable(Of PluginManagement.TabControl.PluginTabItem)
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

    Dim _Plugins As IEnumerable(Of PluginManagement.Plugin) = Nothing
    Public Property Plugins As IEnumerable(Of PluginManagement.Plugin)
        Get
            Return _Plugins
        End Get
        Set(value As IEnumerable(Of PluginManagement.Plugin))
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
                New DesignerSupport.DesignerSupportPlugin("Tennis"), _
                New DesignerSupport.DesignerSupportPlugin("Fußball"), _
                New DesignerSupport.DesignerSupportPlugin("Anderer Stuff")
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
        Dim Temp As New List(Of PluginManagement.TabControl.PluginTabItem)
        For Each i In _Plugins
            If PluginManagement.PluginActiveStates.IsInUse(i.PluginGuid) Then
                Temp.Add(New PluginManagement.TabControl.PluginTabItem(i))
            End If
        Next
        _MainTabItems = Temp
        OnPropertyChanged("MainTabItems")
    End Sub

End Class
