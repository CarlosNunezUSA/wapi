'
' Runner Assemblies should implement this interface
'


Namespace Processor

    Public Interface IRunnerAssembly

        Function RunTask(Of T)(params As T) As RunnerResult

    End Interface

End Namespace