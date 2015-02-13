
Namespace PluginManagement.Settings

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
            If Not System.IO.File.Exists(CasparCG_Overlays.Settings.IO.PluginSettingsXmlFile.Path) Then
                Return False
            End If
            Dim XmlDocument = XDocument.Load(CasparCG_Overlays.Settings.IO.PluginSettingsXmlFile.Path)
            Dim SettingsDocument As New SettingsDocument(XmlDocument.Root)
            Dim Node = SettingsDocument.GetRootNode(SettingsStructure.GetType)
            If Node Is Nothing Then
                Return False
            End If
            RecursiveLoadSettings(SettingsStructure, Node)
            Return True
        End Function

        Public Sub SaveSettings(SettingsStructure As PluginSettings.SettingsStructure) Implements PluginInterfaces.ISettingsProvider.SaveSettings
            Dim XmlDocument As XDocument
            Dim RootElement As XElement
            If System.IO.File.Exists(CasparCG_Overlays.Settings.IO.PluginSettingsXmlFile.Path) Then
                XmlDocument = XDocument.Load(CasparCG_Overlays.Settings.IO.PluginSettingsXmlFile.Path)
                RootElement = XmlDocument.Root
            Else
                XmlDocument = New XDocument
                RootElement = New XElement("PluginSettings")
                XmlDocument.Add(RootElement)
            End If
            Dim SettingsDocument As New SettingsDocument(XmlDocument.Root)
            Dim Node = SettingsDocument.GetRootNode(SettingsStructure.GetType)
            Dim RootNodeElemet As XElement
            If Node Is Nothing Then
                RootNodeElemet = New XElement("RootNode")
                RootNodeElemet.SetAttributeValue("TypeName", SettingsStructure.GetType.AssemblyQualifiedName)
                SettingsDocument.Source.Add(RootNodeElemet)
            Else
                RootNodeElemet = Node.Source
                RootNodeElemet.RemoveNodes()
            End If
            RecursiveSaveSettings(SettingsStructure, RootNodeElemet)
            XmlDocument.Save(CasparCG_Overlays.Settings.IO.PluginSettingsXmlFile.Path)
        End Sub

        Private Sub RecursiveLoadSettings(Settings As PluginSettings.SettingsStructure, Node As SettingsNode)
            For Each i In Settings.Properties
                Dim Prop = Node.GetProperty(i.Name)
                If Not i.Type.IsAssignableFrom(Prop.Type) Then
                    Throw New Exceptions.PropertyTypeIncompatibleException( _
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

        Private Sub RecursiveSaveSettings(Settings As PluginSettings.SettingsStructure, Node As XElement)
            For Each i In Settings.Properties
                Dim Converter = System.ComponentModel.TypeDescriptor.GetConverter(i.Type)
                If Converter Is Nothing Then
                    Throw New Exceptions.TypeConverterNotFoundException(String.Format("Es wurde kein TypeConverter für die Konvertierung nach '{0}' gefunden.", i.Type.AssemblyQualifiedName))
                End If
                Dim ValueString = Converter.ConvertToString(i.Value)
                Dim PropertyElement As New XElement("Property")
                Node.Add(PropertyElement)
                PropertyElement.SetAttributeValue("Name", i.Name)
                PropertyElement.SetAttributeValue("StaticType", i.Type.AssemblyQualifiedName)
                PropertyElement.SetAttributeValue("Value", ValueString)
            Next
            For Each i In Settings.SubStructures
                Dim SubNode As New XElement("Node")
                Node.Add(SubNode)
                SubNode.SetAttributeValue("Name", i.Name)
                RecursiveSaveSettings(i, SubNode)
            Next
        End Sub

    End Class

End Namespace
