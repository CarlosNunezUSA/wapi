Imports HermesFramework.Processor

<TestClass()> Public Class TestFramework

    <TestMethod()>
    Public Sub TestSchedulerCanGetJobs()

        Dim timenow As DateTime
        Dim result As List(Of Job)

        ' check the job is returned as scheduled when is set to RunAlways
        Dim test As New List(Of Job)
        Dim j As DllJob
        j = New DllJob
        j.Id = 1
        j.RunAlways = True
        j.IsEnabled = True
        test.Add(j)

        ' check the job  is returned when the run time/day are set
        j = New DllJob
        j.Id = 2
        j.Friday = True
        j.RunTime = "08:00"
        j.IsEnabled = True
        test.Add(j)

        ' check the job doesn't run if its disabled
        j = New DllJob
        j.Id = 3
        j.Friday = True
        j.RunTime = "08:00"
        j.IsEnabled = False
        test.Add(j)

        ' check the job is returned on runonce when date/time are set
        j = New DllJob
        j.Id = 4
        j.RunOnce = New Date(2016, 1, 1, 8, 0, 0)
        j.IsEnabled = True
        test.Add(j)

        ' this date is a Friday at 8:00am
        timenow = New Date(2016, 1, 1, 8, 0, 0)

        result = Scheduler.GetScheduledJobs(timenow, test)

        Assert.IsTrue(result.Count > 0, "the schedule should return 1+ jobs")
        Assert.IsTrue((result.Find(Function(x) x.Id = 1)) IsNot Nothing, "the schedule should return the job #1")
        Assert.IsTrue((result.Find(Function(x) x.Id = 2)) IsNot Nothing, "the schedule should return the job #2")
        Assert.IsTrue((result.Find(Function(x) x.Id = 3)) Is Nothing, "the schedule should not return the job #3. It is disabled")
        Assert.IsTrue((result.Find(Function(x) x.Id = 4)) IsNot Nothing, "the schedule should return the job #4")

    End Sub

    <TestMethod()>
    Public Sub TestRunnerGetAllJobs()

        Dim r As New Runner
        r.Connection = Connections.Dashboard
        r.ScheduleFolder = "C:\TEMP\Hermes\schedule"

        Dim result As List(Of Job) = r.GetAllJobs()

        'Assert.IsTrue(result.Count > 0, "the schedule should return 1+ jobs")
        'Assert.IsTrue((result.Find(Function(x) x.Id=1)) isnot nothing, "the schedule should return the job #1")
        'Assert.IsTrue((result.Find(Function(x) x.Id=2)) isnot nothing, "the schedule should return the job #2")
        'Assert.IsTrue((result.Find(Function(x) x.Id=3)) is nothing, "the schedule should not return the job #3. It is disabled")
        'Assert.IsTrue((result.Find(Function(x) x.Id=4)) isnot nothing, "the schedule should return the job #4")

    End Sub


End Class