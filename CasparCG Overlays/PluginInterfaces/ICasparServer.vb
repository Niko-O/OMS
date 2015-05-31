﻿Public Interface ICasparServer
    Inherits System.ComponentModel.INotifyPropertyChanged

    ReadOnly Property IsConnected As Boolean

    Sub LoadTemplate(Template As ITemplate)
    Sub UnloadTemplate(Template As ITemplate)
    Function ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) As CasparServerCommands.ICommandResponse
    Sub Connect()
    Sub Disconnect()

End Interface
