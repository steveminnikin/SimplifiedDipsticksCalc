@ModelType SimplifiedDipsticksCalc.HorizDishEnds
@Html.Partial("_CalculateTop")
<div class="row">
    <div class="col-xs-4 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.DishDiameter)
            </dt>
            <dt>
                @Html.DisplayNameFor(Function(model) model.stLength)
            </dt>
            @If Not Model.OvLength.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.OvLength)
                </dt>
            End If
            @If Not Model.DishEndRad.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.DishEndRad)
                </dt>
            End If
            @If Not Model.KnuckleRad.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.KnuckleRad)
                </dt>
            End If
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dt>
                @Html.DisplayFor(Function(model) model.DishDiameter)
            </dt>
            <dt>
                @Html.DisplayFor(Function(model) model.stLength)
            </dt>
            @If Not Model.OvLength.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayFor(Function(model) model.OvLength)
                </dt>
            End If
            @If Not Model.DishEndRad.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayFor(Function(model) model.DishEndRad)
                </dt>
            End If
            @If Not Model.KnuckleRad.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayFor(Function(model) model.KnuckleRad)
                </dt>
            End If
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
        <p class="lead">@Html.DisplayNameFor(Function(model) model.FullVol) = @ViewBag.fullVolume @Model.GetVolume at @Html.DisplayFor(Function(model) model.DishDiameter) @Model.GetLength &nbsp; - &nbsp; SWC (97%) = @ViewBag.swc @Model.GetVolume</p>

        <p class="lead"><span id="chartNote"></span></p>
    </div>
</div>
