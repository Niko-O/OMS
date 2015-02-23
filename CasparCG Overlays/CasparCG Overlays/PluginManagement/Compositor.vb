
Namespace PluginManagement

    ''' <summary>
    ''' Agiert als <see cref="PluginInterfaces.ICompositor"/>.
    ''' </summary>
    Public Class Compositor
        Inherits Singleton(Of Compositor)
        Implements PluginInterfaces.ICompositor

        Public Event CatalogChanged(sender As Object, e As System.EventArgs) Implements PluginInterfaces.ICompositor.CatalogChanged

        Public Sub AddPluginDirectoryPath(DirectoryPath As String, Recursive As Boolean) Implements PluginInterfaces.ICompositor.AddPluginDirectoryPath
            Dim Info As New System.IO.DirectoryInfo(DirectoryPath)
            If Recursive Then
                RecursiveAddDirectoryCatalog(Info)
            Else
                AddDirectoryCatalog(Info)
            End If
            RaiseEvent CatalogChanged(Me, EventArgs.Empty)
        End Sub

        Private Sub RecursiveAddDirectoryCatalog(Info As System.IO.DirectoryInfo)
            If DirectoryCatalogs.Any(Function(i) i.FullPath = Info.FullName) Then
                Return
            End If
            If Info.Exists Then
                Info.Create()
            End If
            AddDirectoryCatalog(Info)
            For Each i In Info.GetDirectories
                RecursiveAddDirectoryCatalog(i)
            Next
        End Sub

        Private Sub AddDirectoryCatalog(Info As System.IO.DirectoryInfo)
            Dim NewCatalog As New DirectoryCatalog(Info.FullName)
            DirectoryCatalogs.Add(NewCatalog)
            Catalog.Catalogs.Add(NewCatalog)
        End Sub

        Public Sub Compose(Obj As Object) Implements PluginInterfaces.ICompositor.Compose
            Compositions.ComposeParts(Obj)
        End Sub

        Dim Catalog As New AggregateCatalog
        Dim DirectoryCatalogs As New List(Of DirectoryCatalog)
        Dim Compositions As New CompositionContainer(Catalog)

    End Class

End Namespace
