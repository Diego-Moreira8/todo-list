Public Class TodoNotFoundException
    Inherits Exception

    Public Sub New(id As Integer)
        MyBase.New($"Todo with ID {id} not found")
    End Sub
End Class
