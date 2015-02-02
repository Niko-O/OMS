Public Class DesignerSupportPlugin
    Inherits Plugin

    Public Sub New()
        Me.New("NamelessDesignerSupportPlugin")
    End Sub

    Public Sub New(NewDebugName As String)
        MyBase.New(New DesignerSupportPluginSource(NewDebugName))
    End Sub

End Class
