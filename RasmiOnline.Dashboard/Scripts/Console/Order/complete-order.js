/// <reference path="../jquery-1.10.2.min.js" />
var paymentGatewaysTxt = "";
var changePanelFactory = function (strategy) {
    switch (strategy) {
        case 'reset-flickity-f':
            panel.forward();
            $('.flickity-enabled').flickity('resize');
            break;

        case 'reset-flickity-b':
            panel.backward();
            $('.flickity-enabled').flickity('resize');
            break;

        //case 'add-delivery-cost-f':
        //    $('.gateway .delivery-row').remove();
        //    let cost = $('.panels .addresses').data('deliver-cost');
        //    if ($('input[name="DeliverPaymentByMyself"]:checked').val() === 'true')
        //        cost = 0;
        //    let $tr = '<tr class="delivery-row" data-cost="' + cost + '">'
        //        + '<td class="text-right">هزینه نماینده رسمی آنلاین </td>'
        //        + '<td colspan="4"></td>'
        //        + '<td dir="ltr">' + cost.toString().cThSeperator() + '</td>'
        //        + '</tr>';
        //    $($tr).insertBefore('.gateway tr.total-row');
        //    let totalSum = $('.total-sum').data('total-sum') + cost;
        //    $('.gateway .total-sum').data('total-sum', totalSum).find('.val').text(totalSum.toString().cThSeperator());
        //    $('.payment-warn').text(paymentGatewaysTxt.replace('{0}', totalSum.toString().cThSeperator()));
        //    panel.forward();
        //    break;

        //case 'payment-title-f':
        //    $('.payment-warn').text(paymentGatewaysTxt.replace('{0}', $('.total-sum').data('total-sum').toString().cThSeperator()));
        //    panel.forward();
        //    $('.flickity-enabled').flickity('resize');
        //    break;

        case 'f':
            panel.forward();
            break;

        case 'b':
            panel.backward();
            break;
    }
}

$(document).on('ready', function () {
    paymentGatewaysTxt = $('.payment-warn').text();

    
    $(document).on('click', '.next-panel', function () {
        changePanelFactory($(this).data('strategy'));
    });
    $(document).on('click', '.prev-panel', function () {
        changePanelFactory($(this).data('strategy'));
    });

    //add discount event
    let disTry = 0;
    $(document).on('click', '.btn-discount', function () {
        $('.discount-message').text('');
        let $btn = $(this);
        if ($('.summary .discount-item').length > 0) {
            $('.discount-message').text('تنها بکیار امکان استفاده از کد تخفبف وجود دارد.');
            return;
        }
        submitAjaxForm($btn, function (rep) {
            console.log(rep);
            $('.discount-input').val('');
            let $tr = $('<tr class="discount-item" data-code="' + rep.Result.Code + '">'
                + '<td class="text-right"><i class="discount-remove zmdi zmdi-close" data-value="' + rep.Result.Value + '"></i>' + 'تخفیف شما با کد ' + rep.Result.Code + '</td>'
                + '<td colspan="5"></td>'
                + '<td dir="ltr">-' + rep.Result.Value.toString().cThSeperator() + '</td>'
                + '</tr>');
            $tr.insertBefore('.discount tr.total-row');
            let totalSum = parseInt($('.discount tr.total-row .total-sum').data('total-sum')) - rep.Result.Value;
            //$btn.prop('disabled', true);
            $('.discount tr.total-row .total-sum').data('total-sum', totalSum).text(totalSum.toString().cThSeperator());
            let $disTr = $tr.clone();
            $disTr.find('.discount-remove').remove();
            $disTr.insertBefore('.gateway tr.total-row');

            $('.gateway .total-sum').data('total-sum', totalSum).text(totalSum.toString().cThSeperator());
        }, function (rep) {
            $('.discount-input').val();
            $('.discount-message').text(rep.Message);
        });
    });

    //remove discount event
    $(document).on('click', '.discount-item .discount-remove', function () {
        let $elm = $(this), val = parseInt($elm.data('value'));
        let totalSum = parseInt($('.discount tr.total-row .total-sum').data('total-sum')) + val;
        $('.discount tr.total-row .total-sum').data('total-sum', totalSum).text(totalSum.toString().cThSeperator());
        $elm.closest('tr').remove();
        $('.discount-message').text('');
        let $dis = $('.gateway .discount-item');
        if ($dis.length > 0) {
            let totalSum2 = $('.gateway .total-sum').data('total-sum');
            totalSum2 = totalSum2 + val;
            $('.gateway .total-sum').data('total-sum', totalSum2).text(totalSum2.toString().cThSeperator());
            $dis.remove();
        }
        $('.btn-discount').prop('disabled', false);
    });

    //comlete order 
    $(document).on('click', '.btn-complete-order', function () {
        let model = {};
        model.orderId = $('#OrderId').val();
        let $disTr = $('.discount-item');
        model.DiscountCode = $disTr.length > 0 ? $disTr.data('code') : "";
        model.DeliveryType = $('input[name="DeliveryType"]:checked').val();
        if (model.DeliveryType == 'ByDeliveryMan') {
            model.addressId = $('#AddressId').val();
        }
        model.PaymentType = $('input[name="PaymentType"]:checked').val();
        model.paymentGatewayId = $('#PaymentGatewayId').val();
        let $btn = $(this), url = $btn.data('url');
        ajaxBtn.inProgress($btn);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            url: url,
            data: JSON.stringify({ model: model }),
            success: function (rep) {
                ajaxBtn.normal();
                if (rep.IsSuccessful) {
                    window.location.href = rep.Result;
                }
                else notify(false, rep.Message);
            },
            error: function (e) {
                ajaxBtn.normal();
                console.log(e);
            }
        });
    });


    //init addreses plugin
    var o = $('.flickity-enabled').flickity({
        rightToLeft: true,
        contain: true,
        pageDots: false
    });
    fireGoogleMap();
});

//var model = {
//    discount: {value :0,code:""};
//    orderItems =[]
//};

//var init = function () {
//    $('.discount tr.content-row').each(function () {

//    });
//};


//var gatewayPanel = function () {
//    var totalSum = 0;
//    this.warnText = "";

//}