@ModelType SimplifiedDipsticksCalc.Elliptical
<div class="form-horizontal">
    <div class="col-lg-6">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.MajorDiameter, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.MajorDiameter, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.MinorDiameter, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.MinorDiameter, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.ElliptLength, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.ElliptLength, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>  
    <div class="col-lg-offset-3 col-lg-9">
        <button id="btnSubmit" type="submit" class="btn btn-primary btnSubmit">Create</button>
    </div>
    </div>
</div>
</form>