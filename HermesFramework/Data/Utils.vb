
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.Utils.vb
'	Created on:		5/12/2016 @ 10:45 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json


Namespace Data
    Public Module Utils
        <Extension>
        Public Function Parse(Of T)(row As DataRow, columnName As String, Optional [default] As T = Nothing) As T
            Dim index As Integer = row.Table.Columns.IndexOf(columnName)
            If index < 0 OrElse TypeOf (row(index)) Is DBNull Then
                Return [default]
            Else
                Dim value As Object = row(index)
                Return _
                    If((TypeOf value Is T), DirectCast(value, T), DirectCast(Convert.ChangeType(value, GetType(T)), T))
            End If
        End Function

        Public Function toJSON(o As Object) As String
            Return JsonConvert.SerializeObject(o)
        End Function

        Public Function RemovePrefix(prefix As String, text As String)
            If text.StartsWith(prefix) Then
                Return text.Replace(prefix, "")
            Else
                Return text
            End If
        End Function
    End Module
End Namespace