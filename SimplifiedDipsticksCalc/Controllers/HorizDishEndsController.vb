Imports System.Web.Mvc

Namespace Controllers

    Public Class HorizDishEndsController
        Inherits System.Web.Mvc.Controller

        Private _horizDishEndsService As New HorizDishEndsService
        Private _tankService As New TankService

        Sub New()

        End Sub

        Protected Sub New(horizDishEndsService As HorizDishEndsService, tankService As TankService)
            _horizDishEndsService = horizDishEndsService
            _tankService = tankService
        End Sub

        '
        '   GET /HorizDishEnds/Calculate

        Function Calculate() As ActionResult

            Return View()
        End Function

        '
        ' POST: /HorizDishEnds/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="DishDiameter,OvLength,StLength,DishEndRad,KnuckleRad,Tilt,dipPoint,Increments,regDip, Dimensions,EngraveCode")> horizDishEnds As HorizDishEnds) As ActionResult
            horizDishEnds.IncrementList.Clear()

            horizDishEnds.InitialConversionValues = _tankService.GetinitialConversionValues(horizDishEnds)
            horizDishEnds.convertedHorizDishEndsDimensions = _horizDishEndsService.GetConvertedHorizFlatEndsDimensions(horizDishEnds)
            horizDishEnds.IncrementList = _horizDishEndsService.CalculateIncrements(horizDishEnds)

            ViewBag.fullVolume = Math.Round(horizDishEnds.FullVol, 1)
            ViewBag.topHeight = IIf(horizDishEnds.GetLength.Equals("Millimetres"), horizDishEnds.DishDiameter, Math.Round(horizDishEnds.convertedHorizDishEndsDimensions.dia, 1))
            ViewBag.swc = Math.Round(horizDishEnds.FullVol * 0.97, 0)
            If horizDishEnds.EngraveCode Then
                _tankService.DownloadEngraveCode(horizDishEnds)
            End If
            Return View(horizDishEnds)
        End Function

    End Class
End Namespace