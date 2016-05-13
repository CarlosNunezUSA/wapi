
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.SchemaField.vb
'	Created on:		5/12/2016 @ 10:45 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Data
    Public Class SchemaField
        Public Property ColumnName As String

        Public Property ColumnOrdinal As Integer

        Public Property ColumnSize As String

        Public Property NumericPrecision As Integer

        Public Property NumericScale As Integer

        Public Property IsUnique As Boolean

        Public Property BaseColumnName As String

        Public Property BaseTableName As String

        Public Property DataType As String

        Public Property AllowDBNull As Boolean

        Public Property ProviderType As String

        Public Property IsIdentity As Boolean

        Public Property IsAutoIncrement As Boolean

        Public Property IsRowVersion As Boolean

        Public Property IsLong As Boolean

        Public Property IsReadOnly As Boolean

        Public Property ProviderSpecificDataType As String

        Public Property DataTypeName As String

        Public Property UdtAssemblyQualifiedName As String

        Public Property NewVersionedProviderType As Integer

        Public Property IsColumnSet As String

        Public Property RawProperties As String

        Public Property NonVersionedProviderType As String

        Public Function IsDefinedSize() As Boolean
            Select Case Me.DataTypeName.ToUpper
                Case "BIT", "BINARY", "VARBINARY", "IMAGE", "DATETIME", "SMALLDATETIME", "FLOAT", "REAL", "INT", "BIGINT", "SMALLINT", "TINYINT", "MONEY", "SMALLMONEY", "DECIMAL", "NUMERIC", "UNIQUEIDENTIFIER", "TIMESTAMP", "TEXT", "NTEXT"
                    Return True
                Case Else
                    Return False
            End Select
        End Function
    End Class
End Namespace