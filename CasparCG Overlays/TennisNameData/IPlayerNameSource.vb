Public Interface IPlayerNameSource

    Event NamesChanged As EventHandler
    ReadOnly Property Names As IEnumerable(Of IPlayerName)

End Interface
