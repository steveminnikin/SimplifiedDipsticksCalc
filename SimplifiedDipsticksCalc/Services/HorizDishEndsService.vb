Imports System.Math

Public Class HorizDishEndsService
    Inherits TankService

    Private incrementList As New Dictionary(Of Double, Double)
    Private convertedHorizDishEndsDimensions As IconvertedHorizDishEndsDimensions
    Private dr, a, lr, h, vol, d2, p1, yd, y2, vd, lm, volcor, ai, firstvol, ht, iv, vb, t2 As Double

    Function GetFullVol(horizDishEnds As HorizDishEnds) As Double
        Dim vt, f1, f2, f3, f4, f5, fv As Double

        vt = vd + vb
        If convertedHorizDishEndsDimensions.til = 0 Then
            iv = vt / horizDishEnds.InitialConversionValues.cor
            horizDishEnds.FullVol = iv
            Return horizDishEnds.FullVol
        Else
            t2 = (convertedHorizDishEndsDimensions.til - (convertedHorizDishEndsDimensions.dip * convertedHorizDishEndsDimensions.til / convertedHorizDishEndsDimensions.l)) * Cos(FnB(convertedHorizDishEndsDimensions.til / convertedHorizDishEndsDimensions.l))
            convertedHorizDishEndsDimensions.dip = convertedHorizDishEndsDimensions.l - convertedHorizDishEndsDimensions.dip
            f1 = FnA((convertedHorizDishEndsDimensions.dia - 2 * t2) / convertedHorizDishEndsDimensions.dia)
            f2 = Sqrt(t2 * (convertedHorizDishEndsDimensions.dia - t2))
            f3 = 3 * convertedHorizDishEndsDimensions.dia ^ 2 / 4 - convertedHorizDishEndsDimensions.dia * t2 + t2 ^ 2
            f4 = (3 * convertedHorizDishEndsDimensions.dia ^ 2 / 4) * (t2 - convertedHorizDishEndsDimensions.dia / 2)
            f5 = 0.0003872 * t2 ^ 2 * (3 * convertedHorizDishEndsDimensions.dia / 2 - t2)
            fv = (f2 * f3 + f4 * f1) * convertedHorizDishEndsDimensions.dip / (3 * t2) + f5
            iv = (vt - fv) / horizDishEnds.InitialConversionValues.cor
        End If
        horizDishEnds.FullVol = iv
        Return horizDishEnds.FullVol
    End Function

    Function CalculateIncrements(horizDishEnds As HorizDishEnds) As Dictionary(Of Double, Double)

        Dim amax, ap, v0, xinc As Double
        Dim init, swit As Boolean

        volcor = 1
        If convertedHorizDishEndsDimensions.ovl = 0 Then
            dr = convertedHorizDishEndsDimensions.ra - (Sqrt(convertedHorizDishEndsDimensions.ra ^ 2 - convertedHorizDishEndsDimensions.dia ^ 2 / 4))
        Else
            dr = (convertedHorizDishEndsDimensions.ovl - convertedHorizDishEndsDimensions.l) / 2
        End If
        If convertedHorizDishEndsDimensions.kr <> 0 Then Call KnuckleRadOption()
        lm = convertedHorizDishEndsDimensions.l + dr
        lr = convertedHorizDishEndsDimensions.dip + dr / 2
        If horizDishEnds.Tilt <> 0 Then Call TiltConsts(horizDishEnds)
        BasicConstants()
        If horizDishEnds.regDip And horizDishEnds.Tilt = 0 Then
            Call RegdipCalcCylDish(horizDishEnds)
        Else
Volcalcs:   Call VolCalcs(horizDishEnds)
            If init = False Then GoTo 6740
            If swit = True Then GoTo 6760
            If v0 < vol Then GoTo 6640
            GoTo 6780
6640:       If horizDishEnds.regDip = True Then
                Call RegdipCalcCylDish(horizDishEnds)
            Else
                swit = True
                iv = vol
                If iv < xinc Then GoTo 6780
                h = convertedHorizDishEndsDimensions.dia * (1 - Cos(a)) / 2
                'IncrementList.Add(Round(iv), FinalConversionRounding(h))
                ai = 0.1
6710:           xinc = xinc + convertedHorizDishEndsDimensions.inc
                If xinc > iv Then GoTo 6780
                GoTo 6710
6740:           init = True
                GoTo 6780
6760:           If vol > xinc And ai < 0.001 Then GoTo 6920
                If vol > xinc Then GoTo 6820
6780:           v0 = vol
                a = a + ai
                If amax = a Then GoTo 6830
                GoTo 6860
6820:           amax = a
6830:           a = a - ai / 2
                ai = ai / 2
                GoTo Volcalcs
6860:           If a > PI And ai < 0.001 Then
                    GetFullVol(horizDishEnds)
                Else
                    If a > PI Then GoTo 6890
                    GoTo Volcalcs
6890:               a = a - ai / 2
                    ai = ai / 2
                    GoTo 6860
6920:               ap = ai * (xinc - v0) / (vol - v0) + a - ai
                    a = ap
                    VolCalcs(horizDishEnds)
                    iv = vol
                    h = convertedHorizDishEndsDimensions.dia * (1 - Cos(ap)) / 2
                    incrementList.Add(Round(iv), horizDishEnds.FinalConversionRounding(h))
                    xinc = xinc + convertedHorizDishEndsDimensions.inc
                    ai = 0.05
                    GoTo 6780
                End If
            End If
        End If
        If Not horizDishEnds.regDip Then incrementList.Add(Round(horizDishEnds.FullVol), horizDishEnds.FinalConversionRounding(convertedHorizDishEndsDimensions.dia))
        Return incrementList
    End Function
    Private Sub RegdipCalcCylDish(horizDishEnds As HorizDishEnds)
        Dim hi, iv, blankh, h1, h As Double
        Dim jump, n As Integer
        If horizDishEnds.Tilt <> 0 Then
            hi = convertedHorizDishEndsDimensions.dia * (1 - Cos(a)) / 2
            blankh = Int(hi / convertedHorizDishEndsDimensions.inc)
            h = -convertedHorizDishEndsDimensions.inc
            For n = 0 To blankh - 1
                iv = 0
                h += convertedHorizDishEndsDimensions.inc
                horizDishEnds.IncrementList.Add(horizDishEnds.FinalConversionRounding(h), Round(iv))
            Next n
            iv = firstvol
            h += convertedHorizDishEndsDimensions.inc
            incrementList.Add(horizDishEnds.FinalConversionRounding(h), Round(iv))
            h1 = Int(hi / convertedHorizDishEndsDimensions.inc) * convertedHorizDishEndsDimensions.inc + convertedHorizDishEndsDimensions.inc
        End If
        For h = h1 + 0.001 To ht Step convertedHorizDishEndsDimensions.inc
            a = FnA(1 - 2 * h / convertedHorizDishEndsDimensions.dia)
            Call VolCalcs(horizDishEnds)
            If jump = 1 Then
                iv = 0
            ElseIf iv > vol Then
                jump = 1
                iv = horizDishEnds.FullVol
            Else : iv = vol
            End If
            incrementList.Add(horizDishEnds.FinalConversionRounding(h), Round(iv))
        Next h
        GetFullVol(horizDishEnds)
    End Sub
    Private Sub KnuckleRadOption()
        'sub for when a knuckle radius is entered
        Dim eg, gf, hg, xr, hf, hb, ah, f6, f7, f8, vk, dv, v2, v3, le As Double
        eg = convertedHorizDishEndsDimensions.dia / 2 - convertedHorizDishEndsDimensions.kr
        gf = Sqrt((convertedHorizDishEndsDimensions.ra - convertedHorizDishEndsDimensions.kr) * (convertedHorizDishEndsDimensions.ra - convertedHorizDishEndsDimensions.kr) - eg ^ 2)
        hg = convertedHorizDishEndsDimensions.kr * gf / (convertedHorizDishEndsDimensions.ra - convertedHorizDishEndsDimensions.kr)
        xr = hg / convertedHorizDishEndsDimensions.kr
        hf = gf + hg
        hb = Sqrt(convertedHorizDishEndsDimensions.ra ^ 2 - hf ^ 2)
        ah = convertedHorizDishEndsDimensions.ra - hf
        f6 = eg ^ 2 * hg + convertedHorizDishEndsDimensions.kr ^ 2 * hg - hg ^ 3 / 3
        f7 = eg * hg * Sqrt(convertedHorizDishEndsDimensions.kr ^ 2 - hg ^ 2)
        f8 = eg * convertedHorizDishEndsDimensions.kr ^ 2 * Atan(xr / Sqrt(1 - xr ^ 2))
        vk = PI * (f6 + f7 + f8)
        dv = PI * ah * (3 * hb ^ 2 + ah ^ 2) / 6
        v2 = (vk + dv) * 2    'Vol of two dished ends
        vd = (dr ^ 2 + 3 * convertedHorizDishEndsDimensions.dia ^ 2 / 4) * dr * PI / 3 'calc of equiv straight length
        v3 = v2 - vd
        le = v3 / (PI * convertedHorizDishEndsDimensions.dia ^ 2 / 4)
        convertedHorizDishEndsDimensions.l += le
    End Sub
    Private Sub TiltConsts(horizDishEnds As HorizDishEnds)
        Dim hz As Double
        h = 0
        hz = convertedHorizDishEndsDimensions.dip * convertedHorizDishEndsDimensions.til / convertedHorizDishEndsDimensions.l
        a = FnA((convertedHorizDishEndsDimensions.dia - 2 * hz) / convertedHorizDishEndsDimensions.dia)
        d2 = lr * convertedHorizDishEndsDimensions.dia ^ 2
        p1 = dr * convertedHorizDishEndsDimensions.dia ^ 2 / 2
        yd = convertedHorizDishEndsDimensions.til * convertedHorizDishEndsDimensions.dia * lr * (dr - lr) / convertedHorizDishEndsDimensions.l
        y2 = convertedHorizDishEndsDimensions.til ^ 2 * (dr ^ 3 / 8 + convertedHorizDishEndsDimensions.dip ^ 3) / (convertedHorizDishEndsDimensions.l ^ 2)
        firstvol = VolCalcs(horizDishEnds)
    End Sub
    Private Function VolCalcs(horizDishEnds As HorizDishEnds) As Double
        Dim c3, c4, c5, c6 As Double
        c3 = d2 * (a - Sin(a) * Cos(a)) / 4
        c4 = p1 * (Sin(a)) ^ 3 * Cos(a) / 6
        c5 = yd * Sin(a) / 2
        c6 = y2 * Cos(a) / (3 * Sin(a))
        vol = volcor * (c3 - c4 + c5 + c6) / horizDishEnds.InitialConversionValues.cor
        Return vol
    End Function
    Protected Friend Sub BasicConstants()
        'additional constants for vol cals
        Dim ll, y, g6, xinc As Double
        ht = convertedHorizDishEndsDimensions.dia / Cos(t2)
        ll = convertedHorizDishEndsDimensions.l - convertedHorizDishEndsDimensions.dip + dr / 2
        t2 = FnB(convertedHorizDishEndsDimensions.til / convertedHorizDishEndsDimensions.l)
        d2 = lm * convertedHorizDishEndsDimensions.dia ^ 2
        p1 = (lm - convertedHorizDishEndsDimensions.l) * convertedHorizDishEndsDimensions.dia ^ 2
        y = Tan(FnB(convertedHorizDishEndsDimensions.til / convertedHorizDishEndsDimensions.l))
        yd = y * convertedHorizDishEndsDimensions.dia * (lr ^ 2 - ll ^ 2)
        y2 = y ^ 2 * (lr ^ 3 + ll ^ 3)
        'Empiric Vol Correction Factor
        vd = (dr ^ 2 + 3 * convertedHorizDishEndsDimensions.dia ^ 2 / 4) * dr * PI / 3
        vb = PI * convertedHorizDishEndsDimensions.dia ^ 2 * convertedHorizDishEndsDimensions.l / 4
        g6 = vd * (1 + (dr / convertedHorizDishEndsDimensions.dia) * dr / convertedHorizDishEndsDimensions.dia)
        volcor = (vb + g6) / (vd + vb)
        'End of Vol Correction Calc
        xinc = convertedHorizDishEndsDimensions.inc
        a = 0.1
        ai = 0.01
    End Sub

    Function GetConvertedHorizFlatEndsDimensions(horizDishEnds As HorizDishEnds) As IconvertedHorizDishEndsDimensions
        convertedHorizDishEndsDimensions.dia = horizDishEnds.DishDiameter * horizDishEnds.InitialConversionValues.m
        convertedHorizDishEndsDimensions.l = horizDishEnds.stLength * horizDishEnds.InitialConversionValues.m
        convertedHorizDishEndsDimensions.inc = horizDishEnds.Increments * horizDishEnds.InitialConversionValues.incAdj
        convertedHorizDishEndsDimensions.til = IIf(horizDishEnds.Tilt.Equals(Nothing), 0.0, horizDishEnds.Tilt * horizDishEnds.InitialConversionValues.m)
        convertedHorizDishEndsDimensions.ovl = IIf(horizDishEnds.OvLength.Equals(Nothing), 0.0, horizDishEnds.OvLength * horizDishEnds.InitialConversionValues.m)
        convertedHorizDishEndsDimensions.kr = IIf(horizDishEnds.KnuckleRad.Equals(Nothing), 0.0, horizDishEnds.KnuckleRad * horizDishEnds.InitialConversionValues.m)
        convertedHorizDishEndsDimensions.ra = IIf(horizDishEnds.DishEndRad.Equals(Nothing), 0.0, horizDishEnds.DishEndRad * horizDishEnds.InitialConversionValues.m)
        convertedHorizDishEndsDimensions.dip = IIf(horizDishEnds.dipPoint.Equals(Nothing), 0.0, horizDishEnds.dipPoint * horizDishEnds.InitialConversionValues.m)

        Return convertedHorizDishEndsDimensions

    End Function

    Structure IConvertedHorizDishEndsDimensions
        Public ovl As Double
        Public ra As Nullable(Of Double)
        Public kr As Nullable(Of Double)
        Public dia As Double
        Public inc As Double
        Public til As Nullable(Of Double)
        Public dip As Nullable(Of Double)
        Public l As Nullable(Of Double)
    End Structure

End Class
