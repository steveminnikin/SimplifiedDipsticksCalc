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

        sessionStorage.setItem("hasCodeRunBefore", true);
    }
    else {
        $('form input').each(function (i, e) {

            retrievedData = JSON.parse(sessionStorage.getItem('Data'));
            e.value = retrievedData[i].value;
        });
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

        sessionStorage.setItem('Data', JSON.stringify(Data));
        sessionStorage.setItem('Client', JSON.stringify(Client));

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
        var retrievedClient = JSON.parse(sessionStorage.getItem('Client'));

        var $clientTitle = $('#clientTitle');
        var $clientData = $('#clientData');
        if (retrievedClient.Name) {
            $clientTitle.append($('<dt>Client<dt>'));
            $clientData.append($('<span>' + retrievedClient.Name + '<span><br />'));
        }
        if (retrievedClient.Ref) {
            $clientTitle.append($('<dt>Ref<dt>'));
            $clientData.append($('<span>' + retrievedClient.Ref + '<span><br />'));
        }
        if (retrievedClient.Notes) {
            $clientTitle.append($('<dt>Notes<dt>'));
            $clientData.append($('<span>' + retrievedClient.Notes + '<span><br />'));
        }

        $clientTitle.append($('<dt>Date<dt>'));
        $clientData.append($('<span>' + retrievedClient.Date + '<span><br />'));

        if (retrievedClient.TankRef) {
            $clientTitle.append($('<dt>Tank Ref<dt>'));
            $clientData.append($('<span>' + retrievedClient.TankRef + '<span><br />'));
        }
        if (retrievedClient.OurRef) {
            $clientTitle.append($('<dt>Chart No<dt>'));
            $clientData.append($('<span>' + retrievedClient.OurRef + '<span><br />'));
        }

        //Toggle display of the client info on the screen
        var $clientInfo = $('#clientInfo');
        $clientInfo.toggleClass('visible-print');

    });

    var isEven = function (someNumber) {
        return someNumber % 2 === 0 ? true : false;
    };

    $('#btnNote').click(function () {
        var myNote = prompt("Add a note to the chart", "Add 6mm to dip reading before using chart to allow for striker plate");
        $('#chartNote').text(myNote);
    });
});

  
