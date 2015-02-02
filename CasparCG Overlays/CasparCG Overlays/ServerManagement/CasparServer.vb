Public Class CasparServer
    Inherits NotifyPropertyChanged
    Implements PluginInterfaces.ICasparServer

    Private Shared _Instance As CasparServer = Nothing
    Public Shared ReadOnly Property Instance As CasparServer
        Get
            If _Instance Is Nothing Then
                _Instance = New CasparServer
            End If
            Return _Instance
        End Get
    End Property

    Public ReadOnly Property ToggleConnectionCommand As DelegateCommand
        Get
            Static Temp As New DelegateCommand(Sub() ToggleConnection())
            Return Temp
        End Get
    End Property

    Dim _IsConnected As Boolean = False
    Public ReadOnly Property IsConnected As Boolean Implements PluginInterfaces.ICasparServer.IsConnected
        Get
            Return _IsConnected
        End Get
    End Property

    Dim _IpAddress As String = Nothing
    Public Property IpAddress As String
        Get
            Return _IpAddress
        End Get
        Set(value As String)
            If Not _IpAddress = value Then
                _IpAddress = value
                OnPropertyChanged("IpAddress")
            End If
        End Set
    End Property

    Public Property Template As PluginInterfaces.ITemplate Implements PluginInterfaces.ICasparServer.Template
        Get

        End Get
        Set(value As PluginInterfaces.ITemplate)

        End Set
    End Property

    Public Sub ToggleConnection()

    End Sub

End Class
