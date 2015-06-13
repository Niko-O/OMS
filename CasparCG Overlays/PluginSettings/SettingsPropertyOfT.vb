
''' <summary>
''' Stellt eine Eigenschaft mit Standardwert dar und kapstelt einen Wert vom Typ T.
''' </summary>
Public NotInheritable Class SettingsProperty(Of T)
    Inherits SettingsProperty

    Dim _Value As T
    ''' <summary>
    ''' Der Wert der Eigenschaft.
    ''' </summary>
    Public Shadows Property Value As T
        Get
            Return _Value
        End Get
        Set(value As T)
            _Value = value
        End Set
    End Property

    Dim _DefaultValue As T
    ''' <summary>
    ''' Der Standardwert der Eigenschaft.
    ''' </summary>
    Public Shadows Property DefaultValue As T
        Get
            Return _DefaultValue
        End Get
        Set(value As T)
            _DefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Konstruktor.
    ''' </summary>
    ''' <param name="NewOwner"><see cref="Owner"/></param>
    ''' <param name="NewName"><see cref="Name"/></param>
    ''' <param name="NewDefaultValue"><see cref="DefaultValue"/></param>
    Public Sub New(NewOwner As SettingsStructure, NewName As String, NewDefaultValue As T)
        MyBase.New(NewOwner, NewName, GetType(T))
        _DefaultValue = NewDefaultValue
        _Value = NewDefaultValue
    End Sub

    Friend Overrides Function GetValue() As Object
        Return _Value
    End Function

    Friend Overrides Sub SetValue(NewValue As Object)
        _Value = DirectCast(NewValue, T)
    End Sub

    Friend Overrides Function GetDefaultValue() As Object
        Return _DefaultValue
    End Function

End Class
