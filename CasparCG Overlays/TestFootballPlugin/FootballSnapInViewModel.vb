Public Class FootballSnapInViewModel
    Inherits ViewModelBase

    Dim _AvailableCountries As IEnumerable(Of CommonEventPluginData.ICountry) = Nothing
    Public Property AvailableCountries As IEnumerable(Of CommonEventPluginData.ICountry)
        Get
            Return _AvailableCountries
        End Get
        Set(value As IEnumerable(Of CommonEventPluginData.ICountry))
            ChangeIfDifferent(_AvailableCountries, value, "AvailableCountries")
        End Set
    End Property

    Dim _SelectedCountry As CommonEventPluginData.ICountry = Nothing
    Public Property SelectedCountry As CommonEventPluginData.ICountry
        Get
            Return _SelectedCountry
        End Get
        Set(value As CommonEventPluginData.ICountry)
            ChangeIfDifferent(_SelectedCountry, value, "SelectedCountry")
        End Set
    End Property

    Public Sub New()
        MyBase.New(True)
        If IsInDesignMode Then
            AvailableCountries = {New DesignTimeCountry("Austria", "AUT"), _
                                  New DesignTimeCountry("Germany", "GER"), _
                                  New DesignTimeCountry("England", "ENG")}
        End If
    End Sub

End Class