@Code
    ViewBag.Title = "Error - Dipsticks Calculator"
End Code

<div class="container body-content">
    <div class="row" style="margin-top: 50px;">
        <div class="col-md-8 col-md-offset-2">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <span class="glyphicon glyphicon-exclamation-sign"></span>
                        Oops! Something Went Wrong
                    </h3>
                </div>
                <div class="panel-body">
                    <h4>We encountered an unexpected error</h4>
                    <p>We apologize for the inconvenience. An error occurred while processing your request.</p>

                    <hr />

                    <h5><strong>What you can do:</strong></h5>
                    <ul>
                        <li>Check that all input values are valid numbers</li>
                        <li>Ensure all required fields are filled in</li>
                        <li>Verify that dimensions are positive values</li>
                        <li>Try refreshing the page and submitting again</li>
                    </ul>

                    <div style="margin-top: 30px;">
                        <a href="@Url.Action("Index", "Home")" class="btn btn-primary btn-lg">
                            <span class="glyphicon glyphicon-home"></span> Return to Home
                        </a>
                        <button onclick="window.history.back();" class="btn btn-default btn-lg">
                            <span class="glyphicon glyphicon-arrow-left"></span> Go Back
                        </button>
                    </div>
                </div>
                <div class="panel-footer">
                    <small class="text-muted">
                        If this problem persists, please contact Dipsticks Engineering Services Ltd for assistance.
                    </small>
                </div>
            </div>
        </div>
    </div>
</div>
