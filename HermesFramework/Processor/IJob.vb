Imports RunnerResult = HermesFramework.Processor.RunnerResult

Namespace Processor
    Public Interface IJob
        Function Run(timenow As DateTime, workingFolder As String) As RunnerResult
    End Interface

End NameSpace