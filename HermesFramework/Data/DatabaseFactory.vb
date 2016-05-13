
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.DatabaseFactory.vb
'	Created on:		5/12/2016 @ 10:43 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Imports System.Configuration
Imports System.Data.Common


Namespace Data
    Public Structure DatabaseFactory
        Public Property Factory As DbProviderFactory

        Public Property ConnectionString As String

        Public Sub New(settings As ConnectionStringSettings)
            Try

                Me.ConnectionString = settings.ConnectionString
                Me.Factory = DbProviderFactories.GetFactory(settings.ProviderName)

            Catch ex As Exception
                Throw
            End Try
        End Sub
    End Structure
End Namespace