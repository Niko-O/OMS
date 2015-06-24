
''' <summary>
''' Kapselt eine Verbindung zu einem SQL-Server.
''' </summary>
Public Class Connector
    Inherits Singleton(Of Connector)

    Private Enum SqlName As Integer
        PlayerNamesTable = 1
        PlayerNamesIdColumn = 2
        PlayerNamesFirstNameColumn = 3
        PlayerNamesLastNameColumn = 4
        PlayerNamesShortNameColumn = 5
    End Enum

    Public Event IsConnectedChanged()
    Public Event NamesTableChanged()

    Public ReadOnly Property IsConnected As Boolean
        Get
            Return Adapter.IsConnected
        End Get
    End Property

    Dim _ServerName As String
    Public Property ServerName As String
        Get
            Return _ServerName
        End Get
        Set(value As String)
            _ServerName = value
        End Set
    End Property

    Dim _SchemaName As String
    Public Property SchemaName As String
        Get
            Return _SchemaName
        End Get
        Set(value As String)
            _SchemaName = value
        End Set
    End Property

    Dim _UserName As String
    Public Property UserName As String
        Get
            Return _UserName
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    Dim _Password As System.Security.SecureString
    Public Property Password As System.Security.SecureString
        Get
            Return _Password
        End Get
        Set(value As System.Security.SecureString)
            _Password = value
        End Set
    End Property

    Dim Adapter As ISqlAdapter
    Dim Names As New Dictionary(Of SqlName, String)

    Public Sub New()
        Adapter = New MySqlAdapter
    End Sub

    Public Sub Connect()
        Adapter.Connect(_ServerName, _SchemaName, _UserName, _Password)
        Names(SqlName.PlayerNamesTable) = "PlayerNames"
        Names(SqlName.PlayerNamesIdColumn) = "Id"
        Names(SqlName.PlayerNamesFirstNameColumn) = "FirstName"
        Names(SqlName.PlayerNamesLastNameColumn) = "LastName"
        Names(SqlName.PlayerNamesShortNameColumn) = "ShortName"
        RaiseEvent IsConnectedChanged()
    End Sub

    Public Sub Disconnect()
        Adapter.Disconnect()
        RaiseEvent IsConnectedChanged()
    End Sub

    Private Sub EnsureConnected()
        If Not IsConnected Then
            Throw New NotConnectedException
        End If
    End Sub

    Public Function GetPlayerNames() As IEnumerable(Of PlayerName)
        EnsureConnected()
        Dim Rows = Adapter.Select(Names(SqlName.PlayerNamesTable), _
                                  {Names(SqlName.PlayerNamesIdColumn), _
                                   Names(SqlName.PlayerNamesFirstNameColumn), _
                                   Names(SqlName.PlayerNamesLastNameColumn), _
                                   Names(SqlName.PlayerNamesShortNameColumn)}, _
                                  {Names(SqlName.PlayerNamesLastNameColumn)})
        Dim Result As New List(Of PlayerName)
        For Each i In Rows
            Result.Add(New PlayerName(i.GetValue(Of Guid)(Names(SqlName.PlayerNamesIdColumn)), _
                                      i.GetValue(Of String)(Names(SqlName.PlayerNamesFirstNameColumn)), _
                                      i.GetValue(Of String)(Names(SqlName.PlayerNamesLastNameColumn)), _
                                      i.GetValue(Of String)(Names(SqlName.PlayerNamesShortNameColumn))))
        Next
        Return Result
    End Function

    Public Function AddNewPlayerName(NewFirstName As String, NewLastName As String, NewShortName As String) As PlayerName
        EnsureConnected()
        Dim NewId = Guid.NewGuid
        Dim Result = Adapter.Insert(Names(SqlName.PlayerNamesTable), _
                                    {New ColumnValue(Names(SqlName.PlayerNamesIdColumn), NewId), _
                                     New ColumnValue(Names(SqlName.PlayerNamesFirstNameColumn), NewFirstName), _
                                     New ColumnValue(Names(SqlName.PlayerNamesLastNameColumn), NewLastName), _
                                     New ColumnValue(Names(SqlName.PlayerNamesShortNameColumn), NewShortName)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
        RaiseEvent NamesTableChanged()
        Return New PlayerName(NewId, NewFirstName, NewLastName, NewShortName)
    End Function

    Public Sub UpdatePlayerName(Name As PlayerName, NewFirstName As String, NewLastName As String, NewShortName As String)
        EnsureConnected()
        Dim Result = Adapter.Update(Names(SqlName.PlayerNamesTable), _
                                    {New ColumnValue(Names(SqlName.PlayerNamesIdColumn), Name.Guid)}, _
                                    {New ColumnValue(Names(SqlName.PlayerNamesFirstNameColumn), NewFirstName), _
                                     New ColumnValue(Names(SqlName.PlayerNamesLastNameColumn), NewLastName), _
                                     New ColumnValue(Names(SqlName.PlayerNamesShortNameColumn), NewShortName)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
        Name.FirstName = NewFirstName
        Name.LastName = NewLastName
        Name.ShortName = NewShortName
        RaiseEvent NamesTableChanged()
    End Sub

    Public Sub DeletePlayerName(Name As PlayerName)
        EnsureConnected()
        Dim Result = Adapter.Delete(Names(SqlName.PlayerNamesTable), {New ColumnValue(Names(SqlName.PlayerNamesIdColumn), Name.Guid)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
        RaiseEvent NamesTableChanged()
    End Sub

    Public Shared Function IReallyNeedThisPasswordAsANormalString(Password As System.Security.SecureString) As String
        If Password Is Nothing Then
            Return Nothing
        End If
        Dim Pointer = IntPtr.Zero
        Try
            Pointer = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(Password)
            Return System.Runtime.InteropServices.Marshal.PtrToStringUni(Pointer)
        Finally
            If Not Pointer = IntPtr.Zero Then
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(Pointer)
            End If
        End Try
    End Function

End Class
