Imports System.Math
Public Class TankService

    Function GetinitialConversionValues(tank As Tank) As IInitialConversionValues

        Dim initialConversionValues As IInitialConversionValues

        With initialConversionValues
            Select Case tank.Dimensions

                Case Tank.Dimension.LitresMMs
                    .m = 0.1
                    .cor = 1000
                    .incAdj = IIf(tank.regDip, 0.1, 1.0)
                Case Tank.Dimension.GallonsInches
                    .m = 1 / 25.4
                    .cor = 277.42
                    .incAdj = 1.0
                Case Tank.Dimension.GallonsMMs
                    .m = 1 / 25.4
                    .cor = 277.42
                    .incAdj = IIf(tank.regDip, 0.1, 1.0)
                Case Tank.Dimension.CubicMetresMMs
                    .m = 0.01
                    .cor = 1000
                    .incAdj = IIf(tank.regDip, 0.01, 1.0)
                Case Tank.Dimension.USGallonsMMs
                    .m = 1 / 25.4
                    .cor = 277.42 * 0.8327
                    .incAdj = IIf(tank.regDip, 0.1, 1.0)
                Case Tank.Dimension.USGallonsInches
                    .m = 1 / 25.4
                    .cor = 277.42 * 0.8327
                    .incAdj = 1.0
                Case Else

            End Select
        End With

        Return initialConversionValues

    End Function

    Sub DownloadEngraveCode(tank As Tank)

        Dim fileName As String = Me.ToString.Remove(0, 21) + "_FV " + Round(tank.FullVol).ToString + "_INCS " + tank.Increments.ToString
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

    Protected Friend Function FnA(x As Single) As Single
        'defines function for use in vol calcs
        Return CSng(PI / 2 - Atan(x / Sqrt(-x * x + 1)))
    End Function
    Protected Friend Function FnB(x As Single) As Single
        'defines function for use in vol calcs
        Return CSng(Atan(x / Sqrt(-x * x + 1)))
    End Function

    Structure IInitialConversionValues
        Public m As Double
        Public cor As Double
        Public incAdj As Double
    End Structure

End Class
