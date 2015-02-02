
''' <summary>
''' Stellt ein Plugin dar und stellt, wenn vorhanden, ein WPF-<see cref="System.Windows.Controls.UserControl"/> bereit, das Einstellungen für dieses Plugin anzeigt.
''' </summary>
Public Interface IPlugin

    ''' <summary>
    ''' Gibt ein WPF-<see cref="System.Windows.Controls.UserControl"/> zurück, das Einstellungen für dieses Plugin anzeigt.
    ''' Gibt Null zurück, wenn keine Einstellungen verfügbar sind. 
    ''' </summary>
    Function GetSnapIn() As System.Windows.Controls.UserControl

    ''' <summary>
    ''' Benachrichtigt das Plugin, dass es geladen wurde. In dieser Methode können z.B. Einstellungen geladen werden.
    ''' </summary>
    Sub Created()

    ''' <summary>
    ''' Benachrichtigt das Plugin, dass die Anwendung beendet wird. In dieser Methode können z.B. Einstellungen gespeichert werden.
    ''' </summary>
    Sub Unloaded()

    ''' <summary>
    ''' Benachrichtigt das Plugin, dass es aktiviert wurde.
    ''' </summary>
    Sub Enabled()

    ''' <summary>
    ''' Benachrichtigt das Plugin, dass es deaktiviert wurde.
    ''' </summary>
    Sub Disabled()

End Interface
