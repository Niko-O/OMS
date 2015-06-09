
''' <summary>
''' Stellt eine minimalistische Implementierung von <see cref="ISettingsProvider"/> dar, die keine Funktionalitäten beinhaltet.
''' </summary>
Friend Class NullSettingsProvider
    Implements ISettingsProvider

    Public Function LoadSettings(Settings As PluginSettings.SettingsStructure) As Boolean Implements ISettingsProvider.LoadSettings
        Return False
    End Function

    Public Sub SaveSettings(Settings As PluginSettings.SettingsStructure) Implements ISettingsProvider.SaveSettings
    End Sub

End Class
