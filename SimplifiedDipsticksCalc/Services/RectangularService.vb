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
        CalculateFullVolume(rectangular)

        If rectangular.Slope.Equals(Nothing) Then
            If rectangular.RegDip Then
                For h = 0 To convertedRectDimensions.height Step rectangular.ConvertedRectDimensions.inc
                    iv = area * h / rectangular.InitialConversionValues.cor
                    incrementList.Add(rectangular.FinalConversionRounding(h), Round(iv))
                Next
            Else
                For iv = rectangular.ConvertedRectDimensions.inc To CalculateFullVolume(rectangular) Step rectangular.ConvertedRectDimensions.inc
                    h = iv * rectangular.InitialConversionValues.cor / area
                    vol = IIf((iv < 1), iv, Round(iv, 1)) + rectangular.Adjustments
                    incrementList.Add(vol, rectangular.FinalConversionRounding(h))
                Next
                ''Add final fullvolume figures to increment list
                incrementList.Add(CalculateFullVolume(rectangular), rectangular.FinalConversionRounding(convertedRectDimensions.height))
            End If
        Else
            incrementList = TiltCalc(rectangular)
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

    Function TiltCalc(rectangular As Rectangular) As Dictionary(Of Double, Double)
        Dim incrementList As New Dictionary(Of Double, Double)
        Dim dbdb, varl, vol1, mark, vtilt, volt, iv, h1, v2, vol, volr, sinc, tv, v3, d, d3, d2, ds, dp, d4, d1, l, w, h, til, i As Single

        If rectangular.RegDip = True Then
            l = rectangular.Length / 100
            w = rectangular.Width / 100
            h = rectangular.Height / 100
            til = rectangular.Slope / 100
            dp = rectangular.PointofDip / 100
            i = rectangular.Increments / 100

            mark = 0
            dbdb = til * dp / l           'distance below dipstick base to ground level
            vtilt = l * w * til / 2        'volume of tilted section
            'calc for tilted section
            For count = dbdb To til Step i
                varl = l / til * count         'varl = variable length as height increases
                vol = varl * w * count / 2
                incrementList.Add(mark, Round(vol))
                mark = mark + (i * 100)
            Next
            'calc for part that passes through junction of til and regular section
            vol1 = vtilt - vol
            h1 = dbdb + (mark / 100) - til
            v2 = l * w * h1
            vol = vol1 + v2 + vol
            'iv = vol
            incrementList.Add(mark, Round(vol))
            'calc for regular section
            For x = i To h - (dbdb + (mark / 100)) Step i
                volr = l * w * x
                volt = vol + volr
                mark = mark + (i * 100)
                incrementList.Add(mark, Round(volt))
            Next
            'h = (h - dbdb) * 100
            'vol = (l * w * h) - vtilt
            'dipTable.Rows.Add(Round(vol), mark)
        Else
            l = rectangular.Length / 100
            w = rectangular.Width / 100
            h = rectangular.Height / 100
            til = rectangular.Slope
            dp = rectangular.PointofDip / 100
            i = rectangular.Increments
            Dim hv, vt, s, vds As Single
            'used to add min vol to form

            sinc = i
            hv = l * w * h
            'volume of tilted section
            tv = l * w * til / 200
            vt = hv - tv
            s = tv / i
            vds = dp * w * dp * til / (200 * l)
            'volume of lower top edge
            For i = i To tv Step i
                d = (((Sqrt(i * til * 2 / (l * w))) * 10) - (til * dp / l))
                incrementList.Add(Round(i), Round(d, 1))
            Next
            'calc of inc that passes through til
            d1 = h * 100 * sinc / hv 'distance per vol inc up tank
            v2 = tv - (i - sinc)
            v3 = i - tv
            ds = til * dp / l 'distance from horiz to dipsticks base
            d3 = v3 / sinc * d1
            d2 = til - (d + ds)
            d4 = d3 + d2 + d
            incrementList.Add(Round(i), Round(d4, 1))
            'increments up straight tank
            Do Until d4 > (h * 100) - ds
                i += sinc
                d4 += d1
                incrementList.Add(Round(i), Round(d4, 1))
            Loop
        End If
        'adjustment to convert output volume to US Gallons
        'If ObjUnits.OutputVolume = Units.Volume.USGallons Then
        '    For j As Integer = 0 To DipTable.Rows.Count - 1
        '        DipTable.Rows(j).Item(0) = Round(DipTable.Rows(j).Item(0) * 0.264172)
        '    Next
        'End If
        Return incrementList
    End Function

    Function GetConvertedRectDimensions(rectangular As Rectangular) As IConvertedRectangularDimensions

        convertedRectDimensions.length = rectangular.Length * rectangular.InitialConversionValues.m
        convertedRectDimensions.width = rectangular.Width * rectangular.InitialConversionValues.m
        convertedRectDimensions.height = rectangular.Height * rectangular.InitialConversionValues.m
        convertedRectDimensions.inc = rectangular.Increments * rectangular.InitialConversionValues.incAdj
        convertedRectDimensions.tilt = IIf(rectangular.Tilt.Equals(Nothing), 0.0, rectangular.Tilt * rectangular.InitialConversionValues.m)
        convertedRectDimensions.dip = IIf(rectangular.DipPoint.Equals(Nothing), 0.0, rectangular.DipPoint * rectangular.InitialConversionValues.m)

        Return convertedRectDimensions

    End Function

    Function getTankDetails(rect As Rectangular) As String
        Dim tankDetails As String

        tankDetails = "_(" + rect.Length.ToString() + "_" + rect.Width.ToString() + "_" + rect.Height.ToString() + ")"
        Return tankDetails
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
