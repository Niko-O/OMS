Public Structure ColumnValue

    Private _ColumnName As String
    Public ReadOnly Property ColumnName As String
        Get
            Return _ColumnName
        End Get
    End Property

    Private _Value As Object
    Public ReadOnly Property Value As Object
        Get
            Return _Value
        End Get
    End Property

    Public Sub New(NewColumnName As String, NewValue As Object)
        _ColumnName = NewColumnName
        _Value = NewValue
    End Sub

End Structure
