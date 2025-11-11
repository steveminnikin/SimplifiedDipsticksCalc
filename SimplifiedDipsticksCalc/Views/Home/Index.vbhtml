@Code
    ViewBag.Title = "Tank Calibration Calculator"
End Code

<div class="row" style="margin-bottom: 15px;">
    <div class="col-md-12">
        <h2>Select Tank Type</h2>
    </div>
</div>

<div class="row">
    <div class="col-md-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-minus"></span> Horizontal Cylindrical Dished Ends
                </h3>
            </div>
            <div class="panel-body">
                <p>For horizontal cylindrical tanks with dished (hemispherical) end caps.</p>
                <p><strong>Parameters:</strong> Diameter, Straight Length, Overall Length, Dished End Radius, Knuckle Radius</p>
                <a href="@Url.Action("Index", "HorizDishEnds")" class="btn btn-primary btn-block btn-lg">
                    Calculate <span class="glyphicon glyphicon-arrow-right"></span>
                </a>
            </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-minus"></span> Horizontal Cylindrical Flat Ends
                </h3>
            </div>
            <div class="panel-body">
                <p>For horizontal cylindrical tanks with flat end caps.</p>
                <p><strong>Parameters:</strong> Diameter, Length</p>
                <a href="@Url.Action("Index", "HorizFlatEnds")" class="btn btn-info btn-block btn-lg">
                    Calculate <span class="glyphicon glyphicon-arrow-right"></span>
                </a>
            </div>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-6">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-stop"></span> Rectangular
                </h3>
            </div>
            <div class="panel-body">
                <p>For rectangular tanks and containers.</p>
                <p><strong>Parameters:</strong> Length, Width, Height, Slope (optional)</p>
                <a href="@Url.Action("Index", "Rectangular")" class="btn btn-success btn-block btn-lg">
                    Calculate <span class="glyphicon glyphicon-arrow-right"></span>
                </a>
            </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="panel panel-warning">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-record"></span> Vertical Cylindrical
                </h3>
            </div>
            <div class="panel-body">
                <p>For vertical cylindrical tanks standing upright.</p>
                <p><strong>Parameters:</strong> Diameter, Height, Dished End Depth (optional)</p>
                <a href="@Url.Action("Index", "VertCyl")" class="btn btn-warning btn-block btn-lg">
                    Calculate <span class="glyphicon glyphicon-arrow-right"></span>
                </a>
            </div>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-6 col-md-offset-3">
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-adjust"></span> Elliptical
                </h3>
            </div>
            <div class="panel-body">
                <p>For tanks with elliptical cross-sections.</p>
                <p><strong>Parameters:</strong> Major Axis, Minor Axis, Length</p>
                <a href="@Url.Action("Index", "Elliptical")" class="btn btn-danger btn-block btn-lg">
                    Calculate <span class="glyphicon glyphicon-arrow-right"></span>
                </a>
            </div>
        </div>
    </div>
</div>

<div class="row" style="margin-top: 40px;">
    <div class="col-md-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">About This Calculator</h3>
            </div>
            <div class="panel-body">
                <h4>Features:</h4>
                <ul>
                    <li>Multiple tank geometries supported</li>
                    <li>Workshop format (fixed volume increments) or Chart format (fixed height increments)</li>
                    <li>Multiple unit systems: Litres/Millimetres, Gallons/Inches, Cubic Metres/Millimetres</li>
                    <li>CSV export for CNC engraving machines</li>
                    <li>Calculation history and configuration management</li>
                    <li>Client-side validation for accurate data entry</li>
                </ul>
                <h4>How It Works:</h4>
                <ol>
                    <li>Select your tank type from the options above</li>
                    <li>Enter tank dimensions and select your preferred units</li>
                    <li>Choose between Workshop or Chart format</li>
                    <li>Specify increment values</li>
                    <li>Click Calculate to generate your dipstick calibration table</li>
                </ol>
            </div>
        </div>
    </div>
</div>
