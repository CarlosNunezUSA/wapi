Imports System.IO
Imports HermesFramework.Processor

Public Class FileCheck
    Implements ITaskLibrary

    Private Function RunTask(params As Object) As RunnerResult Implements ITaskLibrary.RunTask

        Dim result As New RunnerResult
        result.RunStart = DateTime.Now
        Try
            If File.Exists("c:\temp\log.txt") Then
                result.RanSuccessfully = True
            Else
                result.RanSuccessfully = False
            End If
        Catch ex As Exception
            result.Error = ex
            result.RanSuccessfully = False
        Finally
            result.RunEnd = DateTime.Now
        End Try

        Return result

    End Function

End Class
