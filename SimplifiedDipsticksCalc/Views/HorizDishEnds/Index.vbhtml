@ModelType SimplifiedDipsticksCalc.HorizDishEnds
@Code
    ViewBag.Title = "Horizontal Cylindrical Dished Ends Tank Calculator"
End Code

<br />
<div class="row">
    <div class="col-md-12">
        <h2>Horizontal Cylindrical Dished Ends Tank Calibration</h2>
        <p class="lead">Calculate dipstick calibration tables for horizontal cylindrical tanks with dished ends.</p>
    </div>
</div>

@Html.ValidationSummary(False, "", New With {.class = "alert alert-danger"})

<form action="@Url.Action("Calculate", "HorizDishEnds")" method="post">
    <div class="row">
        <div class="col-md-6">
            @Html.Partial("_ClientPartial")
        </div>
        <div class="col-md-6">
            @Html.Partial("_DimensionsPartial")
        </div>
    </div>
    <hr />
    <div class="row">
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
                    <button type="submit" class="btn btn-primary btn-lg">Calculate</button>
                    <a href="@Url.Action("Index", "Home")" class="btn btn-default btn-lg">Back to Tank Selection</a>
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
    </div>
</form>

@section Scripts
    <script>
        $(document).ready(function() {
            // Form validation on submit
            $('form').submit(function(e) {
                if (!FormValidator.validate()) {
                    e.preventDefault();
                    alert('Please correct the validation errors before submitting.');
                    return false;
                }
            });
        });
    </script>
End Section
