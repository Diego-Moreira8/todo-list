Imports System

Module Program
    Dim todoList As TodoList = New TodoList()

    Sub Main(args As String())
        'Testes
        todoList.AddTodo("Item 0")
        todoList.AddTodo("Item 1")
        todoList.AddTodo("Item 2")

        Dim optionInput As String
        Dim inputError As String = ""

        'Loop principal - mantém o programa rodando enquanto o usuário não escolher sair
        Do
            RenderMainMenu(inputError)

            inputError = ""

            optionInput = Console.ReadLine().Trim()

            Select Case optionInput
                Case "0"
                    ReloadHeader("Saindo...")
                Case "1"
                    AddTodoSubmenu()
                Case "2"
                    If todoList.IsEmpty() Then
                        inputError = "Lista vazia, não há nada para editar!"
                    Else
                        EditTodoSubmenu()
                    End If
                Case "3"
                    If todoList.IsEmpty() Then
                        inputError = "Lista vazia, não há nada para apagar!"
                    Else
                        DeleteTodoSubmenu()
                    End If
                Case Else
                    inputError = "Opção inválida, tente novamente."
            End Select
        Loop While optionInput <> "0"
    End Sub

    Sub ReloadHeader(menuTitle As String)
        'Limpa o console e renderiza o cabeçalho
        'O cabaçalho mostra a lista de tarefas atual
        Console.Clear()
        Console.WriteLine("=====[ LISTA DE TAREFAS ]=====")
        Console.WriteLine()
        todoList.PrintTasks()
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
            "Adicionar tarefa",
            "Editar tarefa",
            "Apagar tarefa"
        }

        ReloadHeader("MENU PRINCIPAL")

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

    Sub AddTodoSubmenu()
        ReloadHeader("ADICIONAR TAREFA")

        Console.WriteLine("Digite uma descrição da tarefa (ou deixe em branco para cancelar) e pressione enter:")
        Dim newTodoDescription As String = Console.ReadLine()

        If Not String.IsNullOrWhiteSpace(newTodoDescription) Then
            todoList.AddTodo(newTodoDescription)
        End If

    End Sub

    Sub EditTodoSubmenu()
        Dim success As Boolean = False
        Dim input As String
        Dim todoId As Integer
        Dim inputError As String = ""

        While Not success
            ReloadHeader("EDITAR TAREFA")

            'Mostra erro, se houver
            If Not String.IsNullOrWhiteSpace(inputError) Then
                Console.WriteLine(inputError)
                Console.WriteLine()
                inputError = ""
            End If

            Console.WriteLine("Digite o ID da tarefa que deseja editar (ou deixe em branco para cancelar) e pressione enter")
            input = Console.ReadLine()

            If String.IsNullOrWhiteSpace(input) Then Return

            Try
                'Lança uma FormatException para um valor não numérico
                todoId = Convert.ToInt32(input)

                'Apenas para checar se existe. Se não, irá lançar uma TodoNotFoundException
                todoList.GetTodo(todoId)

                Console.WriteLine("Digite uma nova descrição para a tarefa (ou deixe em branco para cancelar) e pressione enter")
                Dim newDescriptionInput As String = Console.ReadLine()

                If Not String.IsNullOrWhiteSpace(newDescriptionInput) Then
                    todoList.EditTodoDescription(todoId, newDescriptionInput)
                End If


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

    Sub DeleteTodoSubmenu()
        Dim success As Boolean = False
        Dim input As String
        Dim todoId As Integer
        Dim inputError As String = ""

        While Not success
            ReloadHeader("Apagar tarefa")

            'Mostra erro, se houver
            If Not String.IsNullOrWhiteSpace(inputError) Then
                Console.WriteLine(inputError)
                Console.WriteLine()
                inputError = ""
            End If

            Console.WriteLine("Digite o ID da tarefa que deseja apagar (ou deixe em branco para cancelar) e pressione enter")
            input = Console.ReadLine()

            If String.IsNullOrWhiteSpace(input) Then Return

            Try
                'Lança uma FormatException para um valor não numérico
                todoId = Convert.ToInt32(input)

                'Apenas para checar se existe. Se não, irá lançar uma TodoNotFoundException
                todoList.GetTodo(todoId)

                todoList.DeleteTodoDescription(todoId)

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