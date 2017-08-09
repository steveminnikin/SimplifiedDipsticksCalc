Imports System.Math

Public Class HorizFlatEndsService
    Inherits TankService

    Private convertedHorizFlatEndsDimensions As IConvertedFlatEndsDimensions

    Function GetFullVol(horizFlatEnds As HorizFlatEnds) As Double
        horizFlatEnds.FullVol = PI * Pow((convertedHorizFlatEndsDimensions.dia / 2), 2) * convertedHorizFlatEndsDimensions.l / horizFlatEnds.InitialConversionValues.cor
        Return horizFlatEnds.FullVol
    End Function

    Public Function CalculateIncrements(horizFlatEnds As HorizFlatEnds) As Dictionary(Of Double, Double)

        Dim incrementList As New Dictionary(Of Double, Double)
        Dim T9, TN, X, area, AN, AT, IV, R, H As Single

        T9 = 0.1
        R = convertedHorizFlatEndsDimensions.dia / 2

        If horizFlatEnds.regDip Then
            For H = 0 + 0.00001 To R Step convertedHorizFlatEndsDimensions.inc
                AN = 2 * (FnA(1 - H / R))
                AT = 0.5 * R ^ 2 * Sin(AN)
                area = AN * R ^ 2 / 2 - AT
                IV = area * convertedHorizFlatEndsDimensions.l / horizFlatEnds.InitialConversionValues.cor
                incrementList.Add(horizFlatEnds.FinalConversionRounding(H), Round(IV))
                If H > R Then Exit For
            Next H
            'Height = Diameter is this required in this version?
            For H = H To convertedHorizFlatEndsDimensions.dia Step convertedHorizFlatEndsDimensions.inc
                AN = 2 * (FnA(2 * H / convertedHorizFlatEndsDimensions.dia - 1))
                AT = 0.5 * R ^ 2 * Sin(AN)
                area = AN * R ^ 2 / 2 - AT
                IV = (PI * R ^ 2 - area) * convertedHorizFlatEndsDimensions.l / horizFlatEnds.InitialConversionValues.cor
                incrementList.Add(horizFlatEnds.FinalConversionRounding(H), Round(IV))
            Next H
        Else
            'Workshop Format
            For TIV = convertedHorizFlatEndsDimensions.inc To horizFlatEnds.FullVol Step convertedHorizFlatEndsDimensions.inc
X:              X = T9 - Sin(T9) - (2 * horizFlatEnds.InitialConversionValues.cor * TIV / (R ^ 2 * convertedHorizFlatEndsDimensions.l))
                X = X / (1 - Cos(T9))
                TN = T9 - X
                If Abs(TN) > 150000.0! Then
                    TIV = TIV + 0.001
                    T9 = 0.1
                    GoTo X
                End If
                If Abs(TN - T9) >= 0.00001 Then
                    T9 = TN
                    GoTo X
                End If
                H = R - R * Cos(TN / 2)

                incrementList.Add(Round(TIV), horizFlatEnds.FinalConversionRounding(H))
            Next TIV
            incrementList.Add(Round(horizFlatEnds.FullVol), horizFlatEnds.FinalConversionRounding(convertedHorizFlatEndsDimensions.dia))
        End If
        Return incrementList
    End Function

    Function GetConvertedHorizFlatEndsDimensions(horizFlatEnds As HorizFlatEnds) As IConvertedFLatEndsDimensions

        convertedHorizFlatEndsDimensions.dia = horizFlatEnds.FlatDiameter * horizFlatEnds.InitialConversionValues.m
        convertedHorizFlatEndsDimensions.l = horizFlatEnds.FlatLength * horizFlatEnds.InitialConversionValues.m
        convertedHorizFlatEndsDimensions.inc = horizFlatEnds.Increments * horizFlatEnds.InitialConversionValues.incAdj
        convertedHorizFlatEndsDimensions.til = IIf(horizFlatEnds.Tilt.Equals(Nothing), 0.0, horizFlatEnds.Tilt * horizFlatEnds.InitialConversionValues.m)
        convertedHorizFlatEndsDimensions.dip = IIf(horizFlatEnds.dipPoint.Equals(Nothing), 0.0, horizFlatEnds.dipPoint * horizFlatEnds.InitialConversionValues.m)

        Return convertedHorizFlatEndsDimensions
    End Function

    Structure IConvertedFlatEndsDimensions
        Public l As Double
        Public dia As Double
        Public inc As Double
        Public til As Nullable(Of Double)
        Public dip As Nullable(Of Double)
    End Structure

End Class
