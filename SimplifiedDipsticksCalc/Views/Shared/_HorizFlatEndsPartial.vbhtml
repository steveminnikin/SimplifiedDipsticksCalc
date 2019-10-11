@ModelType SimplifiedDipsticksCalc.HorizFlatEnds
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
        <button id="btnSubmit" type="submit" class="btn btn-primary btnSubmit">Create</button>
    </div>
    </div>
    <div class="col-lg-6">
        <div >
            <h4>For tilted flat end tanks</h4> 
            <p>Use the dished end calculator and set the <em>Straight Length</em> and <em>Overall Length</em> the same.</p>

        </div>
    </div>
</div>