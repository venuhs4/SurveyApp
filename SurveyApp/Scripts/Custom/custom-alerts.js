warningAlert = function (message, duration) {
    var alertId = new Date().getTime();
    var alertDiv = '<div class="alert alert-warning" id="id' + alertId + '"> ' +
     ' <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> ' +
     ' <strong>Warning!</strong> ' + message +
   ' </div>';
    addAlert(alertDiv, alertId, duration);
};

successAlert = function (message, duration) {
    var alertId = new Date().getTime();
    var alertDiv = '<div class="alert alert-success" id="id' + alertId + '"> ' +
     ' <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> ' +
     ' <strong>Success!</strong> ' + message +
   ' </div>';
    addAlert(alertDiv, alertId, duration);
};

dangerAlert = function (message, duration) {
    var alertId = new Date().getTime();
    var alertDiv = '<div class="alert alert-danger" id="id' + alertId + '"> ' +
     ' <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> ' +
     ' <strong>Danger!</strong> ' + message +
   ' </div>';
    addAlert(alertDiv, alertId, duration);
};

infoAlert = function (message, duration) {
    var alertId = new Date().getTime();
    var alertDiv = '<div class="alert alert-info" id="id' + alertId + '"> ' +
     ' <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> ' +
     ' <strong>Info!</strong> ' + message +
   ' </div>';
    addAlert(alertDiv, alertId, duration);
};

function addAlert(alert, id, duration)
{
    $('#alertsArea').append(alert);
    if (!duration || duration == 0) {
        duration = 5000;
    }
    $("#id" + id).fadeTo(duration, 500).slideUp(500, function () {
        $("#id" + id).alert('close');
    });
};

httpError = function (res) {
    warningAlert("Error Getting data. " + JSON.stringify(error));
};