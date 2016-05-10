
Imports System.Configuration
Imports System.IO
Imports System.Reflection
Imports HermesFramework.Data


Namespace Processor

    Public Class Runner

        Public Property Connection As DatabaseFactory

        Public Sub RunTask(timenow As Date)
            'get all jobs
            ' run jobs if in schedule

            Dim jobs As List(Of Job) = GetAll()

            If jobs.Count > 0 Then
                Parallel.ForEach(Of Job)(jobs, Sub(j)
                                                   If j IsNot Nothing AndAlso j.IsEnabled Then
                                                       Dim result As RunnerResult = j.Run(timenow)
                                                   End If
                                               End Sub)
            End If

        End Sub

        Public Function GetAll() As List(Of Job)

            'Dim result As List(Of Job) = Nothing

            '' get from database
            'Dim dt As DataTable = DataFacade.GetDataTableSP(dbfactory, "p_GET_ActiveJobs", Nothing)

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            '    result = New List(Of Job)
            '    For Each r As DataRow In dt.Rows
            '        result.Add(RowToObject(r))
            '    Next

            'End If

            ''get jobs from disk
            'Dim fileJobs As List(Of Job) = GetJobsFromDisk()

            'result.AddRange(fileJobs)

            'Return result

        End Function



        Private Shared Function GetJobFromFile(f As FileInfo) As Job

            Dim jobLines As String() = File.ReadAllLines(f.FullName)

            Dim j As New Job

            Dim aLine As String()

            Dim value As String = ""

            For Each ln As String In jobLines

                aLine = ln.Split(":")

                If aLine IsNot Nothing AndAlso aLine.Count >= 2 Then

                    value = aLine(1).Replace(vbTab, "").Trim

                    If InStr(aLine(0), "@-IsEnabled") Then
                        j.IsEnabled = Boolean.Parse(value)
                    End If
                    If InStr(ln, "@-RunOnce") Then
                        Try
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
                    If InStr(ln, "RunParameters") Then
                        value = value.Replace("@@-Date", String.Format("{1}{0}{1}", DateTime.Now, Chr(34)))
                        value = value.Replace("@@-File", String.Format("{1}{0}{1}", f, Chr(34)))
                        value = value.Replace("@@-User", System.Security.Principal.WindowsIdentity.GetCurrent().Name)
                        j.RunParameters = value
                    End If

                End If

            Next

            'If j IsNot Nothing Then
            '    j.RunBatch = f.Name
            'End If

            Return j

        End Function

        Private Shared Function GetJobsFromDisk() As List(Of Job)

            Dim result As New List(Of Job)

            Dim jobsfolder As String = ConfigurationManager.AppSettings("jobsfolder")
            Dim di As New DirectoryInfo(jobsfolder)
            Dim files = di.GetFiles("*.hrm", SearchOption.AllDirectories)

            For Each f As FileInfo In files
                result.Add(GetJobFromFile(f))
            Next

            Return result

        End Function


    End Class

End Namespace