
Namespace PluginManagement.TabControl

    ''' <summary>
    ''' Ein TabControl, das <see cref="MainTabItem"/> und <see cref="PluginTabItem"/> anzeigt.
    ''' </summary>
    Public Class MainTabControl
        Inherits System.Windows.Controls.TabControl

        Dim _PersistentTabItems As New ObservableCollection(Of MainTabItem)
        Public ReadOnly Property PersistentTabItems As ObservableCollection(Of MainTabItem)
            Get
                Return _PersistentTabItems
            End Get
        End Property

        Public Property BindableTabs As IEnumerable(Of MainTabItem)
            Get
                Return DirectCast(GetValue(BindableTabsProperty), IEnumerable(Of MainTabItem))
            End Get
            Set(value As IEnumerable(Of MainTabItem))
                SetValue(BindableTabsProperty, value)
            End Set
        End Property
        Public Shared ReadOnly BindableTabsProperty As DependencyProperty = _
            DependencyProperty.Register("BindableTabs", _
                                        GetType(IEnumerable(Of MainTabItem)), _
                                        GetType(MainTabControl), _
                                        New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnBindableTabsDependencyPropertyChanged)))
        Private Shared Sub OnBindableTabsDependencyPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            DirectCast(d, MainTabControl).OnBindableTabsPropertyChanged(DirectCast(e.OldValue, IEnumerable(Of MainTabItem)), DirectCast(e.NewValue, IEnumerable(Of MainTabItem)))
        End Sub
        Private Sub OnBindableTabsPropertyChanged(Oldvalue As IEnumerable(Of MainTabItem), NewValue As IEnumerable(Of MainTabItem))
            ActualizeTabs()
        End Sub

        Public Sub New()
            If ViewModelBase.IsInDesignMode Then
                Me.SetBinding(ItemsSourceProperty, New Binding With {.Source = _PersistentTabItems})
            End If
        End Sub

        Private Sub BindableTabsChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs)
            ActualizeTabs()
        End Sub

        Private Sub ActualizeTabs()
            If Not ViewModelBase.IsInDesignMode AndAlso Not _PersistentTabItems Is Nothing AndAlso Not BindableTabsProperty Is Nothing Then
                ItemsSource = {_PersistentTabItems, BindableTabs}.ConcatAll
            End If
        End Sub

    End Class

End Namespace
