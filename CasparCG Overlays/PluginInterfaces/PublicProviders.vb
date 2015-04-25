Public Class PublicProviders

    Private Shared _PluginSettings As ISettingsProvider = New NullSettingsProvider
    Public Shared ReadOnly Property PluginSettings As ISettingsProvider
        <DebuggerStepThrough()>
        Get
            Return _PluginSettings
        End Get
    End Property

    Private Shared _CasparServer As ICasparServer = New NullCasparServer
    Public Shared ReadOnly Property CasparServer As ICasparServer
        <DebuggerStepThrough()>
        Get
            Return _CasparServer
        End Get
    End Property

    Private Shared _MefCompositor As ICompositor = New NullCompositor
    Public Shared ReadOnly Property MefCompositor As ICompositor
        Get
            Return _MefCompositor
        End Get
    End Property

    Private Shared IsInitialized As Boolean = False

    Public Shared Sub Initialize(NewPluginSettings As ISettingsProvider, NewCasparServer As ICasparServer, NewMefCompositor As ICompositor)
        If IsInitialized Then
            Throw New InvalidOperationException("Diese Methode darf nur vom Hostprogramm aufgerufen werden.")
        End If
        _PluginSettings = NewPluginSettings
        _CasparServer = NewCasparServer
        _MefCompositor = NewMefCompositor
    End Sub

End Class
