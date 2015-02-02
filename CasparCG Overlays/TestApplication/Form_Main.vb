
Imports Svt.Caspar
Imports Svt.Network

Public Class Form_Main

    Dim Device As CasparDevice

    Public Sub New()
        InitializeComponent()
        Device = New CasparDevice
        Device.Settings.Hostname = "192.168.10.23"
        Device.Settings.Port = 5250
        AddHandler Device.ConnectionStatusChanged, AddressOf DeviceConnectionStatusChanged
        AddHandler Device.UpdatedDatafiles, AddressOf DeviceUpdatedDataFiles
    End Sub

    Private Sub DeviceConnectionStatusChanged(sender As Object, e As Svt.Network.ConnectionEventArgs)
        If Not e.Exception Is Nothing Then
            Debugger.Break()
        End If
    End Sub

    Private Sub DeviceUpdatedDataFiles(sender As Object, e As EventArgs)

    End Sub

    Protected Overrides Sub OnShown(e As System.EventArgs)

        MyBase.OnShown(e)
    End Sub

    Private Sub Connect() Handles Button_Connect.Click
        Device.Connect()
    End Sub

    Private Sub DoStuff1() Handles Button_DoStuff1.Click
        'Dim Data As New CasparCGDataCollection
        'Data.SetData("team1Score", "4")
        'Device.Channels(0).CG.Update(0, Data)
        Device.Channels(0).CG.Invoke(0, "clockShowHide")
    End Sub

    Private Sub DoStuff2() Handles Button_DoStuff2.Click
        Dim Data As New CasparCGDataCollection
        Data.SetData("team1Name", "AUS")
        Data.SetData("team2Name", "DEN")
        Data.SetData("team1Score", "3")
        Data.SetData("team2Score", "5")
        Data.SetData("halfNum", "2")
        Data.SetData("gameTime", "09:01")
        'C:\CasparCG\CasparCG Server 2.0.3\CasparCG Server 2.0.3\Server\templates
        'FotbollsVM2010/clock
        Device.Channels(0).CG.Add(0, "FotbollsVM2010/clock", True, Data)
    End Sub

End Class
