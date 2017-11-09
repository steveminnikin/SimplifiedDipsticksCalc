@ModelType SimplifiedDipsticksCalc.Tank

<div class="row">
    @If Model.RegDip Then
        Dim item = Model.IncrementList
        If Model.Increments.Equals(0.0625) Then
            Dim tableLength As Integer = Convert.ToInt32(Math.Ceiling((item.Values.Count / 16.0)))
            @<table class="table table-condensed ">
                <!--table row for increments across top of table-->
                <tr>
                    <td class="text-center table-edged-bottom table-edged-right">@Model.GetShortLength</td>                    
                    <th class="text-center table-edged-right">0</th>
                        <th class="text-center table-edged-right">1/16</th>
                        <th class="text-center table-edged-right">1/8</th>
                        <th class="text-center table-edged-right">3/16</th>
                        <th class="text-center table-edged-right">1/4</th>
                    <th class="text-center table-edged-right">5/16</th>
                    <th class="text-center table-edged-right">3/8</th>
                    <th class="text-center table-edged-right">7/16</th>
                    <th class="text-center table-edged-right">1/2</th>
                    <th class="text-center table-edged-right">9/16</th>
                    <th class="text-center table-edged-right">5/8</th>
                     <th class="text-center table-edged-right">11/16</th>
                    <th class="text-center table-edged-right">3/4</th>
                     <th class="text-center table-edged-right">13/16</th>
                    <th class="text-center table-edged-right">7/8</th>
                    <th class="text-center table-edged-right">15/16</th>
                </tr>

                @For i As Integer = 0 To tableLength - 1 Step 1
                    @<tr>
                        <th class="text-center table-edged-bottom">@(Model.Increments * 16 * i)</th>
                        @For j As Integer = 0 To 15 Step 1
                            @If (((i * 16) + j) < item.Keys.Count) Then
                                @<td class="text-center">@item.Values.ElementAt((i * 16) + j)</td>
                            End If
                        Next
                    </tr>
                Next
            </table>
        Else
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
        End If
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
