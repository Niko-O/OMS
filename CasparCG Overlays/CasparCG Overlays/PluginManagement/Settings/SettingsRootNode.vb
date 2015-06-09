
Namespace PluginManagement.Settings

    ''' <summary>
    ''' Repräsentiert eine gespeicherte <see cref="PluginSettings.SettingsStructure"/> oberster Ebene.
    ''' </summary>
    Public Class SettingsRootNode
        Inherits SettingsNode

        Dim _TypeName As String
        ''' <summary>
        ''' Der Name des Typs der Einstellungs-Struktur.
        ''' </summary>
        Public ReadOnly Property TypeName As String
            Get
                Return _TypeName
            End Get
        End Property

        Dim _Type As Type = Nothing
        ''' <summary>
        ''' Der Typ der Einstellungs-Struktur.
        ''' </summary>
        Public ReadOnly Property Type As Type
            Get
                If _Type Is Nothing Then
                    _Type = GetTypeHelper.GetTypeByAssemblyQualifiedNameInLoadedAssemblies(TypeName)
                End If
                Return _Type
            End Get
        End Property

        '<RootNode TypeName="TestFootballPlugin.TestStructure, TestFootballPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
        Public Sub New(NewSource As XElement)
            MyBase.New(NewSource)
            _TypeName = NewSource.Attribute("TypeName").Value
            'If _Type Is Nothing Then
            '    Throw New Exceptions.TypeNotFoundException(String.Format("Der Typ '{0}' der Eigenschaften-Struktur wurde nicht gefunden.", TypeName))
            'End If
        End Sub

    End Class

End Namespace
