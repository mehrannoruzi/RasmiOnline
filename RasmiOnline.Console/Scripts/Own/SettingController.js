var submitFormButton = 'BUTTON#btnSubmitChange';

$(function () {
    settingMng.init();
});

var settingMng = {
    init: function () {

        fireGoogleMap();

        $(document).on('click', submitFormButton, function () {
            submitAjaxForm($(this), function (rep) {
                notify(true, rep.Message);
            }, function (rep) {
                notify(false, rep.Message);
            });
        });
    }
};