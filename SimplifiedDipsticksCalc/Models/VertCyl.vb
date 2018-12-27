Imports System.ComponentModel.DataAnnotations
Imports SimplifiedDipsticksCalc.VertCylService

Public Class VertCyl
    Inherits Tank
    Property Diameter As Double
    <Display(Name:="Height")>
    Property VertHeight As Double
    <Display(Name:="Dished End Depth")>
    Property DishEndDepth As Nullable(Of Double)

    Public convertedVertDimensions As IConvertedVertDimensions

    Public Sub New()
        Shape = "Vertical Cylindrical"

    End Sub

End Class