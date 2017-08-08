Imports System.Math
Public Class VertCylService
    Inherits TankService

    Private convertedVertDimensions As IConvertedVertDimensions

    Function CalculateIncrements(vertCyl As VertCyl) As Dictionary(Of Double, Double)

        Dim incrementList As New Dictionary(Of Double, Double)
        Dim area As Double = PI * (0.5 * convertedVertDimensions.dia) ^ 2
        Dim iv, h As Double

        vertCyl.FullVol = (area * convertedVertDimensions.ht / vertCyl.InitialConversionValues.cor) - vertCyl.Adjustments

        If vertCyl.regDip Then
            For h = 0 To convertedVertDimensions.ht Step convertedVertDimensions.inc
                iv = area * (h / vertCyl.InitialConversionValues.cor) - vertCyl.Adjustments
                If Not vertCyl.Adjustments.Equals(Nothing) And iv < CalculateDishedEndVolume(vertCyl) Then
                    iv = 0
                End If
                incrementList.Add(vertCyl.FinalConversionRounding(h), Round(iv))
            Next
        Else
            For iv = convertedVertDimensions.inc To vertCyl.FullVol Step convertedVertDimensions.inc
                h = iv * vertCyl.InitialConversionValues.cor / area
                incrementList.Add(Round(iv), vertCyl.FinalConversionRounding(h))
            Next
            incrementList.Add(Round(vertCyl.FullVol), vertCyl.FinalConversionRounding(convertedVertDimensions.ht))
        End If

        Return incrementList

    End Function


    Function CalculateDishedEndVolume(vertCyl As VertCyl) As Double

        Dim dishedEndVolume As Double
        Dim area As Double = PI * (0.5 * convertedVertDimensions.dia) ^ 2

        dishedEndVolume = (area * convertedVertDimensions.dish / vertCyl.InitialConversionValues.cor) - vertCyl.Adjustments

        Return dishedEndVolume

    End Function

    Function GetConvertedVertDimensions(vertCyl As VertCyl) As IConvertedVertDimensions
        convertedVertDimensions.dia = vertCyl.Diameter * vertCyl.InitialConversionValues.m
        convertedVertDimensions.ht = vertCyl.VertHeight * vertCyl.InitialConversionValues.m
        convertedVertDimensions.inc = vertCyl.Increments * vertCyl.InitialConversionValues.incAdj
        convertedVertDimensions.dish = IIf(vertCyl.DishEndDepth.Equals(Nothing), 0.0, vertCyl.DishEndDepth * vertCyl.InitialConversionValues.m)

        Return convertedVertDimensions

    End Function

    Structure IConvertedVertDimensions
        Public dia As Double
        Public ht As Double
        Public inc As Double
        Public dish As Nullable(Of Double)
    End Structure
End Class

