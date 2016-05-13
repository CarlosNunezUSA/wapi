
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Processor.IJob.vb
'	Created on:		5/12/2016 @ 10:24 PM
'	Modified on:	5/12/2016 @ 11:04 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Processor
    Public Interface IJob
        Function Run(timenow As DateTime, workingFolder As String) As RunnerResult
    End Interface
End Namespace