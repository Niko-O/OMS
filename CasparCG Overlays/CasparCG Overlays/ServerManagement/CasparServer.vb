
Imports C = CasparCGNETConnector

''' <summary>
''' Verwaltet die Kommunikation mit dem CasparCG-Server.
''' </summary>
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

    ''' <summary>
    ''' Gibt an, ob die Verbindung hergestellt ist.
    ''' </summary>
    Public ReadOnly Property IsConnected As Boolean Implements PluginInterfaces.ICasparServer.IsConnected
        Get
            Return Device.isConnected
        End Get
    End Property

    Dim _IpAddress As String
    ''' <summary>
    ''' Gibt die IP-Adresse des CasparCG-Servers an.
    ''' Kann nicht geändert werden, wenn bereits eine Verbindung besteht.
    ''' </summary>
    Public Property IpAddress As String
        Get
            Return _IpAddress
        End Get
        Set(value As String)
            If Device.isConnected Then
                Throw New InvalidOperationException("Die IP-Adresse kann nicht geändert werden, wenn bereits eine Verbindung besteht.")
            End If
            If ChangeIfDifferent(_IpAddress, value) Then
                OnPropertyChanged("IpAddress")
            End If
        End Set
    End Property

    Dim _Port As Integer
    ''' <summary>
    ''' Gibt den Port zum CasparCG-Server an.
    ''' Kann nicht geändert werden, wenn bereits eine Verbindung besteht.
    ''' </summary>
    Public Property Port As Integer
        Get
            Return _Port
        End Get
        Set(value As Integer)
            If Device.isConnected Then
                Throw New InvalidOperationException("Die IP-Adresse kann nicht geändert werden, wenn bereits eine Verbindung besteht.")
            End If
            If ChangeIfDifferent(_Port, value) Then
                OnPropertyChanged("Port")
            End If
        End Set
    End Property

    Dim WithEvents Device As C.CasparCGConnection

    Private Sub New()
        Device = New C.CasparCGConnection
    End Sub

    Public Function ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) As CasparServerCommands.ICommandResponse Implements PluginInterfaces.ICasparServer.ExecuteCommand
        Return New SpecificImplementationOfICommandResponse(Device.sendCommand(Command.GetCommandString))
    End Function

    Public Sub LoadTemplate(Template As PluginInterfaces.ITemplate) Implements PluginInterfaces.ICasparServer.LoadTemplate
        If Not IsConnected Then
            Throw New InvalidOperationException("Der Server ist nicht verbunden.")
        End If
        ExecuteCommand(New CasparServerCommands.PlayCommand(Template.ChannelId, Template.Layer, Template.Clip, Template.AdditionalParameters))
    End Sub

    Public Sub UnloadTemplate(Template As PluginInterfaces.ITemplate) Implements PluginInterfaces.ICasparServer.UnloadTemplate
        If Not IsConnected Then
            Throw New InvalidOperationException("Der Server ist nicht verbunden.")
        End If
        ExecuteCommand(New CasparServerCommands.StopCommand(Template.ChannelId, Template.Layer))
    End Sub

    Public Sub Connect() Implements PluginInterfaces.ICasparServer.Connect
        Device.connect(_IpAddress, _Port)
    End Sub

    Public Sub Disconnect() Implements PluginInterfaces.ICasparServer.Disconnect
        Device.close()
    End Sub

    Private Sub Device_connected(ByRef sender As Object) Handles Device.connected
        OnPropertyChanged("IsConnected")
    End Sub

    Private Sub Device_disconnected(ByRef sender As Object) Handles Device.disconnected
        OnPropertyChanged("IsConnected")
    End Sub

    ''' <summary>
    ''' Veröffentlicht die vom Server zurückgesendeten Daten, die von einem <see cref="C.CasparCGResponse"/>-Objekt gekapselt werden.
    ''' </summary>
    Private Class SpecificImplementationOfICommandResponse
        Implements CasparServerCommands.ICommandResponse

        Dim Source As C.CasparCGResponse

        Public Sub New(NewSource As C.CasparCGResponse)
            Source = NewSource
        End Sub

        Public ReadOnly Property RawText As String Implements CasparServerCommands.ICommandResponse.RawText
            Get
                Return Source.getServerMessage
            End Get
        End Property

        Public ReadOnly Property ReturnCode As Integer Implements CasparServerCommands.ICommandResponse.StatusCode
            Get
                Return Source.getCode
            End Get
        End Property

        Public ReadOnly Property ReturnedText As String Implements CasparServerCommands.ICommandResponse.ReturnedText
            Get
                Return Source.getData
            End Get
        End Property

    End Class

End Class
