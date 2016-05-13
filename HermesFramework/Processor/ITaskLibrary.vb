
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Processor.ITaskLibrary.vb
'	Created on:		5/12/2016 @ 10:25 PM
'	Modified on:	5/12/2016 @ 11:04 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Processor
    Public Interface ITaskLibrary
        Function RunTask(params As Object) As RunnerResult
    End Interface
End Namespace