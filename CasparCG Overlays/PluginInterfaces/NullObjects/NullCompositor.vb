
''' <summary>
''' Stellt eine minimalistische Implementierung von <see cref="ICompositor"/> dar, die keine Funktionalitäten beinhaltet.
''' </summary>
Public Class NullCompositor
    Implements ICompositor

    Public Event CatalogChanged(sender As Object, e As System.EventArgs) Implements ICompositor.CatalogChanged

    Public Sub AddPluginDirectoryPath(DirectoryPath As String, Recursive As Boolean) Implements ICompositor.AddPluginDirectoryPath
    End Sub

    Public Sub Compose(Obj As Object) Implements ICompositor.Compose
    End Sub

End Class
