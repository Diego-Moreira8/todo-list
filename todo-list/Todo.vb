Imports System.Data

Public Class Todo

    Public ReadOnly Property Id As Integer
    Private Property _description As String
    Public ReadOnly Property CreatedAt As Date

    Public Property DataRow As DataRow

    Public Sub New(id As Integer, description As String, createdAt As Date, dataRow As DataRow)

        If String.IsNullOrWhiteSpace(description) Then
            Throw New InvalidOperationException("Todo list description cannot be empty")
        End If

        Me.Id = id
        Me._description = description
        Me.CreatedAt = createdAt
        Me.DataRow = dataRow

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