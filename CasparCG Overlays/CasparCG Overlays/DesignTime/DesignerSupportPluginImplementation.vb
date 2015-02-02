Public Class DesignerSupportPluginImplementation
    Implements PluginInterfaces.IPlugin

    Public Function GetSnapIn() As System.Windows.Controls.UserControl Implements PluginInterfaces.IPlugin.GetSnapIn
        Return New UserControl
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
