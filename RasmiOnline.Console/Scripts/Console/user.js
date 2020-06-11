/// <reference path="../jquery-1.10.2.min.js" />
$(document).on('ready', function () {

    //keep active panel in refresh
    var profileActivePanel = localStorage.getItem("profileActivePanel");
    if (profileActivePanel != null) {
        $("a.nav-link").removeClass("active");
        $("a[href$='" + profileActivePanel + "']").addClass("active");
        $(".tab-pane").removeClass("active show");
        $(profileActivePanel).addClass("active show");
    }

    //update profile
    $(document).on('click', '#profile-form button', function () {
        submitAjaxForm($(this), function (rep) {
            notify(rep.IsSuccessful, rep.Message);
            if (rep.IsSuccessful)
                setTimeout(function () {
                    window.location.reload();
                }, 3000);
        });
    });

    $("a.nav-link").click(function () {
        localStorage.setItem("profileActivePanel", $(this).attr("href"));
    });

    //init map
    fireGoogleMap();
});

