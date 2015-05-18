
Namespace ServerList

    Public Class EditButtonVisibilityConverter
        Implements IMultiValueConverter

        Public Function Convert(values() As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            Return values.Any(Function(i) TypeOf i Is Boolean AndAlso DirectCast(i, Boolean)).ToVisibility
        End Function

        Public Function ConvertBack(value As Object, targetTypes() As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            Return Nothing
        End Function

    End Class

End Namespace
