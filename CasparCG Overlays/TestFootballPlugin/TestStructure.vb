
Imports PluginSettings

Public Class TestStructure
    Inherits SettingsStructure

    Dim _Foo As SettingsProperty(Of String) = RegisterProperty(Of String)("Foo")
    Public Property Foo As String
        Get
            Return _Foo.Value
        End Get
        Set(value As String)
            _Foo.Value = value
        End Set
    End Property

    Dim _Container As TestStructure_ContainerType = RegisterSubStructure(Of TestStructure_ContainerType)("Container")
    Public ReadOnly Property Container As TestStructure_ContainerType
        Get
            Return _Container
        End Get
    End Property

    Public Class TestStructure_ContainerType
        Inherits SettingsStructure

        Dim _Bar As SettingsProperty(Of Integer) = RegisterProperty(Of Integer)("Bar", 10)
        Public Property Bar As Integer
            Get
                Return _Bar.Value
            End Get
            Set(value As Integer)
                _Bar.Value = value
            End Set
        End Property

    End Class

End Class
