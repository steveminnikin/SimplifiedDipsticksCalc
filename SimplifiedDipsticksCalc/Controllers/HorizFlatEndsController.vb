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
        Function Calculate(<Bind(Include:="FlatDiameter,FlatLength,Tilt,dipPoint,Increments,regDip, Dimensions, EngraveCode")> HorizFlatEnds As HorizFlatEnds) As ActionResult
            HorizFlatEnds.IncrementList.Clear()

            HorizFlatEnds.InitialConversionValues = _tankService.GetinitialConversionValues(HorizFlatEnds)
            HorizFlatEnds.convertedFlatEndsDimensions = _horizFlatEndsService.GetConvertedHorizFlatEndsDimensions(HorizFlatEnds)
            HorizFlatEnds.IncrementList = _horizFlatEndsService.CalculateIncrements(HorizFlatEnds)

            ViewBag.fullVolume = Math.Round(HorizFlatEnds.FullVol, 1)
            ViewBag.topHeight = IIf(HorizFlatEnds.GetLength.Equals("Millimetres"), HorizFlatEnds.FlatDiameter, Math.Round(HorizFlatEnds.convertedFlatEndsDimensions.dia, 1))
            ViewBag.swc = Math.Round(HorizFlatEnds.FullVol * 0.97, 0)
            If HorizFlatEnds.EngraveCode Then
                _tankService.DownloadEngraveCode(HorizFlatEnds)
            End If
            Return View(HorizFlatEnds)
        End Function

    End Class
End Namespace