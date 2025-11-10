Imports System.Web.Mvc

Namespace Controllers
    Public Class RectangularController
        Inherits Controller

        Private _rectangularService As New RectangularService
        Private _tankService As New TankService

        Sub New()

        End Sub

        Protected Sub New(rectangularService As RectangularService, tankService As TankService)
            _rectangularService = rectangularService
            _tankService = tankService
        End Sub

        Function Index() As ActionResult
            Return View()
        End Function

        Function GetGCode() As ActionResult

            Return View()
        End Function

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="Length,Width,Height,Slope,PointofDip,Increments,regDip, Dimensions,EngraveCode, Adjustments, hopperVolume,dipHeightBelowBase ")> rectangular As Rectangular) As ActionResult

            ' Server-side validation
            If rectangular.Length <= 0 OrElse rectangular.Width <= 0 OrElse rectangular.Height <= 0 Then
                ModelState.AddModelError("", "Length, Width, and Height must be positive numbers")
                Return View("Index", rectangular)
            End If

            If rectangular.Increments <= 0 Then
                ModelState.AddModelError("", "Increments must be a positive number")
                Return View("Index", rectangular)
            End If

            rectangular.InitialConversionValues = _tankService.GetinitialConversionValues(rectangular)
            rectangular.ConvertedRectDimensions = _rectangularService.GetConvertedRectDimensions(rectangular)
            rectangular.IncrementList = _rectangularService.CalculateIncrements(rectangular)
            rectangular.Details = _rectangularService.getTankDetails(rectangular)

            ViewData("fullVolume") = Math.Round(rectangular.FullVol, 1)
            ViewData("topHeight") = If(rectangular.GetLength.Equals("Millimetres"), rectangular.Height, Math.Round(rectangular.ConvertedRectDimensions.height, 1))
            ViewData("swc") = Math.Round(rectangular.FullVol * 0.97, 0)

            If rectangular.EngraveCode Then
                _tankService.DownloadEngraveCode(rectangular)
            End If

            Return View(rectangular)
        End Function

    End Class
End Namespace