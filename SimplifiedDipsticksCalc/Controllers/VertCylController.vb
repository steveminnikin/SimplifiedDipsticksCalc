Imports System.Web.Mvc


Namespace Controllers
    Public Class VertCylController
        Inherits Controller
        '   GET /VertCyl/Input

        Private _vertCylService As New VertCylService
        Private _tankService As New TankService
        Public _vertCyl As VertCyl

        Sub New()

        End Sub

        Protected Sub New(vertCylService As VertCylService, tankService As TankService)
            _vertCylService = vertCylService
            _tankService = tankService
        End Sub

        Function Index() As ActionResult
            Return View()
        End Function
        '
        ' POST: /VertCyl/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="Diameter,DishEndDepth,VertHeight,Increments,regDip, Dimensions,EngraveCode,Adjustments,IncrementList")> vertCyl As VertCyl) As ActionResult

            Try
                ' Server-side validation
                If vertCyl.Diameter <= 0 OrElse vertCyl.VertHeight <= 0 Then
                    ModelState.AddModelError("", "Diameter and Height must be positive numbers")
                    Return View("Index", vertCyl)
                End If

                If vertCyl.Increments <= 0 Then
                    ModelState.AddModelError("", "Increments must be a positive number")
                    Return View("Index", vertCyl)
                End If

                _vertCyl = vertCyl

                _vertCyl.InitialConversionValues = _tankService.GetinitialConversionValues(vertCyl)
                _vertCyl.ConvertedVertDimensions = _vertCylService.GetConvertedVertDimensions(vertCyl)

                If _vertCyl.DishEndDepth.HasValue Then
                    _vertCylService.CalculateDishedEndVolume(vertCyl)
                End If
                _vertCyl.IncrementList = _vertCylService.CalculateIncrements(vertCyl)
                _vertCyl.Details = _vertCylService.getTankDetails(vertCyl)

                ViewData("fullVolume") = Math.Round(_vertCyl.FullVol, 1)
                ViewData("topHeight") = If(_vertCyl.GetLength.Equals("Millimetres"), _vertCyl.VertHeight, Math.Round(_vertCyl.ConvertedVertDimensions.ht, 1))
                ViewData("swc") = Math.Round(_vertCyl.FullVol * 0.97, 0)
                ViewData("Title") = "Vertical Cylindrical Calculation"

                If vertCyl.EngraveCode Then
                    _tankService.DownloadEngraveCode(vertCyl)
                End If

                TempData("SuccessMessage") = "Calculation completed successfully!"
                Return View(_vertCyl)

            Catch ex As InvalidOperationException
                ModelState.AddModelError("", "Calculation error: " & ex.Message)
                Return View("Index", vertCyl)
            Catch ex As DivideByZeroException
                ModelState.AddModelError("", "Invalid dimensions resulted in a division by zero. Please check your input values.")
                Return View("Index", vertCyl)
            Catch ex As OverflowException
                ModelState.AddModelError("", "The calculated values are too large. Please check your dimensions and increments.")
                Return View("Index", vertCyl)
            Catch ex As Exception
                ModelState.AddModelError("", "An unexpected error occurred during calculation. Please verify your input values and try again.")
                Return View("Index", vertCyl)
            End Try
        End Function
    End Class
End Namespace