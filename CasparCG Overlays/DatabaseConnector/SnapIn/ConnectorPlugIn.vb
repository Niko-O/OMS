
<Export(GetType(PluginInterfaces.IPlugin))>
<ExportMetadata("DisplayName", "SQL-Server")>
<ExportMetadata("PluginGuid", "e92ea1c6-65c1-4025-b3de-796042d55953")>
Public Class ConnectorPlugIn
    Implements PluginInterfaces.IPlugin

    Public Function GetSnapIn() As System.Windows.Controls.UserControl Implements PluginInterfaces.IPlugin.GetSnapIn
        Return Singleton(Of ConnectorSnapIn).Instance
    End Function

    Public Sub Created() Implements PluginInterfaces.IPlugin.Created
        PluginInterfaces.PublicProviders.PluginSettings.LoadSettings(DataBaseConnectorsSettings.Instance)
        Connector.Instance.ServerName = DataBaseConnectorsSettings.Instance.ServerName
        Connector.Instance.SchemaName = DataBaseConnectorsSettings.Instance.SchemaName
        Connector.Instance.UserName = DataBaseConnectorsSettings.Instance.UserName
    End Sub

    Public Sub Unloaded() Implements PluginInterfaces.IPlugin.Unloaded
        PluginInterfaces.PublicProviders.PluginSettings.SaveSettings(DataBaseConnectorsSettings.Instance)
    End Sub

    Public Sub Enabled() Implements PluginInterfaces.IPlugin.Enabled

    End Sub

    Public Sub Disabled() Implements PluginInterfaces.IPlugin.Disabled
        If Connector.Instance.IsConnected Then
            Connector.Instance.Disconnect()
        End If
    End Sub

End Class
