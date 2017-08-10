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
        '
        ' POST: /VertCyl/Calculate

        <AcceptVerbs(HttpVerbs.Post)>
        Function Calculate(<Bind(Include:="Diameter,DishEndDepth,VertHeight,Increments,regDip, Dimensions,EngraveCode,Adjustments,IncrementList")> vertCyl As VertCyl) As ActionResult

            _vertCyl = vertCyl

            _vertCyl.InitialConversionValues = _tankService.GetinitialConversionValues(vertCyl)
            _vertCyl.convertedVertDimensions = _vertCylService.GetConvertedVertDimensions(vertCyl)

            If Not _vertCyl.DishEndDepth.Equals(Nothing) Then
                _vertCylService.CalculateDishedEndVolume(vertCyl)
            End If
            _vertCyl.IncrementList = _vertCylService.CalculateIncrements(vertCyl)

            ViewBag.fullVolume = Math.Round(_vertCyl.FullVol, 1)
            ViewBag.topHeight = IIf(_vertCyl.GetLength.Equals("Millimetres"), _vertCyl.VertHeight, Math.Round(_vertCyl.convertedVertDimensions.ht, 1))
            ViewBag.swc = Math.Round(_vertCyl.FullVol * 0.97, 0)
            ViewBag.Title = "Vertical Cylindrical Calculation"

            If vertCyl.EngraveCode Then
                _tankService.DownloadEngraveCode(vertCyl)
            End If

            Return View(_vertCyl)
        End Function
    End Class
End Namespace