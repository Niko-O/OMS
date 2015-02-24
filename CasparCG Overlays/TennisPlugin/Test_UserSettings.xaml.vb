Public Class Test_UserSettings

    Public Property HeaderName As String
        Get
            Return DirectCast(MainGroupBox.Header, String)
        End Get
        Set(value As String)
            MainGroupBox.Header = value
        End Set
    End Property

    Dim Model As Test_UserSettingsViewModel

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, Test_UserSettingsViewModel)
    End Sub

End Class
