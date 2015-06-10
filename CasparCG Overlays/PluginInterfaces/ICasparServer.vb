Public Interface ICasparServer
    Inherits System.ComponentModel.INotifyPropertyChanged

    Event IsConnectedChanged()

    ReadOnly Property IsConnected As Boolean

    Sub LoadTemplate(Template As ITemplate)
    Sub UnloadTemplate(Template As ITemplate)
    Function ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) As CasparServerCommands.ICommandResponse
    Sub BeginConnect(Of T)(Callback As Action(Of T, Exception), State As T)
    Sub Disconnect()

End Interface
