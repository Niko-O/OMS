
''' <summary>
''' Stellt eine Quelle für <see cref="ICountry"/> dar.
''' </summary>
Public Interface ICountrySource

    ''' <summary>
    ''' Gibt eine Liste der vorhandenen <see cref="ICountry"/> Bedürfnisse zurück.
    ''' </summary>
    Function GetCountries() As IEnumerable(Of ICountry)

End Interface
