
''' <summary>
''' Repräsentiert eine gespeicherte <see cref="PluginSettings.SettingsProperty"/>.
''' </summary>
Public Class SettingsProperty

    Dim _Source As XElement
    Public ReadOnly Property Source As XElement
        Get
            Return _Source
        End Get
    End Property

    Dim _Name As String
    ''' <summary>
    ''' Der Name der Eigenschaft.
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Dim _Type As Type
    ''' <summary>
    ''' Der Typ der Eigenschaft.
    ''' </summary>
    Public ReadOnly Property Type As Type
        Get
            Return _Type
        End Get
    End Property

    Dim _Converter As System.ComponentModel.TypeConverter
    ''' <summary>
    ''' Der <see cref="System.ComponentModel.TypeConverter"/>, der zur Konvertierung verwendet wird.
    ''' </summary>
    Public ReadOnly Property Converter As System.ComponentModel.TypeConverter
        Get
            Return _Converter
        End Get
    End Property

    Dim LazyValue As Lazy(Of Object)
    ''' <summary>
    ''' Der gespeicherte Wert.
    ''' </summary>
    Public ReadOnly Property Value As Object
        Get
            Return LazyValue.Value
        End Get
    End Property

    '<Property Name="Foo" StaticType="System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Value="Hallo"/>
    Public Sub New(NewSource As XElement)
        _Source = NewSource
        _Name = NewSource.Attribute("Name").Value
        Dim TypeName = NewSource.Attribute("StaticType").Value
        _Type = Type.GetType(TypeName, False)
        If _Type Is Nothing Then
            Throw New TypeNotFoundException(String.Format("Der Typ '{0}' der Eigenschaft {1} wurde nicht gefunden.", TypeName, _Name))
        End If
        _Converter = System.ComponentModel.TypeDescriptor.GetConverter(_Type)
        If Converter Is Nothing Then
            Throw New TypeConverterNotFoundException(String.Format("Es wurde kein TypeConverter für die Konvertierung nach '{0}' gefunden.", _Type.AssemblyQualifiedName))
        End If
        LazyValue = New Lazy(Of Object)(AddressOf LoadValue, False)
    End Sub

    Private Function LoadValue() As Object
        Return Converter.ConvertFrom(Source.Attribute("Value").Value)
    End Function

End Class
