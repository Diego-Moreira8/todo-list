Public Class TodoList
    Public Property Todos As List(Of Todo)
    'Private Property NextTodoId As Integer = 1

    Public Sub New()
        Me.Todos = New List(Of Todo)
    End Sub

    Public Sub PrintTasks(Optional idOfSelectedTodo As Integer = 0)
        Console.WriteLine("ID -- Descrição -- Data/Hora da Criação")

        If Todos.Count = 0 Then
            Console.WriteLine("Lista vazia")
            Return
        End If

        For Each todo As Todo In Todos
            Console.WriteLine($"{If(idOfSelectedTodo = todo.Id, "*", " ")} {todo.Id} -- {todo.Description} -- {todo.CreatedAt}")
        Next
    End Sub

    Public Function GetTodoById(todoId As Integer)
        Dim foundTodo As Todo = Todos.Find(Function(todo) todoId = todo.Id)

        If foundTodo Is Nothing Then
            Throw New TodoNotFoundException(todoId)
        End If

        Return foundTodo
    End Function

    Public Sub AddTodo(newTodo As Todo)
        Todos.Add(newTodo)
    End Sub

    Public Sub EditTodoDescription(todoId As Integer, newDescription As String)
        Dim foundTodo As Todo = Me.GetTodoById(todoId)

        foundTodo.Description = newDescription
    End Sub

    Public Sub DeleteTodo(todoId As Integer)
        Dim foundTodo As Todo = Me.GetTodoById(todoId)

        Todos.Remove(foundTodo)
    End Sub

    Public Function IsEmpty()
        Return Me.Todos.Count = 0
    End Function
End Class