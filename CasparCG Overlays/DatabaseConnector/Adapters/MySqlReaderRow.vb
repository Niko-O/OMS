Public Class MySqlReaderRow
    Implements ISqlRow

    Dim Values As IEnumerable(Of ColumnValue)

    Public Sub New(NewValues As IEnumerable(Of ColumnValue))
        Values = NewValues
    End Sub

    Public Function GetValue(Of T)(ColumnIndex As Integer) As T Implements ISqlRow.GetValue
        Return DirectCast(Values(ColumnIndex).Value, T)
    End Function

    Public Function GetValue(Of T)(ColumnName As String) As T Implements ISqlRow.GetValue
        Return DirectCast(Values.Single(Function(i) i.ColumnName = ColumnName).Value, T)
    End Function

End Class
