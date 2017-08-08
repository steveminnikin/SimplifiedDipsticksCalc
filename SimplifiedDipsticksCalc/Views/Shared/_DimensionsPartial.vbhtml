@ModelType SimplifiedDipsticksCalc.Tank
<div class="form-horizontal">
            <div class="form-group">
                <label class="col-lg-3 control-label">Format</label>
                <div class="col-lg-9">
                    <div class="radio"><label> @Html.RadioButtonFor(Function(model) model.regDip, False, New With {.checked = True}) Workshop</label></div>
                    <div class="radio"><label> @Html.RadioButtonFor(Function(model) model.regDip, True) Chart</label></div>
                </div>
            </div>

            <div class="form-group">
                <label class="col-lg-3 control-label">Dimensions</label>
                <div class="col-lg-9">
                    @Html.EnumDropDownListFor(Function(model) model.Dimensions, New With {.class = "btn btn-default dropdown-toggle"})
                </div>
            </div>


            <div class="form-group">
                <label class="col-lg-3 control-label">Engraving?</label>
                <div class="col-lg-9">
                    @Html.CheckBoxFor(Function(model) model.EngraveCode)
                </div>
            </div>

        <div class="form-group">
            <label class="col-lg-3 control-label">Adjustments</label>
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.Adjustments, New With {.class = "form-control"})
            </div>
        </div>
    <div class="form-group">
        @Html.LabelFor(Function(model) model.Increments, New With {.class = "col-lg-3 control-label"})
        <div class="col-lg-6">
            @Html.TextBoxFor(Function(model) model.Increments, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(model) model.Increments, "", New With {.class = "text-danger"})
        </div>
    </div>
    </div>

