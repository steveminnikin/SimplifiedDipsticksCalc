    
<br />
<form id="submitForm" method="post">
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
        <!-- Nav tabs -->
        <ul class="nav nav-pills nav-justified" role="tablist">
            <li role="presentation" class="active"><a href="#horizDishEnds" aria-controls="horizDishEnds" role="tab" data-toggle="tab">Horizontal Cylindrical Dished Ends</a></li>
            <li role="presentation"><a href="#horizFlatEnds" aria-controls="horizFlatEnds" role="tab" data-toggle="tab">Horizontal Cylindrical Flat Ends</a></li>
            <li role="presentation"><a href="#rectangular" aria-controls="rectangular" role="tab" data-toggle="tab">Rectangular</a></li>
            <li role="presentation"><a href="#vertCyl" aria-controls="vertCyl" role="tab" data-toggle="tab">Vertical Cylindrical</a></li>
            <li role="presentation"><a href="#ellipt" aria-controls="ellipt" role="tab" data-toggle="tab">Elliptical</a></li>
        </ul>
        <br />
        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="horizDishEnds">
                @Html.Partial("_HorizDishEndsPartial")
            </div>
            <div role="tabpanel" class="tab-pane" id="horizFlatEnds">
                @Html.Partial("_HorizFlatEndsPartial")
            </div>
            <div role="tabpanel" class="tab-pane" id="rectangular">
                @Html.Partial("_RectangularPartial")
            </div>
            <div role="tabpanel" class="tab-pane" id="vertCyl">
                @Html.Partial("_VertCylPartial")
            </div>
            <div role="tabpanel" class="tab-pane" id="ellipt">
                @Html.Partial("_EllipticalPartial")
            </div>
        </div>
    </div>
</form>