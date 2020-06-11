/// <reference path="../jquery-1.10.2.min.js" />
//--- objects
//--- <shahrooz (Changed by Danial)>
$(document).on('ready', function () {

    //change password of user event
    $(document).on('click', '.change-pw-btn', function () {
        let $from = $(this).closest('form');
        if ($from.valid()) {
            let $btn = $(this).prop('disabled', true);
            ajaxBtn.inProgress($btn);
            $.post($from.attr('action'), $from.serialize())
                .done(function (rep) {
                    notify(rep.IsSuccessful, rep.Message);
                    ajaxBtn.normal($btn);
                })
                .fail(function (e) {
                    ajaxBtn.normal($btn);
                    notify(false, 'خطایی رخ داده است، لطفا دوباره سعی نمایید.');
                });
        }
    });
});
//--- </shahrooz>

$("a.nav-link").click(function () {
    localStorage.setItem("profileActivePanel", $(this).attr("href"));
});

$().ready(function () {
    var profileActivePanel = localStorage.getItem("profileActivePanel");
    if (profileActivePanel != null) {
        $("a.nav-link").removeClass("active");
        $("a[href$='" + profileActivePanel + "']").addClass("active");
        $(".tab-pane").removeClass("active show");
        $(profileActivePanel).addClass("active show");
    }
});