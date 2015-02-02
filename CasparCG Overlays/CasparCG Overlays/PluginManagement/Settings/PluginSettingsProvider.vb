
''' <summary>
''' Agiert als <see cref="PluginInterfaces.ISettingsProvider"/> und speichert Einstellungen für Plugins.
''' </summary>
Public Class PluginSettingsProvider
    Inherits Singleton(Of PluginSettingsProvider)
    Implements PluginInterfaces.ISettingsProvider

    Public Sub LoadSettings(Settings As PluginSettings.SettingsStructure) Implements PluginInterfaces.ISettingsProvider.LoadSettings

    End Sub

    Public Sub SaveSettings(Settings As PluginSettings.SettingsStructure) Implements PluginInterfaces.ISettingsProvider.SaveSettings

    End Sub

End Class
