<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Dipsticks Calculator", "Index", "Home", New With {.area = ""}, New With {Key .[class] = "navbar-brand"})
                @Html.ActionLink("Tools and References", "Index", "Tools", New With {.area = ""}, New With {Key .[class] = "navbar-brand"})
            </div>
        </div>
    </div>
        @RenderBody()
        <hr />
        <footer>
            <div class="container">
                <p>&copy; @DateTime.Now.Year - Dipsticks Engineering Services Ltd</p>
            </div>
        </footer>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
    <script type="text/javascript" src="~/Scripts/custom.js"></script>
</body>
</html>
