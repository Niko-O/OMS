Public Class TennisSnapIn

    Dim Model As TennisSnapInViewModel

    Public Sub New()
        InitializeComponent()
        Model = DirectCast(Me.DataContext, TennisSnapInViewModel)
    End Sub

End Class
