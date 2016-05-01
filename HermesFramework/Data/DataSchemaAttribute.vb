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
Imports System

Namespace Data

    <AttributeUsage(AttributeTargets.Property, Inherited:=True, AllowMultiple:=False)>
    Public Class DataSchemaAttribute
        Inherits Attribute

        Public Overridable Property DbName As String
        Public Overridable Property DbType As String
        Public Overridable Property DbSize As Integer
        Public Overridable Property IsInsertable As Boolean
        Public Overridable Property IsUpdatable As Boolean

        Public Sub New(dbname As String, dbtype As String, Optional dbsize As Integer = -1, Optional isinsertable As Boolean = True, Optional isupdatable As Boolean = True)
            Me.DbName = dbname
            Me.DbType = dbtype
            Me.DbSize = dbsize
            Me.IsInsertable = isinsertable
            Me.IsUpdatable = isupdatable
        End Sub

    End Class

End Namespace