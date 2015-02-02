
''' <summary>
''' Stellt Metadaten (Name und Guid) für Plugins dar.
''' </summary>
Public Interface IPluginMetadata

    ''' <summary>
    ''' Der Anzeigename des Plugins.
    ''' </summary>
    ReadOnly Property DisplayName As String

    ''' <summary>
    ''' Die String-Darstellung der Plugin-Id als <see cref="Guid"/>.
    ''' Beispiel: "28561b54-68c3-4941-a84b-e25fc612b069"
    ''' </summary>
    ReadOnly Property PluginGuid As String

End Interface
