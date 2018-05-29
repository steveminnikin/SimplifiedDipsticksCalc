@ModelType SimplifiedDipsticksCalc.Rectangular
@Html.Partial("_CalculateTop")
<div class="row">
    <div class="col-xs-2 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Length)
            </dt>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Width)
            </dt>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Height)
            </dt>
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dd>
                @Html.DisplayFor(Function(model) model.Length)
            </dd>
            <dd>
                @Html.DisplayFor(Function(model) model.Width)
            </dd>
            <dd>
                @Html.DisplayFor(Function(model) model.Height)
            </dd>
        </dl>
    </div>

    <div class="col-xs-2 col-xs-offset-1 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Increments)
            </dt>
            @If Not Model.Tilt.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.Slope)
                </dt>
            End If

            @If Not Model.dipPoint.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.PointofDip)
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
                    @Html.DisplayFor(Function(model) model.Slope)
                </dd>
            End If
            @If Not Model.Tilt.Equals(Nothing) Then

                @<dd>
                    @Html.DisplayFor(Function(model) model.PointofDip)
                </dd>
            End If
        </dl>
    </div>
</div><!--row-->
@Html.Partial("_IncrementsView")
<br />
<div class="row">
    <div class="col-xs-11 col-xs-offset-1">
        <p class="lead">@Html.DisplayNameFor(Function(model) model.FullVol) = @ViewBag.fullVolume @Model.GetVolume at @ViewBag.topHeight @Model.GetLength &nbsp; - &nbsp; SWC (97%) = @ViewBag.swc @Model.GetVolume</p>
        <p class="lead"><span id="chartNote"></span></p>
    </div>
</div>

