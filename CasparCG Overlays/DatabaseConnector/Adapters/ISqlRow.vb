
''' <summary>
''' Stellt eine Tabellenzeile dar, die von einem Sql-Command zurückgegeben wird.
''' </summary>
Public Interface ISqlRow

    ''' <summary>
    ''' Ruft die Spalte mit dem angegebenen Index ab.
    ''' </summary>
    ''' <param name="ColumnIndex">Der Index der Spalte.</param>
    Function GetValue(Of T)(ColumnIndex As Integer) As T

    ''' <summary>
    ''' Ruft die Spalte mit dem angegebenen Namen ab.
    ''' </summary>
    ''' <param name="ColumnName">Der Name der Spalte.</param>
    Function GetValue(Of T)(ColumnName As String) As T

End Interface
