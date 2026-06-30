Public Class TodoList
    Private Property _todos As List(Of Todo)
    Private Property NextTodoId As Integer = 1

    Public Sub New()
        Me._todos = New List(Of Todo)
    End Sub

    Public Sub PrintTasks()
        If _todos.Count = 0 Then
            Console.WriteLine("Lista vazia!")
            Return
        End If

        Console.WriteLine($"   ID -- Descrição -- Data/Hora da Criação")

        For Each todo As Todo In _todos
            Console.WriteLine($" > {todo.Id} -- {todo.Description} -- {todo.CreatedAt}")
        Next
    End Sub

    Public Function GetTodo(todoId As Integer)
        Dim foundTodo As Todo = _todos.Find(Function(todo) todoId = todo.Id)

        If foundTodo Is Nothing Then
            Throw New TodoNotFoundException(todoId)
        End If

        Return foundTodo
    End Function

    Public Sub AddTodo(todoDescription As String)
        Dim newTodo As Todo = New Todo(NextTodoId, todoDescription)
        _todos.Add(newTodo)
        NextTodoId += 1
    End Sub

    Public Sub EditTodoDescription(todoId As Integer, newDescription As String)
        Dim foundTodo As Todo = _todos.Find(Function(todo) todo.Id = todoId)

        If foundTodo Is Nothing Then
            Throw New TodoNotFoundException(todoId)
        End If

        foundTodo.Description = newDescription
    End Sub
End Class