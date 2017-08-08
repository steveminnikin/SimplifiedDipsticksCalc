Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        ViewData("Title") = "Dipsticks Calculation Program"

        Return View()
    End Function
End Class
