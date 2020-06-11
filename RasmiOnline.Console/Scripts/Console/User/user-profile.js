/// <reference path="../../jquery-1.10.2.min.js"/>
$(document).on('ready', function () {
    $(document).on('click', '#btn-update-profile', function () {
        let $btn = $(this);
        submitAjaxForm($btn, function (rep) {
            notify(true,'تغییرات مورد نطر ثبت شد، جهت اعمال تغییرات دوباره وارد شوید')
        });
    });
});