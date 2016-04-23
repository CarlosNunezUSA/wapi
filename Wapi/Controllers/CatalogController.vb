Imports System.Web.Http
Imports Wapi.Models

Namespace Controllers
    Public Class CatalogController
        Inherits ApiController

        Private _catalogs As Catalog() = New Catalog() { New Catalog() With{.ID=1 , .Description= "Some", .CreatedOn = DateTime.now}, _
                                                        New Catalog() With{.ID=2 , .Description= "Some 2", .CreatedOn = DateTime.now}                                                
        }

        <Route("api/catalogs")>
        Public Function GetAll() As IEnumerable(Of Catalog)
			Return _catalogs
		End Function

        <Route("api/catalogs/{id:int}")>
		Public Function GetById(id As Integer) As IHttpActionResult
			Dim product = _catalogs.FirstOrDefault(Function(p) p.Id = id)
			If product Is Nothing Then
				Return NotFound()
			End If
			Return Ok(product)
		End Function

    End Class

End Namespace