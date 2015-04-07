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

End Class
