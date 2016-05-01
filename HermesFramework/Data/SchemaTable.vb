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
Namespace Data

    Public Class SchemaTable

        Public Property TableName As String
        Public Property Fields As List(Of SchemaField)

        Public Sub New()
            Me.TableName = "<No Name Set>"
            Me.Fields = New List(Of SchemaField)
        End Sub

    End Class

End Namespace