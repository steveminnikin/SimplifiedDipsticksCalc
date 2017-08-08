<div class="pull-right visible-print">
    <img class="logo img" src="~/DipsticksBlue50.png" />
    <h3> Dipsticks Engineering</h3>
</div>

<div id="clientInfo" class="row visible-print">
    <div class="col-xs-2  text-right">
        <dl id="clientTitle"></dl>
    </div>
    <div id="clientData" class="col-xs-5">


    </div>
</div>
<div class="row ">
    <div class="col-md-10 ">
        <button class="btn btn-warning hidden-print" id="btnNote" onclick="">Chart Note</button>
        <button class="btn btn-warning hidden-print" id="btnClient">Show Client Details</button>
        <button class="btn btn-outline hidden-print" data-toggle="button" aria-pressed="false" id="btnEdit">Enable Editing</button>
        <p class="lead"><strong>The Tank is @Model.GetShape - Dimensions in @Model.GetLength</strong></p>
    </div>
</div>