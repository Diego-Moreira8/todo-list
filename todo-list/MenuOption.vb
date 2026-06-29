Public Class MenuOption
    Public ReadOnly Property Code As Integer
    Public ReadOnly Property Label As String

    Public Sub New(code As Integer, label As String)
        Me.Code = code
        Me.Label = label
    End Sub
End Class
