@ModelType SimplifiedDipsticksCalc.HorizFlatEnds
<div class="form-horizontal">
    <div class="col-lg-6">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.FlatDiameter, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.FlatDiameter, New With {.class = "form-control"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.FlatLength, New With {.class = "col-lg-3 control-label"})
            <div class="col-lg-6">
                @Html.TextBoxFor(Function(model) model.FlatLength, New With {.class = "form-control"})
            </div>
        </div>  
    <div class="col-lg-offset-3 col-lg-9">
        <button id="btnSubmit" type="submit" class="btn btn-primary btnSubmit">Create</button>
    </div>
    </div>
    <div class="col-lg-6">
        For tilted flat end tanks just use the dished end calculator and set the straight length and overall length the same.
    </div>
</div>
</form>