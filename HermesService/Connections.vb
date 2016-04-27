' // |
' // |============================================================================================================
' // |  Project:           Globals / DatabaseSettings.vb  
' // |------------------------------------------------------------------------------------------------------------
' // |
' // |  Last modified by:  Carlos I. Nunez (carlos.nunez@adp.com)
' // |
' // |  On date:           4/14/2016
' // |
' // |------------------------------------------------------------------------------------------------------------
' // |  (c) 2015 ADP TotalSource, Miami - FL
' // |============================================================================================================
' // |  
Imports System.Configuration
Imports DAL

Public Class Connections

    Public Shared Function Dashboard() As DatabaseFactory

        Dim cn As New System.Configuration.ConnectionStringSettings
        cn.Name = "Dashboard"
        ' cn.ConnectionString = "Server=TSRCDTIT0074225;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
        cn.ConnectionString = "Server=VM\SQL2008;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
        cn.ProviderName = "System.Data.SqlClient"
        Dim db As New DatabaseFactory(cn)
        Return db

    End Function

End Class
