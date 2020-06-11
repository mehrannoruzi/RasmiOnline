$(document).ready(function () {

    $(document).on('click', '.SignInPanel', function () {
        let $btn = $(this);
        ajaxBtn.inProgress($btn);
        $.post("/OAuth/SignInPanel", { UserId: $btn.attr('data-userId') })
            .done(function (rep) {
                ajaxBtn.normal();
                if (rep.IsSuccessful) {
                    window.location.href = rep.Result;
                }
                else {
                    msg.error(rep.Message);
                    console.log(rep);
                }
            })
            .fail(function (e) {
                //$btn.text(txt);
                ajaxBtn.normal();
                msg.error('خطایی رخ داده است، دوباره سعی نمایید.');
                console.log(e.responseText);
            });
    });

});