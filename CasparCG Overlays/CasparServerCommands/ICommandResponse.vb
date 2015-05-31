
''' <summary>
''' Stellt eine Rückmeldung dar, die vom CasparCG-Server nach dem Ausführen eines Commands an den Client sendet.
''' </summary>
Public Interface ICommandResponse

    ''' <summary>
    ''' Gibt den rohen Text an, der vom Server zurückgesendet wurde.
    ''' </summary>
    ReadOnly Property RawText As String

    ''' <summary>
    ''' Gibt den Status-Code an.
    ''' Beispiel: 200 für OK
    ''' </summary>
    ReadOnly Property StatusCode As Integer

    ''' <summary>
    ''' Gibt den Text ohne Statusmeldung an.
    ''' </summary>
    ReadOnly Property ReturnedText As String

End Interface
