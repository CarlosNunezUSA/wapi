
Imports HermesFramework.Data

Namespace Processor
    Public Class DllJob
        Inherits Job

        Public Property SystemId As Integer
        Public Property DllName As String
        Public Property ClassName As String



        Private Function RowToObject(r As DataRow) As DllJob

            Dim result As New DllJob
            result.ID = r.Parse(Of Integer)("ID", -1)
            result.SystemId = r.Parse(Of Integer)("SystemID", -1)
            result.RunOnce = r.Parse(Of Nullable(Of DateTime))("RunOnce", Nothing)
            result.RunTime = r.Parse(Of String)("RunTime")
            result.Monday = r.Parse(Of Boolean)("Monday", False)
            result.Tuesday = r.Parse(Of Boolean)("Tuesday", False)
            result.Wednesday = r.Parse(Of Boolean)("Wednesday", False)
            result.Thursday = r.Parse(Of Boolean)("Thursday", False)
            result.Friday = r.Parse(Of Boolean)("Friday", False)
            result.Saturday = r.Parse(Of Boolean)("Saturday", False)
            result.Sunday = r.Parse(Of Boolean)("Sunday", False)
            result.DllName = r.Parse(Of String)("DllName")
            result.ClassName = r.Parse(Of String)("ClassName")
            result.IsEnabled = r.Parse(Of Boolean)("IsEnabled", False)
            result.RunParameters = r.Parse(Of String)("RunParameters")
            Return result

        End Function




        Private Function RunTheJob(j As Job) As RunnerResult(Of T)

            'Dim runner As New Runner

            'Try
            '    Dim jobsfolder As String = ConfigurationManager.AppSettings("jobsfolder")

            '    If Not String.IsNullOrWhiteSpace(RunBatch) Then

            '        Dim res As AssemblyResult(Of Boolean) = runner.RunBatch(jobsfolder, RunBatch, Nothing)


            '    ElseIf Not String.IsNullOrWhiteSpace(RunDll) Then

            '        Dim res As AssemblyResult(Of Boolean) = runner.RunDll(Of Boolean)(jobsfolder, RunDll, Nothing)

            '    Else
            '        Throw New Exception("Job to run not specified!")
            '    End If

            '    result.CodeFailed = False

            'Catch ex As Exception
            '    result.Error = ex
            '    result.CodeFailed = True
            'End Try

            'Return False

        End Function

        Private Function RunDll(Of T)(job As Job) As RunnerResult(Of T)

            Dim result As New AssemblyResult(Of T)
            result.DllName = dllPATH
            result.ClassName = ClassName
            Try
                '
                '  Load assembly from file path. Specify that we will be using 
                '
                Dim asm As Assembly = Assembly.LoadFrom(dllpath)
                Dim asmtype As Type = asm.GetType(ClassName)

                '
                ' We will be invoking a method - TaskRun
                '
                Dim methodInfo = asmtype.GetMethod("TaskRun", New Type() {GetType(Integer), GetType(String)})
                If methodInfo Is Nothing Then
                    ' never throw generic Exception - replace this with some other exception type
                    Throw New Exception("No such method exists.")
                End If

                '
                ' Define parameters for class constructor 
                '
                Dim constructorParameters As Object() = New Object(1) {}
                constructorParameters(0) = 999
                ' First parameter.
                constructorParameters(1) = 2
                ' Second parameter.
                '
                ' Create instance of MyClass.
                '
                Dim o = Activator.CreateInstance(asmtype, constructorParameters)

                '
                ' Specify parameters for the method we will be invoking: 'int MyMethod(int count, string text)'
                '
                Dim parameters As Object() = New Object(1) {}
                parameters(0) = 124
                ' 'count' parameter
                parameters(1) = "Some text."
                ' 'text' parameter
                '
                ' Invoke method 'int MyMethod(int count, string text)'
                '
                Dim r = methodInfo.Invoke(o, parameters)

                result.RanSuccessfully = True

            Catch ex As Exception
                result.Error = ex
                result.RanSuccessfully = False
            Finally
                result.RunEnd = DateTime.Now
            End Try

            Return result

        End Function


        Public Overrides Function Run(timenow As DateTime) As RunnerResult

            'Dim result As New RunnerResult

            'result.StartTime = DateTime.Now

            'Try

            '    If (Not force) AndAlso (Not IsEnabled) Then
            '        result.CodeExecuted = False
            '        Return Nothing
            '    End If

            '    If force OrElse String.Format("{0:MM/dd/yy-hh:mm-tt}", RunOnce) = String.Format("{0:MM/dd/yy-hh:mm-tt}", timenow) Then

            '        ' run this right now
            '        RunTheJob(result)

            '    ElseIf Not String.IsNullOrWhiteSpace(RunTime) Then

            '        ' check if time to run (24 hours format - no date comparison needed)
            '        If RunTime = String.Format("{0:HH:mm}", timenow) Then

            '            Dim runnow As Boolean
            '            Select Case timenow.DayOfWeek
            '                Case DayOfWeek.Monday
            '                    runnow = Monday
            '                Case DayOfWeek.Tuesday
            '                    runnow = Tuesday
            '                Case DayOfWeek.Wednesday
            '                    runnow = Wednesday
            '                Case DayOfWeek.Thursday
            '                    runnow = Thursday
            '                Case DayOfWeek.Friday
            '                    runnow = Friday
            '                Case DayOfWeek.Saturday
            '                    runnow = Saturday
            '                Case DayOfWeek.Sunday
            '                    runnow = Sunday
            '                Case Else
            '                    runnow = False
            '            End Select
            '            If runnow Then
            '                RunTheJob(result)
            '            End If
            '        End If

            '    End If

            'Catch ex As Exception
            '    result.Error = ex
            'Finally
            '    result.EndTime = DateTime.Now
            'End Try

            'Return result

        End Function

    End Class

End NameSpace