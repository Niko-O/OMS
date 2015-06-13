
Namespace PluginManagement.Settings

    ''' <summary>
    ''' Repräsentiert eine gespeicherte <see cref="PluginSettings.SettingsStructure"/>.
    ''' </summary>
    Public MustInherit Class SettingsNode

        Dim _Source As XElement
        Public ReadOnly Property Source As XElement
            Get
                Return _Source
            End Get
        End Property

        Dim Properties As New List(Of SettingsProperty)
        Dim SubNodes As New List(Of SettingsSubNode)

        Public Sub New(NewSource As XElement)
            _Source = NewSource
            For Each i In NewSource...<Property>
                Properties.Add(New SettingsProperty(i))
            Next
            For Each i In NewSource...<Node>
                SubNodes.Add(New SettingsSubNode(i))
            Next
        End Sub

        ''' <summary>
        ''' Sucht eine Eigenschaft mit dem angegebenen Namen.
        ''' </summary>
        ''' <param name="PropertyName">Der Name der Eigenschaft.</param>
        Public Function GetProperty(PropertyName As String) As SettingsProperty
            For Each i In Properties
                If i.Name = PropertyName Then
                    Return i
                End If
            Next
            'Throw New Exceptions.PropertyTypeIncompatibleException(String.Format("In dieser Eigenschaften-Struktur wurde keine Eigenschaft mit dem Namen {0} gefunden.", PropertyName))
            Return Nothing
        End Function

        ''' <summary>
        ''' Sucht eine untergeordnete Einstellungs-Struktur.
        ''' </summary>
        ''' <param name="NodeName">Der Name der Einstellungs-Struktur.</param>
        Public Function GetSubNode(NodeName As String) As SettingsNode
            For Each i In SubNodes
                If i.Name = NodeName Then
                    Return i
                End If
            Next
            Throw New Exceptions.PropertyTypeIncompatibleException(String.Format("In dieser Eigenschaften-Struktur wurde keine untergeordnete Eigenschaft mit dem Namen {0} gefunden.", NodeName))
        End Function

    End Class

End Namespace
