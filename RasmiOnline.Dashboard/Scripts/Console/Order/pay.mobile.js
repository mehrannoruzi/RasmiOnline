/// <reference path="../../jquery-1.10.2.min.js" />
$(document).on('ready', function () {

    $('.order-items').flickity({
        rightToLeft: true,
        contain: true,
        pageDots: false
    });

    //goto payment
    $(document).on('click', '.btn-pay', function () {
        let $btn = $(this);
        ajaxBtn.inProgress($btn);
        $.post($btn.data('url'), { PaymentGatewayId: $('#PaymentGatewayId').val(), OrderId: $('#OrderId').val() })
            .done(function (rep) {
                ajaxBtn.normal();
                if (rep.IsSuccessful) {
                    window.location.href = rep.Result;
                }
                else {
                    notify(false, rep.Message);
                }

            })
            .fail(function () {
                ajaxBtn.normal();
            });
    });
});