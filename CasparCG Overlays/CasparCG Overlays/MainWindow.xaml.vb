Class MainWindow 

    Dim Model As MainWindowViewModel

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, MainWindowViewModel)
        Model.Plugins = PluginManagement.PluginContainer.Instance.Plugins
        For Each i In PluginManagement.PluginContainer.Instance.Plugins
            If PluginManagement.PluginActiveStates.IsInUse(i.PluginGuid) Then
                i.Enabled()
            End If
        Next
    End Sub

    Protected Overrides Sub OnClosing(e As System.ComponentModel.CancelEventArgs)
#If Not Debug Then
        e.Cancel = (MessageBox.Show("Sicher, dass Sie OMS schließen wollen?", "Schließen?", MessageBoxButton.YesNo) = MessageBoxResult.No)
#End If
        MyBase.OnClosing(e)
    End Sub

    Private Sub ToggleCasparServerConnection(sender As System.Object, e As System.Windows.RoutedEventArgs)
        If CasparServer.Instance.IsConnected Then
            CasparServer.Instance.Disconnect()
        Else
            CasparServer.Instance.IpAddress = Model.SelectedServerIp
            CasparServer.Instance.Connect()
        End If
    End Sub

End Class
