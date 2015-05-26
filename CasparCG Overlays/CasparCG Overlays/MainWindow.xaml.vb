Class MainWindow

    Dim ViewModel As MainWindowViewModel

    Public Sub New()
        InitializeComponent()
        ViewModel = DirectCast(Me.DataContext, MainWindowViewModel)
        ViewModel.CasparCGServers = New ViewModelCollection(Of ServerList.CasparCGServer, ServerList.CasparCGServerViewModel)(New ObservableCollectionSource(Of ServerList.CasparCGServer)(ServerList.CasparCGServerCollection.Instance))
        ViewModel.CasparCGServers.SynchronizeWithList(ServerList.CasparCGServerCollection.Instance)
        For Each i In PluginManagement.PluginContainer.Instance.Plugins
            If PluginManagement.PluginActiveStates.IsInUse(i.PluginGuid) Then
                i.Enabled()
            End If
        Next
        ViewModel.Plugins = PluginManagement.PluginContainer.Instance.Plugins
        AddHandler Me.SizeChanged, AddressOf OnSizeChanged
    End Sub

    Protected Overrides Sub OnClosing(e As System.ComponentModel.CancelEventArgs)
#If Not Debug Then
        e.Cancel = (MessageBox.Show("Sicher, dass Sie OMS schließen wollen?", "Schließen?", MessageBoxButton.YesNo) = MessageBoxResult.No)
#End If
        MyBase.OnClosing(e)
    End Sub

    Protected Overrides Sub OnContentRendered(e As System.EventArgs)
        MyBase.OnContentRendered(e)
        LoadWindowSizeFromSettings()
    End Sub

    Private Sub ToggleCasparServerConnection(sender As System.Object, e As System.Windows.RoutedEventArgs)
        If CasparServer.Instance.IsConnected Then
            CasparServer.Instance.Disconnect()
        Else
            CasparServer.Instance.IpAddress = ViewModel.SelectedServerIp
            CasparServer.Instance.Port = 5250
            CasparServer.Instance.Connect()
        End If
    End Sub

    Private Sub RemoveSelectedServer(sender As System.Object, e As System.Windows.RoutedEventArgs)
        ServerList.CasparCGServerCollection.Instance.Remove(ViewModel.SelectedCasparCGServer.Target)
    End Sub

    Private Sub AddNewServer(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim Temp As New ServerList.CasparCGServer("Name", "Adresse")
        ServerList.CasparCGServerCollection.Instance.Add(Temp)
        ViewModel.CasparCGServers.FindViewModel(Temp).IsInEditMode = True
    End Sub

    Private Sub OnSizeChanged(sender As Object, e As SizeChangedEventArgs)
        ViewModel.WindowWidth = e.NewSize.Width
        ViewModel.WindowHeight = e.NewSize.Height
    End Sub

    Private Sub SaveWindowSizeToSettings(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Settings.MainWindow.SavedSize.Width = Me.Width
        Settings.MainWindow.SavedSize.Height = Me.Height
    End Sub

    Private Sub LoadWindowSizeFromSettings(Optional sender As System.Object = Nothing, Optional e As System.Windows.RoutedEventArgs = Nothing)
        Me.Width = Settings.MainWindow.SavedSize.Width
        Me.Height = Settings.MainWindow.SavedSize.Height
    End Sub

End Class
