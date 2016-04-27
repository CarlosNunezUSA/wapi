Imports DAL
Imports DAL.Utils
Imports System.ComponentModel

Namespace Model

    Public Class Job
        Implements IJob

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
        Public Property RepeatMin As Integer
        Public Property RunDll As String
        Public Property IsEnabled As Boolean
        Public Property RunParameters As Byte()

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
            result.RepeatMin = r.ValidRow(Of Integer)("RepeatMin", -1)
            result.RunDll = r.ValidRow(Of String)("RunDll")
            result.IsEnabled = r.ValidRow(Of Boolean)("IsEnabled", False) ' As Boolean
            result.RunParameters = r.ValidRow(Of Byte())("RunParameters", Nothing) ' As Byte()
            Return result

        End Function

        Public Shared Function GetAll() As List(Of Model.Job)

            Dim result As List(Of Job) = Nothing

            Dim dt As DataTable = DataFacade.GetDataTableSP(Connections.Dashboard, "p_GET_ActiveJobs", Nothing)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                result = New List(Of Job)
                For Each r As DataRow In dt.Rows
                    result.Add(RowToObject(r))
                Next

            End If

            Return result

        End Function

        Public Function Run(params As Object) As JobResult Implements IJob.Run

            If Not IsEnabled Then
                Return Nothing
            End If

            Dim result As New JobResult
            result.StartTime = DateTime.Now

            Try
                Dim timenow As DateTime = DateTime.Now

                If RunOnce = timenow Then
                    ' run this right now
                    Throw New NotImplementedException
                ElseIf Not String.IsNullOrWhiteSpace(RunTime) Then
                    ' check the time
                    Dim aTime As String() = RunTime.Split(":")
                    Dim hour As Integer = Integer.Parse(aTime(0))
                    Dim minute As String = Integer.Parse(aTime(1))
                    If hour = timenow.Hour AndAlso minute = timenow.Minute Then
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
                            Try
                                result.CodeExecuted = True
                                RunTheJob(params)
                                result.CodeFailed = False
                            Catch ex As Exception
                                result.Error = ex
                                result.CodeFailed = True
                            End Try
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

        Private Sub RunTheJob(params As Object)
            Throw New NotImplementedException
        End Sub


    End Class

End Namespace