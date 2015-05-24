
Imports System.IO

Public Class Program

    Public Shared Function Main(Args As String()) As Integer
        If Not Args.Length = 1 Then
            Console.WriteLine("Argument fehlt")
            Return 1
        End If
        Dim SolutionDirectory As New DirectoryInfo(Args(0))
        Dim TargetDirectory = SolutionDirectory.CreateSubdirectory("OnUtilsLib")
        Dim AbsoluteSourceDirectory As New DirectoryInfo("D:\Projects\VB\_Libs\OnUtils\OnUtils\bin\Release")
        If Not AbsoluteSourceDirectory.Exists Then
            Console.WriteLine("Lokaler Pfad existiert nicht.")
            Return 0
        End If
        If Not TargetDirectory.Exists Then
            TargetDirectory.Create()
        End If
        For Each i In AbsoluteSourceDirectory.GetFiles
            If Not String.Equals(i.Extension, ".tmp", StringComparison.InvariantCultureIgnoreCase) AndAlso i.Name.Contains("OnUtils") Then
                i.CopyTo(Path.Combine(TargetDirectory.FullName, i.Name), True)
            End If
        Next
        Return 0
    End Function

End Class