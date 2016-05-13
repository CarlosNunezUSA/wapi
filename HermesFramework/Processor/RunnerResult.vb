
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Processor.RunnerResult.vb
'	Created on:		5/12/2016 @ 10:42 PM
'	Modified on:	5/12/2016 @ 11:05 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Processor
    Public Class RunnerResult
        Public Property [RunStart] As DateTime

        Public Property RunEnd As DateTime

        Public Property RanSuccessfully As Boolean

        Public Property [Error] As Exception

        Public Sub New()
            RunStart = DateTime.Now
            RanSuccessfully = False
        End Sub
    End Class
End Namespace
