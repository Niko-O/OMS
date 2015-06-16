
Namespace ServerList

    Public Class CasparCGServerViewModel
        Inherits ViewModelBase(Of CasparCGServer)

        Public ReadOnly Property ToggleEditModeCommand As DelegateCommand
            Get
                Static Temp As New DelegateCommand(Sub() IsInEditMode = Not _IsInEditMode)
                Return Temp
            End Get
        End Property

        Dim _IsInEditMode As Boolean = False
        Public Property IsInEditMode As Boolean
            Get
                Return _IsInEditMode
            End Get
            Set(value As Boolean)
                ChangeIfDifferent(_IsInEditMode, value, "IsInEditMode")
            End Set
        End Property

        <Dependency("IsInEditMode")>
        Public ReadOnly Property EditButtonText As String
            Get
                If _IsInEditMode Then
                    Return "Bestätigen"
                Else
                    Return "Bearbeiten"
                End If
            End Get
        End Property

        <Dependency("IsInEditMode")>
        Public ReadOnly Property EditTextBlockVisibility As Visibility
            Get
                Return (Not _IsInEditMode).ToVisibility
            End Get
        End Property

        <Dependency("IsInEditMode")>
        Public ReadOnly Property EditTextBoxVisibility As Visibility
            Get
                Return _IsInEditMode.ToVisibility
            End Get
        End Property

        Public Sub New(NewTarget As CasparCGServer)
            MyBase.New(True, NewTarget)
        End Sub

    End Class

End Namespace
