var data, table
document.onload = LoadData()

function LoadData() {
    $.ajax({
        type: "post",
        url: "/Books/Index",
        dataType: "json"
    }).done(function (_data) {
        data = _data
        count = Object.keys(data).length
        for (var i = 0; i < count; i++) {
            var epochValue = parseInt(data[i]['PublishDate'].replace('/Date(', '').replace(")/", ""));
            var date = new Date(epochValue).toLocaleString();
            var _date = new Date(date);
            data[i]['PublishDate'] = _date.getFullYear()
            if (data[i]["Cover"] != null) {
                data[i]["Cover"] = '<img width="120" height="120" src="' + data[i]["Cover"] + '">'
            }
            else {
                data[i]["Cover"] = '<img  src="http://i.imgur.com/sJ3CT4V.gif" height=100>'
            }
        }
        $.ajax({
            type: "post",
            url: "/Authors/Index",
            dataType: "json"
        }).done(function (d) {
            _count = Object.keys(d).length
            var select = '<select name="Author">'
            for (var i = 0; i < _count; i++) {
                for (var j = 0; j < count; j++) {
                    if (data[j]['AuthorId'] == i) data[j]['Author'] = d[i]['Name'];
                }
                select += "<option value=" + i + ">" + d[i]["Name"] +"</option>"
            }
            select += '</select>'

            data[count] = { 'Cover': '<input type="file" value="Upload cover" accept=".jpg, .jpeg, .png, .gif" name="Cover" id="Cover">', 'Name': "<input type='text' placeholder='Book title' name='Name'>", 'PublishDate': "<input type='text' placeholder='Book publish date' name='Date'>", 'Author': select }
            LoadTable(data);
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
            var epochValue = parseInt(data[i]['PublishDate'].replace('/Date(', '').replace(")/", ""));
            var date = new Date(epochValue).toLocaleString();
            var _date = new Date(date);
            data[i]['PublishDate'] = _date.getFullYear()
            if (data[i]["Cover"] != null) {
                data[i]["Cover"] = '<img width="120" height="120" src="' + data[i]["Cover"] + '">'
            }
            else {
                data[i]["Cover"] = '<img  src="http://i.imgur.com/sJ3CT4V.gif" height=100>'
            }
        }
        $.ajax({
            type: "post",
            url: "/Authors/Index",
            dataType: "json"
        }).done(function (d) {
            _count = Object.keys(d).length
            var select = '<select name="Author">'
            for (var i = 0; i < _count; i++) {
                for (var j = 0; j < count; j++) {
                    if (data[j]['AuthorId'] == i) data[j]['Author'] = d[i]['Name'];
                }
                select += "<option value=" + i + ">" + d[i]["Name"] +"</option>"
            }
            select += '</select>'

            data[count] = { 'Cover': '<input type="file" value="Upload cover" accept=".jpg, .jpeg, .png, .gif" name="Cover" id="Cover">', 'Name': "<input type='text' placeholder='Book title' name='Name'>", 'PublishDate': "<input type='text' placeholder='Book publish date' name='Date'>", 'Author': select }
            ReloadTable(data);
        })

        
    })
}
function LoadTable() {
    table = $('#test').dataTable({
        data: data,
        "columns": [
        { "data": "Cover" },
        { "data": "Name" },
        { "data": "PublishDate" },
        { "data": "Author" }
        ],
        "autoWidth": false,
        columnDefs: [{ "width": "3%", "targets": 0, "orderable": false }],
        "bFilter": false,
        responsive: true,
    });

    $('#test > tbody  > tr').each(function () {
        $this = $(this)
        if ($this.context._DT_RowIndex != count) {
            $this.append("<td><a href='/Books/Delete/" + $this.context.cells[1].innerHTML + "'><img width='30' height='30' src='http://megaicons.net/static/img/icons_sizes/8/178/512/editing-delete-icon.png'></a><a href='/Books/Edit/" + $this.context.cells[1].innerHTML + "'><img width='30' height='30' src='https://simple-for-ever.appspot.com/img/glyphicons_030_pencil_2x.png'></a></td>");
        }
        else {
            $this.append("<td><button class='btn' type='submit'>Upload book</button></td>")
        }
    });
}

function ReloadTable(data) {
    $('#test').dataTable().fnClearTable()
    $('#test').dataTable().fnAddData(data)

    $('#test > tbody  > tr').each(function () {
        $this = $(this)
        if ($this.context._DT_RowIndex != count) {
            $this.append("<td><a href='/Books/Delete/" + $this.context.cells[1].innerHTML + "'><img width='30' height='30' src='http://megaicons.net/static/img/icons_sizes/8/178/512/editing-delete-icon.png'></a><a href='/Books/Edit/" + $this.context.cells[1].innerHTML + "'><img width='30' height='30' src='https://simple-for-ever.appspot.com/img/glyphicons_030_pencil_2x.png'></a></td>");
        }
        else {
            $this.append("<td><button class='btn' type='submit'>Upload book</button></td>")
        }
    });
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
