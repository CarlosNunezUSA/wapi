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
Public Interface IDataOperation(Of T)

    Function GetObject(r As DataRow) As T

    Function Insert(o As T) As T

    Function Update(o As T) As Integer

    Function Delete(o As T) As Integer

    Function FindAll() As List(Of T)

    Function FindByID(id As Integer) As T

End Interface