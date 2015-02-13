
Namespace PluginManagement.Settings.Exceptions

    ''' <summary>
    ''' Wird ausgelöst, wenn eine Einstellung geladen wird, für derer Typ kein <see cref="System.ComponentModel.TypeConverter"/> gefunden wurde.
    ''' </summary>
    Public Class TypeConverterNotFoundException
        Inherits PluginSettings.SettingsException

        Public Sub New(NewMessage As String)
            MyBase.New(NewMessage)
        End Sub

    End Class

End Namespace
