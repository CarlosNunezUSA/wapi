
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Processor.Job.vb
'	Created on:		5/12/2016 @ 10:40 PM
'	Modified on:	5/12/2016 @ 11:04 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Processor
    Public MustInherit Class Job
        Implements IJob

#Region "Properties"

        Public Property Id As Integer

        Public Property IsEnabled As Boolean

        Public Property RunOnce As Nullable(Of DateTime)

        Public Property RunTime As String

        Public Property Monday As Boolean

        Public Property Tuesday As Boolean

        Public Property Wednesday As Boolean

        Public Property Thursday As Boolean

        Public Property Friday As Boolean

        Public Property Saturday As Boolean

        Public Property Sunday As Boolean

        Public Property RunParameters As String

        Public Property RunAlways As Boolean

        Public Property FileName As String

#End Region

        Public MustOverride Function Run(timenow As DateTime, workingFolder As String) As RunnerResult Implements IJob.Run
    End Class
End Namespace