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

            Try
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
                elliptical.ConvertedEllipticalDimensions = _ellipticalService.GetConvertedEllipticalDimensions(elliptical)
                elliptical.FullVol = _ellipticalService.GetFullVol(elliptical)
                elliptical.IncrementList = _ellipticalService.CalculateIncrements(elliptical)

                ViewData("fullVolume") = Math.Round(elliptical.FullVol, 1)
                ViewData("topHeight") = If(elliptical.GetLength.Equals("Millimetres"), elliptical.MinorDiameter, Math.Round(elliptical.ConvertedEllipticalDimensions.minDia, 1))
                ViewData("swc") = Math.Round(elliptical.FullVol * 0.97, 0)
                If elliptical.EngraveCode Then
                    _tankService.DownloadEngraveCode(elliptical)
                End If
                TempData("SuccessMessage") = "Calculation completed successfully!"
                Return View(elliptical)

            Catch ex As InvalidOperationException
                ModelState.AddModelError("", "Calculation error: " & ex.Message)
                Return View("Index", elliptical)
            Catch ex As DivideByZeroException
                ModelState.AddModelError("", "Invalid dimensions resulted in a division by zero. Please check your input values.")
                Return View("Index", elliptical)
            Catch ex As OverflowException
                ModelState.AddModelError("", "The calculated values are too large. Please check your dimensions and increments.")
                Return View("Index", elliptical)
            Catch ex As Exception
                ModelState.AddModelError("", "An unexpected error occurred during calculation. Please verify your input values and try again.")
                Return View("Index", elliptical)
            End Try
        End Function

    End Class
End Namespace