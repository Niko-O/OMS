Class MainWindowViewModel
    Inherits ViewModelBase

    Public Event RemoveServer As EventHandler(Of ServerList.RemoveServerEventArgs)
    Protected Overridable Sub OnRemoveServer(Server As ServerList.CasparCGServer)
        RaiseEvent RemoveServer(Me, New ServerList.RemoveServerEventArgs(Server))
    End Sub

    Dim _WindowWidth As Double = 1
    Public Property WindowWidth As Double
        Get
            Return _WindowWidth
        End Get
        Set(value As Double)
            ChangeIfDifferent(_WindowWidth, value, "WindowWidth")
        End Set
    End Property

    Dim _WindowHeight As Double = 2
    Public Property WindowHeight As Double
        Get
            Return _WindowHeight
        End Get
        Set(value As Double)
            ChangeIfDifferent(_WindowHeight, value, "WindowHeight")
        End Set
    End Property

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

    Dim WithEvents _Plugins As ViewModelCollection(Of PluginManagement.PluginWrapper, PluginManagement.PluginViewModel) = Nothing
    Public Property Plugins As ViewModelCollection(Of PluginManagement.PluginWrapper, PluginManagement.PluginViewModel)
        Get
            Return _Plugins
        End Get
        Set(value As ViewModelCollection(Of PluginManagement.PluginWrapper, PluginManagement.PluginViewModel))
            If ChangeIfDifferent(_Plugins, value) Then
                OnPropertyChanged("Plugins")
                ActualizeMainTabItems()
            End If
            'ActualizeMainTabItems()
            'ActualizePluginViewModels()
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

    Dim _WaitingForConnection As Boolean = False
    Public Property WaitingForConnection As Boolean
        Get
            Return _WaitingForConnection
        End Get
        Set(value As Boolean)
            ChangeIfDifferent(_WaitingForConnection, value, "WaitingForConnection")
        End Set
    End Property

    Dim _OccurredException As Exception = Nothing
    Public Property OccurredException As Exception
        Get
            Return _OccurredException
        End Get
        Set(value As Exception)
            ChangeIfDifferent(_OccurredException, value, "OccurredException")
        End Set
    End Property

    <Dependency("OccurredException")>
    Public ReadOnly Property OccurredExceptionInfo As String
        Get
            If _OccurredException Is Nothing Then
                Return Nothing
            Else
                Return "Verbindung fehlgeschlagen: " & _OccurredException.GetType.Name & Environment.NewLine & _OccurredException.Message
            End If
        End Get
    End Property

    <Dependency("WaitingForConnection")>
    Public ReadOnly Property ToggleCasparServerConnectionButtonText As String
        Get
            If _WaitingForConnection Then
                Return "Verbindung wird hergestellt..."
            End If
            If CasparServer.Instance.IsConnected Then
                Return "Verbindung trennen"
            Else
                Return "Verbindung herstellen"
            End If
        End Get
    End Property

    <Dependency("WaitingForConnection")>
    Public ReadOnly Property CanToggleConnection As Boolean
        Get
            Return Not _WaitingForConnection
        End Get
    End Property

    Public Sub New()
        MyBase.New(True)
        AddExternalPropertyDependency("ToggleCasparServerConnectionButtonText", CasparServer.Instance, "IsConnected")
        If IsInDesignMode Then
            Dim Temp As New ObservableCollection(Of PluginManagement.PluginWrapper)
            Plugins = New ViewModelCollection(Of PluginManagement.PluginWrapper, PluginManagement.PluginViewModel)(New ObservableCollectionSource(Of PluginManagement.PluginWrapper)(Temp))
            Temp.Add(New DesignerSupport.DesignerSupportPlugin("Tennis"))
            Temp.Add(New DesignerSupport.DesignerSupportPlugin("Fußball"))
            Temp.Add(New DesignerSupport.DesignerSupportPlugin("Anderer Stuff"))
            CasparCGServers = New ViewModelCollection(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel)(New ObservableCollectionSource(Of ServerList.CasparCGServer)(ServerList.CasparCGServerCollection.Instance))
            ServerList.CasparCGServerCollection.Instance.Add(New ServerList.CasparCGServer("Test", "1.2.3.4"))
            ServerList.CasparCGServerCollection.Instance.Add(New ServerList.CasparCGServer("Test 01", "192.168.0.1"))
            Dim a As New ServerList.CasparCGServer("Blubb", "Foo Bar")
            ServerList.CasparCGServerCollection.Instance.Add(a)
            SelectedCasparCGServer = CasparCGServers.FindViewModel(a)
        End If
    End Sub

    Private Sub ActualizeMainTabItems()
        If Not _MainTabItems Is Nothing Then
            For Each i In _MainTabItems
                i.UnloadPlugin()
            Next
        End If
        Dim Temp As New List(Of PluginManagement.TabControl.PluginTabItem)
        For Each i In _Plugins.ViewModels
            If i.Target.IsInUse Then

                Temp.Add(New PluginManagement.TabControl.PluginTabItem(i.Target))
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

    Private Sub _Plugins_CollectionChanged(sender As Object, e As System.EventArgs) Handles _Plugins.CollectionChanged
        ActualizeMainTabItems()
    End Sub

    Private Sub _Plugins_ItemAdded(sender As Object, e As OnUtils.Wpf.ViewModelCollectionItemAddedEventArgs(Of PluginManagement.PluginWrapper, PluginManagement.PluginViewModel)) Handles _Plugins.ItemAdded
        e.ViewModel = New PluginManagement.PluginViewModel(e.Item)
        AddHandler e.ViewModel.Target.IsInUseChanged, AddressOf ActualizeMainTabItems
    End Sub

    Private Sub _Plugins_ItemRemoved(sender As Object, e As OnUtils.Wpf.ViewModelCollectionItemRemovedEventArgs(Of PluginManagement.PluginWrapper, PluginManagement.PluginViewModel)) Handles _Plugins.ItemRemoved
        RemoveHandler e.ViewModel.Target.IsInUseChanged, AddressOf ActualizeMainTabItems
    End Sub

End Class
