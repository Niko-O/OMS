
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

    ''' <summary>
    ''' Der Wert dieser Eigenschaft, als Object.
    ''' </summary>
    Public ReadOnly Property Value As Object
        Get
            Return GetValue()
        End Get
    End Property

    ''' <summary>
    ''' Gibt beim Überschreiben den Wert der Eigenschaft zurück.
    ''' </summary>
    Protected MustOverride Function GetValue() As Object

    ''' <summary>
    ''' Konstruktor.
    ''' </summary>
    ''' <param name="NewOwner"><see cref="Owner"/></param>
    Public Sub New(NewOwner As SettingsStructure)
        _Owner = NewOwner
    End Sub

End Class
