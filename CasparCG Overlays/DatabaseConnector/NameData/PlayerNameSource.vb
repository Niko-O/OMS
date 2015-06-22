
<Export(GetType(TennisNameData.IPlayerNameSource))>
Public Class PlayerNameSource
    Implements TennisNameData.IPlayerNameSource

    Public Event NamesChanged As EventHandler Implements TennisNameData.IPlayerNameSource.NamesChanged

    Public ReadOnly Property Names As System.Collections.Generic.IEnumerable(Of TennisNameData.IPlayerName) Implements TennisNameData.IPlayerNameSource.Names
        Get
            If Connector.Instance.IsConnected Then
                Return Connector.Instance.GetCountries
            Else
                Return New TennisNameData.IPlayerName() {}
            End If
        End Get
    End Property

    Public Sub New()
        AddHandler Connector.Instance.IsConnectedChanged, AddressOf ConnectorIsConnectedChanged
    End Sub

    Private Sub ConnectorIsConnectedChanged()
        RaiseEvent NamesChanged(Me, EventArgs.Empty)
    End Sub

End Class
