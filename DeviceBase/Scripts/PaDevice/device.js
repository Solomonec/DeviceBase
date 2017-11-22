$(document).ready(function () {
    
    $("#content").find("[id^='tab']").hide(); // Hide all content
    $("#tabs li:first").attr("id", "current"); // Activate the first tab
    $("#content #tab1").fadeIn(); // Show first tab's content

    $('#tabs a').click(function (e) {
        e.preventDefault();
        if ($(this).closest("li").attr("id") === "current") { //detection for current tab
            return;
        }
        else {
            $("#content").find("[id^='tab']").hide(); // Hide all content
            $("#tabs li").attr("id", ""); //Reset id's
            $(this).parent().attr("id", "current"); // Activate this
            $('#' + $(this).attr('name')).fadeIn(); // Show content for the current tab
        }
    });

    $("#deviceclasslist").change(function () {

        $("#devicetypelist").empty();
        $("#devicesubtypelist").empty();
        var classvalue = $("#deviceclasslist option:selected").val();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/DeviceTypes/GetDeviceTypesByClass',
            data: { classname: classvalue },
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выбeрите тип...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i] + "\">" + data[i] + "</option>";

                    }
                    $("#devicetypelist").append(results);
                }
            }
        });
    });

    $("#devicetypelist").change(function () {

        $("#devicesubtypelist").empty();
        var typevalue = $("#devicetypelist option:selected").val();
        $.ajax({
            type: 'Post',
            cache: false,
            url: '/DeviceTypes/GetDeviceSubTypesByType',
            data: { typename: typevalue },
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выбeрите подтип...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\"" + data[i] + "\">" + data[i] + "</option>";

                    }
                    $("#devicesubtypelist").append(results);
                }
            }
        });
    });

    $.datetimepicker.setLocale('ru');

    $('#datepicker').datetimepicker({
        format: 'd.m.Y',
        timepicker: false,
        theme: 'dark'
    });
 
});

