var data, table
document.onload = LoadData()

function LoadData() {
    $.ajax({
        type: "post",
        url: "/Books/Index",
        dataType: "json"
    }).done(function (_data) {
        data = _data
        var count = Object.keys(data).length
        for (var i = 0; i < count; i++) {
            var date = parseInt(data[i]['PublishDate']);
            data[i]['PublishDate'] = date
            var cover = data[i]['Cover'];
            data[i]['Cover'] = '<div class="col-md-6"><a href="#" class="thumbnail">'
            if (cover != null) {
                data[i]["Cover"] += '<img src="' + cover + '">'

            }
            else {
                data[i]["Cover"] += '<img  src="http://i.imgur.com/sJ3CT4V.gif" height=100>'

            }

            data[i]["Cover"] += '</a></div>'
            data[i]['Edit'] = "<a href='/Books/Delete/" + data[i]['BookId'] + "'><span class='glyphicon glyphicon-trash' style='font-size: 24px; margin: 15px' aria-hidden='true'></span></a><a href='/Books/Edit/" + data[i]['BookId'] + "'><span class='glyphicon glyphicon-pencil' style='font-size: 24px; margin: 15px' aria-hidden='true'></span>"
        }
        $.ajax({
            type: "post",
            url: "/Authors/Index",
            dataType: "json"
        }).done(function (d) {
            var _count = Object.keys(d).length
            var select = '<select name="Author" class="form-control">'
            for (var i = 0; i < _count; i++) {
                for (var j = 0; j < count; j++) {
                    if (data[j]['AuthorId'] == i + 1) data[j]['Author'] = d[i]['Name'];

                }
                select += "<option value=" + i + ">" + d[i]["Name"] +"</option>"

            }
            select += '</select>'

            data[count] = { 'Cover': '<input type="file" class="file" data-preview-file-type="text" value="Upload cover" accept=".jpg, .jpeg, .png, .gif" name="Cover" id="Cover">', 'Name': "<input type='text' class='form-control' placeholder='Book title' name='Name'>", 'PublishDate': "<input type='text' class='form-control' placeholder='Book publish date' name='Date'>", 'Author': select }
            data[count]['Edit'] = "<button class='btn' type='submit'>Upload book</button>"
            LoadTable(data);
            $("#Cover").fileinput({ 'showUpload': false, 'showPreview': false, 'showCaption': false });

        })
    })
}
function ReloadData(title, author) {
    $.ajax({
        type: "post",
        url: "/Books/Index",
        dataType: "json",
        data: {"Title": title, "Author": author}
    }).done(function (_data) {
        data = _data
        count = Object.keys(data).length
        for (var i = 0; i < count; i++) {
            var date = parseInt(data[i]['PublishDate']);
            data[i]['PublishDate'] = date
            var cover = data[i]['Cover'];
            data[i]['Cover'] = '<div class="col-md-8"><a href="#" class="thumbnail">'
            if (cover != null) {
                data[i]["Cover"] += '<img src="' + cover + '">'

            }
            else {
                data[i]["Cover"] += '<img  src="http://i.imgur.com/sJ3CT4V.gif" height=100>'

            }
            data[i]["Cover"] += '</a></div>'
            data[i]['Edit'] = "<a href='/Books/Delete/" + data[i]['Name'] + "'><span class='glyphicon glyphicon-trash' style='font-size: 24px; margin: 15px' aria-hidden='true'></span></a><a href='/Books/Edit/" + data[i]['Name'] + "'><span class='glyphicon glyphicon-pencil' style='font-size: 24px; margin: 15px' aria-hidden='true'></span>"

        }
        $.ajax({
            type: "post",
            url: "/Authors/Index",
            dataType: "json"
        }).done(function (d) {
            _count = Object.keys(d).length
            var select = '<select name="Author" class="form-control">'
            for (var i = 0; i < _count; i++) {
                for (var j = 0; j < count; j++) {
                    if (data[j]['AuthorId'] == i + 1) data[j]['Author'] = d[i]['Name'];

                }
                select += "<option value=" + i + ">" + d[i]["Name"] + "</option>"

            }
            select += '</select>'

            data[count] = { 'Cover': '<input type="file" value="Upload cover" accept=".jpg, .jpeg, .png, .gif" name="Cover" id="Cover">', 'Name': "<input type='text' class='form-control' placeholder='Book title' name='Name'>", 'PublishDate': "<input type='text' class='form-control' placeholder='Book publish date' name='Date'>", 'Author': select }
            data[count]['Edit'] = "<button class='btn' type='submit'>Upload book</button>"
            ReloadTable(data);
            $("#Cover").fileinput({ 'showUpload': false, 'showPreview': false, 'showCaption': false });

        })
    })
}
function LoadTable() {
    table = $('#table').dataTable({
        data: data,
        "columns": [
        { "data": "Cover" },
        { "data": "Name" },
        { "data": "PublishDate" },
        { "data": "Author" },
        { "data": "Edit" }
        ],
        "bLengthChange": false,
        "autoWidth": false,
        columnDefs: [{ "width": "15%", "targets": 0 }],
        "bFilter": false,
        "bSort": false,
        responsive: true,
    });
}

function ReloadTable(data) {
    $('#table').dataTable().fnClearTable()
    $('#table').dataTable().fnAddData(data)
}

function search(action) {
    if (action == 'clear') {
        ReloadData();

    }
    else {
        var title = document.getElementById('Title').value;
        var author = document.getElementById('Author').value;
        ReloadData(title, author)

    }
}
