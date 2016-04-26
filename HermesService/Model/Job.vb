Namespace Model

    Public Class Job
        Implements IJob

        Public Property ID As Integer
        Public Property SystemID As Integer
        Public Property RunOnce As Nullable(Of DateTime)
        Public Property RunTime As String
        Public Property Monday As Boolean
        Public Property Tuesday As Boolean
        Public Property Wednesday As Boolean
        Public Property Thursday As Boolean
        Public Property Friday As Boolean
        Public Property Saturday As Boolean
        Public Property Sunday As Boolean
        Public Property RepeatMin As Integer
        Public Property RunDll As String
        Public Property IsEnabled As Boolean
        Public Property RunParameters As Byte()

        Public Shared Function GetAll() As List(Of Model.Job)
            Return New List(Of Model.Job)
        End Function

        Public Sub Run(params As Object) Implements IJob.Run
            Throw New NotImplementedException()
        End Sub

    End Class

End Namespace