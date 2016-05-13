
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Processor.DllJob.vb
'	Created on:		5/12/2016 @ 10:40 PM
'	Modified on:	5/12/2016 @ 11:04 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Imports System.IO
Imports System.Reflection
Imports HermesFramework.Data


Namespace Processor
    Public Class DllJob
        Inherits Job

        Public Property SystemId As Integer

        Public Property DllName As String

        Public Property ClassName As String

        Public Function GetAll(connection As DatabaseFactory) As List(Of Job)

            Dim result As New List(Of Job)

            ' get from database
            Dim dt As DataTable = DataFacade.GetDataTableSP(connection, "p_GET_ActiveJobs", Nothing)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each r As DataRow In dt.Rows
                    result.Add(RowToObject(r))
                Next

            End If

            Return result
        End Function

        Public Overrides Function Run(timenow As DateTime, workingFolder As String) As RunnerResult

            Dim result As New RunnerResult

            Try
                '
                '  Load assembly from file path. Specify that we will be using 
                '
                Dim asm As Assembly = Assembly.LoadFrom(Path.Combine(workingFolder, DllName))
                Dim asmtype As Type = asm.GetType(ClassName)

                '
                ' We will be invoking a method - TaskRun
                '
                Dim methodInfo = asmtype.GetMethod("RunTask", New Type() {GetType(Integer), GetType(String)})
                If methodInfo Is Nothing Then
                    ' never throw generic Exception - replace this with some other exception type
                    Throw New Exception("The method RunTask does not exist. Please implement the HermesFramework.Processor.ITaskLibrary Interface.")
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

        Private Function RowToObject(r As DataRow) As DllJob

            Dim result As New DllJob
            result.Id = r.Parse (Of Integer)("ID", - 1)
            result.SystemId = r.Parse (Of Integer)("SystemID", - 1)
            result.RunOnce = r.Parse (Of Nullable(Of DateTime))("RunOnce", Nothing)
            result.RunTime = r.Parse (Of String)("RunTime")
            result.Monday = r.Parse (Of Boolean)("Monday", False)
            result.Tuesday = r.Parse (Of Boolean)("Tuesday", False)
            result.Wednesday = r.Parse (Of Boolean)("Wednesday", False)
            result.Thursday = r.Parse (Of Boolean)("Thursday", False)
            result.Friday = r.Parse (Of Boolean)("Friday", False)
            result.Saturday = r.Parse (Of Boolean)("Saturday", False)
            result.Sunday = r.Parse (Of Boolean)("Sunday", False)
            result.DllName = r.Parse (Of String)("DllName")
            result.ClassName = r.Parse (Of String)("ClassName")
            result.IsEnabled = r.Parse (Of Boolean)("IsEnabled", False)
            result.RunParameters = r.Parse (Of String)("RunParameters")
            Return result
        End Function
    End Class
End Namespace