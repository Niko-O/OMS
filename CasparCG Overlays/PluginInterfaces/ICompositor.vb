
''' <summary>
''' Stellt methoden zur Verfügung, mit denen MEF-Komposition durchgeführt werden kann.
''' </summary>
Public Interface ICompositor

    ''' <summary>
    ''' Wird ausgelöst, wenn ein Katalog hinzugefügt wird.
    ''' </summary>
    Event CatalogChanged As EventHandler

    ''' <summary>
    ''' Fügt den angegebenen Ordner der Liste an Pfaden, in denen nach Kompositions-Assemblies gesucht wird, hinzu.
    ''' Wenn der Ordner nicht existiert, wird er erstellt.
    ''' </summary>
    ''' <param name="DirectoryPath">Der Pfad des Ordners, in dem nach Kompositions-Assemblies gesucht wird.</param>
    ''' <param name="Recursive">Gibt an, ob alle Unterordner des angegebenen Ordners ebenfalls hinzugefügt werden.</param>
    Sub AddPluginDirectoryPath(DirectoryPath As String, Recursive As Boolean)

    ''' <summary>
    ''' Führt eine Komposition für das angegebene Objekt durch und befüllt die <see cref="ImportAttribute"/>- und <see cref="ImportManyAttribute"/>-Schnittstellen.
    ''' </summary>
    ''' <param name="Obj">Das Objekt, dessen Kompositions-Schnittstellen befüllt werden.</param>
    Sub Compose(Obj As Object)

End Interface
