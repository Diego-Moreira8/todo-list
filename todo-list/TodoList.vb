Public Class TodoList
    Private Property _todos As List(Of Todo)
    Private Property NextTodoId As Integer = 1

    Public Sub New()
        Me._todos = New List(Of Todo)
    End Sub

    Public Sub AddTodo(todoDescription As String)
        Dim newTodo As Todo = New Todo(NextTodoId, todoDescription)
        _todos.Add(newTodo)
        NextTodoId += 1
    End Sub

    Public Sub PrintTasks()
        Console.WriteLine($"   ID -- Descrição -- Data/Hora da Criação")

        For Each todo As Todo In _todos
            Console.WriteLine($" > {todo.Id} -- {todo.Description} -- {todo.CreatedAt}")
        Next
    End Sub
End Class