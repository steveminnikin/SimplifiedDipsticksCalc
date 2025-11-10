@ModelType SimplifiedDipsticksCalc.HorizFlatEnds
@Code
    ViewBag.Title = "Horizontal Cylindrical Flat Ends Tank Calculator"
End Code

<br />
<div class="row">
    <div class="col-md-12">
        <h2>Horizontal Cylindrical Flat Ends Tank Calibration</h2>
        <p class="lead">Calculate dipstick calibration tables for horizontal cylindrical tanks with flat ends.</p>
    </div>
</div>

<form action="@Url.Action("Calculate", "HorizFlatEnds")" method="post">
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
                    @Html.LabelFor(Function(model) model.FlatDiameter, New With {.class = "col-lg-3 control-label"})
                    <div class="col-lg-6">
                        @Html.TextBoxFor(Function(model) model.FlatDiameter, New With {.class = "form-control", .placeholder = "MMs"})
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.FlatLength, New With {.class = "col-lg-3 control-label"})
                    <div class="col-lg-6">
                        @Html.TextBoxFor(Function(model) model.FlatLength, New With {.class = "form-control", .placeholder = "MMs"})
                    </div>
                </div>
                <div class="col-lg-offset-3 col-lg-9">
                    <button type="submit" class="btn btn-primary btn-lg">Calculate</button>
                    <a href="@Url.Action("Index", "Home")" class="btn btn-default btn-lg">Back to Tank Selection</a>
                </div>
            </div>
            <div class="col-lg-6">
                <div>
                    <h4>For tilted flat end tanks</h4>
                    <p>Use the dished end calculator and set the <em>Straight Length</em> and <em>Overall Length</em> the same.</p>
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
