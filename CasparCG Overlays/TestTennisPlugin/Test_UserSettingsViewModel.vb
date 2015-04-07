Public Class Test_UserSettingsViewModel
    Inherits ViewModelBase

    Dim _Test_PlayerNames As String()
    Public ReadOnly Property Test_PlayerNames As String()
        Get
            Return _Test_PlayerNames
        End Get
    End Property

    Public ReadOnly Property RandomNumberA As Integer
        Get
            Return CInt(Rnd.Next(0, 10) * 1.5)
        End Get
    End Property

    Public ReadOnly Property RandomNumberB As Integer
        Get
            Return CInt(Rnd.Next(0, 10) * 1.5)
        End Get
    End Property

    Private Shared Rnd As New Random

    Public Sub New()
        MyBase.New(True)
        _Test_PlayerNames = {
            "Leonardo DiCaprio", _
            "Tom Cruise"
        }
    End Sub

End Class