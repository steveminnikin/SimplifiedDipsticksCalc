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
        Function Input() As ActionResult

            Return View()
        End Function

        ' POST: /Elliptical/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="MajorDiameter,MinorDiameter,ElliptLength,Increments,regDip, Dimensions,EngraveCode")> elliptical As Elliptical) As ActionResult

            elliptical.InitialConversionValues = _tankService.GetinitialConversionValues(elliptical)
            elliptical.convertedEllipticalDimensions = _ellipticalService.GetConvertedEllipticalDimensions(elliptical)
            elliptical.FullVol = _ellipticalService.GetFullVol(elliptical)
            elliptical.IncrementList = _ellipticalService.CalculateIncrements(elliptical)

            ViewBag.fullVolume = Math.Round(elliptical.FullVol, 1)
            ViewBag.topHeight = IIf(elliptical.GetLength.Equals("Millimetres"), elliptical.MinorDiameter, Math.Round(elliptical.convertedEllipticalDimensions.minDia, 1))
            ViewBag.swc = Math.Round(elliptical.FullVol * 0.97, 0)
            If elliptical.EngraveCode Then
                _tankService.DownloadEngraveCode(elliptical)
            End If
            Return View(elliptical)
        End Function

    End Class
End Namespace