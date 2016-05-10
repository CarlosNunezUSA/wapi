Namespace Processor

    Public MustInherit Class Job
        Implements IJob


        Public Property Id As Integer
        Public Property IsEnabled As Boolean
        Public Property RunOnce As Nullable(Of DateTime)
        Public Property RunTime As String
        Public Property Monday As Boolean
        Public Property Tuesday As Boolean
        Public Property Wednesday As Boolean
        Public Property Thursday As Boolean
        Public Property Friday As Boolean
        Public Property Saturday As Boolean
        Public Property Sunday As Boolean
        Public Property RunParameters As String


        Public MustOverride Function Run(timenow As DateTime) As RunnerResult Implements IJob.Run

    End Class

End Namespace