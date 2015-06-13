
Namespace PluginManagement

    ''' <summary>
    ''' Beinhaltet eine Liste der verfügbaren Plugins.
    ''' Singleton.
    ''' </summary>
    Public Class PluginContainer
        Inherits Singleton(Of PluginContainer)

        Dim _Plugins As New ObservableCollection(Of PluginWrapper)
        ''' <summary>
        ''' Die Liste der verfügbaren Plugins.
        ''' </summary>
        Public ReadOnly Property Plugins As ObservableCollection(Of PluginWrapper)
            Get
                Return _Plugins
            End Get
        End Property

        Dim Target As New CompositionTarget

        Public Sub New()
            AddHandler Compositor.Instance.CatalogChanged, Sub() ActualizePlugins()
            Compositor.Instance.Compose(Target)
            ActualizePlugins()
        End Sub

        Private Sub ActualizePlugins()
            _Plugins.Synchronize(Target.ImportedPlugins, _
                                 Function(Target, Source) Target.PluginGuid = Guid.Parse(Source.Metadata.PluginGuid), _
                                 Function(Source)
                                     Dim Temp As New PluginWrapper(Source)
                                     Temp.Created()
                                     If Temp.IsInUse Then
                                         Temp.Enabled()
                                     End If
                                     Return Temp
                                 End Function,
                                 Sub(ToDispose) ToDispose.Unloaded())
        End Sub

        Public Sub UnloadAllPlugins()
            For Each i In _Plugins
                If i.IsInUse Then
                    i.Disabled()
                End If
            Next
            For Each i In _Plugins
                i.Unloaded()
            Next
        End Sub

        Private Class CompositionTarget

            <ImportMany(GetType(PluginInterfaces.IPlugin), AllowRecomposition:=True)>
            Public ImportedPlugins As IEnumerable(Of Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata))

        End Class

    End Class

End Namespace
