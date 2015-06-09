
Imports System.IO

Public Class Program

    Public Shared Function Main(Args As String()) As Integer
        Try
            If Args.Length < 3 Then
                OnUtils.Helpers.MessageBox("Argument fehlt")
                Return 1
            End If
            If Args.Length >= 4 AndAlso Args(3) = "ignore" Then
                Return 0
            End If
            Dim SolutionDirectory As New DirectoryInfo(Args(0))
            Dim SourceDirectory As New DirectoryInfo(Args(1))
            Dim SubDirectoryName = Args(2)
            'Helpers.MessageBox(String.Format("SolutionDirectory: {1}{0}SourceDirectory: {2}{0}SubDirectoryName: {3}", _
            '                                 Environment.NewLine, SolutionDirectory, SourceDirectory, SubDirectoryName))
            For Each i In {"CasparCG Overlays\bin\Release\Plugins", "CasparCG Overlays\bin\Debug\Plugins"}
                Dim Result = Copy(SourceDirectory, SolutionDirectory.CreateSubdirectory(Path.Combine(i, SubDirectoryName)))
                If Not Result = 0 Then
                    Return Result
                End If
            Next
            Return 0
        Catch ex As Exception
            OnUtils.Helpers.ShowThreadExceptionDialog(ex)
            Return 2
        End Try
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