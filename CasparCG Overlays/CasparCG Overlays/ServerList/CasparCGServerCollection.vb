
Namespace ServerList

    '<System.ComponentModel.TypeConverter(GetType(CasparCGServerCollectionTypeConverter))>
    ''' <summary>
    ''' Erweitert eine <see cref="List(Of CasparCGServer)"/> um Methoden, mit denen Die Elemente zu XML konvertiert und von XML geladen werden können.
    ''' Singleton.
    ''' </summary>
    Public Class CasparCGServerCollection
        Inherits ObservableCollection(Of CasparCGServer)
        
        Public Shared ReadOnly Property Instance As CasparCGServerCollection
            Get
                Static Temp As New CasparCGServerCollection
                Return Temp
            End Get
        End Property

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gibt ein <see cref="XElement"/> zurück, das alle Elemente der Liste beinhaltet.
        ''' </summary>
        Public Function ToXml() As XElement
            Dim RootElement As New XElement("Servers")
            For Each i In Me
                Dim ItemElement As New XElement("Server")
                RootElement.Add(ItemElement)
                ItemElement.SetAttributeValue("Name", i.Name)
                ItemElement.SetAttributeValue("Address", i.Address)
            Next
            Return RootElement
        End Function

        ''' <summary>
        ''' Leert die Liste und lädt die Elemente aus dem angegebenen <see cref="XElement"/>.
        ''' </summary>
        ''' <param name="RootElement">Ein <see cref="XElement"/>, das die zu ladenden Elemente beinhaltet.</param>
        Public Sub FromXml(RootElement As XElement)
            Clear()
            For Each i In RootElement.Elements
                Add(New CasparCGServer(i.Attribute("Name").Value, i.Attribute("Address").Value))
            Next
        End Sub

    End Class

End Namespace
