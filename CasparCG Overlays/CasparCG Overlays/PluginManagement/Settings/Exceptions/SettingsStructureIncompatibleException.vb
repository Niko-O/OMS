
''' <summary>
''' Wird ausgelöst, wenn die gespeicherten Einstellungen anders aufgebaut sind, als die Einstellungs-Struktur, deren Einstellungen geladen werden sollen.
''' </summary>
Public Class SettingsStructureIncompatibleException
    Inherits Exception

    Public Sub New(NewMessage As String)
        MyBase.New(NewMessage)
    End Sub

End Class
