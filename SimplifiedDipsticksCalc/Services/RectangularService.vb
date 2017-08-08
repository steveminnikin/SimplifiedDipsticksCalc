Imports System.Math

Public Class RectangularService
    Inherits TankService

    Private convertedRectDimensions As IConvertedRectangularDimensions

    Function CalculateAreaOfTank(rectangular As Rectangular) As Double
        Dim area As Double
        area = convertedRectDimensions.width * convertedRectDimensions.length
        Return area
    End Function

    Function CalculateIncrements(rectangular As Rectangular) As Dictionary(Of Double, Double)
        Dim incrementList As New Dictionary(Of Double, Double)
        Dim iv, h, vol, area As Double

        area = CalculateAreaOfTank(rectangular)

        If rectangular.regDip Then
            For h = 0 To convertedRectDimensions.height Step rectangular.convertedRectDimensions.inc
                iv = area * h / rectangular.InitialConversionValues.cor
                incrementList.Add(rectangular.FinalConversionRounding(h), Round(iv))
            Next
        Else
            For iv = rectangular.convertedRectDimensions.inc To CalculateFullVolume(rectangular) Step rectangular.convertedRectDimensions.inc
                h = iv * rectangular.InitialConversionValues.cor / area
                vol = IIf((iv < 1), iv, Round(iv, 1)) + rectangular.Adjustments
                incrementList.Add(vol, rectangular.FinalConversionRounding(h))
            Next
            ''Add final fullvolume figures to increment list
            incrementList.Add(CalculateFullVolume(rectangular), rectangular.FinalConversionRounding(convertedRectDimensions.height))
        End If
        Return incrementList
    End Function

    Function CalculateFullVolume(rectangular As Rectangular) As Double
        Dim fullvol, area As Double

        area = CalculateAreaOfTank(rectangular)
        fullvol = Round(area * convertedRectDimensions.height / rectangular.InitialConversionValues.cor)
        rectangular.FullVol = fullvol

        Return fullvol
    End Function

    Function GetConvertedRectDimensions(rectangular As Rectangular) As IConvertedRectangularDimensions

        convertedRectDimensions.length = rectangular.Length * rectangular.InitialConversionValues.m
        convertedRectDimensions.width = rectangular.Width * rectangular.InitialConversionValues.m
        convertedRectDimensions.height = rectangular.Height * rectangular.InitialConversionValues.m
        convertedRectDimensions.inc = rectangular.Increments * rectangular.InitialConversionValues.incAdj
        convertedRectDimensions.tilt = IIf(rectangular.Tilt.Equals(Nothing), 0.0, rectangular.Tilt * rectangular.InitialConversionValues.m)
        convertedRectDimensions.dip = IIf(rectangular.dipPoint.Equals(Nothing), 0.0, rectangular.dipPoint * rectangular.InitialConversionValues.m)

        Return convertedRectDimensions

    End Function

    Structure IConvertedRectangularDimensions
        Public length As Double
        Public width As Double
        Public height As Double
        Public inc As Double
        Public tilt As Nullable(Of Double)
        Public dip As Nullable(Of Double)
    End Structure

End Class
