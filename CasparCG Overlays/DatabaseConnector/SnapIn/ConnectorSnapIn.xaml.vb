Public Class ConnectorSnapIn

    Dim WithEvents ViewModel As ConnectorSnapInViewModel
    Dim EditWindow As EditPlayerNamesTableWindow = Nothing

    Public Sub New()
        InitializeComponent()
        ViewModel = DirectCast(Me.DataContext, ConnectorSnapInViewModel)
        AddHandler Connector.Instance.IsConnectedChanged, Sub() Dispatcher.Invoke(Sub() ViewModel.IsConnected = Connector.Instance.IsConnected)
        AddHandler PasswordBox_SqlPassword.PasswordChanged, Sub() Connector.Instance.Password = PasswordBox_SqlPassword.SecurePassword
        ViewModel.IsConnected = Connector.Instance.IsConnected
    End Sub

    Private Sub ConnectToSqlServer(sender As Object, e As System.Windows.RoutedEventArgs)
        BeginConnectDisconnect()
    End Sub

    Private Sub BeginConnectDisconnect()
        ViewModel.SqlInfoIsError = False
        Dim DoesDisconnection = Connector.Instance.IsConnected
        ViewModel.IsConnectionChanging = True
        If DoesDisconnection Then
            ViewModel.SqlInfo = "Trenne Verbindung zum Server..."
        Else
            ViewModel.SqlInfo = "Verbinde zum Server..."
        End If
        With New System.Threading.Thread(Sub()
                                             Try
                                                 Dim Success = False
                                                 Dim Exception As ConnectorException = Nothing
                                                 Try
                                                     If DoesDisconnection Then
                                                         Connector.Instance.Disconnect()
                                                     Else
                                                         Connector.Instance.Connect()
                                                     End If
                                                     Success = True
                                                 Catch ex As ConnectorException
                                                     Success = False
                                                     Exception = ex
                                                 End Try
                                                 Dispatcher.Invoke(Sub()
                                                                       ViewModel.IsConnectionChanging = False
                                                                       If Success Then
                                                                           ViewModel.SqlInfo = Nothing
                                                                       Else
                                                                           ViewModel.SqlInfo = If(DoesDisconnection, "Fehler beim Trennen der Verbindung", "Fehler beim Herstellen der Verbindung") & Environment.NewLine & Exception.GetType.Name & Environment.NewLine & Exception.Message
                                                                           ViewModel.SqlInfoIsError = True
                                                                       End If
                                                                   End Sub)
                                             Catch ex As Exception
                                                 'Gotta chatch 'em all!
                                                 System.Windows.MessageBox.Show(ex.GetType.Name & Environment.NewLine & ex.Message & Environment.NewLine & ex.StackTrace)
                                             End Try
                                         End Sub)
            .IsBackground = True
            .Start()
        End With
    End Sub

    Private Sub EditCountries(sender As System.Object, e As System.Windows.RoutedEventArgs)
        If EditWindow Is Nothing Then
            EditWindow = New EditPlayerNamesTableWindow
            AddHandler EditWindow.Closing, AddressOf EditCountriesTableWindowClosing
        End If
        EditWindow.Show()
    End Sub

    Private Sub EditCountriesTableWindowClosing(sender As Object, e As System.ComponentModel.CancelEventArgs)
        EditWindow = Nothing
    End Sub

End Class
