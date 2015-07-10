var data;

$.ajax({
    type: "post",
    url: "/Books/GetAll",
    dataType: "json"
}).done(function (_data) {
    data = _data
    var count = Object.keys(data).length
    for (var i = 0; i < count; i++) {

        var epochValue = parseInt(data[i]['PublishDate'].replace('/Date(', '').replace(")/", ""));
        var utcDateVal = new Date(epochValue);
        var actualDate = new Date(utcDateVal.getTime() + (utcDateVal.getTimezoneOffset() * 60 * 1000))
        data[i]['PublishDate'] = actualDate.getFullYear() + " / " + (actualDate.getMonth() + 1) + " / " + (actualDate.getUTCDate() + 1);
        if (data[i]["Cover"] != null) {
            data[i]["Cover"] = '<img src="' + data[i]["Cover"] + '">'
        }
        else {
            data[i]["Cover"] = '<img class="img-responsive" src="http://i.imgur.com/sJ3CT4V.gif" height=100>'
        }
        
    }

    
    $('#test').DataTable({
        data: data,
        "columns": [
        { "data": "Cover" },
        { "data": "Name" },
        { "data": "PublishDate" },
        { "data": "Author" }
        ],
        columnDefs: [{ "width": "4%", "targets": 0, "orderable": false }],
        "bFilter": false,
        responsive: true
    });
});

