Public Class ImportPluginDialogViewModel
    Inherits ViewModelBase

    Dim _SourceDirectoryPath As String = ""
    Public Property SourceDirectoryPath As String
        Get
            Return _SourceDirectoryPath
        End Get
        Set(value As String)
            ChangeIfDifferent(_SourceDirectoryPath, value, "SourceDirectoryPath")
        End Set
    End Property

    <Dependency("SourceDirectoryPath")>
    Public ReadOnly Property SourceDirectoryPathIsValid As Boolean
        Get
            Return System.IO.Directory.Exists(_SourceDirectoryPath)
        End Get
    End Property

    Dim _TargetDirectoryList As ObservableCollection(Of PluginManagement.Import.TargetDirectory) = Nothing
    Public Property TargetDirectoryList As ObservableCollection(Of PluginManagement.Import.TargetDirectory)
        Get
            Return _TargetDirectoryList
        End Get
        Set(value As ObservableCollection(Of PluginManagement.Import.TargetDirectory))
            ChangeIfDifferent(_TargetDirectoryList, value, "TargetDirectoryList")
        End Set
    End Property

    Dim _SelectedTargetDirectory As PluginManagement.Import.TargetDirectory = Nothing
    Public Property SelectedTargetDirectory As PluginManagement.Import.TargetDirectory
        Get
            Return _SelectedTargetDirectory
        End Get
        Set(value As PluginManagement.Import.TargetDirectory)
            If ChangeIfDifferent(_SelectedTargetDirectory, value, "SelectedTargetDirectory") Then
                For Each i In _TargetDirectoryList
                    i.IsSelectedAsTarget = i Is _SelectedTargetDirectory
                Next
            End If
        End Set
    End Property

    <Dependency("SourceDirectoryPathIsValid", "SelectedTargetDirectory")>
    Public ReadOnly Property CanCloseOk As Boolean
        Get
            Return SourceDirectoryPathIsValid AndAlso _SelectedTargetDirectory IsNot Nothing
        End Get
    End Property

    Public Sub New()
        MyBase.New(True)
        If IsInDesignMode Then
            TargetDirectoryList = New ObservableCollection(Of PluginManagement.Import.TargetDirectory)
            _TargetDirectoryList.Add(New PluginManagement.Import.TargetDirectory("Plugin 1", True))
            _TargetDirectoryList.Add(New PluginManagement.Import.TargetDirectory("Plugin 2", True))
            _TargetDirectoryList.Add(New PluginManagement.Import.TargetDirectory("Plugin 3", True))
            _TargetDirectoryList.Add(New PluginManagement.Import.TargetDirectory("Neuer Ordner 1", False) With {.IsSelectedAsTarget = True})
            _TargetDirectoryList.Add(New PluginManagement.Import.TargetDirectory("Neuer Ordner 2", False))
            _TargetDirectoryList.Add(New PluginManagement.Import.TargetDirectory("Neuer Ordner 3", False))
            SelectedTargetDirectory = _TargetDirectoryList(3)
        End If
    End Sub

End Class