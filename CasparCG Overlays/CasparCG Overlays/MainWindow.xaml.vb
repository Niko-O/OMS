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

End Class
