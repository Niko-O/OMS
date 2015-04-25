
''' <summary>
''' Stellt eine minimalistische Implementierung von <see cref="ICasparServer"/> dar, die keine Funktionalitäten beinhaltet.
''' </summary>
Public Class NullCasparServer
    Implements ICasparServer

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Public ReadOnly Property IsConnected As Boolean Implements ICasparServer.IsConnected
        Get
            Return False
        End Get
    End Property

    Public Sub Connect() Implements ICasparServer.Connect
    End Sub

    Public Sub Disconnect() Implements ICasparServer.Disconnect
    End Sub

    Public Sub ExecuteCommand(Command As CasparServerCommands.ICasparServerCommand) Implements ICasparServer.ExecuteCommand
    End Sub

    Public Sub LoadTemplate(Template As ITemplate) Implements ICasparServer.LoadTemplate
    End Sub

    Public Sub UnloadTemplate(Template As ITemplate) Implements ICasparServer.UnloadTemplate
    End Sub

End Class
