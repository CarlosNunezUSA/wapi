
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Utils.Serializer.vb
'	Created on:		5/13/2016 @ 4:51 PM
'	Modified on:	5/13/2016 @ 4:52 PM 
'	Author:			Nunez, Carlos 
' 
'========================================================================================

Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Namespace Utils
    Public Class FileSerializer

        Public Property FileName As String

        Public Sub SerializeToFile(of T)(o As T)
            Try
                If File.Exists(filename) Then
                    File.Delete(filename)
                End If
                ' serialize
                Dim serialized As Byte() = BinarySerializer.Serialize(o)
                ' to string
                Dim o64 As String = compressor.Zip(Convert.ToBase64String(serialized))
                ' to file
                File.AppendAllText(filename, o64)
            Catch
                Throw
            End Try
        End Sub

        Public Function RestoreSerializedObject(of T) As T
            Dim result As New List(Of String)
            ' from file
            Dim ostring As String = File.ReadAllText(filename)
            Dim serialized As Byte() = Convert.FromBase64String(compressor.UnZip(ostring))
            Return BinarySerializer.Deserialize(Of T)(serialized)
        End Function

    End Class


    Public Class BinarySerializer

        Shared Function Serialize(ByVal data As Object) As Byte()
            Try
                If TypeOf data Is Byte() Then Return data
                Using M As New IO.MemoryStream : Dim F As New BinaryFormatter : F.Serialize(M, data) : Return M.ToArray() : End Using
            Catch ex As Exception
                Throw
            End Try
        End Function

        Shared Function Deserialize(Of T)(ByVal data As Byte()) As T
            Try
                Using M As New IO.MemoryStream(data, False) : Return CType((New BinaryFormatter).Deserialize(M), T) : End Using
            Catch ex As Exception
                Throw
            End Try
        End Function

    End Class



End Namespace