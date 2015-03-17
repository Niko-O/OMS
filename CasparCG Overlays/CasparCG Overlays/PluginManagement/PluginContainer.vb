
Namespace PluginManagement

    ''' <summary>
    ''' Beinhaltet eine Liste der verfügbaren Plugins.
    ''' Singleton.
    ''' </summary>
    Public Class PluginContainer
        Inherits Singleton(Of PluginContainer)

        Dim _Plugins As New List(Of PluginWrapper)
        ''' <summary>
        ''' Die Liste der verfügbaren Plugins.
        ''' </summary>
        Public ReadOnly Property Plugins As IEnumerable(Of PluginWrapper)
            Get
                Return _Plugins
            End Get
        End Property

        <ImportMany(GetType(PluginInterfaces.IPlugin))>
        Dim ImportedPlugins As IEnumerable(Of Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata))

        Public Sub New()
            PluginInterfaces.PublicProviders.MefCompositor.Compose(Me)
            For Each i In ImportedPlugins
                _Plugins.Add(New PluginWrapper(i))
            Next
        End Sub

    End Class

End Namespace
