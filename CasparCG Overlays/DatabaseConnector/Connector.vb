
''' <summary>
''' Kapselt eine Verbindung zu einem SQL-Server.
''' </summary>
Public Class Connector
    Inherits Singleton(Of Connector)

    Public Event IsConnectedChanged()

    ''' <summary>
    ''' Gibt an, ob eine Verbindung zum SQL-Server besteht.
    ''' Verwenden Sie <see cref="Connect"/>, um die Verbindung aufzubauen, und  <see cref="Disconnect"/>, um die Verbindung zu beenden.
    ''' </summary>
    Public ReadOnly Property IsConnected As Boolean
        Get
            Return Adapter.IsConnected
        End Get
    End Property

    Dim _ServerName As String
    ''' <summary>
    ''' Der name, unter dem der Server erreichbar ist. Dies kann der UNC-Name des Computers (z.B. "\\MySqlServer"), eine URL (z.B. "www.mydomain.com") oder eine IP-Adresse (z.B. "192.168.90.104") sein.
    ''' </summary>
    Public Property ServerName As String
        Get
            Return _ServerName
        End Get
        Set(value As String)
            _ServerName = value
        End Set
    End Property

    Dim _SchemaName As String
    ''' <summary>
    ''' Der Name der Datenbank, auf die zugegriffen wird.
    ''' </summary>
    Public Property SchemaName As String
        Get
            Return _SchemaName
        End Get
        Set(value As String)
            _SchemaName = value
        End Set
    End Property

    Dim _UserName As String
    ''' <summary>
    ''' Der SQL-Benutzer-Name, der zur Authentisierung verwendet wird.
    ''' </summary>
    Public Property UserName As String
        Get
            Return _UserName
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    Dim _Password As System.Security.SecureString
    ''' <summary>
    ''' Das Passwort, das dem SQL-Benutzer zugeordnet ist.
    ''' </summary>
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

    ''' <summary>
    ''' Stellt eine Verbindung zum Sql-Server her.
    ''' </summary>
    Public Sub Connect()
        Adapter.Connect(_ServerName, _SchemaName, _UserName, _Password)
        Names(SqlName.Countries) = "countries"
        Names(SqlName.Countries_Id) = "Id"
        Names(SqlName.Countries_FullName) = "FullName"
        Names(SqlName.Countries_ShortName) = "ShortName"
        RaiseEvent IsConnectedChanged()
    End Sub

    ''' <summary>
    ''' Beendet die Verbindung zum Sql-Server.
    ''' </summary>
    Public Sub Disconnect()
        Adapter.Disconnect()
        RaiseEvent IsConnectedChanged()
    End Sub

    Private Sub EnsureConnected()
        If Not IsConnected Then
            Throw New NotConnectedException
        End If
    End Sub

    ''' <summary>
    ''' Ruft alle Länder der Datenbank ab.
    ''' </summary>
    Public Function GetCountries() As IEnumerable(Of SqlCountry)
        EnsureConnected()
        Dim Rows = Adapter.Select(Names(SqlName.Countries), {Names(SqlName.Countries_Id), Names(SqlName.Countries_FullName), Names(SqlName.Countries_ShortName)}, {Names(SqlName.Countries_FullName)})
        Dim Result As New List(Of SqlCountry)
        For Each i In Rows
            Result.Add(New SqlCountry(i.GetValue(Of Guid)(Names(SqlName.Countries_Id)), _
                                      i.GetValue(Of String)(Names(SqlName.Countries_FullName)), _
                                      i.GetValue(Of String)(Names(SqlName.Countries_ShortName))))
        Next
        Return Result
    End Function

    ''' <summary>
    ''' Fügt der Datenbank ein neues Land mit den angegebenen Namen hinzu.
    ''' Die Id wird automatisch erstellt.
    ''' </summary>
    ''' <param name="NewFullName"><see cref="CommonEventPluginData.ICountry.FullName"/></param>
    ''' <param name="NewShortName"><see cref="CommonEventPluginData.ICountry.ShortName"/></param>
    Public Function AddCountry(NewFullName As String, NewShortName As String) As Guid
        EnsureConnected()
        Dim NewId = Guid.NewGuid
        Dim Result = Adapter.Insert(Names(SqlName.Countries), _
                                    {New ColumnValue(Names(SqlName.Countries_Id), NewId), _
                                     New ColumnValue(Names(SqlName.Countries_FullName), NewFullName), _
                                     New ColumnValue(Names(SqlName.Countries_ShortName), NewShortName)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
        Return NewId
    End Function

    ''' <summary>
    ''' Fügt der Datenbank ein neues Land mit den angegebenen Namen hinzu und übernimmt die angegebene Guid.
    ''' Die Id wird automatisch erstellt.
    ''' </summary>
    ''' <param name="NewGuid"><see cref="SqlCountry.Guid"/></param>
    ''' <param name="NewFullName"><see cref="SqlCountry.FullName"/></param>
    ''' <param name="NewShortName"><see cref="SqlCountry.ShortName"/></param>
    Public Sub AddCountry(NewGuid As Guid, NewFullName As String, NewShortName As String)
        EnsureConnected()
        Dim Result = Adapter.Insert(Names(SqlName.Countries), _
                                    {New ColumnValue(Names(SqlName.Countries_Id), NewGuid), _
                                     New ColumnValue(Names(SqlName.Countries_FullName), NewFullName), _
                                     New ColumnValue(Names(SqlName.Countries_ShortName), NewShortName)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
    End Sub

    ''' <summary>
    ''' Ändert die Namen des angegebenen Landes auf die angegebenen Namen.
    ''' Das Land muss bereits in der Datenbank vorhanden sein.
    ''' </summary>
    ''' <param name="CountryGuid">Die Guid des Landes, das geändert wird.</param>
    ''' <param name="NewFullName"><see cref="SqlCountry.FullName"/></param>
    ''' <param name="NewShortName"><see cref="SqlCountry.ShortName"/></param>
    Public Sub UpdateCountry(CountryGuid As Guid, NewFullName As String, NewShortName As String)
        EnsureConnected()
        Dim Result = Adapter.Update(Names(SqlName.Countries), _
                                    {New ColumnValue(Names(SqlName.Countries_Id), CountryGuid)}, _
                                    {New ColumnValue(Names(SqlName.Countries_FullName), NewFullName), _
                                     New ColumnValue(Names(SqlName.Countries_ShortName), NewShortName)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
    End Sub

    ''' <summary>
    ''' Löscht das angegebene Land aus der Datenbank.
    ''' </summary>
    ''' <param name="CountryGuid">Die Guid des Landes, das aus der Datenbank gelöscht wird.</param>
    Public Sub DeleteCountry(CountryGuid As Guid)
        EnsureConnected()
        Dim Result = Adapter.Delete(Names(SqlName.Countries), {New ColumnValue(Names(SqlName.Countries_Id), CountryGuid)})
        If Not Result = 1 Then
            Throw New UnexpectedAffectedRowCountException(1, Result)
        End If
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
