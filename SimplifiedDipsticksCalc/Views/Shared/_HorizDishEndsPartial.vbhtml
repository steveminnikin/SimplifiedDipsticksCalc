@ModelType SimplifiedDipsticksCalc.HorizDishEnds
    <div class="form-horizontal">
        <div class="col-lg-6">
            <div class="form-group">
                @Html.LabelFor(Function(model) model.DishDiameter, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.DishDiameter, New With {.class = "form-control", .placeholder = "MMs"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.stLength, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.stLength, New With {.class = "form-control", .placeholder = "MMs"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.OvLength, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.OvLength, New With {.class = "form-control", .placeholder = "MMs"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.DishEndRad, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.DishEndRad, New With {.class = "form-control", .placeholder = "MMs"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.KnuckleRad, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.KnuckleRad, New With {.class = "form-control", .placeholder = "MMs"})
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
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(model) model.dipPoint, New With {.class = "col-lg-3 control-label"})
                <div class="col-lg-6">
                    @Html.TextBoxFor(Function(model) model.dipPoint, New With {.class = "form-control", .placeholder = "Optional"})
                </div>
            </div>
        </div>
    </div>
</form>