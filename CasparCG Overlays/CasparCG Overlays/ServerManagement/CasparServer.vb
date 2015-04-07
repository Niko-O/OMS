
Imports C = Svt.Caspar

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

    Public ReadOnly Property IsConnected As Boolean Implements PluginInterfaces.ICasparServer.IsConnected
        Get
            Return Device.IsConnected
        End Get
    End Property

    Public Property IpAddress As String
        Get
            Return Device.Settings.Hostname
        End Get
        Set(value As String)
            If ChangeIfDifferent(Device.Settings.Hostname, value) Then
                OnPropertyChanged("IpAddress")
            End If
        End Set
    End Property

    Public Property Port As Integer
        Get
            Return Device.Settings.Port
        End Get
        Set(value As Integer)
            If ChangeIfDifferent(Device.Settings.Port, value) Then
                OnPropertyChanged("Port")
            End If
        End Set
    End Property

    Dim WithEvents Device As C.CasparDevice

    Private Sub New()
        Device = New C.CasparDevice
    End Sub

    Public Sub ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) Implements PluginInterfaces.ICasparServer.ExecuteCommand
        Device.SendString(Command.GetCommandString)
    End Sub

    Public Sub LoadTemplate(Template As PluginInterfaces.ITemplate) Implements PluginInterfaces.ICasparServer.LoadTemplate
        If Not IsConnected Then
            Throw New InvalidOperationException("Der Server ist nicht verbunden.")
        End If
        ExecuteCommand(New CasparServerCommands.PlayCommand(Template.ChannelId, Template.Layer, Template.Clip, Template.AdditionalParameters))
    End Sub

    Public Sub Connect() Implements PluginInterfaces.ICasparServer.Connect
        Device.Connect()
    End Sub

    Public Sub Disconnect() Implements PluginInterfaces.ICasparServer.Disconnect
        Device.Disconnect()
    End Sub

    Private Sub Device_ConnectionStatusChanged(sender As Object, e As Svt.Network.ConnectionEventArgs) Handles Device.ConnectionStatusChanged
        OnPropertyChanged("IsConnected")
    End Sub

End Class
