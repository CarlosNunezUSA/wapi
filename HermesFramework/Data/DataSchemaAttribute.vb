
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.DataSchemaAttribute.vb
'	Created on:		5/12/2016 @ 10:45 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

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