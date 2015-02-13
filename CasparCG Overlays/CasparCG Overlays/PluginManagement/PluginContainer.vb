
Namespace PluginManagement

    ''' <summary>
    ''' Beinhaltet eine Liste der verfügbaren Plugins.
    ''' Singleton.
    ''' </summary>
    Public Class PluginContainer
        Inherits Singleton(Of PluginContainer)

        Dim _Plugins As New List(Of Plugin)
        ''' <summary>
        ''' Die Liste der verfügbaren Plugins.
        ''' </summary>
        Public ReadOnly Property Plugins As IEnumerable(Of Plugin)
            Get
                Return _Plugins
            End Get
        End Property

        <ImportMany(GetType(PluginInterfaces.IPlugin))>
        Dim ImportedPlugins As IEnumerable(Of Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata))

        Public Sub New()
            PluginInterfaces.Composition.Compose(Me)
            For Each i In ImportedPlugins
                _Plugins.Add(New Plugin(i))
            Next
        End Sub

    End Class

End Namespace
