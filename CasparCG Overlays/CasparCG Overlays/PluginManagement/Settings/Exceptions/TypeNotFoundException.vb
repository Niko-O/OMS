
''' <summary>
''' Wird ausgelöst, wenn eine Einstellung geladen wird, für derer hinterlegten Typenname kein <see cref="Type"/> gefunden wurde.
''' </summary>
Public Class TypeNotFoundException
    Inherits Exception

    Public Sub New(NewMessage As String)
        MyBase.New(NewMessage)
    End Sub

End Class
