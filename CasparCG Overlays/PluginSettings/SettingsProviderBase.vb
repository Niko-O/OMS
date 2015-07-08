
''' <summary>
''' Stellt eine Basisklasse für Objekte dar, die Einstellungen speichern und laden und dafür auf die internen Zustände von Einstellungen zugreifen müssen.
''' </summary>
Public MustInherit Class SettingsProviderBase

    ''' <summary>
    ''' Gibt den Wert von <paramref name="Property"/> zurück.
    ''' </summary>
    ''' <param name="Property">Die auszulesende Eigenschaft.</param>
    Protected Function GetPropertyValue([Property] As SettingsProperty) As Object
        Return [Property].GetValue()
    End Function

    ''' <summary>
    ''' Setzt den Wert von <paramref name="Property"/> auf <paramref name="NewValue"/>.
    ''' Der aufrufende Code muss (mithilfe von <see cref="P:SettingsProperty.Type"/>) selbst überprüfen, ob der Wert für den Typ der Eigenschaft geeignet ist. Wird ein ungültiger Wert übergeben, wird eine <see cref="InvalidCastException"/> ausgelöst.
    ''' </summary>
    ''' <param name="Property">Die zu verändernde Eigenschaft.</param>
    ''' <param name="NewValue">Der neue Wert.</param>
    Protected Sub SetPropertyValue([Property] As SettingsProperty, NewValue As Object)
        [Property].SetValue(NewValue)
    End Sub

    ''' <summary>
    ''' Gibt den Standardwert von <paramref name="Property"/> zurück.
    ''' </summary>
    ''' <param name="Property">Die Eigenschaft, von der der Standardwert ausgelesen wird.</param>
    Protected Function GetDefaultValue([Property] As SettingsProperty) As Object
        Return [Property].GetDefaultValue()
    End Function

End Class
