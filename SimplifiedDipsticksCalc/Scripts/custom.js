$(document).ready(function () {
    var count = 0;
    var Data = [];
    var retrievedData = [];

    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        sessionStorage.setItem('activeTab', $(e.target).attr('href'));
    });
    var activeTab = sessionStorage.getItem('activeTab');
    if (activeTab) {
        $('a[href="' + activeTab + '"]').tab('show');
    }

    if (!sessionStorage.getItem("hasCodeRunBefore")) {
        try {
            sessionStorage.setItem("hasCodeRunBefore", true);
        } catch (e) {
            console.error('SessionStorage not available:', e);
        }
    }
    else {
        try {
            $('form input').each(function (i, e) {
                retrievedData = JSON.parse(sessionStorage.getItem('Data'));
                if (retrievedData && retrievedData[i]) {
                    e.value = retrievedData[i].value;
                }
            });
        } catch (e) {
            console.error('Failed to restore form data:', e);
        }
    }

    $('.btnSubmit').click(function () {
        var Client = {
            Name: $('#Name').val(),
            Ref: $('#Ref').val(),
            Notes: $('#Notes').val(),
            Date: $('#Date').val(),
            TankRef: $('#tankRef').val(),
            OurRef: $('#ourRef').val()
        };

        $('form input').each(function (i, e) {
            Data[i] = { id: e.id, value: e.value };
        });

        try {
            sessionStorage.setItem('Data', JSON.stringify(Data));
            sessionStorage.setItem('Client', JSON.stringify(Client));
        } catch (e) {
            console.error('Failed to save form data:', e);
            alert('Could not save form data. Your browser may have cookies/storage disabled.');
        }

        //set the post action on the tab form
        if ($('#horizDishEnds').hasClass('active')) {
            $('#submitForm').attr('action', '/HorizDishEnds/Calculate');
        }
        if ($('#horizFlatEnds').hasClass('active')) {
            $('#submitForm').attr('action', '/HorizFlatEnds/Calculate');
        }
        if ($('#rectangular').hasClass('active')) {
            $('#submitForm').attr('action', '/Rectangular/Calculate');
        }
        if ($('#vertCyl').hasClass('active')) {
            $('#submitForm').attr('action', '/VertCyl/Calculate');
        }
        if ($('#ellipt').hasClass('active')) {
            $('#submitForm').attr('action', '/Elliptical/Calculate');
        }

    }); //btnSubmit.click  

    $('#btnEdit').click(function () {

        if (isEven(count)) {
            document.body.contentEditable = true;
        }
        if (!isEven(count)) {
            document.body.contentEditable = false;
        }
        count += 1;
    });

    $('#btnClient').click(function () {
        try {
            var retrievedClient = JSON.parse(sessionStorage.getItem('Client'));
            if (!retrievedClient) {
                return;
            }

            var $clientTitle = $('#clientTitle');
            var $clientData = $('#clientData');
            if (retrievedClient.Name) {
                $clientTitle.append($('<dt>').text('Client'));
                $clientData.append($('<span>').text(retrievedClient.Name)).append('<br />');
            }
            if (retrievedClient.Ref) {
                $clientTitle.append($('<dt>').text('Ref'));
                $clientData.append($('<span>').text(retrievedClient.Ref)).append('<br />');
            }
            if (retrievedClient.Notes) {
                $clientTitle.append($('<dt>').text('Notes'));
                $clientData.append($('<span>').text(retrievedClient.Notes)).append('<br />');
            }

            $clientTitle.append($('<dt>').text('Date'));
            $clientData.append($('<span>').text(retrievedClient.Date)).append('<br />');

            if (retrievedClient.TankRef) {
                $clientTitle.append($('<dt>').text('Tank Ref'));
                $clientData.append($('<span>').text(retrievedClient.TankRef)).append('<br />');
            }
            if (retrievedClient.OurRef) {
                $clientTitle.append($('<dt>').text('Chart No'));
                $clientData.append($('<span>').text(retrievedClient.OurRef)).append('<br />');
            }

            //Toggle display of the client info on the screen
            var $clientInfo = $('#clientInfo');
            $clientInfo.toggleClass('visible-print');
        } catch (e) {
            console.error('Failed to display client information:', e);
        }
    });

    var isEven = function (someNumber) {
        return someNumber % 2 === 0 ? true : false;
    };

    $('#btnNote').click(function () {
        var myNote = prompt("Add a note to the chart", "Add 6mm to dip reading before using chart to allow for striker plate");
        $('#chartNote').text(myNote);
    });
});

  
