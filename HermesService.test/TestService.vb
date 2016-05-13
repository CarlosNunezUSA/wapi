Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestService

    <TestMethod()>
    Public Sub TestOnTimerTickEvent()

        Try
            Dim hm As New Hermes
            hm.OnTimerTickEvent(Nothing, Nothing)
        Catch ex As Exception
            Assert.Fail("An exception was generated: " & ex.Message)
        End Try

    End Sub

End Class