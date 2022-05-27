/**
 * Preliminary ajax request to fetch device vendors from backend and feed select.
 * This is an async call that will be fired immediately on document ready.
 */
$.ajax("/Api/Vendors").done(function (data) {

    //Truncate the select elem
    $("#device-vendor-fetch").empty();

    //Check for no vendors in ajax request
    if (data.length == 0) {
        $('#device-vendor-fetch').append($('<option>', { value: "", text: 'Kunne ikke hente leverandører' }));
        return;
    }

    //Feed select 
    $('#device-vendor-fetch').append($('<option>', { value: "", text: 'Klik her for at vælge leverandør' }));

    $.each(data, function (id, vendor) {
        $('#device-vendor-fetch').append($('<option>', { value: vendor.id, text: vendor.name }));
    });

})
.fail(function () {
    //Fail handling.
});

/**
 * Preliminary ajax request to fetch rooms from backend and feed select.
 * This is an async call that will be fired immediately on document ready.
 */
$.ajax("/Api/Rooms").done(function (data) {

    //Truncate the select elem
    $("#device-room-fetch").empty();

    //Check for no vendors in ajax request
    if (data.length == 0) {
        $('#device-room-fetch').append($('<option>', { value: "", text: 'Kunne ikke hente værelser' }));
        return;
    }

    //Feed select 
    $('#device-room-fetch').append($('<option>', { value: "", text: 'Klik her for at vælge værelse' }));

    $.each(data, function (id, room) {
        $('#device-room-fetch').append($('<option>', { value: room.id, text: room.name }));
    });

    })
 .fail(function () {
    //Fail handling.
});


/**
 * Bind a change event on the vendor dropdown.
 * Once selected load device models attached to this vendor.
 */
$('#device-vendor-fetch').on('change', function () {

    if ($(this).val() == "") {
        $("#device-model-fetch").trigger("model-reset");
        return;
    }

    console.log("/Api/Devices/" + $(this).val());

    $.ajax("/Api/Devices/" + $(this).val()).done(function (data) {

        //Truncate the select elem
        $("#device-model-fetch").empty();

        //Check for no vendors in ajax request
        if (data.length == 0) {
            $('#device-model-fetch').append($('<option>', { value: "", text: 'Kunne ikke hente modeltyperne' }));
            return;
        }

        //Feed select 
        $('#device-model-fetch').append($('<option>', { value: "", text: 'Klik her for at vælge din model' }));

        $.each(data, function (id, device) {
            $('#device-model-fetch').append($('<option>', { value: device.id, text: device.name }));
        });
        
    })
    .fail(function () {
        //Fail handling.
    });

    $("#device-submit").trigger("validate-form");
});


/**
 * Bind a change event on the model dropdown.
 */
$('#device-model-fetch').on('change', function () {
    $("#device-submit").trigger("validate-form");
});

/**
 * Bind a change event on the room dropdown.
 */
$('#device-room-fetch').on('change', function () {
    $("#device-submit").trigger("validate-form");
});

//Jquery function binds
$("#device-model-fetch").on("model-reset", function () {
    $("#device-model-fetch").empty();
    $("#device-model-fetch").append($("<option>", { value: "", text: "Afventer valg af leverandør .." }));
});

$("#device-submit").on("validate-form", function () {
    var disabled = false;
    if ($("#device-vendor-fetch").val() == "") disabled = true;
    if ($("#device-model-fetch").val() == "") disabled = true;
    if ($("#device-room-fetch").val() == "") disabled = true;

    if (disabled) {
        $("#device-submit").attr("disabled", "disabled");
    } else {
        $("#device-submit").removeAttr("disabled");
    }
});

//Reset the model select 
$("#device-model-fetch").trigger("model-reset");
$("#device-submit").trigger("validate-form");