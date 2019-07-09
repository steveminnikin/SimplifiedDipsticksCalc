@ModelType SimplifiedDipsticksCalc.Rectangular
<div class="form-horizontal">
    <div class="col-lg-6">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Length, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.Length, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Width, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.Width, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Height, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.Height, New With {.class = "form-control", .placeholder = "MMs"})
            </div>
        </div>
        <div class="col-lg-offset-3 col-lg-9">
            <button type="submit" class="btn btn-primary btnSubmit" disabled>Create</button>
        </div>
    </div>
    <div class="col-lg-6">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Slope, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.Slope, New With {.class = "form-control", .placeholder = "Optional"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.PointofDip, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.PointofDip, New With {.class = "form-control", .placeholder = "Optional"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.HopperVolume, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.HopperVolume, New With {.class = "form-control", .placeholder = "Optional"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.DipHeightBelowBase, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.DipHeightBelowBase, New With {.class = "form-control", .placeholder = "Optional"})
            </div>
        </div>
    </div>

</div>