
Imports System.IO

Public Class Program

    Public Shared Function Main(Args As String()) As Integer
        Try
            If Args.Length < 1 Then
                OnUtils.Helpers.MessageBox("Argument fehlt")
                Return 1
            End If
            If Args.Length >= 2 AndAlso Args(1) = "ignore" Then
                Return 0
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
        Catch ex As Exception
            OnUtils.Helpers.ShowThreadExceptionDialog(ex)
            Return 2
        End Try
    End Function

End Class