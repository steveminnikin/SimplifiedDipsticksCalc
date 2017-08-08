@ModelType Tank
<div class="form-horizontal">
    <div class="form-group">
        <label for="clientName" class="col-lg-3 control-label">Name</label>
        <div class="col-lg-9">
            <input id="clientName" type="text" placeholder="Client Name" class="form-control">
        </div>
    </div>
    <div class="form-group">
        <label for="clientRef" class="col-lg-3 control-label">Reference</label>
        <div class="col-lg-9"><input id="clientRef" type="text" placeholder="Their Reference" class="form-control"></div>
    </div>
    <div class="form-group">
        <label for="clientNotes" class="col-lg-3 control-label">Notes</label>
        <div class="col-lg-9"><input id="clientNotes" type="text" placeholder="Optional" class="form-control"></div>
    </div>
    <div class="form-group">
        <label for="clientRef" class="col-lg-3 control-label">Tank Ref</label>
        <div class="col-lg-9"><input id="tankRef" type="text" placeholder="Optional" class="form-control"></div>
    </div>
    <div class="form-group">
        <label for="ourRef" class="col-lg-3 control-label">Chart No</label>
        <div class="col-lg-9"><input id="ourRef" type="text" placeholder="Our Reference" class="form-control"></div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label" for="clientDate">Date</label>
        <div class="col-lg-9">
            <input id="clientDate" type="date" value=@System.DateTime.Today class="form-control" />
        </div>
    </div>
</div>


