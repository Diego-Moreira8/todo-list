Public Class Todo
    Private Property _description As String
    Public ReadOnly Property CreatedAt As Date

    Public Sub New(description As String)
        If String.IsNullOrWhiteSpace(description) Then
            Throw New InvalidOperationException("Todo list description cannot be empty")
        End If

        Me._description = description
        Me._CreatedAt = Date.Now
    End Sub

    Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            If String.IsNullOrWhiteSpace(value) Then
                Throw New InvalidOperationException("Todo list description cannot be empty")
            End If
            _description = value
        End Set
    End Property
End Class