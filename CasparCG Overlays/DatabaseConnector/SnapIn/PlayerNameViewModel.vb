Public Class PlayerNameViewModel
    Inherits ViewModelBase

    Public Event NamesChanged(Sender As PlayerNameViewModel)

    Public Event Remove(Sender As PlayerNameViewModel)
    Public ReadOnly Property RemoveCommand As DelegateCommand
        Get
            Static Temp As New DelegateCommand(Sub() RaiseEvent Remove(Me))
            Return Temp
        End Get
    End Property

    Dim _OriginalPlayerName As PlayerName
    Public ReadOnly Property OriginalPlayerName As PlayerName
        Get
            Return _OriginalPlayerName
        End Get
    End Property

    Dim _FirstName As String = Nothing
    Public Property FirstName As String
        Get
            Return _FirstName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_FirstName, value, "FirstName") Then
                RaiseEvent NamesChanged(Me)
            End If
        End Set
    End Property

    Dim _LastName As String = Nothing
    Public Property LastName As String
        Get
            Return _LastName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_LastName, value, "LastName") Then
                RaiseEvent NamesChanged(Me)
            End If
        End Set
    End Property

    Dim _ShortName As String = Nothing
    Public Property ShortName As String
        Get
            Return _ShortName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_ShortName, value, "ShortName") Then
                RaiseEvent NamesChanged(Me)
            End If
        End Set
    End Property

    Dim _GuidInfo As String
    Public ReadOnly Property GuidInfo As String
        Get
            Return _GuidInfo
        End Get
    End Property

    Public Sub New()
        _OriginalPlayerName = Nothing
        _FirstName = "Vorname"
        _LastName = "Nachname"
        _ShortName = "Kurzname"
        _GuidInfo = "Wird erstellt"
    End Sub

    Public Sub New(NewOriginalPlayerName As PlayerName)
        _OriginalPlayerName = NewOriginalPlayerName
        _FirstName = _OriginalPlayerName.FirstName
        _LastName = _OriginalPlayerName.LastName
        _ShortName = _OriginalPlayerName.ShortName
        _GuidInfo = _OriginalPlayerName.Guid.ToString
    End Sub

End Class
