/// <reference path="../../jquery-1.10.2.min.js" />
$(document).ready(function () {
    $('.radio-wrapper:has(input[value="InPerson"])').hide();
    $(document).on('click', '.next-panel', function () {
        changePanelFactory($(this).data('strategy'));
    });
    $(document).on('click', '.prev-panel', function () {
        changePanelFactory($(this).data('strategy'));
    });

    $(document).on('click', '.go-to-gateway', function () {
        let $btn = $(this), model = {};
        model.PaymentType = $('input[name="PaymentType"]:checked').val();
        model.paymentGatewayId = $('#PaymentGatewayId').val();
        ajaxBtn.inProgress($btn);
        $.post($btn.data("url"), model)
            .done(function (rep) {
                ajaxBtn.normal();
                console.log(rep);
                if (rep.IsSuccessful) {
                    window.location.href = rep.Result;
                }
                else {
                    console.log(rep);
                }
            })
            .fail(function (e) {
                ajaxBtn.normal();
            });
    });
});

var changePanelFactory = function (strategy) {
    switch (strategy) {
        case 'f':
            panel.forward();
            break;

        case 'b':
            panel.backward();
            break;
    }
}