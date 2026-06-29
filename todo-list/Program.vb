Imports System

Module Program
    Sub Main(args As String())
        Dim userTodoList As TodoList = New TodoList()

        'TODO: editar e apagar item
        Dim menuOptions As MenuOption() = {
            New MenuOption(0, "Sair"),
            New MenuOption(1, "Ver lista"),
            New MenuOption(2, "Adicionar item")
        }

        Dim userOptionInput As String = ""
        Dim inputError As String = ""

        'Loop principal - mantém o programa rodando enquanto o usuário não escolher sair
        Do
            ClearConsole()

            'Mostra erro de entrada, se houver
            If Not String.IsNullOrWhiteSpace(inputError) Then
                Console.WriteLine($"Erro: {inputError}")
            End If

            Console.WriteLine("Escolha uma opção no menu digitando o número correspondente e pressionando enter")

            'Mostra as opções de menu definidas
            For Each menuOption As MenuOption In menuOptions
                Console.WriteLine($"[{menuOption.Code}] {menuOption.Label}")
            Next

            userOptionInput = Console.ReadLine()

            ClearConsole()

            Select Case userOptionInput
                Case "0"
                    Console.WriteLine("Saindo...")
                Case "1"
                    PrintTodosSubmenu(menuOptions(1), userTodoList)
                Case "2"
                    AddTodoSubmenu(menuOptions(2), userTodoList)
                Case Else
                    inputError = "Opção inválida, tente novamente."
            End Select
        Loop While userOptionInput <> "0"
    End Sub

    Sub ClearConsole()
        'Limpa o console e renderiza o cabeçalho
        Console.Clear()
        Console.WriteLine("=====[ Lista de Tarefas ]=====")
        Console.WriteLine()
    End Sub

    Sub PrintTodosSubmenu(menuOption As MenuOption, todoList As TodoList)
        Console.WriteLine(menuOption.Label.ToUpper)
        Console.WriteLine()

        todoList.PrintTasks()

        Console.WriteLine()
        Console.WriteLine("Pressione qualquer tecla para voltar para o menu principal")
        Console.ReadKey()
    End Sub

    Sub AddTodoSubmenu(menuOption As MenuOption, todoList As TodoList)
        Console.WriteLine(menuOption.Label.ToUpper)
        Console.WriteLine()

        Console.WriteLine("Digite uma descrição da tarefa e pressione enter:")
        Dim newTodoDescription As String = Console.ReadLine()

        todoList.AddTodo(newTodoDescription)
    End Sub
End Module