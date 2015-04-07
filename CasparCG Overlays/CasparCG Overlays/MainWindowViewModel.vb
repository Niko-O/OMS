Class MainWindowViewModel
    Inherits ViewModelBase

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

    Dim _Test_CasparCGServers As Test_CasparCGServer()
    Public ReadOnly Property Test_CasparCGServers As Test_CasparCGServer()
        Get
            Return _Test_CasparCGServers
        End Get
    End Property

    Dim _SelectedCasparCGServer As Test_CasparCGServer = Nothing
    Public Property SelectedCasparCGServer As Test_CasparCGServer
        Get
            Return _SelectedCasparCGServer
        End Get
        Set(value As Test_CasparCGServer)
            If ChangeIfDifferent(_SelectedCasparCGServer, value, "SelectedCasparCGServer") Then
                If Not _SelectedCasparCGServer Is Nothing Then
                    SelectedServerIp = _SelectedCasparCGServer.IpAddress
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
        End If
        _Test_CasparCGServers = {
            New Test_CasparCGServer With {.Name = "Testserver 1", .IpAddress = "192.168.10.123"}, _
            New Test_CasparCGServer With {.Name = "Lokal", .IpAddress = "127.0.0.1"}
        }
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
