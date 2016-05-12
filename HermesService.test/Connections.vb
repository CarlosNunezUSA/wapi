Imports HermesFramework.Data

Public Class Connections

    <Conditional("DEBUG")>
    Public Sub GetEnvironment()

    End Sub


    Public Shared Function Dashboard() As DatabaseFactory

        Dim cn As New System.Configuration.ConnectionStringSettings
        cn.Name = "Dashboard"
        #If Work
            cn.ConnectionString = "Server=TSRCDTIT0074225;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
        #else
            cn.ConnectionString = "Server=VM\SQL2008;Database=Dashboard;Integrated Security=true;Trusted_Connection=True"
        #end if
        cn.ProviderName = "System.Data.SqlClient"
        Dim db As New DatabaseFactory(cn)
        Return db

    End Function

End Class
