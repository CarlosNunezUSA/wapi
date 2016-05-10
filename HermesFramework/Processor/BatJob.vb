Imports System.Configuration
Imports System.IO

Namespace Processor
    Public Class BatJob
        Inherits Job

        Public Property BatchFileName As String




        Private Function RunBatch(workingFolder As String, fileName As String, params As String) As RunnerResult(Of Boolean)

            Dim result As New AssemblyResult(Of Boolean)
            result.RunStart = DateTime.Now
            Try

                Dim jobFile As String = Path.Combine(workingFolder, fileName)

                If File.Exists(jobFile) Then

                    Dim proc = New Process()
                    proc.StartInfo.WorkingDirectory = workingFolder
                    proc.StartInfo.FileName = fileName
                    proc.StartInfo.CreateNoWindow = True
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    proc.StartInfo.Arguments = params
                    proc.Start()
                    proc.WaitForExit()
                    If proc.ExitCode = 0 Then
                        result.Result = True
                    Else
                        result.Result = False
                    End If

                Else
                    Throw New Exception("The specified BATCH file doesn't exist!")
                End If

            Catch ex As Exception
                result.Error = ex
                result.Result = False
            Finally
                result.RunEnd = DateTime.Now
            End Try

            Return result

        End Function

        Public Overrides Function Run(timenow As DateTime) As RunnerResult
            Throw New NotImplementedException
        End Function

    End Class
End NameSpace