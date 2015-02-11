
''' <summary>
''' Repräsentiert eine gespeicherte <see cref="PluginSettings.SettingsStructure"/> oberster Ebene.
''' </summary>
Public Class SettingsRootNode
    Inherits SettingsNode

    Dim _Type As Type
    ''' <summary>
    ''' Der Typ der Einstellungs-Struktur.
    ''' </summary>
    Public ReadOnly Property Type As Type
        Get
            Return _Type
        End Get
    End Property

    '<RootNode TypeName="TestFootballPlugin.TestStructure, TestFootballPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
    Public Sub New(NewSource As XElement)
        MyBase.New(NewSource)
        Dim TypeName = NewSource.Attribute("TypeName").Value
        _Type = Type.GetType(TypeName, False)
        If _Type Is Nothing Then
            Throw New TypeNotFoundException(String.Format("Der Typ '{0}' der Eigenschaften-Struktur wurde nicht gefunden.", TypeName))
        End If
    End Sub

End Class
