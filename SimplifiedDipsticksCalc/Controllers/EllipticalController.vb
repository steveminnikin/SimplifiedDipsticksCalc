Imports System.Web.Mvc

Namespace Controllers
    Public Class EllipticalController
        Inherits Controller

        Private _ellipticalService As New EllipticalService
        Private _tankService As New TankService

        Sub New()

        End Sub

        Protected Sub New(ellipticalService As EllipticalService, tankService As TankService)
            _ellipticalService = ellipticalService
            _tankService = tankService
        End Sub

        ' GET: Elliptical
        Function Index() As ActionResult
            Return View()
        End Function

        Function Input() As ActionResult

            Return View()
        End Function

        ' POST: /Elliptical/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="MajorDiameter,MinorDiameter,ElliptLength,Increments,regDip, Dimensions,EngraveCode")> elliptical As Elliptical) As ActionResult

            ' Server-side validation
            If elliptical.MajorDiameter <= 0 OrElse elliptical.MinorDiameter <= 0 OrElse elliptical.ElliptLength <= 0 Then
                ModelState.AddModelError("", "Major Axis, Minor Axis, and Length must be positive numbers")
                Return View("Index", elliptical)
            End If

            If elliptical.MinorDiameter > elliptical.MajorDiameter Then
                ModelState.AddModelError("", "Minor Axis must be less than or equal to Major Axis")
                Return View("Index", elliptical)
            End If

            If elliptical.Increments <= 0 Then
                ModelState.AddModelError("", "Increments must be a positive number")
                Return View("Index", elliptical)
            End If

            elliptical.InitialConversionValues = _tankService.GetinitialConversionValues(elliptical)
            elliptical.convertedEllipticalDimensions = _ellipticalService.GetConvertedEllipticalDimensions(elliptical)
            elliptical.FullVol = _ellipticalService.GetFullVol(elliptical)
            elliptical.IncrementList = _ellipticalService.CalculateIncrements(elliptical)

            ViewData("fullVolume") = Math.Round(elliptical.FullVol, 1)
            ViewData("topHeight") = If(elliptical.GetLength.Equals("Millimetres"), elliptical.MinorDiameter, Math.Round(elliptical.convertedEllipticalDimensions.minDia, 1))
            ViewData("swc") = Math.Round(elliptical.FullVol * 0.97, 0)
            If elliptical.EngraveCode Then
                _tankService.DownloadEngraveCode(elliptical)
            End If
            Return View(elliptical)
        End Function

    End Class
End Namespace