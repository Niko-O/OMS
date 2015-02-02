
Imports System.IO

Public Class Program

    Public Shared Function Main(Args As String()) As Integer
        If Not Args.Length = 2 Then
            Console.WriteLine("Argument fehlt")
            Return 1
        End If
        Dim SolutionDirectory As New DirectoryInfo(Args(0))
        Dim SourceDirectory As New DirectoryInfo(Args(1))
        For Each i In {"CasparCG Overlays\bin\Release\Plugins", "CasparCG Overlays\bin\Debug\Plugins"}
            Dim Result = Copy(SourceDirectory, SolutionDirectory.CreateSubdirectory(i))
            If Not Result = 0 Then
                Return Result
            End If
        Next
        Return 0
    End Function

    Private Shared Function Copy(SourceDirectory As DirectoryInfo, TargetDirectory As DirectoryInfo) As Integer
        If Not TargetDirectory.Exists Then
            TargetDirectory.Create()
        End If
        For Each i In SourceDirectory.EnumerateFiles
            If Not String.Equals(i.Extension, ".tmp", StringComparison.InvariantCultureIgnoreCase) Then
                i.CopyTo(System.IO.Path.Combine(TargetDirectory.FullName, i.Name), True)
            End If
        Next
        For Each i In SourceDirectory.EnumerateDirectories
            Dim Temp = Copy(i, TargetDirectory.CreateSubdirectory(i.Name))
            If Not Temp = 0 Then
                Return Temp
            End If
        Next
        Return 0
    End Function

End Class