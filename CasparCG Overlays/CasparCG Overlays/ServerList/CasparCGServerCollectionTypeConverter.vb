
'Namespace ServerList

'    Public Class CasparCGServerCollectionTypeConverter
'        Inherits System.ComponentModel.TypeConverter

'        Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
'            If TypeOf value Is CasparCGServerCollection AndAlso destinationType Is GetType(String) Then
'                Return DirectCast(value, CasparCGServerCollection).ToXml.ToString
'            End If
'            Return MyBase.ConvertTo(context, culture, value, destinationType)
'        End Function
'        Public Overrides Function ConvertFrom(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object) As Object
'            If TypeOf value Is String Then
'                Return CasparCGServerCollection.FromXml(XElement.Parse(DirectCast(value, String)))
'            End If
'            Return MyBase.ConvertFrom(context, culture, value)
'        End Function
'    End Class

'End Namespace
