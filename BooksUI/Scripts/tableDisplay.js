document.onload = LoadTable();

function LoadTable() {
    $('#table').dataTable({
        "bLengthChange": false,
        "autoWidth": false,
        columnDefs: [{ "width": "15%", "targets": 0 }],
        "bFilter": false,
        responsive: true,
    });
    $("#publish").validate({
        rules: {
            Date: {
                required: true,
                number: true
            },
            Name: {
                required: true
            }
        },
        success: "bg-success",
        errorClass: "bg-danger",
        submitHandler: function () { $("#publish"),submit() },
    });
    $("#Cover").fileinput({ 'showUpload': false, 'showPreview': false, 'showCaption': false });
    setInterval(shakeErrors, 2000);
}

jQuery.fn.shake = function (intShakes, intDistance, intDuration) {
    this.each(function () {
        $(this).css("position", "relative");
        for (var x = 1; x <= intShakes; x++) {
            $(this).animate({ left: (intDistance * -1) }, (((intDuration / intShakes) / 4)))
        .animate({ left: intDistance }, ((intDuration / intShakes) / 2))
        .animate({ left: 0 }, (((intDuration / intShakes) / 4)));
        }
    });
    return this;
};


function shakeErrors() {
    $("input.bg-danger").shake(3, 5, 500)
    
}
