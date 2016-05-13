
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Processor.Runner.vb
'	Created on:		5/12/2016 @ 10:42 PM
'	Modified on:	5/12/2016 @ 11:05 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

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
            Try

                Dim alljobs As List(Of Job) = GetAllJobs()

                Dim scheduledjobs As List(Of Job) = Scheduler.GetScheduledJobs(timenow, alljobs)

                If scheduledjobs.Count > 0 Then

                    Parallel.ForEach(Of Job)(scheduledjobs, Sub(scheduledjob)
                                                                Try
                                                                    Dim result As RunnerResult = scheduledjob.Run(timenow, WorkingFolder)
                                                                Catch ex As Exception
                                                                    'todo: log the issue
                                                                End Try
                                                            End Sub)
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
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
                'Throw
            End Try

            Try
                If Not String.IsNullOrWhiteSpace(ScheduleFolder) Then
                    batjobs = New BatJob().GetAll(ScheduleFolder)
                End If
            Catch ex As Exception
                'todo: log this error
                'Throw
            End Try

            dlljobs.AddRange(batjobs)

            Return dlljobs
        End Function
    End Class
End Namespace