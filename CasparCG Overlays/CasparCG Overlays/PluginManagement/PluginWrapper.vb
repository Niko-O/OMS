
Namespace PluginManagement

    ''' <summary>
    ''' Kapselt ein <see cref="PluginInterfaces.IPlugin"/> und seine Metadaten.
    ''' </summary>
    Public Class PluginWrapper
        Inherits NotifyPropertyChanged
        Implements PluginInterfaces.IPlugin

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der Wert der <see cref="IsInUse"/>-Property geändert hat.
        ''' </summary>
        Public Event IsInUseChanged()
        Private Sub OnIsInUseChanged()
            RaiseEvent IsInUseChanged()
        End Sub

        ''' <summary>
        ''' Gibt an, ob das Plugin aktiviert ist.
        ''' </summary>
        Public Property IsInUse As Boolean
            Get
                Return PluginManagement.PluginActiveStates.Instance.IsInUse(PluginGuid)
            End Get
            Set(value As Boolean)
                Dim CurrentState = IsInUse
                If Not CurrentState = value Then
                    PluginManagement.PluginActiveStates.Instance.IsInUse(PluginGuid) = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPluginMetadata.DisplayName"/>
        ''' </summary>
        Public ReadOnly Property DisplayName As String
            Get
                Return Source.Metadata.DisplayName
            End Get
        End Property

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPluginMetadata.PluginGuid"/>
        ''' </summary>
        Public ReadOnly Property PluginGuid As Guid
            Get
                Return Guid.Parse(Source.Metadata.PluginGuid)
            End Get
        End Property

        Dim Source As Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata)

        Public Sub New(NewSource As Lazy(Of PluginInterfaces.IPlugin, PluginInterfaces.IPluginMetadata))
            Source = NewSource
            AddHandler PluginActiveStates.Instance.StatesChanged, AddressOf CheckIsInUseChanged
        End Sub

        Private Sub CheckIsInUseChanged(sender As Object, e As PluginActiveStates.IsInUseChangedEventArgs)
            If e.PluginGuid = PluginGuid Then
                UpdateIsInUse()
            End If
        End Sub

        Private Sub UpdateIsInUse()
            If IsInUse Then
                Enabled()
            Else
                Disabled()
            End If
            OnIsInUseChanged()
            OnPropertyChanged("IsInUse")
        End Sub

        ''' <summary>
        ''' <see cref="PluginInterfaces.IPlugin.GetSnapIn"/>
        ''' </summary>
        Public Function GetSnapIn() As UserControl Implements PluginInterfaces.IPlugin.GetSnapIn
            Return Source.Value.GetSnapIn
        End Function

        ''' <summary>
        ''' Benachrichtigt das Plugin, dass es geladen wurde. In dieser Methode können z.B. Einstellungen geladen werden.
        ''' </summary>
        Public Sub Created() Implements PluginInterfaces.IPlugin.Created
            Source.Value.Created()
        End Sub

        ''' <summary>
        ''' Benachrichtigt das Plugin, dass die Anwendung beendet wird. In dieser Methode können z.B. Einstellungen gespeichert werden.
        ''' </summary>
        Public Sub Unloaded() Implements PluginInterfaces.IPlugin.Unloaded
            Source.Value.Unloaded()
        End Sub

        ''' <summary>
        ''' Benachrichtigt das Plugin, dass es aktiviert wurde.
        ''' </summary>
        Public Sub Enabled() Implements PluginInterfaces.IPlugin.Enabled
            Source.Value.Enabled()
        End Sub

        ''' <summary>
        ''' Benachrichtigt das Plugin, dass es deaktiviert wurde.
        ''' </summary>
        Public Sub Disabled() Implements PluginInterfaces.IPlugin.Disabled
            Source.Value.Disabled()
        End Sub

    End Class

End Namespace
