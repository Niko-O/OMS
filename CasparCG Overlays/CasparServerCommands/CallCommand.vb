Public Class CallCommand
    Implements ICasparServerCommand

    Dim _MethodName As String
    Public ReadOnly Property MethodName As String
        Get
            Return _MethodName
        End Get
    End Property

    Dim _Parameters As Object()
    Public ReadOnly Property Parameters As Object()
        Get
            Return _Parameters
        End Get
    End Property

    Public Sub New(NewMethodName As String, ParamArray NewParameters As Object())
        If String.IsNullOrWhiteSpace(NewMethodName) Then
            Throw New ArgumentNullException("NewMethodName")
        End If
        If NewParameters Is Nothing Then
            Throw New ArgumentNullException("NewParameters")
        End If
        For Each i In NewParameters
            If i Is Nothing Then
                Throw New ArgumentNullException("NewParameters", "Ein Element in NewParameters ist null.")
            End If
        Next
        _MethodName = NewMethodName
        _Parameters = NewParameters
    End Sub

    Public Function GetCommandString() As String Implements ICasparServerCommand.GetCommandString
        Return String.Format("CALL {0}({1})", _MethodName, String.Join(", ", _Parameters))
    End Function

End Class
