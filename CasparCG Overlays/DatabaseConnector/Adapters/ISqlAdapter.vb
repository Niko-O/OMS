
''' <summary>
''' Stellt eine Abstraktionsschicht für den Zugriff auf SQL-Server dar.
''' </summary>
Public Interface ISqlAdapter

    ''' <summary>
    ''' Gibt an, ob eine Verbindung zum Sql-Server besteht.
    ''' </summary>
    ReadOnly Property IsConnected As Boolean

    ''' <summary>
    ''' Stellt eine Verbindung zum Sql-Server her.
    ''' </summary>
    Sub Connect(ServerName As String, SchemaName As String, Username As String, Password As System.Security.SecureString)

    ''' <summary>
    ''' Beendet die Verbindung zum Sql-Server.
    ''' </summary>
    Sub Disconnect()

    ''' <summary>
    ''' Führt ein Select From auf dem Server aus.
    ''' </summary>
    ''' <param name="TableName">Die Tabelle, aus der Daten gelesen werden.</param>
    ''' <param name="Columns">Die Spalten, die ausgewählt werden.</param>
    Function [Select](TableName As String, Columns As String(), OrderByColumns As String()) As IEnumerable(Of ISqlRow)

    ''' <summary>
    ''' Führt ein Insert Into auf dem Server aus.
    ''' </summary>
    ''' <param name="TableName">Die Tabelle, in die Daten eingefügt werden.</param>
    ''' <param name="Columns">Die Spaltennamen und darin eingefügten Werte.</param>
    Function Insert(TableName As String, Columns As ColumnValue()) As Integer

    ''' <summary>
    ''' Führt ein Update auf dem Server aus.
    ''' </summary>
    ''' <param name="TableName">Die Tabelle, in der Daten geändert werden.</param>
    ''' <param name="Selectors">Die Bedingungen, nach denen der zu ändernde Datensatz ausgewählt wird.</param>
    ''' <param name="UpdateValues">Die Spaltennamen und neuen Werte.</param>
    Function Update(TableName As String, Selectors As ColumnValue(), UpdateValues As ColumnValue()) As Integer

    ''' <summary>
    ''' Führt ein Delete From auf dem Server aus.
    ''' </summary>
    ''' <param name="TableName">Die Tabelle, aus der Daten gelöscht werden.</param>
    ''' <param name="Selectors">Die Bedingungen, nach denen die zu löschenden Datensätze ausgewählt werden.</param>
    Function Delete(TableName As String, Selectors As ColumnValue()) As Integer

End Interface
