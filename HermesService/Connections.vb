
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesService / %Namespace%.Connections.vb
'	Created on:		5/12/2016 @ 11:16 PM
'	Modified on:	5/12/2016 @ 11:16 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Imports System.Configuration
Imports HermesFramework.Data


Public Class Connections
    Public Shared Function Dashboard() As DatabaseFactory

        Dim cn As New ConnectionStringSettings
        cn.Name = "Dashboard"
#If Work Then
            cn.ConnectionString = "Server=TSRCDTIT0074225;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
#Else
        cn.ConnectionString = "Server=VM\SQL2008;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
#End If
        cn.ProviderName = "System.Data.SqlClient"
        Dim db As New DatabaseFactory(cn)
        Return db
    End Function
End Class
