Imports System.IO
Imports HermesFramework.Processor

Public Class FileCheck
    Implements IRunnerLibrary(Of Boolean)

    Public Function RunTask(j As Job) As RunnerResult(Of Boolean) Implements IRunnerLibrary(Of Boolean).RunTask

        Dim result As New RunnerResult(Of Boolean)

        Try
            If File.Exists("c:\temp\log.txt") Then
                result.Result = True
            Else
                result.Result = False
            End If

            result.RanSuccessfully = True
        Catch ex As Exception
            result.Error = ex
            result.RanSuccessfully = False
        End Try

        Return result

    End Function

End Class
