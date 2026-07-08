Imports System.Data
Imports Microsoft.Data.SqlClient

Public Class TodoList

    Private Property TodosDataSet As DataSet = New DataSet()
    Private Property NextTodoId As Integer

    Private Property ConnectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=TodoListDB;Integrated Security=True"
    Private Property Adapter As SqlDataAdapter


    Public Sub New()

        ImportFromDataBase()
        InitializeNextTodoId()

    End Sub

    Private Sub ImportFromDataBase()

        'Abre conexão para trazer os dados para a memória
        Using connection As New SqlConnection(ConnectionString)
            Try
                Console.WriteLine("Importando dados da base de dados...")

                connection.Open()
                Adapter = New SqlDataAdapter("SELECT * FROM Todos", connection)

                'Cria uma DataTable de nome "Todos" dentro do DataSet todosDataSet
                Adapter.Fill(Me.TodosDataSet, "Todos")
            Catch ex As Exception
                Console.WriteLine("Conexão com a base de dados falhou!")
                Console.WriteLine(ex.Message)
                Console.WriteLine("Pressione qualquer tecla para sair.")
                Console.ReadKey()
            End Try
        End Using

    End Sub

    Private Sub InitializeNextTodoId()

        'Classifica a tabela por Id descendente
        Dim rows() As DataRow = TodosDataSet.Tables("Todos").Select("", "Id DESC")

        NextTodoId = If(rows.Length = 0, 1, rows(0)("Id") + 1)

    End Sub

    Public Sub SyncDataBase()

        'SqlCommandBuilder irá gerar as queries para sincronizar os dados
        Dim builder As New SqlCommandBuilder(Adapter)

        Using connection As New SqlConnection(ConnectionString)
            Try
                Console.WriteLine("Sincronizando com a base de dados...")
                connection.Open()
                Adapter.SelectCommand.Connection = connection
                Adapter.Update(TodosDataSet, "Todos")

                Console.WriteLine("Dados sincronizados com sucesso!")
                Console.WriteLine("Pressione qualquer tecla para sair.")
                Console.ReadKey()
            Catch ex As Exception
                Console.WriteLine("Conexão com a base de dados falhou!")
                Console.WriteLine(ex.Message)
                Console.WriteLine("Pressione qualquer tecla para sair.")
                Console.ReadKey()
            End Try
        End Using

    End Sub

    Public Sub PrintTasks(Optional idOfSelectedTodo As Integer = 0)

        Console.WriteLine("ID -- Descrição -- Data/Hora da Criação")

        If TodosDataSet.Tables("Todos").Rows.Count = 0 Then
            Console.WriteLine("Lista vazia")
            Return
        End If

        For Each row As DataRow In TodosDataSet.Tables("Todos").Rows
            Dim id As Integer = row("Id")
            Dim description As String = row("DescriptionText")
            Dim createdAt As Date = row("CreatedAt")

            Console.WriteLine($"{If(idOfSelectedTodo = id, "*", " ")} {id} -- {description} -- {createdAt}")
        Next

    End Sub

    'Public Function GetTodoById(todoId As Integer)

    '    Dim foundTodo As Todo = TodosList.Find(Function(todo) todoId = todo.Id)

    '    If foundTodo Is Nothing Then
    '        Throw New TodoNotFoundException(todoId)
    '    End If

    '    Return foundTodo

    'End Function

    Public Sub AddTodo(descriptionText As String)

        Dim newTodoDataRow As DataRow = Me.TodosDataSet.Tables("Todos").NewRow()

        newTodoDataRow("DescriptionText") = descriptionText
        newTodoDataRow("CreatedAt") = Now

        Me.TodosDataSet.Tables("Todos").Rows.Add(newTodoDataRow)

        NextTodoId += 1

    End Sub

    'Public Sub EditTodoDescription(todoId As Integer, newDescription As String)

    '    Dim foundTodo As Todo = Me.GetTodoById(todoId)

    '    foundTodo.Description = newDescription

    'End Sub

    'Public Sub DeleteTodo(todoId As Integer)

    '    Dim foundTodo As Todo = Me.GetTodoById(todoId)

    '    TodosList.Remove(foundTodo)

    'End Sub

    'Public Function IsEmpty()

    '    Return Me.TodosList.Count = 0

    'End Function

End Class