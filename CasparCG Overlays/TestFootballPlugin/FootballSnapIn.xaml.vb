Public Class FootballSnapIn

    Dim Model As FootballSnapInViewModel

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, FootballSnapInViewModel)
    End Sub

    Private Sub ActualizeAvailableCountries(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Model.AvailableCountries = Countries.Instance.AllCountries
    End Sub

End Class
