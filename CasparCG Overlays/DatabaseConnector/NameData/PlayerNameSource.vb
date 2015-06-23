
<Export(GetType(TennisNameData.IPlayerNameSource))>
Public Class PlayerNameSource
    Implements TennisNameData.IPlayerNameSource

    Public Event NamesChanged As EventHandler Implements TennisNameData.IPlayerNameSource.NamesChanged
    Private Sub OnNamesChanged()
        RaiseEvent NamesChanged(Me, EventArgs.Empty)
    End Sub

    Public ReadOnly Property Names As System.Collections.Generic.IEnumerable(Of TennisNameData.IPlayerName) Implements TennisNameData.IPlayerNameSource.Names
        Get
            If Connector.Instance.IsConnected Then
                Return Connector.Instance.GetPlayerNames
            Else
                Return New TennisNameData.IPlayerName() {}
            End If
        End Get
    End Property

    Public Sub New()
        AddHandler Connector.Instance.IsConnectedChanged, AddressOf OnNamesChanged
        AddHandler Connector.Instance.NamesTableChanged, AddressOf OnNamesChanged
    End Sub

End Class
