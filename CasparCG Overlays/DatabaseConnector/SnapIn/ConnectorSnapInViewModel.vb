Public Class ConnectorSnapInViewModel
    Inherits ViewModelBase

    Public Event SqlServerNameChanged()
    Dim _SqlServerName As String = DataBaseConnectorsSettings.Instance.ServerName
    Public Property SqlServerName As String
        Get
            Return _SqlServerName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_SqlServerName, value, "SqlServerName") Then
                DataBaseConnectorsSettings.Instance.ServerName = _SqlServerName
                RaiseEvent SqlServerNameChanged()
            End If
        End Set
    End Property

    Public Event SqlSchemaNameChanged()
    Dim _SqlSchemaName As String = DataBaseConnectorsSettings.Instance.SchemaName
    Public Property SqlSchemaName As String
        Get
            Return _SqlSchemaName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_SqlSchemaName, value, "SqlSchemaName") Then
                DataBaseConnectorsSettings.Instance.SchemaName = _SqlSchemaName
                RaiseEvent SqlSchemaNameChanged()
            End If
        End Set
    End Property

    Public Event SqlUserNameChanged()
    Dim _SqlUserName As String = DataBaseConnectorsSettings.Instance.UserName
    Public Property SqlUserName As String
        Get
            Return _SqlUserName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_SqlUserName, value, "SqlUserName") Then
                DataBaseConnectorsSettings.Instance.UserName = _SqlUserName
                RaiseEvent SqlUserNameChanged()
            End If
        End Set
    End Property

    Dim _SqlInfo As String = Nothing
    Public Property SqlInfo As String
        Get
            Return _SqlInfo
        End Get
        Set(value As String)
            ChangeIfDifferent(_SqlInfo, value, "SqlInfo")
        End Set
    End Property

    Dim _SqlInfoIsError As Boolean = False
    Public Property SqlInfoIsError As Boolean
        Get
            Return _SqlInfoIsError
        End Get
        Set(value As Boolean)
            ChangeIfDifferent(_SqlInfoIsError, value, "SqlInfoIsError")
        End Set
    End Property

    Dim _IsConnected As Boolean = False
    Public Property IsConnected As Boolean
        Get
            Return _IsConnected
        End Get
        Set(value As Boolean)
            ChangeIfDifferent(_IsConnected, value, "IsConnected")
        End Set
    End Property

    Dim _IsConnectionChanging As Boolean = False
    Public Property IsConnectionChanging As Boolean
        Get
            Return _IsConnectionChanging
        End Get
        Set(value As Boolean)
            ChangeIfDifferent(_IsConnectionChanging, value, "IsConnectionChanging")
        End Set
    End Property

    <Dependency("IsConnected", "IsConnectionChanging")>
    Public ReadOnly Property ServerCanInteract As Boolean
        Get
            Return _IsConnected AndAlso Not _IsConnectionChanging
        End Get
    End Property

    <Dependency("IsConnected", "IsConnectionChanging")>
    Public ReadOnly Property EnableServerDataChange As Boolean
        Get
            Return Not _IsConnected AndAlso Not _IsConnectionChanging
        End Get
    End Property

    Private Shared ReadOnly ErrorBackgroundBrush As New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 200, 200))
    <Dependency("SqlInfoIsError")>
    Public ReadOnly Property SqlInfoBackground As System.Windows.Media.Brush
        Get
            If _SqlInfoIsError Then
                Return ErrorBackgroundBrush
            Else
                Return System.Windows.Media.Brushes.Transparent
            End If
        End Get
    End Property

    <Dependency("SqlServerName", "SqlSchemaName", "SqlUserName")>
    Public ReadOnly Property CanChangeConnection As Boolean
        Get
            Return Not _IsConnectionChanging AndAlso _
                   Not String.IsNullOrEmpty(_SqlServerName) AndAlso _
                   Not String.IsNullOrEmpty(_SqlSchemaName) AndAlso _
                   Not String.IsNullOrEmpty(_SqlUserName)
        End Get
    End Property

    Public Sub New()
        MyBase.New(True)
        If IsInDesignMode Then
            SqlInfo = "Sehr viel Text mit Wörtern und so und der geht sicher über den Rand hinaus sodass Zeilenumbrüche benötigt werden."
        End If
    End Sub

#Region "Alt"

    'Dim _IsConnecting As Boolean = False
    'Public Property IsConnecting As Boolean
    '    Get
    '        Return _IsConnecting
    '    End Get
    '    Set(value As Boolean)
    '        ChangeIfDifferent(_IsConnecting, value, "IsConnecting")
    '    End Set
    'End Property

    'Dim _IsDisconnecting As Boolean = False
    'Public Property IsDisconnecting As Boolean
    '    Get
    '        Return _IsDisconnecting
    '    End Get
    '    Set(value As Boolean)
    '        ChangeIfDifferent(_IsDisconnecting, value, "IsDisconnecting")
    '    End Set
    'End Property

    'Dim _IsConnected As Boolean = False
    'Public Property IsConnected As Boolean
    '    Get
    '        Return _IsConnected
    '    End Get
    '    Set(value As Boolean)
    '        ChangeIfDifferent(_IsConnected, value, "IsConnected")
    '    End Set
    'End Property

    '<Dependency("IsConnecting", "IsDisconnecting", "IsConnected")>
    'Public ReadOnly Property EnableServerDataChange As Boolean
    '    Get
    '        Return Not _IsConnecting AndAlso Not _IsConnected
    '    End Get
    'End Property

    '<Dependency("IsConnecting", "IsDisconnecting", "IsConnected")>
    'Public ReadOnly Property ServerCanInteract As Boolean
    '    Get
    '        Return _IsConnected AndAlso Not _IsConnecting
    '    End Get
    'End Property

    '<Dependency("IsConnecting", "IsDisconnecting", "IsConnected")>
    'Public ReadOnly Property SqlInfo As String
    '    Get
    '        If _IsConnecting Then
    '            Return "Verbinde zum Server..."
    '        End If
    '        If _IsDisconnecting Then
    '            Return "Trenne Verbindung zum Server..."
    '        End If
    '        Return String.Empty
    '    End Get
    'End Property

    '<Dependency("IsConnecting", "IsDisconnecting", "IsConnected")>
    'Public ReadOnly Property CanConnectDisconnect As Boolean
    '    Get
    '        Return Not _IsConnecting AndAlso Not _IsDisconnecting
    '    End Get
    'End Property

    '<Dependency("SqlInfoTooltip")>
    'Public ReadOnly Property HasSqlInfoTooltip As Boolean
    '    Get
    '        Return Not String.IsNullOrEmpty(_SqlInfoTooltip)
    '    End Get
    'End Property

    'Dim _SqlInfoTooltip As String = ""
    'Public Property SqlInfoTooltip As String
    '    Get
    '        Return _SqlInfoTooltip
    '    End Get
    '    Set(value As String)
    '        ChangeIfDifferent(_SqlInfoTooltip, value, "SqlInfoTooltip")
    '    End Set
    'End Property

#End Region

End Class