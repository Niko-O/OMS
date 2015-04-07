Public Interface ICasparServer
    Inherits System.ComponentModel.INotifyPropertyChanged

    ReadOnly Property IsConnected As Boolean

    Sub LoadTemplate(Template As ITemplate)
    Sub Connect()
    Sub Disconnect()

End Interface
