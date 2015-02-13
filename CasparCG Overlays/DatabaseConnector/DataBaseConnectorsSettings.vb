Public Class DataBaseConnectorsSettings
    Inherits PluginSettings.SettingsStructure

    Private Shared _Instance As DataBaseConnectorsSettings
    Public Shared ReadOnly Property Instance As DataBaseConnectorsSettings
        Get
            If _Instance Is Nothing Then
                _Instance = New DataBaseConnectorsSettings
            End If
            Return _Instance
        End Get
    End Property

    Dim _ServerName As PluginSettings.SettingsProperty(Of String) = RegisterProperty(Of String)("ServerName")
    Public Property ServerName As String
        Get
            Return _ServerName.Value
        End Get
        Set(value As String)
            _ServerName.Value = value
        End Set
    End Property

    Dim _SchemaName As PluginSettings.SettingsProperty(Of String) = RegisterProperty(Of String)("SchemaName")
    Public Property SchemaName As String
        Get
            Return _SchemaName.Value
        End Get
        Set(value As String)
            _SchemaName.Value = value
        End Set
    End Property

    Dim _UserName As PluginSettings.SettingsProperty(Of String) = RegisterProperty(Of String)("UserName")
    Public Property UserName As String
        Get
            Return _UserName.Value
        End Get
        Set(value As String)
            _UserName.Value = value
        End Set
    End Property

End Class
