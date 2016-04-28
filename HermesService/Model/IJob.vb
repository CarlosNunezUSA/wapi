Namespace Model

    Public Interface IJob
        Function Run(param As Object, timenow As DateTime, Optional force As Boolean = False) As JobResult
    End Interface

End Namespace