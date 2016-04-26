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

Public Class DbSettings

    Public Shared Function DASH As System.Configuration.ConnectionStringSettings

        Dim cn As New System.Configuration.ConnectionStringSettings
        cn.Name = "DASH"
        cn.ConnectionString = "Server=TSRCDTIT0074225;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
        cn.ProviderName = "System.Data.SqlClient"
        Return cn

    End Function

End Class
