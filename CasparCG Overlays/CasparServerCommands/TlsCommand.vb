Public Class TlsCommand
    Implements ICasparServerCommand

    Public Function GetCommandString() As String Implements ICasparServerCommand.GetCommandString
        Return "TLS"
    End Function

End Class
