Imports System.Web.Mvc

Namespace Controllers
    Public Class VertCylController
        Inherits Controller
        '   GET /VertCyl/Input

        Private _vertCylService As New VertCylService
        Private _tankService As New TankService

        Sub New()

        End Sub

        Protected Sub New(vertCylService As VertCylService, tankService As TankService)
            _vertCylService = vertCylService
            _tankService = tankService
        End Sub

        '   GET /VertCyl/Calculate

        Function Calculate() As ActionResult

            Return View()
        End Function

        '
        ' POST: /VertCyl/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="Diameter,DishEndDepth,VertHeight,Increments,regDip, Dimensions,EngraveCode,Adjustments")> vertCyl As VertCyl) As ActionResult
            vertCyl.IncrementList.Clear()

            vertCyl.InitialConversionValues = _tankService.GetinitialConversionValues(vertCyl)
            vertCyl.convertedVertDimensions = _vertCylService.GetConvertedVertDimensions(vertCyl)

            If Not vertCyl.DishEndDepth.Equals(Nothing) Then
                _vertCylService.CalculateDishedEndVolume(vertCyl)
            End If
            vertCyl.IncrementList = _vertCylService.CalculateIncrements(vertCyl)

            ViewBag.fullVolume = Math.Round(vertCyl.FullVol, 1)
            ViewBag.topHeight = IIf(vertCyl.GetLength.Equals("Millimetres"), vertCyl.VertHeight, Math.Round(vertCyl.convertedVertDimensions.ht, 1))
            ViewBag.swc = Math.Round(vertCyl.FullVol * 0.97, 0)

            If vertCyl.EngraveCode Then
                _tankService.DownloadEngraveCode(vertCyl)
            End If

            Return View(vertCyl)
        End Function
    End Class
End Namespace