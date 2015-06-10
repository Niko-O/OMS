Public Class TlsCommand
    Implements ICasparServerCommand

    Public Shared ReadOnly TlsLineRgx As New System.Text.RegularExpressions.Regex("""(?<FullPath>.*)"" (?<FileSize>[0-9]+) (?<Timestamp>[0-9]+)")

    Public Function GetCommandString() As String Implements ICasparServerCommand.GetCommandString
        Return "TLS"
    End Function

    Public Shared Function ParseResponse(Response As ICommandResponse) As IEnumerable(Of TemplatePath)
        Dim Temp As New List(Of TemplatePath)
        For Each i In Response.ReturnedText.Split({Environment.NewLine}, StringSplitOptions.None)
            Dim Match = TlsLineRgx.Match(i)
            If Match IsNot Nothing Then
                Dim FileSize As Integer
                If Integer.TryParse(Match.Groups("FileSize").Value, FileSize) Then
                    Temp.Add(New TemplatePath(Match.Groups("FullPath").Value.Replace("\"c, "/"c), FileSize, Match.Groups("Timestamp").Value))
                End If
            End If
        Next
        Return Temp
    End Function

    Public Class TemplatePath

        Dim _FullPath As String
        Public ReadOnly Property FullPath As String
            Get
                Return _FullPath
            End Get
        End Property

        Dim _FileNameWithoutExtension As String
        Public ReadOnly Property FileNameWithoutExtension As String
            Get
                Return _FileNameWithoutExtension
            End Get
        End Property

        Dim _Directory As String
        Public ReadOnly Property Directory As String
            Get
                Return _Directory
            End Get
        End Property

        Dim _FileSize As Integer
        Public ReadOnly Property FileSize As Integer
            Get
                Return _FileSize
            End Get
        End Property

        Dim _Timestamp As String
        Public ReadOnly Property Timestamp As String
            Get
                Return _Timestamp
            End Get
        End Property

        Public Sub New(NewFullPath As String, NewFileSize As Integer, NewTimestamp As String)
            _FullPath = NewFullPath
            _FileSize = NewFileSize
            _Timestamp = NewTimestamp
            _FileNameWithoutExtension = _FullPath.Substring(_FullPath.LastIndexOf("/"c) + 1)
            _Directory = If(_FullPath.LastIndexOf("/"c) = -1, "", _FullPath.Remove(_FullPath.LastIndexOf("/"c)))
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("{{FileName = {0}; Directory = {1}; FileSize = {2}; TimeStamp = {3}}}", _FileNameWithoutExtension, _Directory, _FileSize, _Timestamp)
        End Function

    End Class

End Class
