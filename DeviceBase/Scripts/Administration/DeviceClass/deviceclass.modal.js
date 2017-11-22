$(function () {

    $("#adddeviceclass").click(function () {
        $(".deviceclass-modal-input").val("");
        $("#modaldeviceclass").modal();
    });

    $("#viewdeviceclass").click(function () {
        $.ajax({
            url: "",
            type: "Post",
            data: { id: this.data('id') },
            success: function (data) {
                if (data === true) {
                    $(".deviceclass-modal-input").val("");
                    $("#statusbar").html('<span class="deviceclass-suссess">Класс устройства успешно сохранен!</span>');

                } else {

                    $("#statusbar").html('<span class="deviceclass-fail">Ошибка! Неудалось сохранить класс устройства!</span>');
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
                    url: '/DeviceClasses/DeleteDeviceClasses',
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
        $(".deviceclass-modal-input").val("");
        $.modal.close();
    });

    $("#deviceclassform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                var result = data;
                if (result === true) {
                    $(".deviceclass-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="deviceclass-success">Класс устройства успешно сохранен!</span>');
                    $("#statusbar").show();
                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="deviceclass-fail">Ошибка! Неудалось сохранить класс устройства!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})