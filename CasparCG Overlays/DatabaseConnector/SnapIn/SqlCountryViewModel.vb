Public Class SqlCountryViewModel
    Inherits ViewModelBase

    Public Event NamesChanged(Sender As SqlCountryViewModel)

    Public Event Remove(Sender As SqlCountryViewModel)
    Public ReadOnly Property RemoveCommand As DelegateCommand
        Get
            Static Temp As New DelegateCommand(Sub() RaiseEvent Remove(Me))
            Return Temp
        End Get
    End Property

    Dim _OriginalCountry As SqlCountry
    Public ReadOnly Property OriginalCountry As SqlCountry
        Get
            Return _OriginalCountry
        End Get
    End Property

    Dim _FullName As String = Nothing
    Public Property FullName As String
        Get
            Return _FullName
        End Get
        Set(value As String)
            If ChangeIfDifferent(_FullName, value, "FullName") Then
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
        _OriginalCountry = Nothing
        _FullName = "Neues Land"
        _ShortName = ""
        _GuidInfo = "Wird erstellt"
    End Sub

    Public Sub New(NewOriginalCountry As SqlCountry)
        _OriginalCountry = NewOriginalCountry
        _FullName = _OriginalCountry.FullName
        _ShortName = _OriginalCountry.ShortName
        _GuidInfo = _OriginalCountry.Guid.ToString
    End Sub

End Class
