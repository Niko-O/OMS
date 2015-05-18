
Imports System.Runtime.CompilerServices

<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
Module Extensions

    <Extension()>
    Public Sub AddRange(Of T)(Target As ObservableCollection(Of T), Items As IEnumerable(Of T))
        For Each i In Items
            Target.Add(i)
        Next
    End Sub

End Module
