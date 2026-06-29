Imports System
Imports System.Environment

Module Program
    Sub Main(args As String())
        Dim userTodoList As TodoList = New TodoList()

        Dim menuOptions As MenuOption() = {
            New MenuOption(0, "Sair"),
            New MenuOption(1, "Ver lista"),
            New MenuOption(2, "Adicionar item")
        }
        Dim userOptionInput As Integer = 0


        'Menu principal
        Console.WriteLine("=====[ Lista de Tarefas ]=====")

        Do
            Console.WriteLine($"{NewLine}{NewLine}Escolha uma opção no menu digitando o número correspondente e pressionando enter")

            For Each menuOption As MenuOption In menuOptions
                Console.WriteLine($"[{menuOption.Code}] {menuOption.Label}")
            Next

            userOptionInput = Console.ReadLine()

            Console.WriteLine()

            Select Case userOptionInput
                Case 0
                    Console.WriteLine("Saindo...")

                Case 1
                    Console.WriteLine(menuOptions(1).Label.ToUpper)
                    userTodoList.PrintTasks()

                Case 2
                    Console.WriteLine(menuOptions(2).Label.ToUpper)
                    Console.WriteLine("Digite uma descrição da tarefa e pressione enter:")
                    Dim newTodoDescription As String = Console.ReadLine()
                    userTodoList.AddTodo(newTodoDescription)

                Case Else
                    Console.WriteLine("Opção inválida, tente novamente.")
            End Select
        Loop While userOptionInput <> 0
    End Sub
End Module


