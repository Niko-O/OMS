Public Class PlayerName
    Implements TennisNameData.IPlayerName

    Private _Guid As Guid
    Public ReadOnly Property Guid As Guid
        Get
            Return _Guid
        End Get
    End Property

    Dim _FirstName As String
    Public Property FirstName As String Implements TennisNameData.IPlayerName.FirstName
        Get
            Return _FirstName
        End Get
        Set(value As String)
            _FirstName = value
        End Set
    End Property

    Dim _LastName As String
    Public Property LastName As String Implements TennisNameData.IPlayerName.LastName
        Get
            Return _LastName
        End Get
        Set(value As String)
            _LastName = value
        End Set
    End Property

    Dim _ShortName As String
    Public Property ShortName As String Implements TennisNameData.IPlayerName.ShortName
        Get
            Return _ShortName
        End Get
        Set(value As String)
            _ShortName = value
        End Set
    End Property

    Public Sub New(NewGuid As Guid, NewFirstName As String, NewLastName As String, NewShortName As String)
        _Guid = NewGuid
        _FirstName = NewFirstName
        _LastName = NewLastName
        _ShortName = NewShortName
    End Sub

End Class
