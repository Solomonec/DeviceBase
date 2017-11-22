$(function () {

    $("#addowner").click(function () {
        $(".owner-modal-input").val("");
        $("#modalowner").modal();
    });

    $("span.editicon").click(function () {
        var id = $(this).data("ownerid");
      
        $.ajax({
            url: "/Owners/GetOwnerInfo",
            type: "Post",
            data: { ownerid: id},
            success: function (data) {
                if (data !== null) {
                    $(".owner-modal-input").val("");
                    $("#ownerid").val(data.OwnerId);
                    $("#ownername").val(data.Name);
                    $("#ownerfullname").val(data.FullName);
                    $("#ownerjob").val(data.Job);
                    $("#ownerslugba").val(data.Slugba);
                    $("#ownerdepartment").val(data.Department);
                    $("#owneraddress").val(data.Address);
                    $("#ownerroom").val(data.Room);
                    $("#ownerfloor").val(data.Floor);
                    $("#ownertel").val(data.Tel);
                    $("#modalowner").modal();

                } else {
                    $(".owner-modal-input").val("");
                    $("#statusbar").html('<span class="owner-fail">Ошибка! Неудалось получить данные ответственного!</span>');
                    $("#statusbar").show();
                    $("#modalowner").modal();

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
                    url: '/Owners/DeleteOwners',
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
        $(".owner-modal-input").val("");
        $.modal.close();
    });

    $("#ownerform").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                var result = data;
                if (result === true) {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="owner-success">Ответственный успешно сохранен!</span>');
                    $("#statusbar").show();
                } else {
                    $("#statusbar").empty();
                    $("#statusbar").html('<span class="owner-fail">Ошибка! Неудалось сохранить ответственного!</span>');
                    $("#statusbar").show();
                }


            }

        });

    });

})