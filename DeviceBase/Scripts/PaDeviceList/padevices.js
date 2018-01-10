
$(document).ready(function () {

  
    $("#adddevice").live("click",function (event) {
        
        window.open("/PaDevice/Index", "_blank");
    });

    $("#statistics").live("click", function (event) {

        document.location.href = "/Statistics/Devices";
    });

    $("#search").live("click", function (event) {

       var searchstr =  $(".menage-search-field").val();
       var ref = $("#search").attr('href');
       $("#search").attr('href', ref + "?searchvalue=" + searchstr);
    });

  

    $("#delete").live("click", function (event) {

        var deleteDevices = new DeleteDevices();
        deleteDevices.DeleteConfirmation();
        deleteDevices.InitYes();
        deleteDevices.InitNo();
       
    });

});


function DeleteDevices() {

    var checkGridSelection = "";

    function getCheckDevices() {

        return $("input[type=checkbox]:checked").map(function () {
            checkGridSelection = "Selected";
            return $(this).val();
        }).get();

    }

    this.DeleteConfirmation = function () {
        getCheckDevices();
        if (checkGridSelection === "Selected") $("#modaldeleteconfirm").modal();
    }

    this.InitYes = $(".button-yes").live("click",
        function (event) {

            var selected = getCheckDevices();

            if (checkGridSelection === "Selected") {
                var selectedIds = selected.join(';');
                var ref = document.location;
                $.ajax({
                    type: 'Post',
                    cache: false,
                    url: '/Home/DeletePaDevices',
                    data: { selectedIds: selectedIds },
                    dataType: 'json',
                    success: function (data) {
                        if (data === true) {
                            document.location = ref;

                        } else {
                            $.modal.close();
                            alert("Произошла ошибка во время удаления записей");
                        }
                    }
                });
            }
        });

    this.InitNo = $(".button-no").live("click",
        function (event) {
            $.modal.close();
        });

}


