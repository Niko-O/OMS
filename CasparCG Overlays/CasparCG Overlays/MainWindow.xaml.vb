Class MainWindow 

    Dim Model As MainWindowViewModel

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, MainWindowViewModel)
        Model.Plugins = PluginContainer.Instance.Plugins
        For Each i In PluginContainer.Instance.Plugins
            If PluginActiveStates.IsInUse(i.PluginGuid) Then
                i.Enabled()
            End If
        Next
    End Sub

End Class
