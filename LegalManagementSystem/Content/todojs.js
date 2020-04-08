var fromDate = new Date().toLocaleDateString("en-US");
$.ajax({
    type: "POST",
    url: "/Todoes/GetPending?employeeId=" + employeeid,
    contentType: 'application/json',
    cache: false,
    statusCode: {
        400: function (response) {
            var err = $.parseJSON(response);
            alert(err.title);
        }
    }
}).done(function (result) {
    try {
        var jsonResult = $.parseJSON(result);

        $.each(jsonResult, function (i, item) {
            var newLi = "<span style='color: #f5a81d;margin-left: 2rem;'>" + i + "</i><br />";
            $("#todos").append(newLi);
            buildTodo(item);
        });        
    }
    catch (er) {       
        alert(result);
        console.log(er);
    }
    $(".spinner-1").hide();
}).fail(function (err) {
    $(".spinner-1").hide();
    alert(err.statusText);
    console.log(err);
});

$(document).on('click', '.todo-check', function (e) {
    var id = e.currentTarget.id;
    $.ajax({
        type: "POST",
        url: '/Todoes/ChangeStatus?id=' + id,
        contentType: 'application/json',
        cache: false,
    }).done(function (result) {
        console.log(result);
    }).fail(function (err) {
        alert(err.statusText);
        console.log(err);
    });
});


$("#save-todo").click(function (e) {

    var _todo = {};
    _todo.Title = $("#title-msg").val();
    _todo.Details = $("#details-msg").val();
    _todo.EmployeeId = employeeid;
    _todo.CreatedDate = new Date();

    $.ajax({
        type: "POST",
        url: "/HR/PostToServerAsync?urlPart=Todo?from=" + fromDate,
        contentType: 'application/json',
        data: JSON.stringify(_todo),
        cache: false,
        statusCode: {
            400: function (response) {
                var err = $.parseJSON(response);
                alert(err.title);
            }
        }
    }).done(function (result) {
        try {
            var _result = $.parseJSON(result);
            if (_result != undefined) {
                buildTodo(_result);
            }
            else {
                $(".spinner-1").hide();
                alert("system error has occurred. Contact IT Support");
            }
            console.log(_result);
        }
        catch (er) {
            alert(result);
            console.log(er);
        }
        $(".spinner-1").hide();
        $("#title-msg").val('');
        $("#details-msg").val('');
    }).fail(function (err) {
        alert(err.statusText);
        console.log(err);
    });
});

function buildTodo(dataList) {
    //$("#todos").empty();
    $(".spinner-1").hide();
    if (dataList == undefined || dataList.length == 0) {
        $("#todos").html('<i> add a todo list to your workspace </i>');
        return;
    }

    $.each(dataList, function (i, item) {

        var checked = item.done ? 'checked' : '';
        
        var newLi = "<li><label class='fancy-checkbox mb-0'>" +
            "<input id=" + item.id + " type='checkbox' class='todo-check' " + checked + ">" +
            "<span style='color:#550eb3;font-weight:bold;'>" + item.title + "</span></label>" +
            "<div class='m-l-35 m-b-30'>" +
            "<span class='text-black'>" + item.details.replace(/(?:\r\n|\r|\n)/g, '<br>'); + "</small>" +
            "</div></li>";

        $("#todos").append(newLi);
    });

    $('#addevent').modal('hide');
}