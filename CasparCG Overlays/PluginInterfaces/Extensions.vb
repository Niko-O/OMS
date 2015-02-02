
Imports System.Runtime.CompilerServices

<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
Module Extensions

    <Extension()>
    Public Function GetGuid(Target As IPlugin) As Guid
        Return Guid.Parse(GetMetadata(Of String)(Target, "PluginGuid"))
    End Function

    <Extension()>
    Public Function GetDisplayName(Target As IPlugin) As String
        Return GetMetadata(Of String)(Target, "DisplayName")
    End Function

    Private Function GetMetadata(Of T)(Target As IPlugin, Name As String) As T
        Dim Metadata = GetMetadata(Target, Name)
        If Not TypeOf Metadata Is String Then
            Throw New InvalidCastException(String.Format("Der vom Plugin exportierte Wert {0} mit dem Schlüssel {1} hat nicht den erwarteten Typ {2}", Metadata.ToString, Name, GetType(T).Name))
        End If
        Return DirectCast(Metadata, T)
    End Function

    Private Function GetMetadata(Target As IPlugin, Name As String) As Object
        For Each i In Target.GetType.GetCustomAttributes(GetType(ExportMetadataAttribute), True)
            With DirectCast(i, ExportMetadataAttribute)
                If .Name = Name Then
                    Return .Value
                End If
            End With
        Next
        Throw New KeyNotFoundException("Das Plugin exportiert keine Metadaten mit dem Schlüssel '" & Name & "'")
    End Function

End Module
