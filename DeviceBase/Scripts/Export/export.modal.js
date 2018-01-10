$(function() {

    $("#export").click(function() {
        
        $("#deviceclasslist").empty();
        $.ajax({
            type: 'Post',
            cache: true,
            url: "/DeviceTypes/GetDeviceTypesClasses",
            dataType: 'json',
            success: function(data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выбирите класс...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i] + "\">" + data[i] + "</option>";

                    }
                    $("#deviceclasslist").append(results);
                }
            },
            complete: function () {
                $("#locationlist").empty();
                $.ajax({
                    type: 'Post',
                    cache: false,
                    url: '/Locations/GetLocations',
                    dataType: 'json',
                    success: function (data) {

                        if (data !== null) {
                            var results = "<option value=\"\">Выбирите площадку...</option>";
                            for (var i = 0; i < data.length; i++) {

                                results += "<option value=\"" + data[i].LocationName + "\">" + data[i].LocationName + "</option>";

                            }
                            $("#locationlist").append(results);
                        }
                    }
                });
        }
        });
        $("#modalexport").modal();

    });

    $("#deviceclasslist").change(function() {

        $("#devicetypelist").empty();
        var classvalue = $("#deviceclasslist option:selected").val();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/DeviceTypes/GetDeviceTypesByClass',
            data: { classname: classvalue },
            dataType: 'json',
            success: function(data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выбирите тип...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i] + "\">" + data[i] + "</option>";

                    }
                    $("#devicetypelist").append(results);
                }
            }
        });
    });

    $(".button-cancel").click(function() {

        $.modal.close();
    });

});