' ' |
' ' |============================================================================================================
' ' |  Project:           DAL
' ' |------------------------------------------------------------------------------------------------------------
' ' |  
' ' |  Last modified by:  Carlos I. Nunez (carlos@compexc.com)
' ' |
' ' |  On date:           4/26/2016
' ' |
' ' |------------------------------------------------------------------------------------------------------------
' ' |  (c) 2010-2016 Carlos I. Nunez, Miami - FL. All rights reserved.      
' ' |============================================================================================================
' ' |
Imports Newtonsoft.Json
Imports System.ComponentModel

Public Module Utils

    <Runtime.CompilerServices.Extension>
    Public Function ValidRow(Of T)(row As DataRow, columnName As String, Optional [default] As T = Nothing) As T
        Dim index As Integer = row.Table.Columns.IndexOf(columnName)
        If index < 0 OrElse TypeOf (row(index)) Is DBNull Then
            Return [default]
        Else
            Dim value As Object = row(index)
            Return If((TypeOf value Is T), DirectCast(value, T), DirectCast(Convert.ChangeType(value, GetType(T)), T))
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
