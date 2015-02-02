Public Class DesignTimeCountry
    Implements CommonEventPluginData.ICountry

    Dim _FullName As String
    Public Property FullName As String Implements CommonEventPluginData.ICountry.FullName
        Get
            Return _FullName
        End Get
        Set(value As String)
            _FullName = value
        End Set
    End Property

    Dim _ShortName As String
    Public Property ShortName As String Implements CommonEventPluginData.ICountry.ShortName
        Get
            Return _ShortName
        End Get
        Set(value As String)
            _ShortName = value
        End Set
    End Property

    Public Sub New(NewFullName As String, NewShortName As String)
        _FullName = NewFullName
        _ShortName = NewShortName
    End Sub

End Class
