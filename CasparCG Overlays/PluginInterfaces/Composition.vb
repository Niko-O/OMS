
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
        Dim Info As New System.IO.DirectoryInfo(DirectoryPath)
        RecursiveAddDirectory(Info)
        RaiseEvent CatalogChanged()
    End Sub

    Private Shared Sub RecursiveAddDirectory(Info As System.IO.DirectoryInfo)
        If DirectoryCatalogs.Any(Function(i) i.FullPath = Info.FullName) Then
            Return
        End If
        If Info.Exists Then
            Info.Create()
        End If
        Dim NewCatalog As New DirectoryCatalog(Info.FullName)
        DirectoryCatalogs.Add(NewCatalog)
        Catalog.Catalogs.Add(NewCatalog)
        For Each i In Info.GetDirectories
            RecursiveAddDirectory(i)
        Next
    End Sub

    ''' <summary>
    ''' Führt eine Komposition für das angegebene Objekt durch und befüllt die <see cref="ImportAttribute"/>- und <see cref="ImportManyAttribute"/>-Schnittstellen.
    ''' </summary>
    ''' <param name="Obj">Das Objekt, dessen Kompositions-Schnittstellen befüllt werden.</param>
    Public Shared Sub Compose(Obj As Object)
        Compositions.ComposeParts(Obj)
    End Sub

    Private Shared Catalog As New AggregateCatalog
    Private Shared DirectoryCatalogs As New List(Of DirectoryCatalog)
    Private Shared Compositions As New CompositionContainer(Catalog)

End Class
