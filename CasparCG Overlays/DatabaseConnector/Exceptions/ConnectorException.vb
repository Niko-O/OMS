
''' <summary>
''' Wird ausgelöst, wenn bei der Kommunikation mit dem SQL-Server ein Problem auftritt.
''' </summary>
Public Class ConnectorException
    Inherits Exception

    Public Sub New(NewMessage As String)
        MyBase.New(NewMessage)
    End Sub

    Public Sub New(NewMessage As String, NewInnerException As Exception)
        MyBase.New(NewMessage, NewInnerException)
    End Sub

End Class
