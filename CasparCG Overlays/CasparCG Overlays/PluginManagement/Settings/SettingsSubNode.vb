
''' <summary>
''' Repräsentiert eine gespeicherte <see cref="PluginSettings.SettingsStructure"/>, die einer anderen Einstellungs-Struktur untergeordnet ist.
''' </summary>
Public Class SettingsSubNode
    Inherits SettingsNode

    Dim _Name As String
    ''' <summary>
    ''' Der Name der Einstellungs-Struktur.
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    '<Node Name="Container">
    Public Sub New(NewSource As XElement)
        MyBase.New(NewSource)
        _Name = NewSource.Attribute("Name").Value
    End Sub

End Class
