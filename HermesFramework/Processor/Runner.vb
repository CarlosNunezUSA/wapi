
Imports System.Reflection

Namespace Processor

    Public Class Runner

        Public Function RunDll(Of T)(dllpath As String, classname As String, params As T) As AssemblyResult

            Dim result As New AssemblyResult
            result.DllName = dllpath
            result.ClassName = classname
            Try
                '
                '  Load assembly from file path. Specify that we will be using 
                '
                Dim asm As Assembly = Assembly.LoadFrom(dllpath)
                Dim asmtype As Type = asm.GetType(classname)

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

                result.CodeFailed = False
                result.CodeExecuted = True

            Catch ex As Exception
                result.Error = ex
                result.CodeFailed = True
            End Try

            Return result

        End Function

    End Class

End Namespace