
''' <summary>
''' Stellt einen gemeinsamen Sammelpunkt für Komposition bereit.
''' </summary>
Public Class Composition

    ''' <summary>
    ''' Wird ausgelöst, wenn ein Katalog hinzugefügt wird.
    ''' </summary>
    Public Shared Event CatalogChanged()

    ''' <summary>
    ''' Fügt den angegebenen Ordner der Liste an Pfaden, in denen nach Kompositions-Assemblies gesucht wird, hinzu.
    ''' Wenn der Ordner nicht existiert, wird er erstellt.
    ''' </summary>
    ''' <param name="DirectoryPath">Der Ordnerpfad, an dem nach Kompositions-Assemblies gesucht wird.</param>
    Public Shared Sub AddPluginDirectoryPath(DirectoryPath As String)
        If Not System.IO.Directory.Exists(DirectoryPath) Then
            System.IO.Directory.CreateDirectory(DirectoryPath)
        End If
        Catalog.Catalogs.Add(New DirectoryCatalog(DirectoryPath))
        RaiseEvent CatalogChanged()
    End Sub

    ''' <summary>
    ''' Führt eine Komposition für das angegebene Objekt durch und befüllt die <see cref="ImportAttribute"/>- und <see cref="ImportManyAttribute"/>-Schnittstellen.
    ''' </summary>
    ''' <param name="Obj">Das Objekt, dessen Kompositions-Schnittstellen befüllt werden.</param>
    Public Shared Sub Compose(Obj As Object)
        Compositions.ComposeParts(Obj)
    End Sub

    Private Shared Catalog As New AggregateCatalog
    Private Shared Compositions As New CompositionContainer(Catalog)

End Class
