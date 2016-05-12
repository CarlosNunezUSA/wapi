
Imports System.Configuration
Imports System.IO
Imports System.Reflection
Imports HermesFramework.Data


Namespace Processor

    Public Class Runner

        Public Property Connection As Nullable(Of DatabaseFactory)
        Public Property ScheduleFolder As String
        Public Property WorkingFolder As String

        Public Sub RunScheduledTasks(timenow As Date)

            ' get all jobs to run 
            ' determine which ones to run
            ' run all in parallel

            Dim alljobs As List(Of Job) = GetAllJobs()

            Dim scheduledjobs As List(Of Job) = Scheduler.GetScheduledJobs(timenow, alljobs)

            If scheduledjobs.Count > 0 Then

                Parallel.ForEach(Of Job)(scheduledjobs, Sub(scheduledjob)
                                                            Try
                                                                Dim result As RunnerResult = scheduledjob.Run(timenow)
                                                            Catch ex As Exception
                                                                'todo: log the issue
                                                            End Try
                                                        End Sub)
            End If

        End Sub

        Public Function GetAllJobs() As List(Of Job)

            Dim dlljobs As New List(Of Job)
            Dim batjobs As New List(Of Job)

            Try
                If Connection.HasValue AndAlso Not String.IsNullOrWhiteSpace(Connection.Value.ConnectionString) Then
                    dlljobs = New DllJob().GetAll(Connection)
                End If
            Catch ex As Exception
                'todo: log this error
            End Try

            Try
                If Not String.IsNullOrWhiteSpace(ScheduleFolder) Then
                    batjobs = New BatJob().GetAll(ScheduleFolder)
                End If
            Catch ex As Exception
                'todo: log this error
            End Try

            dlljobs.AddRange(batjobs)

            Return dlljobs

        End Function

    End Class

End Namespace