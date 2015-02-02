
<Export(GetType(PluginInterfaces.IPlugin))>
<ExportMetadata("DisplayName", "Fußball")>
<ExportMetadata("PluginGuid", "94f67cc3-2dd7-4e3b-9398-d01335bf4e75")>
Public Class FootballPlugin
    Implements PluginInterfaces.IPlugin

    Dim Settings As New TestStructure

    Public Function GetSnapIn() As System.Windows.Controls.UserControl Implements PluginInterfaces.IPlugin.GetSnapIn
        Return Singleton(Of FootballSnapIn).Instance
    End Function

    Public Sub Created() Implements PluginInterfaces.IPlugin.Created
        PluginInterfaces.PublicProviders.PluginSettings.LoadSettings(Settings)
    End Sub

    Public Sub Unloaded() Implements PluginInterfaces.IPlugin.Unloaded
        PluginInterfaces.PublicProviders.PluginSettings.SaveSettings(Settings)
    End Sub

    Public Sub Enabled() Implements PluginInterfaces.IPlugin.Enabled
        PluginInterfaces.Composition.Compose(Countries.Instance)
        Settings.Foo = "Welt"
        Settings.Container.Bar = 1324
    End Sub

    Public Sub Disabled() Implements PluginInterfaces.IPlugin.Disabled

    End Sub

End Class
