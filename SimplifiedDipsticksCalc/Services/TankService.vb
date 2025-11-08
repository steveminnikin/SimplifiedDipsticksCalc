Imports System.Math
Public Class TankService

    ' Unit conversion constants
    Private Const MM_TO_CM As Double = 0.1
    Private Const CM_TO_M As Double = 0.01
    Private Const CM_TO_INCH As Double = 1 / 25.4
    Private Const CUBIC_INCHES_PER_IMPERIAL_GALLON As Double = 277.42
    Private Const US_TO_IMPERIAL_GALLON_RATIO As Double = 0.8327
    Private Const LITRES_PER_CUBIC_METER As Double = 1000

    Function GetinitialConversionValues(tank As Tank) As IInitialConversionValues

        Dim initialConversionValues As IInitialConversionValues

        With initialConversionValues
            Select Case tank.Dimensions

                Case Tank.Dimension.LitresMMs
                    .m = MM_TO_CM
                    .cor = LITRES_PER_CUBIC_METER
                    .incAdj = If(tank.RegDip, MM_TO_CM, 1.0)
                Case Tank.Dimension.GallonsInches
                    .m = CM_TO_INCH
                    .cor = CUBIC_INCHES_PER_IMPERIAL_GALLON
                    .incAdj = 1.0
                Case Tank.Dimension.GallonsMMs
                    .m = CM_TO_INCH
                    .cor = CUBIC_INCHES_PER_IMPERIAL_GALLON
                    .incAdj = If(tank.RegDip, MM_TO_CM, 1.0)
                Case Tank.Dimension.CubicMetresMMs
                    .m = CM_TO_M
                    .cor = LITRES_PER_CUBIC_METER
                    .incAdj = If(tank.RegDip, CM_TO_M, 1.0)
                Case Tank.Dimension.USGallonsMMs
                    .m = CM_TO_INCH
                    .cor = CUBIC_INCHES_PER_IMPERIAL_GALLON * US_TO_IMPERIAL_GALLON_RATIO
                    .incAdj = If(tank.RegDip, MM_TO_CM, 1.0)
                Case Tank.Dimension.USGallonsInches
                    .m = CM_TO_INCH
                    .cor = CUBIC_INCHES_PER_IMPERIAL_GALLON * US_TO_IMPERIAL_GALLON_RATIO
                    .incAdj = 1.0
                Case Else

            End Select
        End With

        Return initialConversionValues

    End Function

    Sub DownloadEngraveCode(tank As Tank)

        Dim fileName As String = "FV " + Round(tank.FullVol).ToString + "_INCS " + tank.Increments.ToString + tank.Details
        Dim text As String = ""
        For Each row As KeyValuePair(Of Double, Double) In tank.IncrementList
            text = text & CStr(row.Value) & "," & CStr(row.Key) & vbCr
        Next
        Dim attachment As String = String.Format("attachment; filename=" + fileName)
        With HttpContext.Current.Response
            .ClearHeaders()
            .ClearContent()
            .AddHeader("content-disposition", attachment)
            .ContentType = "text/plain"
            .Write(text)
            .End()
        End With
    End Sub

    Protected Friend Function FnA(x As Double) As Double
        'defines function for use in vol calcs
        Return PI / 2 - Atan(x / Sqrt(-x * x + 1))
    End Function
    Protected Friend Function FnB(x As Double) As Double
        'defines function for use in vol calcs
        Return Atan(x / Sqrt(-x * x + 1))
    End Function

    Structure IInitialConversionValues
        Public m As Double
        Public cor As Double
        Public incAdj As Double
    End Structure

End Class
