Imports System

Module Program
    Sub Main(args As String())
        Dim todoList As TodoList = New TodoList()

        '!!!!!!!!!!!!!TEMP
        todoList.AddTodo("Item 0")
        todoList.AddTodo("Item 1")
        todoList.AddTodo("Item 2")

        Dim optionInput As String = ""
        Dim inputError As String = ""

        'Loop principal - mantém o programa rodando enquanto o usuário não escolher sair
        Do
            RenderMainMenu(inputError)

            inputError = ""

            optionInput = Console.ReadLine().Trim()

            Select Case optionInput
                Case "0"
                    ClearConsole("Saindo...")
                Case "1"
                    PrintTodosSubmenu(todoList)
                Case "2"
                    AddTodoSubmenu(todoList)
                Case "3"
                    EditTodoSubmenu(todoList)
                Case Else
                    inputError = "Opção inválida, tente novamente."
            End Select
        Loop While optionInput <> "0"
    End Sub

    Sub ClearConsole(menuTitle As String)
        'Limpa o console e renderiza o cabeçalho
        Console.Clear()
        Console.WriteLine("=====[ LISTA DE TAREFAS ]=====")
        Console.WriteLine()
        Console.WriteLine(menuTitle.ToUpper())
        Console.WriteLine()
    End Sub

    Sub PauseConsole()
        Console.WriteLine()
        Console.WriteLine("Pressione qualquer tecla para voltar para o menu principal")
        Console.ReadKey()
    End Sub

    Sub RenderMainMenu(inputError As String)
        'TODO: editar e apagar item
        Dim menuOptions As String() = {
            "Sair",
            "Ver tarefas",
            "Adicionar tarefa",
            "Editar tarefa"
        }

        ClearConsole("MENU PRINCIPAL")

        'Mostra erro de entrada, se houver
        If Not String.IsNullOrWhiteSpace(inputError) Then
            Console.WriteLine($"Erro: {inputError}")
            Console.WriteLine()
        End If

        Console.WriteLine("Escolha uma opção no menu digitando o número correspondente e pressionando enter")

        'Mostra as opções de menu definidas
        For i As Integer = 0 To menuOptions.Length - 1
            Console.WriteLine($"[{i}] {menuOptions(i)}")
        Next
    End Sub

    Sub PrintTodosSubmenu(todoList As TodoList)
        ClearConsole("VER TAREFAS")

        todoList.PrintTasks()

        PauseConsole()
    End Sub

    Sub AddTodoSubmenu(todoList As TodoList)
        ClearConsole("ADICIONAR TAREFA")

        Console.WriteLine("Digite uma descrição da tarefa e pressione enter:")
        Dim newTodoDescription As String = Console.ReadLine()

        todoList.AddTodo(newTodoDescription)
    End Sub

    Sub EditTodoSubmenu(todoList As TodoList)
        Dim success As Boolean = False
        Dim todoId As Integer
        Dim inputError As String = ""

        While Not success
            ClearConsole("EDITAR TAREFA")

            'Mostra erro, se houver
            If Not String.IsNullOrWhiteSpace(inputError) Then
                Console.WriteLine(inputError)
                Console.WriteLine()
                inputError = ""
            End If

            Console.WriteLine("Digite o ID da tarefa que deseja editar e pressione enter")
            Dim input As String = Console.ReadLine()

            Try
                'Lança uma FormatException para um valor não numérico
                todoId = Convert.ToInt32(input)

                'Apenas para checar se existe. Se não, irá lançar uma TodoNotFoundException
                todoList.GetTodo(todoId)

                Console.WriteLine("Digite uma nova descrição para a tarefa e pressione enter")
                Dim newDescriptionInput As String = Console.ReadLine()

                todoList.EditTodoDescription(todoId, newDescriptionInput)

                success = True
            Catch ex As FormatException
                inputError = "ID inválido! Precisa ser um número inteiro maior que 1."
                success = False
            Catch ex As TodoNotFoundException
                inputError = $"Tarefa com ID {todoId} não existe! Tente novamente."
                success = False
            End Try
        End While
    End Sub
End Module