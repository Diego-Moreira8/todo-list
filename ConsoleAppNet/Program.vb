Imports ClassLibrary

Module Program

    ReadOnly todoList = New TodoList()

    Sub Main()

        AppMainLoop()

    End Sub

    ''' <summary>
    '''Mantém o programa rodando enquanto o usuário não escolher sair
    ''' </summary>
    Sub AppMainLoop()


        Dim optionInput As String
        Dim inputError As String = ""

        Do
            RenderMainMenu(inputError)

            inputError = ""

            optionInput = Console.ReadLine().Trim()

            Select Case optionInput
                Case "0"
                    ReloadHeader("Saindo...")
                    todoList.SyncDataBase()
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

    ''' <summary>
    ''' Limpa o console e renderiza o cabeçalho.
    ''' </summary>
    ''' <param name="menuTitle">Título que será exibido abaixo da lista de tarefas.</param>
    ''' <param name="idOfSelectedTodo">ID da tarefa selecionada. Será indicada com um * na interface.</param>
    ''' <remarks>
    ''' O cabaçalho também mostra a lista de tarefas atual.
    ''' </remarks>
    Sub ReloadHeader(menuTitle As String, Optional idOfSelectedTodo As Integer = 0)


        Console.Clear()

        Console.WriteLine("=====[ LISTA DE TAREFAS ]=====")
        Console.WriteLine()
        todoList.PrintTasks(idOfSelectedTodo)
        Console.WriteLine()
        Console.WriteLine(menuTitle.ToUpper())
        Console.WriteLine()

    End Sub

    Sub RenderMainMenu(inputError As String)

        Dim menuOptions As String() = {
            "Sair (e salvar alterações na base de dados)",
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

                'Apenas para checar se existe antes de solicitar a nova descrição.
                'Se não existir, irá lançar uma TodoNotFoundException
                todoList.FindTodoByIdOrThrow(todoId)

                ReloadHeader("EDITAR TAREFA", todoId)

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

                'Apenas para checar se existe antes de solicitar a nova descrição.
                'Se não existir, irá lançar uma TodoNotFoundException
                todoList.FindTodoByIdOrThrow(todoId)

                ReloadHeader("Apagar tarefa", todoId)

                Console.WriteLine($"Tem certeza que deseja apagar a tarefa #{todoId}? Essa ação não poderá ser desfeita!")
                Console.WriteLine("([S] para sim / [N] para não)")

                Dim deleteConfirmationInput As String = Console.ReadLine().ToUpper()

                If deleteConfirmationInput = "S" Then
                    todoList.DeleteTodo(todoId)
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

End Module