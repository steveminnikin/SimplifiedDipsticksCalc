Imports System.ComponentModel.DataAnnotations
Imports SimplifiedDipsticksCalc.HorizFlatEndsService

Public Class HorizFlatEnds
    Inherits Tank

    <Required>
    <Display(Name:="Diameter")>
    Property FlatDiameter As Double
    <Required>
    <Display(Name:="Length")>
    Property FlatLength As Double

    Public convertedFlatEndsDimensions As IConvertedFLatEndsDimensions

    Public Sub New()
        Shape = "Horizontal Cylindrical with Flat Ends"
    End Sub


End Class
