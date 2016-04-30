Imports System.Text
Imports HermesService.Model
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestJob

    <TestMethod()> Public Sub TestJobRunOnce()

        Dim result As JobResult

        Dim timenow As DateTime = DateTime.Now

        Dim j As New Job
        j.ID = 1
        j.RunOnce = timenow
        j.IsEnabled = True

        result = j.Run(timenow)

        Assert.IsTrue(result.CodeExecuted = True, "Result should return that the Code was executed")

    End Sub

    <TestMethod()> Public Sub TestJobRunAtTimeAndDay()

        Dim result As JobResult

        Dim timenow As DateTime = New DateTime(2016, 4, 10, 14, 20, 4)

        Dim j As New Job
        j.ID = 1
        j.RunOnce = Nothing
        j.IsEnabled = True
        j.Monday = True
        j.Tuesday = True
        j.Wednesday = True
        j.Thursday = True
        j.Friday = True
        j.Saturday = True
        j.Sunday = True

        j.RunTime = "14:20"

        result = j.Run(timenow)

        Assert.IsTrue(result.CodeExecuted = True, "Result should return that the Code was executed")

    End Sub

    <TestMethod()> Public Sub TestJobRunForced()

        Dim result As JobResult

        Dim timenow As DateTime = New DateTime(2016, 4, 10, 14, 20, 4)

        Dim j As New Job
        j.ID = 1
        j.RunOnce = Nothing
        j.IsEnabled = False

        result = j.Run(timenow, True)

        Assert.IsTrue(result.CodeExecuted = True, "Result should return that the Code was executed")

    End Sub

    <Ignore>
    <TestMethod()> Public Sub TestJobGetAll()

        Dim j = Job.GetAll()
        Assert.IsTrue(j IsNot Nothing, "Result should return that the Code was executed")

    End Sub

    <TestMethod()> Public Sub TestRunTheJob()

        Dim jobs As List(Of Model.Job) = Job.GetAll()
        Dim j As Job = jobs(1)
        Dim timenow As DateTime = DateTime.Now
        j.Run(timenow, True)

        Assert.IsTrue(j IsNot Nothing, "Result should return that the Code was executed")

    End Sub


End Class