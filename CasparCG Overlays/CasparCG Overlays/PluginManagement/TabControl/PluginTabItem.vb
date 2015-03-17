
Namespace PluginManagement.TabControl

    ''' <summary>
    ''' Ein TabItem, das im <see cref="MainTabControl"/> ein Plugin darstellt.
    ''' </summary>
    Public Class PluginTabItem
        Inherits MainTabItem

        Dim _Plugin As PluginWrapper
        ''' <summary>
        ''' Das <see cref="Plugin"/>.
        ''' </summary>
        Public ReadOnly Property Plugin As PluginWrapper
            Get
                Return _Plugin
            End Get
        End Property

        Public Sub New(NewPlugin As PluginWrapper)
            _Plugin = NewPlugin
            Me.Header = _Plugin.DisplayName
            Me.Content = _Plugin.GetSnapIn
        End Sub

        Public Sub UnloadPlugin()
            _Plugin = Nothing
            Me.Header = Nothing
            Me.Content = Nothing
        End Sub

    End Class

End Namespace
