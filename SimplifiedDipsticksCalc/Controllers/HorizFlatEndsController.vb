Imports System.Web.Mvc

Namespace Controllers
    Public Class HorizFlatEndsController
        Inherits Controller

        Private _horizFlatEndsService As New HorizFlatEndsService
        Private _tankService As New TankService

        Sub New()

        End Sub

        Protected Sub New(horizFlatEndsService As HorizFlatEndsService, tankService As TankService)
            _horizFlatEndsService = horizFlatEndsService
            _tankService = tankService
        End Sub

        ' GET: HorizFlatEnds
        Function Input() As ActionResult

            Return View()
        End Function

        ' POST: /HorizCylDishEnds/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="FlatDiameter,FlatLength,Tilt,dipPoint,Increments,regDip, Dimensions, EngraveCode")> horizFlatEnds As HorizFlatEnds) As ActionResult

            horizFlatEnds.InitialConversionValues = _tankService.GetinitialConversionValues(horizFlatEnds)
            horizFlatEnds.convertedFlatEndsDimensions = _horizFlatEndsService.GetConvertedHorizFlatEndsDimensions(horizFlatEnds)
            horizFlatEnds.FullVol = _horizFlatEndsService.GetFullVol(horizFlatEnds)
            horizFlatEnds.IncrementList = _horizFlatEndsService.CalculateIncrements(horizFlatEnds)
            horizFlatEnds.Details = _horizFlatEndsService.getTankDetails(horizFlatEnds)

            ViewBag.fullVolume = Math.Round(horizFlatEnds.FullVol, 1)
            ViewBag.topHeight = IIf(horizFlatEnds.GetLength.Equals("Millimetres"), horizFlatEnds.FlatDiameter, Math.Round(horizFlatEnds.convertedFlatEndsDimensions.dia, 1))
            ViewBag.swc = Math.Round(horizFlatEnds.FullVol * 0.97, 0)
            If horizFlatEnds.EngraveCode Then
                _tankService.DownloadEngraveCode(horizFlatEnds)
            End If
            Return View(horizFlatEnds)
        End Function

    End Class
End Namespace