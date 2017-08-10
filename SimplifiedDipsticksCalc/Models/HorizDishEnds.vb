Imports System.ComponentModel.DataAnnotations
Imports SimplifiedDipsticksCalc.HorizDishEndsService

Public Class HorizDishEnds

    Inherits Tank

        <Required>
        <Display(Name:="Diameter")>
        Property DishDiameter As Double
        <Required>
    <Display(Name:="Straight Length")>
    Property StLength As Double
    <Display(Name:="Overall Length")>
        Property OvLength As Nullable(Of Double)
        <Display(Name:="Dished End Radius")>
        Property DishEndRad As Nullable(Of Double)
    <Display(Name:="Knuckle Radius")>
    Property KnuckleRad As Nullable(Of Double)
    Property ConvertedHorizDishEndsDimensions As IConvertedHorizDishEndsDimensions

    Public Sub New()
        Shape = "Horizontal Cylindrical with Dished Ends"
    End Sub







End Class

