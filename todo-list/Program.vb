Imports System

Module Program
    Sub Main(args As String())
        Dim todoList As TodoList = New TodoList()

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

    Sub RenderMainMenu(inputError As String)
        'TODO: editar e apagar item
        Dim menuOptions As String() = {
            "Sair",
            "Ver lista",
            "Adicionar item"
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

        Console.WriteLine()
        Console.WriteLine("Pressione qualquer tecla para voltar para o menu principal")
        Console.ReadKey()
    End Sub

    Sub AddTodoSubmenu(todoList As TodoList)
        ClearConsole("ADICIONAR TAREFA")

        Console.WriteLine("Digite uma descrição da tarefa e pressione enter:")
        Dim newTodoDescription As String = Console.ReadLine()

        todoList.AddTodo(newTodoDescription)
    End Sub
End Module