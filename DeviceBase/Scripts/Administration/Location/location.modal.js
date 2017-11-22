$(function () {

    $("#addlocation").click(function () {
        $(".location-modal-input").val("");
        $("#modallocation").modal();
    });

    $("#viewlocation").click(function () {
        $.ajax({
            url: "",
            type: "Post",
            data: { id: this.data('id') },
            success: function (data) {
                if (data === true) {
                    $(".location-modal-input").val("");
                    $("#statusbar").html('<span class="location-suссess">Площадка успешно сохранена</span>');

                } else {

                    $("#statusbar").html('<span class="location-fail">Ошибка! Неудалось сохранить площадку!</span>');
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
                    url: '/Locations/DeleteLocations',
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
        $(".location-modal-input").val("");
        $.modal.close();
    });

    $("#locationform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                var result = data;
                if (result === true) {
                    $(".location-modal-input").val("");
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="location-success">Площадка успешно сохранена!</span>');
                    $("#statusbar").show();
                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="location-fail">Ошибка! Неудалось сохранить площадку!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})