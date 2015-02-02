Public Class DesignerSupportPluginSource
    Inherits Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata)

    Public Sub New(NewDebugName As String)
        MyBase.New(Function() New DesignerSupportPluginImplementation, New DesignerSupportPluginMetadataImplementation(NewDebugName))
    End Sub

End Class
