
Namespace PluginManagement.Settings

    ''' <summary>
    ''' Repräsentiert die Xml-Datei, in der Einstellungen gespeichert werden.
    ''' </summary>
    Public Class SettingsDocument

        Dim _Source As XElement
        Public ReadOnly Property Source As XElement
            Get
                Return _Source
            End Get
        End Property

        Dim RootNodes As New List(Of SettingsRootNode)

        Public Sub New(NewSource As XElement)
            _Source = NewSource
            For Each i In NewSource...<RootNode>
                RootNodes.Add(New SettingsRootNode(i))
            Next
        End Sub

        ''' <summary>
        ''' Sucht eine Einstellungs-Struktur oberster Ebene anhand des angegebenen Typs.
        ''' Gibt Nothing zurück, wenn keine Einstellungs-Struktur gefunden wurde.
        ''' </summary>
        ''' <param name="RootNodeType">Der Typ der Einstellungs-Struktur.</param>
        Public Function GetRootNode(RootNodeType As Type) As SettingsRootNode
            For Each i In RootNodes
                If i.Type Is RootNodeType Then
                    Return i
                End If
            Next
            Return Nothing
        End Function

    End Class

End Namespace
