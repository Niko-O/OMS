Public Interface ICasparServer
    Inherits System.ComponentModel.INotifyPropertyChanged

    Event IsConnectedChanged()
    Event TemplateChanged()

    ReadOnly Property IsConnected As Boolean
    ReadOnly Property Template As ITemplate

    Sub LoadTemplate(Template As ITemplate)
    Sub UnloadTemplate(Template As ITemplate)
    Function ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) As CasparServerCommands.ICommandResponse
    Sub BeginConnect(Of T)(Callback As Action(Of T, Exception), State As T)
    Sub Disconnect()

End Interface
