Namespace Processor

    Public Class RunnerResult

        Public Property [RunStart] As DateTime
        Public Property RunEnd As DateTime
        Public Property RanSuccessfully As Boolean
        Public Property [Error] As Exception

        Public Sub New()
            RunStart = DateTime.Now
            RanSuccessfully = False
        End Sub

    End Class

End Namespace
