
//Declare a global API object
window.API = {}
window.API.Status = false;

//Perform a ping check towards the API.
function pingCheck() {

    var url = "/Api/Ping";

    $.ajax(url)
        .done(function () {
            window.API.Status = true;
        })
        .fail(function () {
            window.API.Status = false;
        })
        .always(function () {
            $("#api-check").css("color", window.API.Status ? "green" : "red");
            $("#api-check").html(window.API.Status ? "API ONLINE" : "API OFFLINE");
        });
}

pingCheck(); //Run immediately
setInterval(pingCheck, 5000); //Ping the API every n ms.

///////////////////////////// 
// PAGE SPECIFIC SCRIPTING //
/////////////////////////////

var path = window.location.pathname;
var script = "";

switch (path) {
    case "/Device/Add":
        script = "/js/page-device-add.js";
        break;
}

if (script)
    $.getScript(script);
