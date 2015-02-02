
<Export(GetType(PluginInterfaces.IPlugin))>
<ExportMetadata("DisplayName", "Tennis")>
<ExportMetadata("PluginGuid", "07a07076-2c85-486c-b824-5bb4c85e7271")>
Public Class TennisEventPlugin
    Implements PluginInterfaces.IPlugin

    Public Function GetGuiSnapIn() As System.Windows.Controls.UserControl Implements PluginInterfaces.IPlugin.GetSnapIn
        Return Singleton(Of TennisSnapIn).Instance
    End Function

    Public Sub Created() Implements PluginInterfaces.IPlugin.Created

    End Sub

    Public Sub Unloaded() Implements PluginInterfaces.IPlugin.Unloaded

    End Sub

    Public Sub Enabled() Implements PluginInterfaces.IPlugin.Enabled

    End Sub

    Public Sub Disabled() Implements PluginInterfaces.IPlugin.Disabled

    End Sub

End Class
