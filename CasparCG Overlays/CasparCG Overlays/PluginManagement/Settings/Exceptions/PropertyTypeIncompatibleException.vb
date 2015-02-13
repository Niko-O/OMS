
''' <summary>
''' Wird ausgelöst, wenn die gespeicherte Einstellung einen zum Typ der zu ladenden Einstellung inkompatiblen Typ aufweist. <!-- Grammatik, b****es! -->
''' </summary>
Public Class PropertyTypeIncompatibleException
    Inherits PluginSettings.SettingsException

    Public Sub New(NewMessage As String)
        MyBase.New(NewMessage)
    End Sub

End Class
