Public Class Countries
    Inherits Singleton(Of Countries)

    Public ReadOnly Property AllCountries As IEnumerable(Of CommonEventPluginData.ICountry)
        Get
            If ImportedCountrySources Is Nothing Then
                Return New CommonEventPluginData.ICountry() {}
            End If
            Return ImportedCountrySources.SelectMany(Function(i) i.Value.GetCountries)
        End Get
    End Property

    <ImportMany(GetType(CommonEventPluginData.ICountrySource))>
    Private ImportedCountrySources As IEnumerable(Of Lazy(Of CommonEventPluginData.ICountrySource, Object))

End Class
