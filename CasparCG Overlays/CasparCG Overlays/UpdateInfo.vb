
''' <summary>
''' Kapselt die Schnittstelle zum UpdateSystem.Net.
''' Singleton.
''' </summary>
Public Class UpdateInfo

    Private Shared _Instance As UpdateInfo
    Public Shared ReadOnly Property Instance As UpdateInfo
        Get
            If _Instance Is Nothing Then
                _Instance = New UpdateInfo
            End If
            Return _Instance
        End Get
    End Property

    Dim _CurrentApplicationVersion As Version = Nothing
    Public ReadOnly Property CurrentApplicationVersion As Version
        Get
            If _CurrentApplicationVersion Is Nothing Then
                _CurrentApplicationVersion = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version
            End If
            Return _CurrentApplicationVersion
        End Get
    End Property

    Dim _UpdateManager As updateSystemDotNet.updateController
    Public ReadOnly Property UpdateManager As updateSystemDotNet.updateController
        Get
            If _UpdateManager Is Nothing Then
                Throw New InvalidOperationException("Zuerst Initialize aufrufen.")
            End If
            Return _UpdateManager
        End Get
    End Property

    Public Sub Initialize()
        _UpdateManager = New updateSystemDotNet.updateController
        _UpdateManager.updateUrl = "http://baellchen.hol.es/ccg"
        _UpdateManager.projectId = "89d28181-2325-4eb1-8006-d72aa52ee34c"
        _UpdateManager.publicKey = _
            <RSAKeyValue>
                <Modulus>
                    ssmUWkhjJzneeNZmDuw71cuB1RjYzIU/SO7MtmCxpTwoUhHTSyvQOjmBhBtj2nixgmg6no7ymHFwmNSoY67o2+aBOZd29nCq2KtpQqQ+cw7ONg0DkgzSHyhhjQGXxNEO7/GciArikb0RZA5ekmuXoxspcw6dzF+xH4y2tlcwNVDiS+0sFc8wYnnrvNZh+eH4U3ceW6dQJjC6RV3Qy0Zj15ZF0cpauZlG1z7wna8t9RHQP8fsHdEoxjLhi367xlHD4eTGS18j1wAmbTNul6Abfw/gONk7zbTifyLkxkqK76BmjMZaKUjMz2cC+1f8bbPEt0ArhT/cpbGzsdJwAJPEsIG+rZsaV9lr8cm5GGe2YI92g38O1Vce/lMG7RFQ6OPKyPwixBFvjXo2Fw98W9ev1iGhjHk5D6VJQ+lf0/Imwos8PDrRDEjMJFMhqEIWouppwKMH+d1Xopsl27gTnHcqYu2Z+tE0WvsVyUlK8/1AoTGtJxIlz/u+T7sEKrTeX2RPqgstUXUkih9Oe60LO+gQlHXbH8P5iZK0Kt4w0FnM9Q4qwPwN6HEhRNxsyE2xdzZnNVXdcb8bw7T5tcRy/nwlcEVARu13tsmFkRLxcpdsOJb3EoHgErBXwJknQR/Gu4i35UxqClRFrxPWfGM7ed3sqgMlk0QiaP86Q5pHUovf1qk=
                </Modulus>
                <Exponent>
                    AQAB
                </Exponent>
            </RSAKeyValue>.ToString
        _UpdateManager.releaseInfo.Version = CurrentApplicationVersion.ToString
        _UpdateManager.releaseFilter.checkForFinal = True
        _UpdateManager.releaseFilter.checkForBeta = False
        _UpdateManager.releaseFilter.checkForAlpha = False
        _UpdateManager.autoCloseHostApplication = True
        _UpdateManager.restartApplication = True
        _UpdateManager.retrieveHostVersion = True
        _UpdateManager.showTaskBarProgress = True
        AddHandler _UpdateManager.updateFound, AddressOf UpdateFound
    End Sub

    Public Sub CheckForUpdates(Interactive As Boolean)
        If Interactive Then
            _UpdateManager.updateInteractive()
        Else
            _UpdateManager.checkForUpdatesAsync()
        End If
    End Sub

    Private Sub UpdateFound(sender As Object, e As updateSystemDotNet.appEventArgs.updateFoundEventArgs)
        If e.Result.UpdatesAvailable Then
            CheckForUpdates(True)
        End If
    End Sub

End Class
