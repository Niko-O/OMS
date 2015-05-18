Class MainWindowViewModel
    Inherits ViewModelBase

    Public Event RemoveServer As EventHandler(Of ServerList.RemoveServerEventArgs)
    Protected Overridable Sub OnRemoveServer(Server As ServerList.CasparCGServer)
        RaiseEvent RemoveServer(Me, New ServerList.RemoveServerEventArgs(Server))
    End Sub

    Dim _SelectedServerIp As String = ""
    Public Property SelectedServerIp As String
        Get
            Return _SelectedServerIp
        End Get
        Set(value As String)
            ChangeIfDifferent(_SelectedServerIp, value, "SelectedServerIp")
        End Set
    End Property

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

    Dim _Plugins As IEnumerable(Of PluginManagement.PluginWrapper) = Nothing
    Public Property Plugins As IEnumerable(Of PluginManagement.PluginWrapper)
        Get
            Return _Plugins
        End Get
        Set(value As IEnumerable(Of PluginManagement.PluginWrapper))
            If ChangeIfDifferent(_Plugins, value, "Plugins") Then
                ActualizeMainTabItems()
                ActualizePluginViewModels()
            End If
        End Set
    End Property

    Dim WithEvents _CasparCGServers As ViewModelCollection(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel) = Nothing
    Public Property CasparCGServers As ViewModelCollection(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel)
        Get
            Return _CasparCGServers
        End Get
        Set(value As ViewModelCollection(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel))
            If ChangeIfDifferent(_CasparCGServers, value) Then
                OnPropertyChanged("CasparCGServers")
            End If
        End Set
    End Property

    Dim _SelectedCasparCGServer As ServerList.CasparCGServerViewModel = Nothing
    Public Property SelectedCasparCGServer As ServerList.CasparCGServerViewModel
        Get
            Return _SelectedCasparCGServer
        End Get
        Set(value As ServerList.CasparCGServerViewModel)
            If ChangeIfDifferent(_SelectedCasparCGServer, value, "SelectedCasparCGServer") Then
                If Not _SelectedCasparCGServer Is Nothing Then
                    SelectedServerIp = _SelectedCasparCGServer.Target.Address
                End If
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
            CasparCGServers = New ViewModelCollection(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel)(New ObservableCollectionSource(Of ServerList.CasparCGServer)(ServerList.CasparCGServerCollection.Instance))
            ServerList.CasparCGServerCollection.Instance.Add(New ServerList.CasparCGServer("Test", "1.2.3.4"))
            ServerList.CasparCGServerCollection.Instance.Add(New ServerList.CasparCGServer("Test 01", "192.168.0.1"))
            Dim a As New ServerList.CasparCGServer("Blubb", "Foo Bar")
            ServerList.CasparCGServerCollection.Instance.Add(a)
            SelectedCasparCGServer = CasparCGServers.FindViewModel(a)
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

    Private Sub _CasparCGServers_ItemAdded(sender As Object, e As OnUtils.Wpf.ViewModelCollectionItemAddedEventArgs(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel)) Handles _CasparCGServers.ItemAdded
        e.ViewModel = New ServerList.CasparCGServerViewModel(e.Item)
        AddHandler e.ViewModel.Remove, AddressOf CasparCGServers_Item_Remove
    End Sub

    Private Sub _CasparCGServers_ItemRemoved(sender As Object, e As OnUtils.Wpf.ViewModelCollectionItemRemovedEventArgs(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel)) Handles _CasparCGServers.ItemRemoved
        RemoveHandler e.ViewModel.Remove, AddressOf CasparCGServers_Item_Remove
    End Sub

    Private Sub CasparCGServers_Item_Remove(Sender As ServerList.CasparCGServerViewModel)
        OnRemoveServer(Sender.Target)
    End Sub

End Class
