
'Könnte nochmal nützlich werden.
#Const ThrowOnAmbiguity = True

''' <summary>
''' <see cref="M:Type.[GetType](String)"/> versucht, die im Namen angegebene Assembly zu laden.
''' Bei der Verwendung von MEF schlägt die Suche nach dem Typ fehl. Genaueres dazu hier: https://msdn.microsoft.com/en-us/library/dd153782.aspx
''' </summary>
Public Class GetTypeHelper

    ''' <summary>
    ''' Durchsucht alle geladenen Assemblies in <see cref="AppDomain.GetAssemblies"/> nach einem Typ mit dem selben <see cref="Type.AssemblyQualifiedName"/>.
    ''' </summary>
    ''' <param name="Name">Der <see cref="Type.AssemblyQualifiedName"/> des Typs.</param>
    Public Shared Function GetTypeByAssemblyQualifiedNameInLoadedAssemblies(Name As String) As Type
#If ThrowOnAmbiguity Then
        Dim Candidates As New List(Of Type)
#End If
        For Each i In AppDomain.CurrentDomain.GetAssemblies
            For Each j In i.GetTypes
                If j.AssemblyQualifiedName = Name Then
#If ThrowOnAmbiguity Then
                    Candidates.Add(j)
#Else
                    Return j
#End If
                End If
            Next
        Next
#If ThrowOnAmbiguity Then
        Select Case Candidates.Count
            Case 0
                Return Nothing
            Case 1
                Return Candidates(0)
            Case Else
                'PropertyTypeAmbiguityException
                Throw New Exception(String.Format("Für den Typenname '{0}' wurden {1} gleichnamige Typen gefunden.", Name, Candidates.Count))
        End Select
#Else
        Return Nothing
#End If
    End Function

End Class
