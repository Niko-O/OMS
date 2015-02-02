Public Class ConnectorSnapIn

    Dim WithEvents Model As ConnectorSnapInViewModel
    Dim EditCountriesTableWindowInstance As EditCountriesTableWindow = Nothing

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, ConnectorSnapInViewModel)
        AddHandler Connector.Instance.IsConnectedChanged, Sub() Dispatcher.Invoke(Sub() Model.IsConnected = Connector.Instance.IsConnected)
        AddHandler PasswordBox_SqlPassword.PasswordChanged, Sub() Connector.Instance.Password = PasswordBox_SqlPassword.SecurePassword
        Model.IsConnected = Connector.Instance.IsConnected
        'Model.SqlServerName = "localhost"
        'Model.SqlSchemaName = "omsdata"
        'Model.SqlUserName = "root"
        'PasswordBox_SqlPassword.Password = "Password1"
    End Sub

    Private Sub SqlServerNameChanged() Handles Model.SqlServerNameChanged
        Connector.Instance.ServerName = Model.SqlServerName
    End Sub

    Private Sub SqlUserNameChanged() Handles Model.SqlUserNameChanged
        Connector.Instance.UserName = Model.SqlUserName
    End Sub

    Private Sub SqlSchemaNameChanged() Handles Model.SqlSchemaNameChanged
        Connector.Instance.SchemaName = Model.SqlSchemaName
    End Sub

    Private Sub ConnectToSqlServer(sender As Object, e As System.Windows.RoutedEventArgs)
        BeginConnectDisconnect()
    End Sub

    Private Sub BeginConnectDisconnect()
        Model.SqlInfoIsError = False
        Dim DoesDisconnection = Connector.Instance.IsConnected
        Model.IsConnectionChanging = True
        If DoesDisconnection Then
            Model.SqlInfo = "Trenne Verbindung zum Server..."
        Else
            Model.SqlInfo = "Verbinde zum Server..."
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
                                                                       Model.IsConnectionChanging = False
                                                                       If Success Then
                                                                           Model.SqlInfo = Nothing
                                                                       Else
                                                                           Model.SqlInfo = If(DoesDisconnection, "Fehler beim Trennen der Verbindung", "Fehler beim Herstellen der Verbindung") & Environment.NewLine & Exception.GetType.Name & Environment.NewLine & Exception.Message
                                                                           Model.SqlInfoIsError = True
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
        If EditCountriesTableWindowInstance Is Nothing Then
            EditCountriesTableWindowInstance = New EditCountriesTableWindow
            AddHandler EditCountriesTableWindowInstance.Closing, AddressOf EditCountriesTableWindowClosing
        End If
        EditCountriesTableWindowInstance.Show()
    End Sub

    Private Sub EditCountriesTableWindowClosing(sender As Object, e As System.ComponentModel.CancelEventArgs)
        EditCountriesTableWindowInstance = Nothing
    End Sub

End Class
