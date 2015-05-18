
Namespace ServerList

    Public Class RemoveServerEventArgs
        Inherits EventArgs

        Dim _Server As CasparCGServer
        Public ReadOnly Property Server As CasparCGServer
            Get
                Return _Server
            End Get
        End Property

        Public Sub New(NewServer As CasparCGServer)
            _Server = NewServer
        End Sub

    End Class

End Namespace
