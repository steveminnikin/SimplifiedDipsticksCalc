Imports System.Math
Imports System.ComponentModel.DataAnnotations
Imports SimplifiedDipsticksCalc.TankService

Public Class Tank
    <Display(Name:="Full Volume")>
    Property FullVol As Double
    <Required>
    Property Dimensions As Dimension
    Property Tilt As Nullable(Of Double)
    <Display(Name:="Dip Point")>
    Property dipPoint As Nullable(Of Double)
    <Required>
    Property regDip As Boolean
    Property EngraveCode As Boolean
    Property Increments As Double
    Property IncrementList As New Dictionary(Of Double, Double)
    Property Adjustments As Double
    Property Shape As String
    Property InitialConversionValues As IInitialConversionValues

    Function FinalConversionRounding(h As Double) As Double
        Select Case Dimensions
            Case Dimension.LitresMMs
                Return IIf(regDip, Round(h * 10), Round(h * 10, 1))
            Case Dimension.GallonsInches, Dimension.USGallonsInches
                Return Round(h, 2)
            Case Dimension.GallonsMMs, Dimension.USGallonsMMs
                Return IIf(regDip, Round(h * 25.4), Round(h * 25.4, 1))
            Case Dimension.CubicMetresMMs
                Return IIf(regDip, Round(h * 10), Round(h * 100, 1))
            Case Else
                Return h
        End Select

    End Function


    Function GetLength() As String
        Dim len As String
        Select Case Dimensions
            Case Dimension.LitresMMs, Dimension.GallonsMMs, Dimension.USGallonsMMs, Dimension.CubicMetresMMs
                len = "Millimetres"
            Case Else
                len = "Inches"
        End Select
        Return len
    End Function
    Function GetShape() As String
        Return Shape
    End Function
    Function GetVolume() As String
        Dim vol As String
        Select Case Dimensions
            Case Dimension.LitresMMs
                vol = "Litres"
            Case Dimension.GallonsInches, Dimension.GallonsMMs
                vol = "Gallons"
            Case Dimension.CubicMetresMMs
                vol = "Cubic Metres"
            Case Else
                vol = "US Gallons"
        End Select
        Return vol
    End Function

    Function GetShortLength() As String
        Dim len As String
        Select Case Dimensions
            Case Dimension.LitresMMs, Dimension.CubicMetresMMs, Dimension.GallonsMMs, Dimension.USGallonsMMs
                len = "MMs"
            Case Else
                len = "Inches"
        End Select
        Return len
    End Function

    Enum Dimension
        <Display(Name:="Litres & MMs")>
        LitresMMs
        <Display(Name:="Gallons & Inches")>
        GallonsInches
        <Display(Name:="Gallons & MMs")>
        GallonsMMs
        <Display(Name:="US Gallons & MMs")>
        USGallonsMMs
        <Display(Name:="US Gallons & Inches")>
        USGallonsInches
        <Display(Name:="Cubic Metres & MMs")>
        CubicMetresMMs
    End Enum
End Class



