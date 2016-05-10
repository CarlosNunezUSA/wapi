
Namespace Processor

    Public Class Scheduler

        Public Shared Function GetScheduledJobs(timenow As DateTime, jobs As List(Of Job)) As List(Of Job)

            Dim result As New List(Of Job)

            For Each j As Job In jobs

                If (Not j.IsEnabled) Then
                    Continue For
                End If

                If (j.RunAlways) OrElse String.Format("{0:MM/dd/yy-hh:mm-tt}", j.RunOnce) = String.Format("{0:MM/dd/yy-hh:mm-tt}", timenow) Then

                    ' run this right now
                    result.Add(j)

                ElseIf Not String.IsNullOrWhiteSpace(j.RunTime) Then

                    ' check if time to run (24 hours format - no date comparison needed)
                    If j.RunTime = String.Format("{0:HH:mm}", timenow) Then

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