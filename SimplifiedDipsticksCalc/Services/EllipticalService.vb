Imports System.Math

Public Class EllipticalService
    Inherits TankService

    Private convertedEllipticalDimensions As IConvertedEllipticalDimensions
    Private r1 As Double

    Function GetFullVol(elliptical As Elliptical) As Double

        r1 = convertedEllipticalDimensions.majDia / convertedEllipticalDimensions.minDia
        elliptical.FullVol = r1 * PI * Pow((convertedEllipticalDimensions.minDia / 2), 2) * convertedEllipticalDimensions.len / elliptical.InitialConversionValues.cor
        Return elliptical.FullVol
    End Function

    Public Function CalculateIncrements(elliptical As Elliptical) As Dictionary(Of Double, Double)

        Dim incrementList As New Dictionary(Of Double, Double)
        Dim T9, TN, X, area, AN, AT, IV, R, H, FV As Single

        T9 = 0.1
        R = convertedEllipticalDimensions.minDia / 2
        FV = elliptical.FullVol / r1

        If elliptical.regDip Then
            For H = 0.00001 To R Step convertedEllipticalDimensions.inc
                AN = 2 * (FnA(1 - H / R))
                AT = 0.5 * R ^ 2 * Sin(AN)
                area = AN * R ^ 2 / 2 - AT
                IV = r1 * area * convertedEllipticalDimensions.len / elliptical.InitialConversionValues.cor
                incrementList.Add(elliptical.FinalConversionRounding(H), Round(IV))
                If H > R Then Exit For
            Next H
            For H = H To convertedEllipticalDimensions.minDia Step convertedEllipticalDimensions.inc
                AN = 2 * (FnA(2 * H / convertedEllipticalDimensions.minDia - 1))
                AT = 0.5 * R ^ 2 * Sin(AN)
                area = AN * R ^ 2 / 2 - AT
                IV = r1 * (PI * R ^ 2 - area) * convertedEllipticalDimensions.len / elliptical.InitialConversionValues.cor
                incrementList.Add(elliptical.FinalConversionRounding(H), Round(IV))
            Next H
        Else
            'Workshop Format
            convertedEllipticalDimensions.inc /= r1
            For TIV = convertedEllipticalDimensions.inc To FV Step convertedEllipticalDimensions.inc
X:              X = T9 - Sin(T9) - (2 * elliptical.InitialConversionValues.cor * TIV / (R ^ 2 * convertedEllipticalDimensions.len))
                X = X / (1 - Cos(T9))
                TN = T9 - X
                If Abs(TN) > 150000.0! Then
                    TIV += 0.001
                    T9 = 0.1
                    GoTo X
                End If
                If Abs(TN - T9) >= 0.00001 Then
                    T9 = TN
                    GoTo X
                End If
                H = R - R * Cos(TN / 2)
                IV = TIV * r1
                incrementList.Add(Round(IV), elliptical.FinalConversionRounding(H))
            Next TIV
            incrementList.Add(Round(elliptical.FullVol), elliptical.FinalConversionRounding(convertedEllipticalDimensions.minDia))
        End If
        Return incrementList
    End Function

    Function GetConvertedEllipticalDimensions(Elliptical As Elliptical) As IConvertedEllipticalDimensions

        convertedEllipticalDimensions.majDia = Elliptical.MajorDiameter * Elliptical.InitialConversionValues.m
        convertedEllipticalDimensions.minDia = Elliptical.MinorDiameter * Elliptical.InitialConversionValues.m
        convertedEllipticalDimensions.len = Elliptical.ElliptLength * Elliptical.InitialConversionValues.m
        convertedEllipticalDimensions.inc = Elliptical.Increments * Elliptical.InitialConversionValues.incAdj

        Return convertedEllipticalDimensions
    End Function

    Structure IConvertedEllipticalDimensions
        Dim majDia As Double
        Dim minDia As Double
        Dim len As Double
        Dim inc As Double
    End Structure

End Class
