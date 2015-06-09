
Namespace PluginManagement

    ''' <summary>
    ''' Eine Liste von Zuständen, die angeben, ob Plugins aktiv sind. Die Plugins werden an ihren Guids erkannt.
    ''' </summary>
    Public Class PluginActiveStates
        Inherits Singleton(Of PluginActiveStates)

        ''' <summary>
        ''' Wird ausgelöst, wenn sich ein Zustand ändert.
        ''' </summary>
        Public Event StatesChanged As EventHandler(Of IsInUseChangedEventArgs)
        Private Sub OnStatesChanged(PluginGuid As Guid, NewState As Boolean)
            RaiseEvent StatesChanged(Nothing, New IsInUseChangedEventArgs(PluginGuid, NewState))
        End Sub

        Public Property DefaultState As Boolean = True

        Private States As New Dictionary(Of Guid, Boolean)

        ''' <summary>
        ''' Ruft ab oder legt fest, ob das angegebene Plugin aktiv ist.
        ''' </summary>
        ''' <param name="PluginGuid">Das Plugin, dessen Aktivitäts-Zustand abgerufen bzw. festgelegt wird.</param>
        Public Property IsInUse(PluginGuid As Guid) As Boolean
            Get
                IsInUse = DefaultState
                If Not States.TryGetValue(PluginGuid, IsInUse) Then
                    States.Add(PluginGuid, DefaultState)
                    IsInUse = DefaultState
                End If
            End Get
            Set(value As Boolean)
                Dim PreviousValue As Boolean = Nothing
                Dim Exists = States.TryGetValue(PluginGuid, PreviousValue)
                If Not Exists OrElse Not PreviousValue = value Then
                    States(PluginGuid) = value
                    OnStatesChanged(PluginGuid, value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gibt ein <see cref="XElement"/> zurück, das alle Zustände beinhaltet.
        ''' </summary>
        Public Function ToXml() As XElement
            Dim RootElement As New XElement("ActiveStates")
            For Each i In States
                Dim ItemElement As New XElement("State")
                RootElement.Add(ItemElement)
                ItemElement.SetAttributeValue("PluginGuid", i.Key.ToString)
                ItemElement.SetAttributeValue("IsInUse", i.Value.ToString)
            Next
            Return RootElement
        End Function

        ''' <summary>
        ''' Lädt die Zustände aus dem angegebenen <see cref="XElement"/>.
        ''' </summary>
        ''' <param name="RootElement">Ein <see cref="XElement"/>, das die zu ladenden Zustände beinhaltet.</param>
        Public Sub FromXml(RootElement As XElement)
            States.Clear()
            For Each i In RootElement.Elements
                States.Add(Guid.Parse(i.Attribute("PluginGuid").Value), Boolean.Parse(i.Attribute("IsInUse").Value))
            Next
        End Sub

        Public Class IsInUseChangedEventArgs
            Inherits EventArgs

            Dim _PluginGuid As Guid
            Public ReadOnly Property PluginGuid As Guid
                Get
                    Return _PluginGuid
                End Get
            End Property

            Dim _NewState As Boolean
            Public ReadOnly Property NewState As Boolean
                Get
                    Return _NewState
                End Get
            End Property

            Public Sub New(NewPluginGuid As Guid, NewNewState As Boolean)
                _PluginGuid = NewPluginGuid
                _NewState = NewNewState
            End Sub

        End Class

    End Class

End Namespace
