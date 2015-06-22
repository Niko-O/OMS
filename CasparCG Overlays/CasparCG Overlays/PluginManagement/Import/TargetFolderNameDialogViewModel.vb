Public Class TargetFolderNameDialogViewModel
    Inherits ViewModelBase

    Dim _DirectoryName As String = ""
    Public Property DirectoryName As String
        Get
            Return _DirectoryName
        End Get
        Set(value As String)
            ChangeIfDifferent(_DirectoryName, value, "DirectoryName")
        End Set
    End Property

    <Dependency("DirectoryName")>
    Public ReadOnly Property NameIsValid As Boolean
        Get
            Return Not String.IsNullOrWhiteSpace(_DirectoryName) AndAlso _
                   Not System.IO.Directory.Exists(System.IO.Path.Combine(ImportPluginDialog.PluginsDirectory.FullName, _DirectoryName)) AndAlso _
                   Not _DirectoryName.Any(AddressOf System.IO.Path.GetInvalidPathChars.Contains)
        End Get
    End Property

    Public Sub New()
        MyBase.New(True)
    End Sub

End Class