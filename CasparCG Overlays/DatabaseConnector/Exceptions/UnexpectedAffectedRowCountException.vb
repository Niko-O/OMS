Public Class UnexpectedAffectedRowCountException
    Inherits ConnectorException

    Dim _ExpectedResult As Integer
    Public ReadOnly Property ExpectedResult As Integer
        Get
            Return _ExpectedResult
        End Get
    End Property

    Dim _ActualResult As Integer
    Public ReadOnly Property ActualResult As Integer
        Get
            Return _ActualResult
        End Get
    End Property

    Public Sub New(NewExpectedResult As Integer, NewActualResult As Integer)
        MyBase.New("Die SQL-Abfrage hat eine unerwartete Anzahl an betroffenen Zeilen zurückgegeben. Erwartet: " & NewExpectedResult.ToString & " Zuückgegeben: " & NewActualResult)
        _ExpectedResult = NewExpectedResult
        _ActualResult = NewActualResult
    End Sub

End Class
