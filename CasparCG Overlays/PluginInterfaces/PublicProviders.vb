Public Class PublicProviders

    Private Shared _PluginSettings As ISettingsProvider = Nothing
    Public Shared ReadOnly Property PluginSettings As ISettingsProvider
        Get
            Return _PluginSettings
        End Get
    End Property

    Private Shared _CasparServer As ICasparServer = Nothing
    Public Shared ReadOnly Property CasparServer As ICasparServer
        Get
            Return _CasparServer
        End Get
    End Property

    Public Shared Sub Initialize(NewPluginSettings As ISettingsProvider, NewCasparServer As ICasparServer)
        If Not (_PluginSettings Is Nothing AndAlso _CasparServer Is Nothing) Then
            Throw New InvalidOperationException("Diese Methode darf nur vom Hostprogramm aufgerufen werden.")
        End If
        _PluginSettings = NewPluginSettings
        _CasparServer = NewCasparServer
    End Sub

End Class
