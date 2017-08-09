@ModelType SimplifiedDipsticksCalc.Elliptical
@Html.Partial("_CalculateTop")
<div class="row">
    <div class="col-xs-4 text-right">
        <dl>
            <dt>
                @Html.DisplayNameFor(Function(model) model.MajorDiameter)
            </dt>
            <dt>
            @Html.DisplayNameFor(Function(model) model.MinorDiameter)
            </dt>
            <dt>
                @Html.DisplayNameFor(Function(model) model.ElliptLength)
            </dt>
        </dl>
    </div>
    <div class="col-xs-2">
        <dl>
            <dd>
                @Html.DisplayFor(Function(model) model.MajorDiameter)
            </dd>
            <dd>
                @Html.DisplayFor(Function(model) model.MinorDiameter)
            </dd>
            <dd>
                @Html.DisplayFor(Function(model) model.ElliptLength)
            </dd>
        </dl>

    </div>

    <div class="col-xs-2 text-right">
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
        <p>@Html.DisplayNameFor(Function(model) model.FullVol) = @ViewBag.fullVolume @Model.GetVolume at @ViewBag.topHeight @Model.GetLength &nbsp; - &nbsp; SWC (97%) = @ViewBag.swc @Model.GetVolume</p>
        <br />
        <p><span id="chartNote"></span></p>
    </div>
</div>

