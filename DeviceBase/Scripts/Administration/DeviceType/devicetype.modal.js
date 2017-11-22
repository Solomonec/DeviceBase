$(function () {

    $("#adddevicetype").click(function () {
        $("#statusbar").empty();
        $(".devicetype-modal-input").val("");
        $("#deviceclasslist").empty();
        $.ajax({
            type: 'Post',
            cache: true,
            url: '/DeviceTypes/GetDeviceTypesClasses',
            dataType: 'json',
            success: function (data) {
                
                if (data !== null) {
                    var results = "<option>Выбирите класс...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option>" + data[i] + "</option>";

                    }
                    $("#deviceclasslist").append(results);
                }
            }
        });
        $("#modaldevicetype").modal();
    });

    $("span.editicon").click(function () {
        $("#deviceclasslist").empty();
        $.ajax({
            type: 'Post',
            cache: true,
            url: '/DeviceTypes/GetDeviceTypesClasses',
            dataType: 'json',
            success: function (data) {

                if (data !== null) {
                    var results = "<option value=\"\">Выбирите класс...</option>";
                    for (var i = 0; i < data.length; i++) {

                        results += "<option value=\""+ data[i] +"\">" + data[i] + "</option>";

                    }
                    $("#deviceclasslist").append(results);
                }
            }
        });

        var id = $(this).data("devicetypeid");

        $.ajax({
            url: "/DeviceTypes/GetDeviceTypeInfo",
            type: "Post",
            data: { devicetypeid: id },
            success: function (data) {
                if (data !== null) {
                    $(".devicetype-modal-input").val("");
                    $("#devicetypeid").val(data.Id);
                    $("#devicetypename").val(data.DeviceTypeName);
                    $("#devicesubtypename").val(data.DeviceSubTypeName);
                    $("#deviceclasslist").val(data.DeviceClassName);
                    $("#modaldevicetype").modal();

                } else {
                    $(".devicetype-modal-input").val("");
                    $("#statusbar").html('<span class="modaldevicetype-fail">Ошибка! Неудалось получить данные типа!</span>');
                    $("#statusbar").show();
                    $("modaldevicetype").modal();

                }


            }

        });

    });

     $("#delete").live("click", function (event) {


        var checkGridSelection = "";
        var selected = $("input[type=checkbox]:checked").map(function () {
            checkGridSelection = "Selected";
            return $(this).val();
        }).get();

        if (checkGridSelection === "Selected") {
            var r = confirm("Выбранные вами записи будут удалены. Удалить записи?");
            if (r === true) {
                var selectedIds = selected.join(';');
                var ref = document.location;
                $.ajax({
                    type: 'Post',
                    cache: false,
                    url: '/DeviceTypes/DeleteDeviceTypes',
                    data: { selectedIds: selectedIds },
                    dataType: 'json',
                    success: function (data) {
                        if (data === true)
                            document.location = ref;
                        else
                            alert("Произошла ошибка во время удаления записей");
                    }
                });
            }
        }
       
    });



    $(".button-cancel").click(function () {
        $(".devicetype-modal-input").val("");
        $.modal.close();
    });

    $("#devicetypeform").on("submit", function (e) {
        e.preventDefault();
        var isSelected = $("div.devicetype-modal-context-select").hasClass("select-custom-validation-error");
        if ($("#deviceclasslist")[0].selectedIndex === 0) {
            if (!isSelected) {
                $("div.devicetype-modal-context-select").css("border", "3px solid #b94a48");
                $("div.devicetype-modal-context-select").addClass("select-custom-validation-error");
              
            }
        } else {
            if (isSelected) {
                $("div.devicetype-modal-context-select").removeClass("select-custom-validation-error");
                $("div.devicetype-modal-context-select").css("border", "3px solid #6A6A6A");
               
            }
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                dataType: 'json',
                success: function(data) {
                    var result = data;
                    if (result === true) {
                        $(".devicetype-modal-input").val("");
                        $("#statusbar").empty();
                        $("#statusbar").html('<span class="devicetype-success">Новый тип устройства успешно создан!</span>');
                        $("#statusbar").show();
                    } else {
                        $("#statusbar").empty();
                        $("#statusbar").html('<span class="devicetype-fail">Ошибка! Неудалось создать тип устройства!</span>');
                        $("#statusbar").show();
                    }


                }

            });
        }

    });

})