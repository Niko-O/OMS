
Imports System.IO

Public Class Program

    Public Shared Function Main(Args As String()) As Integer
        If Not Args.Length = 1 Then
            Console.WriteLine("Argument fehlt")
            Return 1
        End If
        Dim SolutionDirectory As New DirectoryInfo(Args(0))
        Dim OnUtilsTargetDirectory = SolutionDirectory.CreateSubdirectory("ApplicationSettingsLib")
        Dim AbsoluteOnUtilsSourceDirectory As New DirectoryInfo("D:\Projects\VB\_Libs\ApplicationSettings\ApplicationSettings\bin\Release")
        If Not AbsoluteOnUtilsSourceDirectory.Exists Then
            Console.WriteLine("Lokaler Pfad existiert nicht.")
            Return 0
        End If
        If Not OnUtilsTargetDirectory.Exists Then
            OnUtilsTargetDirectory.Create()
        End If
        For Each i In AbsoluteOnUtilsSourceDirectory.GetFiles
            If Not String.Equals(i.Extension, ".tmp", StringComparison.InvariantCultureIgnoreCase) AndAlso i.Name.Contains("ApplicationSettings") Then
                i.CopyTo(Path.Combine(OnUtilsTargetDirectory.FullName, i.Name), True)
            End If
        Next
        Return 0
    End Function

End Class