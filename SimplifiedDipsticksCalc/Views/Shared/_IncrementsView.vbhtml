@ModelType SimplifiedDipsticksCalc.Tank

<div class="row">
    @If Model.regDip Then
        Dim item = Model.IncrementList
        Dim tableLength As Integer = Convert.ToInt32(Math.Ceiling((item.Values.Count / 10.0)))
        @<table class="table table-condensed ">
            <!--table row for increments across top of table-->
            <tr>
                <td class="text-center table-edged-bottom table-edged-right">@Model.GetShortLength</td>
                @For k As Integer = 0 To 9 Step 1

                    @<th class="text-center table-edged-right">@(k * Model.Increments)</th>
                Next

            </tr>

            @For i As Integer = 0 To tableLength - 1 Step 1

                @<tr>

                    <th class="text-center table-edged-bottom">@(Model.Increments * 10 * i)</th>
                    @For j As Integer = 0 To 9 Step 1

                        @If (((i * 10) + j) < item.Keys.Count) Then


                            @<td class="text-center">@item.Values.ElementAt((i * 10) + j)</td>
                        End If
                    Next

                </tr>
            Next

        </table>

    Else

        @<div class="col-sm-12 col-md-8 col-md-offset-2">
            <div>
                <table class="table table-condensed">
                    <tr>
                        @For l As Integer = 0 To 2 Step 1
                            @<th class="text-center">@Model.GetVolume</th>
                            @<th class="text-center">@Model.GetLength</th>
                        Next
                    </tr>

                </table>
            </div>
            <div class="text-left wrappedCol">
                <div>
                    <table class="table table-condensed table-striped table-edged-bottom">

                        @For Each listItem In Model.IncrementList

                            @<tr>
                                <td class="text-center">@Html.DisplayFor(Function(modelitem) listItem.Key)</td>
                                <td class="text-center">@Html.DisplayFor(Function(modelitem) listItem.Value)</td>
                            </tr>
                        Next
                    </table>
                </div>
            </div>
        </div>
    End If
</div>
