
Namespace Processor
    Public Class Scheduler
        Public Shared Function GetScheduledJobs(timenow As DateTime, jobs As List(Of Job)) As List(Of Job)

            Dim result As New List(Of Job)

            For Each j As Job In jobs

                If ( Not j.IsEnabled ) Then
                    Continue For
                End If

                If ( j.RunAlways ) OrElse String.Format("{0:MM/dd/yy-hh:mm-tt}", j.RunOnce) = String.Format("{0:MM/dd/yy-hh:mm-tt}", timenow) Then

                    ' run this right now
                    result.Add(j)

                ElseIf Not String.IsNullOrWhiteSpace(j.RunTime) Then

                    ' check if time to run (24 hours format - no date comparison needed)
                    Dim finalTime = "00:00"
                    Try
                        ' validating time
                        Dim aTime As String() = j.RunTime.Split(":")
                        Dim h As Integer = Integer.Parse(aTime(0))
                        If h < 0 OrElse h > 24 Then
                            Throw New Exception("The hour value in Runtime parameter should be from 0..24")
                        End If
                        Dim m As Integer = Integer.Parse(aTime(1))
                        If m < 0 OrElse m > 59 Then
                            Throw New Exception("The minute value in Runtime parameter should be from 0..59")
                        End If
                        finalTime = String.Format("{0:00}:{1:00}", h, m)
                    Catch ex As Exception
                        Throw New Exception("Invalid Runtime parameter value. Job.RunTime should be in format hh:mm. " & ex.Message)
                    End Try
                    If finalTime = String.Format("{0:HH:mm}", timenow) Then

                        Dim runnow As Boolean
                        Select Case timenow.DayOfWeek
                            Case DayOfWeek.Monday
                                runnow = j.Monday
                            Case DayOfWeek.Tuesday
                                runnow = j.Tuesday
                            Case DayOfWeek.Wednesday
                                runnow = j.Wednesday
                            Case DayOfWeek.Thursday
                                runnow = j.Thursday
                            Case DayOfWeek.Friday
                                runnow = j.Friday
                            Case DayOfWeek.Saturday
                                runnow = j.Saturday
                            Case DayOfWeek.Sunday
                                runnow = j.Sunday
                            Case Else
                                Continue For
                        End Select

                        If runnow Then result.Add(j)

                    End If

                End If

            Next

            Return result
        End Function
    End Class
End Namespace