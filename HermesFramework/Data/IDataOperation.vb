
'========================================================================================
' Copyright (c) 2016 Carlos I. Nunez. All rights reserved.
'========================================================================================
'
'	Project File:	HermesFramework / Data.IDataOperation.vb
'	Created on:		5/12/2016 @ 10:45 PM
'	Modified on:	5/12/2016 @ 11:06 PM 
'	Author:			Carlos Nunez 
' 
'========================================================================================

Namespace Data
    Public Interface IDataOperation(Of T)

        Function GetObject(r As DataRow) As T

        Function Insert(o As T) As T

        Function Update(o As T) As Integer

        Function Delete(o As T) As Integer

        Function FindAll() As List(Of T)

        Function FindByID(id As Integer) As T
    End Interface
End Namespace