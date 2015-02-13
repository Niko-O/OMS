
''' <summary>
''' Stellt eine Basisklasse für Ausnahmen dar, die beim Speichern und Laden von Einstellungen auftreten.
''' </summary>
Public Class SettingsException
    Inherits Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(NewMessage As String)
        MyBase.New(NewMessage)
    End Sub

    Public Sub New(NewMessage As String, NewInnerException As Exception)
        MyBase.New(NewMessage, NewInnerException)
    End Sub

End Class
