Public Class TennisSnapInViewModel
    Inherits ViewModelBase

    Dim _Test_Templates As String()
    Public ReadOnly Property Test_Templates As String()
        Get
            Return _Test_Templates
        End Get
    End Property

    Dim _Test_Effects As String()
    Public ReadOnly Property Test_Effects As String()
        Get
            Return _Test_Effects
        End Get
    End Property

    Public Sub New()
        MyBase.New(True)
        _Test_Templates = {
            "Standard Template"
        }
        _Test_Effects = {
            "Kontinuierliches Scrollen - rechts nach links"
        }
    End Sub

End Class