$(document).ready(function () {
    var count = 0;


    $('.btnSubmit').click(function () {
        var Client = {
            Name: $('#clientName').val(),
            Ref: $('#clientRef').val(),
            Notes: $('#clientNotes').val(),
            Date: $('#clientDate').val(),
            TankRef: $('#tankRef').val(),
            OurRef: $('#ourRef').val()
        };

        localStorage.setItem('Client', JSON.stringify(Client));

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
    }); //btnSubmit.click  

    $('#btnEdit').click(function () {

        if (isEven(count)) {
            document.body.contentEditable = true;
        }
        if (!isEven(count)) {
            document.body.contentEditable = false;
        }

        count = count + 1;

    });

    $('#btnClient').click(function () {
        var retrievedClient = JSON.parse(localStorage.getItem('Client'));

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

        //$('#Client').text(retrievedClient.Name);
        //$('#Ref').text(retrievedClient.Ref);
        //$('#Notes').text(retrievedClient.Notes);
        //$('#Date').text(retrievedClient.Date);
        //$('#TankRef').text(retrievedClient.TankRef);
        //$('#OurRef').text(retrievedClient.OurRef);

        
        //Toggle display of the client info on the scree
        var $clientInfo = $('#clientInfo');
        $clientInfo.toggleClass('visible-print');


    });

    var isEven = function (someNumber) {
        return (someNumber % 2 === 0) ? true : false;
    };

    $('#btnNote').click(function () {
        var myNote = prompt("Add a note to the chart", "Add 6mm to dip reading before using chart to allow for striker plate");
        $('#chartNote').text(myNote);
    });

});