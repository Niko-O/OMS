
Imports System.IO

Public Class Program

    Public Shared Function Main(Args As String()) As Integer
        If Not Args.Length = 1 Then
            Console.WriteLine("Argument fehlt")
            Return 1
        End If
        Dim SolutionDirectory As New DirectoryInfo(Args(0))
        Dim TargetDirectory = SolutionDirectory.CreateSubdirectory("UpdateSystemDotNetLib")
        Dim AbsoluteSourceDirectory As New DirectoryInfo("C:\Program Files\updateSystem.NET")
        If Not AbsoluteSourceDirectory.Exists Then
            Console.WriteLine("Lokaler Pfad existiert nicht.")
            Return 0
        End If
        If Not TargetDirectory.Exists Then
            TargetDirectory.Create()
        End If
        For Each i In AbsoluteSourceDirectory.GetFiles
            If {"updateSystemDotNet.Controller.dll", "updateSystemDotNet.Controller.pdb"}.Contains(i.Name) Then
                i.CopyTo(Path.Combine(TargetDirectory.FullName, i.Name), True)
            End If
        Next
        Return 0
    End Function

End Class