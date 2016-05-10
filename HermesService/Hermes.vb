
Imports System.Configuration
Imports HermesFramework.Processor


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
            Dim rn As New Runner
            rn.Connection = Connections.Dashboard()
            rn.ScheduleFolder = configurationmanager.AppSettings("ScheduleFolder")
            rn.RunScheduledTasks(timenow)
        Catch ex As Exception
            Throw
        End Try

    End Sub


    Protected Overrides Sub OnStop()

        Me._maintimer.Stop()
        Me._maintimer.Enabled = False

    End Sub


End Class




