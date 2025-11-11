<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Professional dipstick calibration calculator for rectangular, cylindrical, elliptical, and dished end tanks. Generate accurate volume-to-height conversion tables for tank calibration." />
    <meta name="keywords" content="dipstick calculator, tank calibration, volume calculation, height conversion, cylindrical tank, rectangular tank, elliptical tank, dished ends, tank measurement" />
    <meta name="author" content="Dipsticks Engineering Services Ltd" />
    <meta name="robots" content="index, follow" />
    <title>@ViewBag.Title</title>
    <link rel="icon" type="image/png" href="~/favicon.png" />
    <link rel="apple-touch-icon" href="~/DipsticksBlue50.png" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous">
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body class="container">
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Dipsticks Calculator", "Index", "Home", New With {.area = ""}, New With {Key .[class] = "navbar-brand"})
            </div>
        </div>
    </div>
    <div class="container" style="margin-top: 70px;">
        @If IsSectionDefined("Breadcrumb") Then
            @<ol class="breadcrumb hidden-print">
                <li><a href="@Url.Action("Index", "Home")"><span class="glyphicon glyphicon-home"></span> Home</a></li>
                @RenderSection("Breadcrumb", required:=False)
            </ol>
        End If
    </div>
    @RenderBody()

    <!-- Loading Spinner Overlay -->
    <div id="loading-overlay">
        <div class="spinner-container">
            <div class="spinner"></div>
            <div class="spinner-text">Calculating...</div>
        </div>
    </div>

    <hr />
    <footer class="hidden-print">
        <div>
            <p>&copy; @DateTime.Now.Year - Dipsticks Engineering Services Ltd</p>
        </div>
    </footer>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/js/bootstrap.min.js" integrity="sha384-aJ21OjlMXNL5UyIl/XNwTMqvzeRMZH2w8c5cRVpzpU8Y5bApTppSuUkhZXN0VxHd" crossorigin="anonymous"></script>
    @RenderSection("scripts", required:=False)
    <script type="text/javascript" src="~/Scripts/custom.js"></script>
</body>
</html>
