
Namespace ServerList

    Public Class CasparCGServerViewModel
        Inherits ViewModelBase(Of CasparCGServer)

        Public Event Remove(Sender As CasparCGServerViewModel)
        Public ReadOnly Property RemoveCommand As DelegateCommand
            Get
                Static Temp As New DelegateCommand(Sub() RaiseEvent Remove(Me))
                Return Temp
            End Get
        End Property

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

        Public Sub New(NewTarget As CasparCGServer)
            MyBase.New(NewTarget)
        End Sub

    End Class

End Namespace
