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
        Function Index() As ActionResult
            Return View()
        End Function

        Function Input() As ActionResult

            Return View()
        End Function

        ' POST: /HorizCylDishEnds/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="FlatDiameter,FlatLength,Tilt,dipPoint,Increments,regDip, Dimensions, EngraveCode")> horizFlatEnds As HorizFlatEnds) As ActionResult

            Try
                ' Server-side validation
                If horizFlatEnds.FlatDiameter <= 0 OrElse horizFlatEnds.FlatLength <= 0 Then
                    ModelState.AddModelError("", "Diameter and Length must be positive numbers")
                    Return View("Index", horizFlatEnds)
                End If

                If horizFlatEnds.Increments <= 0 Then
                    ModelState.AddModelError("", "Increments must be a positive number")
                    Return View("Index", horizFlatEnds)
                End If

                horizFlatEnds.InitialConversionValues = _tankService.GetinitialConversionValues(horizFlatEnds)
                horizFlatEnds.ConvertedFlatEndsDimensions = _horizFlatEndsService.GetConvertedHorizFlatEndsDimensions(horizFlatEnds)
                horizFlatEnds.FullVol = _horizFlatEndsService.GetFullVol(horizFlatEnds)
                horizFlatEnds.IncrementList = _horizFlatEndsService.CalculateIncrements(horizFlatEnds)
                horizFlatEnds.Details = _horizFlatEndsService.getTankDetails(horizFlatEnds)

                ViewData("fullVolume") = Math.Round(horizFlatEnds.FullVol, 1)
                ViewData("topHeight") = If(horizFlatEnds.GetLength.Equals("Millimetres"), horizFlatEnds.FlatDiameter, Math.Round(horizFlatEnds.ConvertedFlatEndsDimensions.dia, 1))
                ViewData("swc") = Math.Round(horizFlatEnds.FullVol * 0.97, 0)
                If horizFlatEnds.EngraveCode Then
                    _tankService.DownloadEngraveCode(horizFlatEnds)
                End If
                TempData("SuccessMessage") = "Calculation completed successfully!"
                Return View(horizFlatEnds)

            Catch ex As InvalidOperationException
                ModelState.AddModelError("", "Calculation error: " & ex.Message)
                Return View("Index", horizFlatEnds)
            Catch ex As DivideByZeroException
                ModelState.AddModelError("", "Invalid dimensions resulted in a division by zero. Please check your input values.")
                Return View("Index", horizFlatEnds)
            Catch ex As OverflowException
                ModelState.AddModelError("", "The calculated values are too large. Please check your dimensions and increments.")
                Return View("Index", horizFlatEnds)
            Catch ex As Exception
                ModelState.AddModelError("", "An unexpected error occurred during calculation. Please verify your input values and try again.")
                Return View("Index", horizFlatEnds)
            End Try
        End Function

    End Class
End Namespace