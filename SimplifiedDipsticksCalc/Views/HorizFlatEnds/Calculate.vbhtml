@ModelType SimplifiedDipsticksCalc.HorizFlatEnds
@Html.Partial("_CalculateTop")
<div class="row">
    <div class="col-xs-4 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.FlatDiameter)
            </dt>
            <dt>
                @Html.DisplayNameFor(Function(model) model.FlatLength)
            </dt>
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dd>
                @Html.DisplayFor(Function(model) model.FlatDiameter)
            </dd>
            <dd>
                @Html.DisplayFor(Function(model) model.FlatLength)
            </dd>
        </dl>

    </div>

    <div class="col-xs-2 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Increments)
            </dt>
            @If Not Model.Tilt.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.Tilt)
                </dt>
            End If

            @If Not Model.dipPoint.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.dipPoint)
                </dt>
            End If
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dd>
                @Html.DisplayFor(Function(model) model.Increments)
            </dd>
            @If Not Model.Tilt.Equals(Nothing) Then
                @<dd>
                    @Html.DisplayFor(Function(model) model.Tilt)
                </dd>
            End If
            @If Not Model.Tilt.Equals(Nothing) Then

                @<dd>
                    @Html.DisplayFor(Function(model) model.dipPoint)
                </dd>
            End If
        </dl>
    </div>
</div><!--row-->
@Html.Partial("_IncrementsView")

<br />
<div class="row">
    <div class="col-xs-11 col-xs-offset-1">
        <p>@Html.DisplayNameFor(Function(model) model.FullVol) = @ViewBag.fullVolume @Model.GetVolume at @ViewBag.topHeight @Model.GetLength &nbsp; - &nbsp; SWC (97%) = @ViewBag.swc @Model.GetVolume</p>
        <br />
        <p><span id="chartNote"></span></p>
    </div>
</div>

