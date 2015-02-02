
''' <summary>
''' Stellt ein Land und das dafür verwendete Länderkürzel dar.
''' </summary>
Public Interface ICountry

    ''' <summary>
    ''' Der vorllständige Name des Landes.
    ''' </summary>
    Property FullName As String

    ''' <summary>
    ''' Das Namenskürzel des Landes.
    ''' </summary>
    Property ShortName As String

End Interface
