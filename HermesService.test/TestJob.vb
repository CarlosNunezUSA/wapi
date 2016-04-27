Imports System.Text
Imports HermesService.Model
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestJob

    <TestMethod()> Public Sub TestJobRun()

        Dim result As JobResult

        Dim params As Object = Nothing
        Dim j As New Job
        j.ID = 1
        j.RunOnce = DateTime.Now
        j.IsEnabled = False
        result = j.Run(params)

        Assert.IsTrue(result Is Nothing, "Result should return nothing when is not enabled")

        j.IsEnabled = True

        result = j.Run(params)

        Assert.IsTrue(result.CodeExecuted = True, "Code should have executed")





    End Sub

End Class