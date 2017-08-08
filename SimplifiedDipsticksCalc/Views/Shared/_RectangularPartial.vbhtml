@ModelType SimplifiedDipsticksCalc.Rectangular

    <div class="form-horizontal">
        <div class="col-lg-6">
            <div class="form-group ">
                @Html.LabelFor(Function(model) model.Length, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.Length, New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.Length, "", New With {.class = "text-danger", .placeholder = "MMs"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Width, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.Width, New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.Width, "", New With {.class = "text-danger", .placeholder = "MMs"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Height, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.Height, New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.Height, "", New With {.class = "text-danger", .placeholder = "MMs"})
                </div>
            </div>
            <div class="col-lg-offset-3 col-lg-9">
                <button type="submit" class="btn btn-primary btnSubmit">Create</button>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Tilt, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.Tilt, New With {.class = "form-control", .placeholder = "Optional"})
                    @Html.ValidationMessageFor(Function(model) model.Tilt, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.dipPoint, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.dipPoint, New With {.class = "form-control", .placeholder = "Optional"})
                    @Html.ValidationMessageFor(Function(model) model.dipPoint, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.hopperVolume, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.hopperVolume, New With {.class = "form-control", .placeholder = "Optional"})
                    @Html.ValidationMessageFor(Function(model) model.hopperVolume, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.dipHeightBelowBase, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.dipHeightBelowBase, New With {.class = "form-control", .placeholder = "Optional"})
                    @Html.ValidationMessageFor(Function(model) model.dipHeightBelowBase, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>
        
    </div>
</form>