
Imports MySql.Data

Public Class MySqlAdapter
    Implements ISqlAdapter

    Private Shared GetFieldValueMethod As System.Reflection.MethodInfo = GetType(MySqlClient.MySqlDataReader).GetMethod("GetFieldValue", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
    Private Shared ValueProperty As System.Reflection.PropertyInfo = GetType(MySqlClient.MySqlDataReader).Assembly.GetType("MySql.Data.Types.IMySqlValue").GetProperty("Value", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)

    Dim Connection As MySqlClient.MySqlConnection

    Dim _IsConnected As Boolean
    Public ReadOnly Property IsConnected As Boolean Implements ISqlAdapter.IsConnected
        Get
            Return _IsConnected
        End Get
    End Property

    Public Sub Connect(ServerName As String, SchemaName As String, Username As String, Password As System.Security.SecureString) Implements ISqlAdapter.Connect
        Connection = New MySqlClient.MySqlConnection(String.Format("Server={0};Database={1};Uid={2};Pwd={3};", ServerName, SchemaName, Username, Connector.IReallyNeedThisPasswordAsANormalString(Password)))
        Try
            Connection.Open()
        Catch ex As MySqlClient.MySqlException
            Throw New ConnectorException("Beim Verbinden mit dem MySql-Server ist ein Fehler aufgetreten:" & Environment.NewLine & ex.Message, ex)
        End Try
        _IsConnected = True
    End Sub

    Public Sub Disconnect() Implements ISqlAdapter.Disconnect
        Connection.Close()
        Connection.Dispose()
        Connection = Nothing
        _IsConnected = False
    End Sub

    Public Function ListTables() As System.Collections.Generic.IEnumerable(Of ISqlRow) Implements ISqlAdapter.ListTables
        Throw New NotImplementedException
    End Function

    Public Function ListTableColumns(TableName As String) As IEnumerable(Of ISqlRow) Implements ISqlAdapter.ListTableColumns
        Throw New NotImplementedException
    End Function

    Public Function [Select](TableName As String, Columns() As String, OrderByColumns As String()) As System.Collections.Generic.IEnumerable(Of ISqlRow) Implements ISqlAdapter.Select
        'Return {New DebugSqlRow(New ColumnValue("Id", Guid.NewGuid.ToString), New ColumnValue("FullName", "Austria"), New ColumnValue("ShortName", "AUT")), _
        '        New DebugSqlRow(New ColumnValue("Id", Guid.NewGuid.ToString), New ColumnValue("FullName", "Nope"), New ColumnValue("ShortName", "NOP")), _
        '        New DebugSqlRow(New ColumnValue("Id", Guid.NewGuid.ToString), New ColumnValue("FullName", "England"), New ColumnValue("ShortName", "ENG"))}
        Dim Rows As New List(Of MySqlReaderRow)
        Using Command = Connection.CreateCommand
            If OrderByColumns Is Nothing OrElse OrderByColumns.Length = 0 Then
                Command.CommandText = String.Format("SELECT {0} FROM {1}", _
                                                    String.Join(", ", Columns), _
                                                    TableName)
            Else
                Command.CommandText = String.Format("SELECT {0} FROM {1} ORDER BY {2}", _
                                                    String.Join(", ", Columns), _
                                                    TableName, _
                                                    String.Join(", ", OrderByColumns))
            End If
            Using Reader = Command.ExecuteReader
                While Reader.Read
                    Dim Values As New List(Of ColumnValue)
                    For i = 0 To Reader.FieldCount - 1
                        Dim MySqlValue = GetFieldValueMethod.Invoke(Reader, New Object() {i, False})
                        Dim Value = ValueProperty.GetValue(MySqlValue, Nothing)
                        Values.Add(New ColumnValue(Columns(i), Value))
                    Next
                    Rows.Add(New MySqlReaderRow(Values))
                End While
            End Using
        End Using
        Return Rows
    End Function

    Public Function Insert(TableName As String, Columns() As ColumnValue) As Integer Implements ISqlAdapter.Insert
        Using Command = Connection.CreateCommand
            Command.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", _
                                                TableName, _
                                                String.Join(", ", Columns.Select(Function(i) i.ColumnName)), _
                                                String.Join(", ", Columns.Select(Function(i) "@Param_Column_" & i.ColumnName)))
            For Each i In Columns
                Command.Parameters.Add(New MySqlClient.MySqlParameter("@Param_Column_" & i.ColumnName, i.Value))
            Next
            Return Command.ExecuteNonQuery
        End Using
    End Function

    Public Function Update(TableName As String, Selectors() As ColumnValue, UpdateValues() As ColumnValue) As Integer Implements ISqlAdapter.Update
        Using Command = Connection.CreateCommand
            Command.CommandText = String.Format("UPDATE {0} SET {1} WHERE {2}", _
                                                TableName, _
                                                String.Join(", ", UpdateValues.Select(Function(i) i.ColumnName & " = @Param_UpdateValue_" & i.ColumnName)), _
                                                String.Join(" AND ", Selectors.Select(Function(i) i.ColumnName & " = @Param_Selector_" & i.ColumnName)))
            For Each i In UpdateValues
                Command.Parameters.Add(New MySqlClient.MySqlParameter("@Param_UpdateValue_" & i.ColumnName, i.Value))
            Next
            For Each i In Selectors
                Command.Parameters.Add(New MySqlClient.MySqlParameter("@Param_selector_" & i.ColumnName, i.Value))
            Next
            Return Command.ExecuteNonQuery
        End Using
    End Function

    Public Function Delete(TableName As String, Selectors() As ColumnValue) As Integer Implements ISqlAdapter.Delete
        Using Command = Connection.CreateCommand
            Command.CommandText = String.Format("DELETE FROM {0} WHERE {1}", _
                                                TableName, _
                                                String.Join(" AND ", Selectors.Select(Function(i) i.ColumnName & " = @Param_Selector_" & i.ColumnName)))
            For Each i In Selectors
                Command.Parameters.Add(New MySqlClient.MySqlParameter("@Param_selector_" & i.ColumnName, i.Value))
            Next
            Return Command.ExecuteNonQuery
        End Using
    End Function

End Class
