Public Class CheckForUpdatesCommand
    Inherits DelegateCommand

    Public Sub New()
    End Sub

    Public Overrides Function CanExecute(Parameter As Object) As Boolean
        Return True
    End Function

    Public Overrides Sub Execute(Parameter As Object)
        UpdateInfo.Instance.CheckForUpdates(True)
    End Sub

End Class
