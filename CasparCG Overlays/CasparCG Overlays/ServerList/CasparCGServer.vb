
Namespace ServerList

    ''' <summary>
    ''' Stellt einen Eintrag in der Server-Liste des CasparCG-Tabs dar.
    ''' </summary>
    Public Class CasparCGServer
        Inherits NotifyPropertyChanged

        Dim _Name As String = Nothing
        ''' <summary>
        ''' Der Name des Eintrags.
        ''' </summary>
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                ChangeIfDifferent(_Name, value, "Name")
            End Set
        End Property

        Dim _Address As String = Nothing
        ''' <summary>
        ''' Die Adresse des Eintrags.
        ''' </summary>
        Public Property Address As String
            Get
                Return _Address
            End Get
            Set(value As String)
                ChangeIfDifferent(_Address, value, "Address")
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Sub New(NewName As String, NewAddress As String)
            _Name = NewName
            _Address = NewAddress
        End Sub

    End Class

End Namespace
