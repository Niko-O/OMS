
Namespace PluginManagement.Import

    Public Class TargetDirectory
        Inherits ViewModelBase

        Dim _Name As String
        Public ReadOnly Property Name As String
            Get
                Return _Name
            End Get
        End Property

        Dim _IsExistingDirectory As Boolean
        Public ReadOnly Property IsExistingDirectory As Boolean
            Get
                Return _IsExistingDirectory
            End Get
        End Property

        Dim _IsSelectedAsTarget As Boolean = False
        Public Property IsSelectedAsTarget As Boolean
            Get
                Return _IsSelectedAsTarget
            End Get
            Set(value As Boolean)
                ChangeIfDifferent(_IsSelectedAsTarget, value, "IsSelectedAsTarget")
            End Set
        End Property

        <Dependency("IsSelectedAsTarget")>
        Public ReadOnly Property Icon As ImageSource
            Get
                If _IsSelectedAsTarget Then
                    'TargetDirectory
                    Return New BitmapImage(New Uri("pack://application:,,,/Resources/TargetDirectory.png"))
                Else
                    If _IsExistingDirectory Then
                        'ExistingDirectory
                        Return New BitmapImage(New Uri("pack://application:,,,/Resources/ExistingDirectory.png"))
                    Else
                        'DeletedDirectory
                        Return New BitmapImage(New Uri("pack://application:,,,/Resources/DeletedDirectory.png"))
                    End If
                End If
            End Get
        End Property

        Public Sub New(NewName As String, NewIsExistingDirectory As Boolean)
            MyBase.New(True)
            _Name = NewName
            _IsExistingDirectory = NewIsExistingDirectory
        End Sub

    End Class

End Namespace
