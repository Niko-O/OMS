
''' <summary>
''' Beinhaltet Eigenschaften, die Werte beinhalten, und weitere, untergeordnete Strukturen, die weitere Eigenschaften beinhalten können.
''' </summary>
Public MustInherit Class SettingsStructure

    Dim _Owner As SettingsStructure = Nothing
    ''' <summary>
    ''' Die Ebene, der diese Eigenschaften-Struktur untergeordnet ist.
    ''' Wenn die aktuelle Instanz die oberste Ebene ist, ist Owner Nothing.
    ''' </summary>
    Public ReadOnly Property Owner As SettingsStructure
        Get
            Return _Owner
        End Get
    End Property

    Dim _Properties As New List(Of SettingsProperty)
    ''' <summary>
    ''' Eine Auflistung aller in dieser Ebene enthaltenen Eigenschaften.
    ''' Eigenschaften untergeordneter Strukturen sind nicht enthalten.
    ''' </summary>
    Public ReadOnly Property Properties As IEnumerable(Of SettingsProperty)
        Get
            Return _Properties
        End Get
    End Property

    Dim _SubStructures As New List(Of SettingsStructure)
    ''' <summary>
    ''' Eine Auflistung aller dieser Ebene untergeordneten Strukturen.
    ''' </summary>
    Public ReadOnly Property SubStructures As IEnumerable(Of SettingsStructure)
        Get
            Return _SubStructures
        End Get
    End Property

    'Public Function GetProperties() As IEnumerable(Of SettingsProperty)
    '    Dim Temp As New List(Of SettingsProperty)
    '    Temp.AddRange(_Properties)
    '    For Each i In _SubContainers
    '        Temp.AddRange(i.GetProperties)
    '    Next
    '    Return Temp
    'End Function

    ''' <summary>
    ''' Registriert eine Eigenschaft in dieser Ebene und gibt ein <see cref="SettingsProperty(Of T)"/>-Objekt zurück, das den Wert dieser Eigenschaft kapselt.
    ''' Der Standardwert der Eigenschaft entspricht dem Standardwert des <paramref name="T"/>-Typs.
    ''' </summary>
    Public Function RegisterProperty(Of T)() As SettingsProperty(Of T)
        Return RegisterProperty(Of T)(Nothing)
    End Function

    ''' <summary>
    ''' Registriert eine Eigenschaft in dieser Ebene und gibt ein <see cref="SettingsProperty(Of T)"/>-Objekt zurück, das den Wert dieser Eigenschaft kapselt.
    ''' Der Standardwert der Eigenschaft entspricht dem in <paramref name="DefaultValue"/> angegebenen Wert.
    ''' </summary>
    Public Function RegisterProperty(Of T)(DefaultValue As T) As SettingsProperty(Of T)
        Dim Temp As New SettingsProperty(Of T)(Me, DefaultValue)
        _Properties.Add(Temp)
        Return Temp
    End Function

    ''' <summary>
    ''' Registriert eine weitere Ebene in dieser Ebene. Untergeordnete Einstellungs-Strukturen müssen einen öffentlichen, parameterlosen Konstruktor besitzen.
    ''' </summary>
    Public Function RegisterSubStructure(Of T As {SettingsStructure, New})() As T
        Dim Temp As New T
        Temp._Owner = Me
        _SubStructures.Add(Temp)
        Return Temp
    End Function

End Class
