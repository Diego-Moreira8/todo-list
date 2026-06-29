Imports System

Module Program
    Sub Main(args As String())
        Dim newTodoList As TodoList = New TodoList()

        newTodoList.AddTodo("Primeira tarefa")
        newTodoList.AddTodo("Segunda tarefa")
        'newTodoList.AddTodo("   ")

        newTodoList.PrintTasks()
    End Sub
End Module
