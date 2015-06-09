
Imports System.IO

Public Class ImportPluginDialog

    Public Shared ReadOnly PluginsDirectory As New System.IO.DirectoryInfo(Path.Combine(New FileInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).Directory.FullName, "Plugins"))

    Public ReadOnly Property SourceDirectory As String
        Get
            Return ViewModel.SourceDirectoryPath
        End Get
    End Property

    Public ReadOnly Property TargetDirectoryName As String
        Get
            If ViewModel.SelectedTargetDirectory Is Nothing Then
                Return Nothing
            End If
            Return ViewModel.SelectedTargetDirectory.Name
        End Get
    End Property

    Dim ViewModel As ImportPluginDialogViewModel
    Dim TargetDirectories As New ObservableCollection(Of PluginManagement.Import.TargetDirectory)

    Public Sub New()
        InitializeComponent()
        ViewModel = DirectCast(Me.DataContext, ImportPluginDialogViewModel)
        ViewModel.TargetDirectoryList = TargetDirectories
        For Each i In PluginsDirectory.GetDirectories
            TargetDirectories.Add(New PluginManagement.Import.TargetDirectory(i.Name, True))
        Next
    End Sub

    Private Sub OpenSourceDirectory(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim Dlg As New VistaFBD.WpfFolderBrowserDialog
        If Dlg.ShowDialog(Me) Then
            ViewModel.SourceDirectoryPath = Dlg.SelectedPath
        End If
    End Sub

    Private Sub AddTargetFolder(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim Dlg As New TargetFolderNameDialog
        Dlg.Owner = Me
        Dlg.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner
        If Dlg.ShowDialog Then
            Dim NewDirectory As New PluginManagement.Import.TargetDirectory(Dlg.DirectoryName, False)
            TargetDirectories.Add(NewDirectory)
            ViewModel.SelectedTargetDirectory = NewDirectory
        End If
    End Sub

    Private Sub CloseOk(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub CloseCancel(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
        Me.Close()
    End Sub

End Class
