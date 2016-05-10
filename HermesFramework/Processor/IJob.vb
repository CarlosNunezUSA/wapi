Imports RunnerResult = HermesFramework.Processor.RunnerResult

Namespace Processor
    Public Interface IJob
        Function Run(timenow As DateTime) As RunnerResult
    End Interface

End NameSpace