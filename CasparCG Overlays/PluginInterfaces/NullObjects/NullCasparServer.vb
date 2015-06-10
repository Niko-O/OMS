
''' <summary>
''' Stellt eine minimalistische Implementierung von <see cref="ICasparServer"/> dar, die keine Funktionalitäten beinhaltet.
''' </summary>
Friend Class NullCasparServer
    Implements ICasparServer

    Public Event IsConnectedChanged() Implements ICasparServer.IsConnectedChanged
    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Public ReadOnly Property IsConnected As Boolean Implements ICasparServer.IsConnected
        Get
            Return False
        End Get
    End Property

    Public Sub BeginConnect(Of T)(Callback As System.Action(Of T, Exception), State As T) Implements ICasparServer.BeginConnect
    End Sub

    Public Sub Disconnect() Implements ICasparServer.Disconnect
    End Sub

    Public Function ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) As CasparServerCommands.ICommandResponse Implements ICasparServer.ExecuteCommand
        Return Nothing
    End Function

    Public Sub LoadTemplate(Template As ITemplate) Implements ICasparServer.LoadTemplate
    End Sub

    Public Sub UnloadTemplate(Template As ITemplate) Implements ICasparServer.UnloadTemplate
    End Sub

End Class
