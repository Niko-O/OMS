Public Class EditCountriesTableWindowViewModel
    Inherits ViewModelBase

    Dim OriginalCountries As IEnumerable(Of SqlCountry) = Nothing

    Public ReadOnly Property AddCountryCommand As DelegateCommand
        Get
            Static Temp As New DelegateCommand(Sub() AddNewCountry())
            Return Temp
        End Get
    End Property

    Dim _ChangedCountries As New List(Of SqlCountryViewModel)
    Public ReadOnly Property ChangedCountries As IEnumerable(Of SqlCountryViewModel)
        Get
            Return _ChangedCountries
        End Get
    End Property

    Dim _AddedCountries As New List(Of SqlCountryViewModel)
    Public ReadOnly Property AddedCountries As IEnumerable(Of SqlCountryViewModel)
        Get
            Return _AddedCountries
        End Get
    End Property

    Dim _RemovedCountries As New List(Of Guid)
    Public ReadOnly Property RemovedCountries As IEnumerable(Of Guid)
        Get
            Return _RemovedCountries
        End Get
    End Property

    Dim _CountriesViewModels As New System.Collections.ObjectModel.ObservableCollection(Of SqlCountryViewModel)
    Public ReadOnly Property CountriesViewModels As System.Collections.ObjectModel.ObservableCollection(Of SqlCountryViewModel)
        Get
            Return _CountriesViewModels
        End Get
    End Property

    Public ReadOnly Property HasChanges As Boolean
        Get
            Return _ChangedCountries.Count <> 0 OrElse _AddedCountries.Count <> 0 OrElse _RemovedCountries.Count <> 0
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
            SetOriginalCountries({New SqlCountry(Guid.NewGuid, "Austria 1", "AU1"), _
                                  New SqlCountry(Guid.NewGuid, "Austria 2", "AU2"), _
                                  New SqlCountry(Guid.NewGuid, "Austria 3", "AU3"), _
                                  New SqlCountry(Guid.NewGuid, "Austria 4", "AU4"), _
                                  New SqlCountry(Guid.NewGuid, "Austria 5", "AU5"), _
                                  New SqlCountry(Guid.NewGuid, "Austria 6", "AU6"), _
                                  New SqlCountry(Guid.NewGuid, "Austria 7", "AU7")})
        End If
    End Sub

    Private Sub OnHasChangesChanged()
        OnPropertyChanged("HasChanges")
    End Sub

    Public Sub SetOriginalCountries(NewOriginalCountries As IEnumerable(Of SqlCountry))
        OriginalCountries = NewOriginalCountries
        _ChangedCountries.Clear()
        _RemovedCountries.Clear()
        _AddedCountries.Clear()
        _CountriesViewModels.Clear()
        For Each i In OriginalCountries
            Dim NewVm As New SqlCountryViewModel(i)
            AddHandler NewVm.Remove, AddressOf RemoveSqlCountry
            AddHandler NewVm.NamesChanged, AddressOf SqlCountryNamesChanged
            _CountriesViewModels.Add(NewVm)
        Next
        OnHasChangesChanged()
    End Sub

    Private Sub RemoveSqlCountry(Sender As SqlCountryViewModel)
        If Sender.OriginalCountry Is Nothing Then
            _AddedCountries.Remove(Sender)
        Else
            _RemovedCountries.Add(Sender.OriginalCountry.Guid)
        End If
        _CountriesViewModels.Remove(Sender)
        OnHasChangesChanged()
    End Sub

    Private Sub SqlCountryNamesChanged(Sender As SqlCountryViewModel)
        If Not Sender.OriginalCountry Is Nothing Then
            If Not _ChangedCountries.Contains(Sender) Then
                _ChangedCountries.Add(Sender)
                OnHasChangesChanged()
            End If
        End If
    End Sub

    Private Sub AddNewCountry()
        Dim NewVm As New SqlCountryViewModel
        AddHandler NewVm.Remove, AddressOf RemoveSqlCountry
        AddHandler NewVm.NamesChanged, AddressOf SqlCountryNamesChanged
        _AddedCountries.Add(NewVm)
        _CountriesViewModels.Add(NewVm)
        OnHasChangesChanged()
    End Sub

End Class