@ModelType Tank
<div class="form-horizontal">
    <div class="form-group">
        <label for="Name" class="col-lg-3 control-label">Name</label>
        <div class="col-lg-9">
            <input id="Name" type="text" placeholder="Client Name" class="form-control">
        </div>
    </div>
    <div class="form-group">
        <label for="Ref" class="col-lg-3 control-label">Reference</label>
        <div class="col-lg-9"><input id="Ref" type="text" placeholder="Their Reference" class="form-control"></div>
    </div>
    <div class="form-group">
        <label for="Notes" class="col-lg-3 control-label">Notes</label>
        <div class="col-lg-9"><input id="Notes" type="text" placeholder="Optional" class="form-control"></div>
    </div>
    <div class="form-group">
        <label for="Ref" class="col-lg-3 control-label">Tank Ref</label>
        <div class="col-lg-9"><input id="tankRef" type="text" placeholder="Optional" class="form-control"></div>
    </div>
    <div class="form-group">
        <label for="ourRef" class="col-lg-3 control-label">Chart No</label>
        <div class="col-lg-9"><input id="ourRef" type="text" placeholder="Our Reference" class="form-control"></div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label" for="Date">Date</label>
        <div class="col-lg-9">
            <input id="Date" type="date" value=@System.DateTime.Today class="form-control" />
        </div>
    </div>
</div>


