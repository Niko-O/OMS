Public Class PluginTabItem
    Inherits MainTabItem

    Dim _Plugin As Plugin
    Public ReadOnly Property Plugin As Plugin
        Get
            Return _Plugin
        End Get
    End Property

    Public Sub New(NewPlugin As Plugin)
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
