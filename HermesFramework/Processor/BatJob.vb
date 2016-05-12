Imports System.Configuration
Imports System.IO

Namespace Processor
    Public Class BatJob
        Inherits Job

        Public Function GetAll(scheduleFolder As String) As List(Of Job)
            Return GetJobsFromDisk(scheduleFolder)
        End Function

        Public Overrides Function Run(timenow As DateTime, workingFolder As String) As RunnerResult

            Dim runfile As String = Path.Combine(workingFolder, FileName)

            If Not File.Exists(runfile) Then
                Throw New Exception("Batch File not found or not specified.")
            End If

            Dim result As New RunnerResult
            result.RunStart = DateTime.Now

            Try

                Dim proc = New Process()
                proc.StartInfo.WorkingDirectory = workingFolder
                proc.StartInfo.FileName = Me.FileName
                proc.StartInfo.CreateNoWindow = True
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                proc.StartInfo.Arguments = Me.RunParameters
                proc.Start()
                proc.WaitForExit()
                If proc.ExitCode = 0 Then
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

        Private Function GetJobsFromDisk(scheduleFolder As String) As List(Of Job)

            Dim result As New List(Of Job)

            Dim di As New DirectoryInfo(scheduleFolder)
            Dim files = di.GetFiles("*.hrm", SearchOption.AllDirectories)

            For Each f As FileInfo In files
                result.Add(GetJobFromFile(f))
            Next

            Return result

        End Function

        Private Function GetJobFromFile(f As FileInfo) As Job

            Dim jobLines As String() = File.ReadAllLines(f.FullName)

            Dim j As New BatJob

            Dim aLine As String()

            Dim value As String = ""

            For Each ln As String In jobLines

                aLine = ln.Split("=")

                If aLine IsNot Nothing AndAlso aLine.Count >= 2 Then

                    value = aLine(1).Replace(vbTab, "").Trim

                    If InStr(aLine(0), "@-IsEnabled") Then
                        j.IsEnabled = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-RunOnce") Then
                        Try
                            j.RunOnce = ReplaceParameters(value, j)
                            j.RunOnce = DateTime.Parse(value)
                        Catch ex As Exception
                            j.RunOnce = Nothing
                        End Try
                    End If
                    If InStr(ln, "@-RunTime") Then
                        j.RunTime = value
                    End If
                    If InStr(ln, "@-Monday") Then
                        j.Monday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-Tuesday") Then
                        j.Tuesday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-Wednesday") Then
                        j.Wednesday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-Thursday") Then
                        j.Thursday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-Friday") Then
                        j.Friday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-Saturday") Then
                        j.Saturday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-Sunday") Then
                        j.Sunday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-RunAlways") Then
                        j.Sunday = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-FileName") Then
                        j.FileName = IIf(String.IsNullOrWhiteSpace(value), "", value.Trim())
                    End If
                    If InStr(ln, "RunParameters") Then
                        j.RunParameters = ReplaceParameters(value, j, f.Name)
                    End If

                End If

            Next

            'If j IsNot Nothing Then
            '    j.RunBatch = f.Name
            'End If

            Return j

        End Function


        Private Function ReplaceParameters(value As String, j As BatJob, optional file As string = "") As String
            value = value.Replace("@@-Date", String.Format("{1}{0}{1}", DateTime.Now, Chr(34)))
            value = value.Replace("@@-File", String.Format("{1}{0}{1}", file, Chr(34)))
            value = value.Replace("@@-User", System.Security.Principal.WindowsIdentity.GetCurrent().Name)
            value = value.Replace("@@-Today", DateTime.Now.Date)
            value = value.Replace("@@-RunTime", j.RunTime)
            value = value.Replace("@@-Now", DateTime.Now)
            Return value
        End Function

    End Class

End Namespace