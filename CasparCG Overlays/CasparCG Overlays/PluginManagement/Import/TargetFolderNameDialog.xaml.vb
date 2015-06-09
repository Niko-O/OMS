Public Class TargetFolderNameDialog

    Public Property DirectoryName As String
        Get
            Return ViewModel.DirectoryName
        End Get
        Set(value As String)
            ViewModel.DirectoryName = value
        End Set
    End Property

    Dim ViewModel As TargetFolderNameDialogViewModel

    Public Sub New()
        InitializeComponent()
        ViewModel = DirectCast(Me.DataContext, TargetFolderNameDialogViewModel)
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
