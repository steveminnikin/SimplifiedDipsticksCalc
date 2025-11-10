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
        '   GET /HorizDishEnds/Index

        Function Index() As ActionResult
            Return View()
        End Function

        '
        ' POST: /HorizDishEnds/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="DishDiameter,OvLength,StLength,DishEndRad,KnuckleRad,Tilt,dipPoint,Increments,regDip, Dimensions,EngraveCode")> horizDishEnds As HorizDishEnds) As ActionResult

            ' Server-side validation
            If horizDishEnds.DishDiameter <= 0 OrElse horizDishEnds.OvLength <= 0 OrElse horizDishEnds.StLength <= 0 OrElse horizDishEnds.DishEndRad <= 0 Then
                ModelState.AddModelError("", "Diameter, Overall Length, Straight Length, and Dished End Radius must be positive numbers")
                Return View("Index", horizDishEnds)
            End If

            If horizDishEnds.Increments <= 0 Then
                ModelState.AddModelError("", "Increments must be a positive number")
                Return View("Index", horizDishEnds)
            End If

            If ModelState.IsValid Then
                horizDishEnds.InitialConversionValues = _tankService.GetinitialConversionValues(horizDishEnds)
                horizDishEnds.convertedHorizDishEndsDimensions = _horizDishEndsService.GetConvertedHorizFlatEndsDimensions(horizDishEnds)
                horizDishEnds.IncrementList = _horizDishEndsService.CalculateIncrements(horizDishEnds)
                horizDishEnds.Details = _horizDishEndsService.getTankDetails(horizDishEnds)

                ViewData("fullVolume") = Math.Round(horizDishEnds.FullVol, 1)
                ViewData("topHeight") = If(horizDishEnds.GetLength.Equals("Millimetres"), horizDishEnds.DishDiameter, Math.Round(horizDishEnds.convertedHorizDishEndsDimensions.dia, 1))
                ViewData("swc") = Math.Round(horizDishEnds.FullVol * 0.97, 0)
                If horizDishEnds.EngraveCode Then
                    _tankService.DownloadEngraveCode(horizDishEnds)
                End If
                TempData("SuccessMessage") = "Calculation completed successfully!"
            End If
            Return View(horizDishEnds)

        End Function

    End Class
End Namespace