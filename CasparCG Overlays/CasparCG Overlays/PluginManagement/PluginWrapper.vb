
Namespace PluginManagement

    ''' <summary>
    ''' Kapselt ein <see cref="PluginInterfaces.IPlugin"/> und seine Metadaten.
    ''' </summary>
    Public Class PluginWrapper
        Implements PluginInterfaces.IPlugin

        Dim Source As Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata)

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPluginMetadata.DisplayName"/>
        ''' </summary>
        Public ReadOnly Property DisplayName As String
            Get
                Return Source.Metadata.DisplayName
            End Get
        End Property

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPluginMetadata.PluginGuid"/>
        ''' </summary>
        Public ReadOnly Property PluginGuid As Guid
            Get
                Return Guid.Parse(Source.Metadata.PluginGuid)
            End Get
        End Property

        Public Sub New(NewSource As Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata))
            Source = NewSource
            AddHandler PluginActiveStates.IsInUseChanged, AddressOf CheckIsInUseChanged
        End Sub

        Private Sub CheckIsInUseChanged(sender As Object, e As PluginActiveStates.IsInUseChangedEventArgs)
            If e.PluginGuid = PluginGuid Then
                If e.NewState Then
                    Enabled()
                Else
                    Disabled()
                End If
            End If
        End Sub

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPlugin.GetSnapIn"/>
        ''' </summary>
        Public Function GetSnapIn() As UserControl Implements PluginInterfaces.IPlugin.GetSnapIn
            Return Source.Value.GetSnapIn
        End Function

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPlugin.Created"/>
        ''' </summary>
        Public Sub Created() Implements PluginInterfaces.IPlugin.Created
            Source.Value.Created()
        End Sub

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPlugin.Unloaded"/>
        ''' </summary>
        Public Sub Unloaded() Implements PluginInterfaces.IPlugin.Unloaded
            Source.Value.Unloaded()
        End Sub

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPlugin.Enabled"/>
        ''' </summary>
        Public Sub Enabled() Implements PluginInterfaces.IPlugin.Enabled
            Source.Value.Enabled()
        End Sub

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPlugin.Disabled"/>
        ''' </summary>
        Public Sub Disabled() Implements PluginInterfaces.IPlugin.Disabled
            Source.Value.Disabled()
        End Sub

    End Class

End Namespace
