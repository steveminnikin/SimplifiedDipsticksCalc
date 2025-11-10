<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
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
    <hr />
    <footer class="hidden-print">
        <div>
            <p>&copy; @DateTime.Now.Year - Dipsticks Engineering Services Ltd - For Best Results Use Microsoft Edge Browser</p>
        </div>
    </footer>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
    <script type="text/javascript" src="~/Scripts/custom.js"></script>
</body>
</html>
