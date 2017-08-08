@ModelType SimplifiedDipsticksCalc.VertCyl
@Html.Partial("_CalculateTop")
<div class="row">
    <div class="col-xs-2 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Diameter)
            </dt>
            <dt>
                @Html.DisplayNameFor(Function(model) model.VertHeight)
            </dt>
            @If Not Model.DishEndDepth.Equals(Nothing) Then
                @<dt>
                    @Html.DisplayNameFor(Function(model) model.DishEndDepth)
                </dt>
            End If
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dd>
                @Html.DisplayFor(Function(model) model.Diameter)
            </dd>
            <dd>
                @Html.DisplayFor(Function(model) model.VertHeight)
            </dd>
            @If Not Model.DishEndDepth.Equals(Nothing) Then
                @<dd>
                    @Html.DisplayFor(Function(model) model.DishEndDepth)
                </dd>
            End If
        </dl>
    </div>

    <div class="col-xs-2 col-xs-offset-1 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.Increments)
            </dt>
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dd>
                @Html.DisplayFor(Function(model) model.Increments)
            </dd>
        </dl>
    </div>
</div><!--row-->
@Html.Partial("_IncrementsView")

<br />
<div class="row">
    <div class="col-xs-11 col-xs-offset-1">
        <p class="lead">@Html.DisplayNameFor(Function(model) model.FullVol) = @ViewBag.fullVolume @Model.GetVolume at @Html.DisplayFor(Function(model) model.VertHeight) @Model.GetLength &nbsp; - &nbsp; SWC (97%) = @ViewBag.swc @Model.GetVolume</p>
        <p class="lead"><span id="chartNote"></span></p>
    </div>
</div>

