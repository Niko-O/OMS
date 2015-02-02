
''' <summary>
''' Stellt Methoden zur Verfügung, mit denen Einstellungen geladen und gespeichert werden können.
''' Einstellungen sind Plugin-Typ-lokal. D.h. mehrere Instanzen des gleichen Plugin-Typs verwenden die gleichen Einstellungen. Davon abgesehen sollte nie mehr als eine Instanz jedes Plugins erstellt werden.
''' </summary>
Public Interface ISettingsProvider

    ''' <summary>
    ''' Speichert rekursiv die in der angegebenen Einstellungs-Struktur enthaltenen Eigenschaften.
    ''' </summary>
    ''' <param name="Settings">Die Einstellungs-Struktur, derer Eigenschaften gespeichert werden.</param>
    Sub SaveSettings(Settings As PluginSettings.SettingsStructure)

    ''' <summary>
    ''' Lädt rekursiv die Eigenschaften für die angegebene Einstellungs-Struktur.
    ''' </summary>
    ''' <param name="Settings">Die Einstellungs-Struktur, derer Eigenschaften geladen werden.</param>
    Sub LoadSettings(Settings As PluginSettings.SettingsStructure)

End Interface
