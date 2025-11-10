@ModelType SimplifiedDipsticksCalc.VertCyl
@Code
    ViewBag.Title = "Vertical Cylindrical Tank Calculator"
End Code

<br />
<div class="row">
    <div class="col-md-12">
        <h2>Vertical Cylindrical Tank Calibration</h2>
        <p class="lead">Calculate dipstick calibration tables for vertical cylindrical tanks.</p>
    </div>
</div>

@Html.ValidationSummary(False, "", New With {.class = "alert alert-danger"})

<form action="@Url.Action("Calculate", "VertCyl")" method="post">
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
                    <button type="submit" class="btn btn-primary btn-lg btnSubmit">Calculate</button>
                    <button type="button" class="btn btn-warning btn-lg btnClear">Clear Form</button>
                    <a href="@Url.Action("Index", "Home")" class="btn btn-default btn-lg">Back to Tank Selection</a>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.DishEndDepth, New With {.class = "col-lg-3 control-label"})
                    <div class="col-lg-6">
                        @Html.TextBoxFor(Function(model) model.DishEndDepth, New With {.class = "form-control", .placeholder = "Optional"})
                    </div>
                </div>
                <div class="col-lg-12">
                    <p>For dished bases use overall length of tank (including base).</p>
                    <p><em>Adjustment</em> is volume outside of dished end i.e. the volume <strong>not</strong> part of the tank</p>
                    <p>To calculate volume of Adjustment:</p>
                    <ul>
                        <li>Use horizontal flat end calculator with OL equal to .</li>
                        <li>Use the horizontal dished calculator with SL and OL equal to vert tank with dished base and dished top</li>
                        <li>Subtract one from the other and then divide by 2.</li>
                    </ul>
                    <p>
                        <a href="http://secure.czltd.com/Engineering/dh_calculate.html" target="_blank">Dished End Calculator</a><br />
                        <a href="https://checalc.com/calc/vessel.html" target="_blank">Volume Calculator</a>
                    </p>
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

@section Breadcrumb
    <li class="active">Vertical Cylindrical Tank</li>
End Section
