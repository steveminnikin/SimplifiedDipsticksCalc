@ModelType SimplifiedDipsticksCalc.VertCyl
<div class="form-horizontal">
    <div class="col-lg-6">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Diameter, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.Diameter, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.VertHeight, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.VertHeight, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
    <div class="col-lg-offset-3 col-lg-9">
        <button id="btnSubmit" type="submit" class="btn btn-primary btnSubmit">Create</button>
    </div>
    </div>
        <div class="col-lg-6">
            <div class="form-group">
                @Html.LabelFor(Function(model) model.DishEndDepth, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.DishEndDepth, New With {.class = "form-control", .placeholder = "Optional"})
                </div>
            </div>
            For dished bases use overall length of tank (including base).<br />
            <em>Adjustment</em> is volume outside of dished end i.e. the volume <strong>not</strong> part of the tank <br />
            <ul>
                To calculate volume of Adjustment
                <li>Use horizontal flat end calculator with OL equal to .</li>
                <li>Use the horital dished calulator with SL and OL equal to vert tank with dished base and dished top</li>
                <li>Subtract one from the other and then divide by 2.</li>
            </ul>
            <a href="http://secure.czltd.com/Engineering/dh_calculate.html">Dished End Calculator</a><br />
            <a href="https://checalc.com/calc/vessel.html">Volume Calculator</a>
        </div>
</div>
</form>
