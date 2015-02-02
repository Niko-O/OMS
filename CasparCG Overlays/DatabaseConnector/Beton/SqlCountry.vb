
''' <summary>
''' Ein Land, das im Sql-Server hinterlegt ist.
''' </summary>
Public Class SqlCountry
    Implements CommonEventPluginData.ICountry

    Private _Guid As Guid
    ''' <summary>
    ''' Gibt die Guid an, die im Server hinterlegt ist.
    ''' </summary>
    Public ReadOnly Property Guid As Guid
        Get
            Return _Guid
        End Get
    End Property

    Private _FullName As String
    ''' <summary>
    ''' <see cref="CommonEventPluginData.ICountry.FullName"/>
    ''' </summary>
    Public Property FullName As String Implements CommonEventPluginData.ICountry.FullName
        Get
            Return _FullName
        End Get
        Set(value As String)
            _FullName = value
        End Set
    End Property

    Private _ShortName As String
    ''' <summary>
    ''' <see cref="CommonEventPluginData.ICountry.ShortName"/>
    ''' </summary>
    Public Property ShortName As String Implements CommonEventPluginData.ICountry.ShortName
        Get
            Return _ShortName
        End Get
        Set(value As String)
            _ShortName = value
        End Set
    End Property

    Public Sub New(NewId As Guid, NewFullName As String, NewShortName As String)
        _Guid = NewId
        _FullName = NewFullName
        _ShortName = NewShortName
    End Sub

    ''' <summary>
    ''' Erstellt ein neues Land mit einer neuen <see cref="Guid"/> und dem angegebenen Namen und Kürzel.
    ''' </summary>
    Public Shared Function CreateNew(NewFullName As String, NewShortName As String) As SqlCountry
        Return New SqlCountry(Guid.NewGuid, NewFullName, NewShortName)
    End Function

End Class
