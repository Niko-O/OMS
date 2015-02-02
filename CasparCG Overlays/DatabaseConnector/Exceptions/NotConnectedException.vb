Public Class NotConnectedException
    Inherits ConnectorException

    Public Sub New()
        MyBase.New("Die Verbindung zum Server wurde nicht hergestellt.")
    End Sub

End Class
