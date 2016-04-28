Imports HermesService.Model


Public Class Hermes


    Private Const RUN_INTERVAL As Integer = 60000
    Private _maintimer As System.Timers.Timer


    '
    ' Clock Started
    '
    Protected Overrides Sub OnStart(ByVal args() As String)

        Try
            _maintimer = New System.Timers.Timer(RUN_INTERVAL)
            AddHandler _maintimer.Elapsed, AddressOf Me.OnTimerTickEvent
            _maintimer.Enabled = True
            _maintimer.Start()
        Catch ex As Exception
            Throw
        End Try

    End Sub


    '
    ' Clock tick event
    '
    Public Sub OnTimerTickEvent(source As Object, e As System.Timers.ElapsedEventArgs)

        Dim timenow As DateTime = DateTime.Now

        Try

            Dim jobs As List(Of Model.Job) = Job.GetAll()

            If jobs.Count > 0 Then
                Parallel.ForEach(Of Job)(jobs, Sub(j)
                                                   If j IsNot Nothing AndAlso j.IsEnabled Then
                                                       Dim result As JobResult = j.Run(j.RunParameters, timenow)
                                                   End If
                                               End Sub)
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Function ShouldRun(CompareToDate As DateTime, ScheduledDate As DateTime) As Boolean

        Try

            If IsNothing(ScheduledDate) Then
                ' no time set / run job
                Return True
            Else
                If ScheduledDate.CompareTo(CompareToDate) >= 1 Then
                    ' time set / future
                    Return False
                Else
                    ' time set / past 
                    Return True
                End If
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function


   Protected Overrides Sub OnStop()

        Me._maintimer.Stop()
        Me._maintimer.Enabled = False

    End Sub


End Class




