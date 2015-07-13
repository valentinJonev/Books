var data
LoadData();

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
            data[i]['PublishDate'] = _date.getFullYear() + "/" + (_date.getMonth() + 1) + "/" + _date.getDate()
            if (data[i]["Cover"] != null) {
                data[i]["Cover"] = '<img width="120" height="120" src="' + data[i]["Cover"] + '">'
            }
            else {
                data[i]["Cover"] = '<img  src="http://i.imgur.com/sJ3CT4V.gif" height=100>'
            }
        }
        for (var i = 0; i < count; i++) {
            if (data[i]['AuthorId'] == 0) data[i]['Author'] = 'Terry Pratchett';
            if (data[i]['AuthorId'] == 1) data[i]['Author'] = 'Douglas Adams'
            if (data[i]['AuthorId'] == 2) data[i]['Author'] = 'J. K. Rowling'
            if (data[i]['AuthorId'] == 3) data[i]['Author'] = 'J.R.R. Tolkien'
        }
        data[count] = { 'Cover': '<input type="file" value="Upload cover" accept=".jpg, .jpeg, .png, .gif" name="Cover" id="Cover">', 'Name': "<input type='text' placeholder='Book title' name='Name'>", 'PublishDate': "<input type='text' placeholder='Book publish date' name='Date'>", 'Author': '<select name="Author"><option value="0">Terry Pratchett</option><option value="1">Douglas Adams</option><option value="2">J. K. Rowling</option><option value="3">J.R.R. Tolkien</option></select>' }
        LoadTable();
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
            data[i]['PublishDate'] = _date.getFullYear() + "/" + (_date.getMonth() + 1) + "/" + _date.getDate()
            if (data[i]["Cover"] != null) {
                data[i]["Cover"] = '<img width="120" height="120" src="' + data[i]["Cover"] + '">'
            }
            else {
                data[i]["Cover"] = '<img  src="http://i.imgur.com/sJ3CT4V.gif" height=100>'
            }
        }
        for (var i = 0; i < count; i++) {
            if (data[i]['AuthorId'] == 0) data[i]['Author'] = 'Terry Pratchett';
            if (data[i]['AuthorId'] == 1) data[i]['Author'] = 'Douglas Adams'
            if (data[i]['AuthorId'] == 2) data[i]['Author'] = 'J. K. Rowling'
            if (data[i]['AuthorId'] == 3) data[i]['Author'] = 'J.R.R. Tolkien'
        }
        data[count] = { 'Cover': '<input type="file" value="Upload cover" accept=".jpg, .jpeg, .png, .gif" name="Cover" id="Cover">', 'Name': "<input type='text' placeholder='Book title' name='Name'>", 'PublishDate': "<input type='text' placeholder='Book publish date' name='Date'>", 'Author': '<select name="Author"><option value="0">Terry Pratchett</option><option value="1">Douglas Adams</option><option value="2">J. K. Rowling</option><option value="3">J.R.R. Tolkien</option></select>' }
        ReloadTable();
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

function ReloadTable() {
    table.fnClearTable()
    table.fnAddData(data)
}

function search(action) {
    $('#test').dataTable().fnDestroy()
    if (action == 'clear') {
        ReloadData();
    }
    else {
        var title = document.getElementById('Title').value;
        var author = document.getElementById('Author').value;
        ReloadData(title, author)
    }
}