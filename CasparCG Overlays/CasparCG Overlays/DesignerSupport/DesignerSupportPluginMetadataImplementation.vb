
Namespace DesignerSupport

    Public Class DesignerSupportPluginMetadataImplementation
        Implements PluginInterfaces.IPluginMetadata

        Dim _DisplayName As String
        Public ReadOnly Property DisplayName As String Implements PluginInterfaces.IPluginMetadata.DisplayName
            Get
                Return _DisplayName
            End Get
        End Property

        Public ReadOnly Property PluginGuid As String Implements PluginInterfaces.IPluginMetadata.PluginGuid
            Get
                Static Temp As String = Guid.NewGuid.ToString
                Return Temp
            End Get
        End Property

        Public Sub New(NewDisplayName As String)
            _DisplayName = NewDisplayName
        End Sub

    End Class

End Namespace
