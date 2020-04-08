var fromDate = new Date().getFullYear() + "/01/01";
$.ajax({
    type: "POST",
    url: "/HR/GetFromServer?urlPart=Announcement?from=" + fromDate,
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
        events.push(jsonResult);
        addToTable(jsonResult);
    }
    catch (er) {
        alert(result);
        console.log(er);
    }
}).fail(function (err) {
    alert(err.statusText);
    console.log(err);
});

function addToTable(dataList) {

    if (dataList === undefined || dataList.length === 0) {
        $("#msg-body").append('<i>HR is typing . . . </i>');
        return;
    }

    $.each(dataList, function (i, item) {

        var enddate = " ";
        if (item.from != item.to) {
            enddate = " - " + item.to;
        }
        let className = "warning";
        if (new Date(item.from) >= new Date()) {
            className = "green";
        }

        var newDiv = "<div class='timeline-item " + className + "'>" +
            "<span class='date'>" + item.from + enddate + "</span>" +
            "<h6 style='color:#550eb3;font-weight:bold;'>" + item.title + "</h6>" +
            "<div>" + item.details + "</div></div>";

        $("#msg-body").append(newDiv);

    });
}