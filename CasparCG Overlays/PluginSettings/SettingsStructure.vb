
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

    Dim _Name As String = Nothing
    ''' <summary>
    ''' Der Name der Eigenschaften-Struktur.
    ''' Wenn die aktuelle Instanz die oberste Ebene ist, ist Name Nothing.
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return _Name
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
    ''' <param name="Name">Der Name der Eigenschaft. Darf nicht Nothing sein.</param>
    Public Function RegisterProperty(Of T)(Name As String) As SettingsProperty(Of T)
        Return RegisterProperty(Of T)(Name, Nothing)
    End Function

    ''' <summary>
    ''' Registriert eine Eigenschaft in dieser Ebene und gibt ein <see cref="SettingsProperty(Of T)"/>-Objekt zurück, das den Wert dieser Eigenschaft kapselt.
    ''' Der Standardwert der Eigenschaft entspricht dem in <paramref name="DefaultValue"/> angegebenen Wert.
    ''' <param name="Name">Der Name der Eigenschaft. Darf nicht Nothing sein.</param>
    ''' <param name="DefaultValue">Der Standardwert der Eigenschaft.</param>
    ''' </summary>
    Public Function RegisterProperty(Of T)(Name As String, DefaultValue As T) As SettingsProperty(Of T)
        If String.IsNullOrEmpty(Name) Then
            Throw New ArgumentNullException("Name", "Der Name darf nicht Nothing sein.")
        End If
        Dim Temp As New SettingsProperty(Of T)(Me, Name, DefaultValue)
        _Properties.Add(Temp)
        Return Temp
    End Function

    ''' <summary>
    ''' Registriert eine weitere Ebene in dieser Ebene. Untergeordnete Einstellungs-Strukturen müssen einen öffentlichen, parameterlosen Konstruktor besitzen.
    ''' </summary>
    Public Function RegisterSubStructure(Of T As {SettingsStructure, New})(Name As String) As T
        Dim Temp As New T
        Temp._Owner = Me
        Temp._Name = Name
        _SubStructures.Add(Temp)
        Return Temp
    End Function

End Class
