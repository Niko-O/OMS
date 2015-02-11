
''' <summary>
''' Agiert als <see cref="PluginInterfaces.ISettingsProvider"/> und speichert Einstellungen für Plugins.
''' </summary>
Public Class PluginSettingsProvider
    'Inherits Singleton(Of PluginSettingsProvider)
    Inherits PluginSettings.SettingsProviderBase
    Implements PluginInterfaces.ISettingsProvider

    Private Shared _Instance As PluginSettingsProvider = Nothing
    Public Shared ReadOnly Property Instance As PluginSettingsProvider
        Get
            If _Instance Is Nothing Then
                _Instance = New PluginSettingsProvider
            End If
            Return _Instance
        End Get
    End Property
    Private Sub New()
    End Sub

    Public Function LoadSettings(SettingsStructure As PluginSettings.SettingsStructure) As Boolean Implements PluginInterfaces.ISettingsProvider.LoadSettings
        If Not System.IO.File.Exists(Settings.IO.PluginSettingsXmlFile.Path) Then
            Return False
        End If
        Dim XmlDocument = XDocument.Load(Settings.IO.PluginSettingsXmlFile.Path)
        Dim SettingsDocument As New SettingsDocument(XmlDocument.Root)
        Dim Node = SettingsDocument.GetRootNode(SettingsStructure.GetType)
        If Node Is Nothing Then
            Return False
        End If
        RecursiveLoadSettings(SettingsStructure, Node)
        Return True
    End Function

    Public Sub SaveSettings(Settings As PluginSettings.SettingsStructure) Implements PluginInterfaces.ISettingsProvider.SaveSettings

    End Sub

    Private Sub RecursiveLoadSettings(Settings As PluginSettings.SettingsStructure, Node As SettingsNode)
        For Each i In Settings.Properties
            Dim Prop = Node.GetProperty(i.Name)
            If Not i.Type.IsAssignableFrom(Prop.Type) Then
                Throw New PropertyTypeIncompatibleException( _
                    String.Format("Für Property {0} in {1} wurde Typ '{2}' erwartet, der Typ des gespeicherten Wertes ist jedoch '{3}'.", _
                                  i.Name, Settings.Name, i.Type.AssemblyQualifiedName, Prop.Type.AssemblyQualifiedName))
            End If
            SetPropertyValue(i, Prop.Value)
        Next
        For Each i In Settings.SubStructures
            Dim SubNode = Node.GetSubNode(i.Name)
            RecursiveLoadSettings(i, SubNode)
        Next
    End Sub

End Class
