Public Interface ICasparServer
    Inherits System.ComponentModel.INotifyPropertyChanged

    ReadOnly Property IsConnected As Boolean
    Property Template As ITemplate

End Interface
