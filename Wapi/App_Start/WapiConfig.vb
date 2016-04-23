Imports System.Net.Http.Headers
Imports System.Web.Http

Public NotInheritable Class WapiConfig
	Private Sub New()
	End Sub

	Public Shared Sub Register(config As HttpConfiguration)
		' Web API routes
		config.MapHttpAttributeRoutes()
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html") )
		config.Routes.MapHttpRoute(name := "DefaultApi", routeTemplate := "api/{controller}/{id}", defaults := New With { Key .id = RouteParameter.[Optional]})
	End Sub

End Class
