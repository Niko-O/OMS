
''' <summary>
''' Stellt den untypisierten Zugriff auf eine <see cref="SettingsProperty(Of T)"/> dar, z.B. für die Verwendung in Listen ungleichartiger Eigenschaften.
''' </summary>
Public MustInherit Class SettingsProperty

    Dim _Owner As SettingsStructure
    ''' <summary>
    ''' Die Einstellungs-Struktur, zu der diese Eigenschaft gehört.
    ''' </summary>
    Public ReadOnly Property Owner As SettingsStructure
        Get
            Return _Owner
        End Get
    End Property

    Dim _Name As String
    ''' <summary>
    ''' Der Name der Eigenschaft.
    ''' Innerhalb einer Einstellungs-Struktur dürfen Namen nicht mehrmals verwendet werden.
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Dim _Type As Type
    ''' <summary>
    ''' Der Typ der Eigenschaft.
    ''' </summary>
    Public ReadOnly Property Type As Type
        Get
            Return _Type
        End Get
    End Property

    ''' <summary>
    ''' Gibt beim Überschreiben den Wert der Eigenschaft als Object zurück.
    ''' </summary>
    Friend MustOverride Function GetValue() As Object

    ''' <summary>
    ''' Setzt beim Überschreiben den Wert der Eigenschaft auf den angegebenen Wert.
    ''' Diese Methode ist für die Verwendung von <see cref="SettingsProviderBase"/> vorgesehen.
    ''' </summary>
    ''' <param name="NewValue">Der neue Wert.</param>
    Friend MustOverride Sub SetValue(NewValue As Object)

    ''' <summary>
    ''' Gibt beim Überschreiben den Standardwert der Eigenschaft als Object zurück.
    ''' </summary>
    Friend MustOverride Function GetDefaultValue() As Object

    ''' <summary>
    ''' Konstruktor.
    ''' </summary>
    ''' <param name="NewOwner"><see cref="Owner"/></param>
    ''' <param name="NewType"><see cref="Type"/></param>
    Public Sub New(NewOwner As SettingsStructure, NewName As String, NewType As Type)
        _Owner = NewOwner
        _Name = NewName
        _Type = NewType
    End Sub

End Class
