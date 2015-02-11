
''' <summary>
''' Stellt Methoden zur Verfügung, mit denen Einstellungen geladen und gespeichert werden können.
''' Einstellungen werden anhand des Laufzeittyps der an die <see cref="ISettingsProvider.SaveSettings"/>- und <see cref="ISettingsProvider.LoadSettings"/>-Funktionen übergebenen <see cref="PluginSettings.SettingsStructure"/> identifiziert.
''' Mehrere Plugins, die unterschiedliche Instanzen des selben <see cref="PluginSettings.SettingsStructure"/>-Typs laden, verwenden automatisch die selben Einstellungen.
''' Für weitere Details siehe Systemdokumentation.
''' </summary>
Public Interface ISettingsProvider

    ''' <summary>
    ''' Speichert rekursiv die in der angegebenen Einstellungs-Struktur enthaltenen Eigenschaften.
    ''' </summary>
    ''' <param name="Settings">Die Einstellungs-Struktur, derer Eigenschaften gespeichert werden.</param>
    Sub SaveSettings(Settings As PluginSettings.SettingsStructure)

    ''' <summary>
    ''' Lädt rekursiv die Eigenschaften für die angegebene Einstellungs-Struktur.
    ''' Gibt False zurück, wenn für die angegebene Einstellungs-Struktur keine Einstellungen gefunden wurden.
    ''' </summary>
    ''' <param name="Settings">Die Einstellungs-Struktur, derer Eigenschaften geladen werden.</param>
    Function LoadSettings(Settings As PluginSettings.SettingsStructure) As Boolean

End Interface
