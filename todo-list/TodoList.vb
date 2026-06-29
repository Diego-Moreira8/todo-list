Public Class TodoList
    Private Property _todos As List(Of Todo)

    Public Sub New()
        Me._todos = New List(Of Todo)
    End Sub

    Public Sub AddTodo(todoDescription As String)
        Dim newTodo As Todo = New Todo(todoDescription)
        _todos.Add(newTodo)
    End Sub

    Public Sub PrintTasks()
        For Each todo As Todo In _todos
            Console.WriteLine($" > {todo.CreatedAt} -- {todo.Description}")
        Next
    End Sub
End Class