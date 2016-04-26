' ' |
' ' |============================================================================================================
' ' |  Project:           DAL
' ' |------------------------------------------------------------------------------------------------------------
' ' |  
' ' |  Last modified by:  Carlos I. Nunez (carlos@compexc.com)
' ' |
' ' |  On date:           9/27/2014
' ' |
' ' |------------------------------------------------------------------------------------------------------------
' ' |  (c) 2014 Carlos I. Nunez, Miami - FL. All rights reserved.      
' ' |============================================================================================================
' ' |
Public Structure DatabaseFactory

    Public Property Factory As System.Data.Common.DbProviderFactory
    Public Property ConnectionString As String

    Public Sub New(settings As System.Configuration.ConnectionStringSettings)
        Try

            Me.ConnectionString = settings.ConnectionString
            Me.Factory = System.Data.Common.DbProviderFactories.GetFactory(settings.ProviderName)

        Catch ex As Exception
            Throw
        End Try

    End Sub

End Structure
