
''' <summary>
''' Stellt Länder aus Sql-Servern zur Verfügung.
''' </summary>
<Export(GetType(CommonEventPluginData.ICountrySource))>
Public Class SqlCountrySource
    Implements CommonEventPluginData.ICountrySource

    ''' <summary>
    ''' <see cref="CommonEventPluginData.ICountrySource.GetCountries"/>
    ''' </summary>
    Public Function GetCountries() As IEnumerable(Of CommonEventPluginData.ICountry) Implements CommonEventPluginData.ICountrySource.GetCountries
        If Connector.Instance.IsConnected Then
            Return Connector.Instance.GetCountries
        Else
            Return New CommonEventPluginData.ICountry() {}
        End If
    End Function

End Class
