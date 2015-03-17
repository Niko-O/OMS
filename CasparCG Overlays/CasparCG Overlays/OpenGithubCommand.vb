Public Class OpenGithubCommand
    Inherits DelegateCommand

    Public Overrides Sub Execute(Parameter As Object)
        Using P As New Process
            P.StartInfo.FileName = "https://github.com/Niko-O/OMS/"
            P.Start()
        End Using
    End Sub

End Class
