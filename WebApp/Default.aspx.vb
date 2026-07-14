Imports ClassLibrary

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim todoList = New TodoList()

        lblResult.Text = todoList.FindTodoByIdOrThrow(2)("DescriptionText").ToString()
    End Sub

End Class