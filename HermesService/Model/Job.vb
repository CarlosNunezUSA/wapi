Imports DAL
Imports DAL.Utils
Imports System.ComponentModel
Imports System.Configuration
Imports System.Globalization
Imports System.IO

Namespace Model

    Public Class Job
        Implements IJob

        Public Property IsEnabled As Boolean
        Public Property ID As Integer
        Public Property SystemID As Integer
        Public Property RunOnce As Nullable(Of DateTime)
        Public Property RunTime As String
        Public Property Monday As Boolean
        Public Property Tuesday As Boolean
        Public Property Wednesday As Boolean
        Public Property Thursday As Boolean
        Public Property Friday As Boolean
        Public Property Saturday As Boolean
        Public Property Sunday As Boolean
        Public Property RunDll As String
        Public Property RunBatch As String
        Public Property RunParameters As String

        Public Shared Function RowToObject(r As DataRow) As Job

            Dim result As New Job
            result.ID = r.ValidRow(Of Integer)("ID", -1)
            result.SystemID = r.ValidRow(Of Integer)("SystemID", -1)
            result.RunOnce = r.ValidRow(Of Nullable(Of DateTime))("RunOnce", Nothing)
            result.RunTime = r.ValidRow(Of String)("RunTime")
            result.Monday = r.ValidRow(Of Boolean)("Monday", False)
            result.Tuesday = r.ValidRow(Of Boolean)("Tuesday", False)
            result.Wednesday = r.ValidRow(Of Boolean)("Wednesday", False)
            result.Thursday = r.ValidRow(Of Boolean)("Thursday", False)
            result.Friday = r.ValidRow(Of Boolean)("Friday", False)
            result.Saturday = r.ValidRow(Of Boolean)("Saturday", False)
            result.Sunday = r.ValidRow(Of Boolean)("Sunday", False)
            result.RunBatch = r.ValidRow(Of String)("RunBatch")
            result.RunDll = r.ValidRow(Of String)("RunDll")
            result.IsEnabled = r.ValidRow(Of Boolean)("IsEnabled", False)
            result.RunParameters = Nothing 'r.ValidRow(Of Byte())("RunParameters", Nothing) ' As Byte()
            Return result

        End Function

        Public Shared Function GetAll() As List(Of Model.Job)

            Dim result As List(Of Job) = Nothing

            ' get from database
            Dim dt As DataTable = DataFacade.GetDataTableSP(Connections.Dashboard, "p_GET_ActiveJobs", Nothing)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                result = New List(Of Job)
                For Each r As DataRow In dt.Rows
                    result.Add(RowToObject(r))
                Next

            End If

            'get jobs from disk
            Dim fileJobs As List(Of Job) = GetJobsFromDisk()

            result.AddRange(fileJobs)

            Return result

        End Function

        Public Function Run(timenow As DateTime, Optional force As Boolean = False) As JobResult Implements IJob.Run

            Dim result As New JobResult
            result.StartTime = DateTime.Now

            Try

                If (Not force) AndAlso (Not IsEnabled) Then
                    result.CodeExecuted = False
                    Return Nothing
                End If

                If force OrElse String.Format("{0:MM/dd/yy-hh:mm-tt}", RunOnce) = String.Format("{0:MM/dd/yy-hh:mm-tt}", timenow) Then

                    ' run this right now
                    RunTheJob(result)

                ElseIf Not String.IsNullOrWhiteSpace(RunTime) Then

                    ' check if time to run (24 hours format - no date comparison needed)
                    If RunTime = String.Format("{0:HH:mm}", timenow) Then

                        Dim runnow As Boolean
                        Select Case timenow.DayOfWeek
                            Case DayOfWeek.Monday
                                runnow = Monday
                            Case DayOfWeek.Tuesday
                                runnow = Tuesday
                            Case DayOfWeek.Wednesday
                                runnow = Wednesday
                            Case DayOfWeek.Thursday
                                runnow = Thursday
                            Case DayOfWeek.Friday
                                runnow = Friday
                            Case DayOfWeek.Saturday
                                runnow = Saturday
                            Case DayOfWeek.Sunday
                                runnow = Sunday
                            Case Else
                                runnow = False
                        End Select
                        If runnow Then
                            RunTheJob(result)
                        End If
                    End If

                End If

            Catch ex As Exception
                result.Error = ex
            Finally
                result.EndTime = DateTime.Now
            End Try

            Return result

        End Function

        Private Function RunTheJob(ByRef result As JobResult) As Boolean

            Try
                result.CodeExecuted = True

                'todo: execute code here *****************
                If Not String.IsNullOrWhiteSpace(RunBatch) Then

                    Dim jobFile As String = Path.Combine(ConfigurationManager.AppSettings("jobsfolder"), RunBatch)
                    If File.Exists(jobFile) Then

                        Dim proc = New Process()
                        proc.StartInfo.WorkingDirectory = ConfigurationManager.AppSettings("jobsfolder")
                        proc.StartInfo.FileName = RunBatch
                        proc.StartInfo.CreateNoWindow = True
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        proc.Start()
                        proc.WaitForExit()

                        Return True

                        'Dim psi As New ProcessStartInfo(jobFile)
                        'psi.RedirectStandardError = True
                        'psi.RedirectStandardOutput = True
                        'psi.CreateNoWindow = False
                        'psi.WindowStyle = ProcessWindowStyle.Hidden
                        'psi.UseShellExecute = False
                        'psi.WorkingDirectory = ConfigurationManager.AppSettings("jobsfolder")
                        'Dim process As Process = Process.Start(psi)
                    Else
                        Throw New Exception("The specified BATCH file doesn't exist!")
                    End If

                ElseIf String.IsNullOrWhiteSpace(RunDll) Then

                Else
                    Throw New Exception("Job to run not specified!")
                End If

                result.CodeFailed = False
            Catch ex As Exception
                result.Error = ex
                result.CodeFailed = True
            End Try

            Return False

        End Function

        Private Shared Function GetJobsFromDisk() As List(Of Job)

            Dim result As New List(Of Job)

            Dim jobsfolder As String = ConfigurationManager.AppSettings("jobsfolder")
            Dim di As New DirectoryInfo(jobsfolder)
            Dim files = di.GetFiles("*.hrm.bat", SearchOption.AllDirectories)

            For Each f As FileInfo In files
                result.Add(GetJobFromFile(f))
            Next

            Return result

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
                        j.RunParameters = value
                    End If

                End If

            Next

            If j IsNot Nothing Then
                j.RunBatch = f.Name
            End If

            Return j

        End Function

    End Class

End Namespace