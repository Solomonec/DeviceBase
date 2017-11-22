$(function() {

        var persons = "";
        var currentindex = "";

        $("#btnpersonmodal").click(function () { $("div.person-box").empty(); $('#modalperson').modal(); });
        
        $(".person-search-button").click(function () {
            $("div.person-box").empty();
            var searchval = $(".person-search-input").val();
            $.ajax({
                type: 'Post',
                cache: false,
                url: '/ItDevice/GetOwnersList',
                data: { searchval: searchval },
                dataType: 'json',
                success: function(data) {
                    persons = data;
                    var results = "";
                    if (data !== null) {
                        for (var i = 0; i < data.length; i++) {
                            results += "<div class=\"person-item\" data-index=\"" + i + "\"><div class=\"person-item-tip\"></div>" + data[i].Name + "</div>";
                        }
                        $("div.person-box").append(results);
                    }
                }
            });
        });


    $("div.person-item").live('click', function() {
        $("div.person-item-selected").each(function () {
            if ($(this).hasClass("person-item-selected")) { $(this).removeClass("person-item-selected"); $(this).addClass("person-item"); }
        });
        $(this).removeClass("person-item");
        $(this).addClass("person-item-selected");
        currentindex = $(this).data("index");
    });

    $(".button-ok").click(function () {
        $("#fullname").val(persons[currentindex].Name);
        $("#job").val(persons[currentindex].Job);
        $("#slugba").val(persons[currentindex].Slugba);
        $("#department").val(persons[currentindex].Department);
        $("#address").val(persons[currentindex].Address);
        $("#floor").val(persons[currentindex].Floor);
        $("#room").val(persons[currentindex].Room);
        $("#tel").val(persons[currentindex].Tel);

        $.modal.close();
    });

    $(".button-cancel").click(function() {

        $.modal.close();
    });

});