Public Class DataError

    Public Property Messages As List(Of String)
    Public Property ExceptionData As Exception

    Public Sub New()
        Me.Messages = New List(Of String)
        Me.ExceptionData = New NotImplementedException
    End Sub

End Class
