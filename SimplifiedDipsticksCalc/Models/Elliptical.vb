Imports System.ComponentModel.DataAnnotations
Imports SimplifiedDipsticksCalc.EllipticalService

Public Class Elliptical
    Inherits Tank

    <Required>
    <Display(Name:=" MajorDiameter")>
    Property MajorDiameter As Double
    <Required>
    <Display(Name:=" MinorDiameter")>
    Property MinorDiameter As Double
    <Required>
    <Display(Name:="Length")>
    Property ElliptLength As Double

    Public convertedEllipticalDimensions As IConvertedEllipticalDimensions

    Public Sub New()
        Shape = "Elliptical"
    End Sub


End Class
