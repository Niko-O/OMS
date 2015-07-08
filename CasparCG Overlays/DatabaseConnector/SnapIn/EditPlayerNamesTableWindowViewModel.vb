Public Class EditPlayerNamesTableWindowViewModel
    Inherits ViewModelBase

    Dim OriginalPlayerNames As IEnumerable(Of PlayerName) = Nothing

    Public ReadOnly Property AddCountryCommand As DelegateCommand
        Get
            Static Temp As New DelegateCommand(Sub() AddNewCountry())
            Return Temp
        End Get
    End Property

    Dim _ChangedPlayerNames As New List(Of PlayerNameViewModel)
    Public ReadOnly Property ChangedPlayerNames As IEnumerable(Of PlayerNameViewModel)
        Get
            Return _ChangedPlayerNames
        End Get
    End Property

    Dim _AddedPlayerNames As New List(Of PlayerNameViewModel)
    Public ReadOnly Property AddedPlayerNames As IEnumerable(Of PlayerNameViewModel)
        Get
            Return _AddedPlayerNames
        End Get
    End Property

    Dim _RemovedPlayerNames As New List(Of PlayerName)
    Public ReadOnly Property RemovedPlayerNames As IEnumerable(Of PlayerName)
        Get
            Return _RemovedPlayerNames
        End Get
    End Property

    Dim _PlayerNamesViewModels As New System.Collections.ObjectModel.ObservableCollection(Of PlayerNameViewModel)
    Public ReadOnly Property PlayerNamesViewModels As System.Collections.ObjectModel.ObservableCollection(Of PlayerNameViewModel)
        Get
            Return _PlayerNamesViewModels
        End Get
    End Property

    Public ReadOnly Property HasChanges As Boolean
        Get
            Return _ChangedPlayerNames.Count <> 0 OrElse _AddedPlayerNames.Count <> 0 OrElse _RemovedPlayerNames.Count <> 0
        End Get
    End Property

    Dim _IsConnected As Boolean = True
    Public Property IsConnected As Boolean
        Get
            Return _IsConnected
        End Get
        Set(value As Boolean)
            ChangeIfDifferent(_IsConnected, value, "IsConnected")
        End Set
    End Property

    Dim _IsLoading As Boolean = False
    Public Property IsLoading As Boolean
        Get
            Return _IsLoading
        End Get
        Set(value As Boolean)
            ChangeIfDifferent(_IsLoading, value, "IsLoading")
        End Set
    End Property

    <Dependency("IsConnected", "IsLoading")>
    Public ReadOnly Property CanInteract As Boolean
        Get
            Return _IsConnected AndAlso Not _IsLoading
        End Get
    End Property

    Public Sub New()
        MyBase.New(True)
        If IsInDesignMode Then
            SetOriginalPlayerNames({New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai"), _
                                    New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai"), _
                                    New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai"), _
                                    New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai"), _
                                    New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai"), _
                                    New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai"), _
                                    New PlayerName(Guid.NewGuid, "Sepp", "Maier", "SMai")})
        End If
    End Sub

    Private Sub OnHasChangesChanged()
        OnPropertyChanged("HasChanges")
    End Sub

    Public Sub SetOriginalPlayerNames(NewOriginalCountries As IEnumerable(Of PlayerName))
        OriginalPlayerNames = NewOriginalCountries
        _ChangedPlayerNames.Clear()
        _RemovedPlayerNames.Clear()
        _AddedPlayerNames.Clear()
        _PlayerNamesViewModels.Clear()
        For Each i In OriginalPlayerNames
            Dim NewVm As New PlayerNameViewModel(i)
            AddHandler NewVm.Remove, AddressOf RemovePlayerName
            AddHandler NewVm.NamesChanged, AddressOf PlayerNameNamesChanged
            _PlayerNamesViewModels.Add(NewVm)
        Next
        OnHasChangesChanged()
    End Sub

    Private Sub RemovePlayerName(Sender As PlayerNameViewModel)
        If Sender.OriginalPlayerName Is Nothing Then
            _AddedPlayerNames.Remove(Sender)
        Else
            _RemovedPlayerNames.Add(Sender.OriginalPlayerName)
        End If
        _PlayerNamesViewModels.Remove(Sender)
        OnHasChangesChanged()
    End Sub

    Private Sub PlayerNameNamesChanged(Sender As PlayerNameViewModel)
        If Not Sender.OriginalPlayerName Is Nothing Then
            If Not _ChangedPlayerNames.Contains(Sender) Then
                _ChangedPlayerNames.Add(Sender)
                OnHasChangesChanged()
            End If
        End If
    End Sub

    Private Sub AddNewCountry()
        Dim NewVm As New PlayerNameViewModel
        AddHandler NewVm.Remove, AddressOf RemovePlayerName
        AddHandler NewVm.NamesChanged, AddressOf PlayerNameNamesChanged
        _AddedPlayerNames.Add(NewVm)
        _PlayerNamesViewModels.Add(NewVm)
        OnHasChangesChanged()
    End Sub

End Class