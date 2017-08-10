Imports System.ComponentModel.DataAnnotations
Imports SimplifiedDipsticksCalc.RectangularService

Public Class Rectangular
    Inherits Tank
    <Required>
    Property Length As Double
    <Required>
    Property Width As Double
    <Required>
    Property Height As Double
    <Display(Name:="Hopper Volume")>
    Property HopperVolume As Double?
    <Display(Name:="Dip Height Below Base")>
    Property DipHeightBelowBase As Double?
    Property ConvertedRectDimensions As IConvertedRectangularDimensions

    Public Sub New()
        Shape = "Rectangular"
    End Sub

End Class
