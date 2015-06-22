Public Class EditCountriesTableWindow

    Dim WithEvents Model As EditCountriesTableWindowViewModel

    Dim AllowClose As Boolean = False

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, EditCountriesTableWindowViewModel)
        AddHandler Connector.Instance.IsConnectedChanged, Sub() Model.IsConnected = Connector.Instance.IsConnected
        Model.IsConnected = Connector.Instance.IsConnected
        Model.SetOriginalPlayerNames(Connector.Instance.GetCountries)
    End Sub

    Private Sub CloseOk(sender As System.Object, e As System.Windows.RoutedEventArgs)
        DoWhileLoading(AddressOf DoApplyChanges, _
                       Sub()
                           AllowClose = True
                           Me.Close()
                       End Sub)
    End Sub

    Private Sub CloseCancel(sender As System.Object, e As System.Windows.RoutedEventArgs)
        AllowClose = True
        Me.Close()
    End Sub

    Private Sub ApplyChanges(sender As System.Object, e As System.Windows.RoutedEventArgs)
        DoWhileLoading(AddressOf DoApplyChanges, Nothing)
    End Sub

    Private Sub DoWhileLoading(Of T)(ToDo As Func(Of T), FinishedCallback As Action(Of T))
        Model.IsLoading = True
        With New System.Threading.Thread(Sub()
                                             Dim Result = ToDo()
                                             Dispatcher.Invoke(Sub()
                                                                   If Not FinishedCallback Is Nothing Then
                                                                       FinishedCallback(Result)
                                                                   End If
                                                                   Model.IsLoading = False
                                                               End Sub)
                                         End Sub)
            .IsBackground = True
            .Start()
        End With
    End Sub

    Private Function DoApplyChanges() As IEnumerable(Of PlayerName)
        For Each i In Model.RemovedPlayerNames
            Connector.Instance.DeletePlayerName(i)
        Next
        For Each i In Model.ChangedPlayerNames
            Connector.Instance.UpdatePlayerName(i.OriginalPlayerName, i.FirstName, i.LastName, i.ShortName)
        Next
        For Each i In Model.AddedPlayerNames
            Connector.Instance.AddNewPlayerName(i.FirstName, i.LastName, i.ShortName)
        Next
        Return Connector.Instance.GetCountries
    End Function

    Private Sub RefreshList(sender As System.Object, e As System.Windows.RoutedEventArgs)
        If Not Model.HasChanges OrElse PromptDiscardChanges() Then
            DoWhileLoading(Function() Connector.Instance.GetCountries, Sub(Result) Model.SetOriginalPlayerNames(Result))
        End If
    End Sub

    Protected Overrides Sub OnClosing(e As System.ComponentModel.CancelEventArgs)
        If Not AllowClose Then
            If Model.HasChanges AndAlso Not PromptDiscardChanges() Then
                e.Cancel = True
            End If
        End If
        MyBase.OnClosing(e)
    End Sub

    Private Function PromptDiscardChanges() As Boolean
        Return System.Windows.MessageBox.Show("Ungespeicherte Änderungen werden nicht übernommen!", "Ungespeicherte Änderungen", Windows.MessageBoxButton.OKCancel, Windows.MessageBoxImage.Warning) = Windows.MessageBoxResult.OK
    End Function

End Class
