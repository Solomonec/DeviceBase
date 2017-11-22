
$(document).ready(function () {

  $("#adddevice").live("click",function (event) {
        
        window.open("/AsppDevice/Index", "_blank");
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
                    url: '/Home/DeleteAsppDevices',
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

});



