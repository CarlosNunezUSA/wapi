
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.DataError.vb
'	Created on:		5/12/2016 @ 10:45 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Data
    Public Class DataError
        Public Property Messages As List(Of String)

        Public Property ExceptionData As Exception

        Public Sub New()
            Me.Messages = New List(Of String)
            Me.ExceptionData = New NotImplementedException
        End Sub
    End Class
End Namespace