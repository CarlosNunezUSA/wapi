
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.SchemaTable.vb
'	Created on:		5/12/2016 @ 10:45 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Data
    Public Class SchemaTable
        Public Property TableName As String

        Public Property Fields As List(Of SchemaField)

        Public Sub New()
            Me.TableName = "<No Name Set>"
            Me.Fields = New List(Of SchemaField)
        End Sub
    End Class
End Namespace