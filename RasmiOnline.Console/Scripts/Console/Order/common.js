/// <reference path="../../jquery-1.10.2.min.js" />
$(document).ready(function () {

    $(".main").addClass('removeSidebar');

    //add order tab select
    $(document).on('click', '.add .nav.nav-tabs > li', function () {
        let $elm = $(this).addClass('active');
        $elm.find('a').removeClass('active');

    });

    //order detail delivery type select event
    $(document).on('change', 'input[name="DeliveryType"]', function () {
        let val = $('input[name="DeliveryType"]:checked').val();
        let $btnText = $('.panel.gateway active').find('button.next-panel > .text');
        let $byGatewayWrapper = $('input[name="PaymentType"][value="ByGateway"]').closest('.radio-wrapper'), $inPersonWrapper = $('input[name="PaymentType"][value="InPerson"]').closest('.radio-wrapper');
        switch (val) {
            case 'ByMySelf':
                $('.address_list').hide();
                $('.setting').show().find('.simple-schema').show();
                $('.setting').find('.post-schema').hide();
                $inPersonWrapper.show();
                break;
            case 'ByDeliveryMan':
                $('.address_list').show();
                $('.setting').css({ 'display': 'none' });
                $('.address_list').css({ 'display': 'block' });
                $inPersonWrapper.show();
                break;
            case 'ByPost':
                $('.address_list').hide();
                $('.setting').show().find('.post-schema').show();
                $('.setting').find('.simple-schema').hide();
                $byGatewayWrapper.addClass('activate').find('input').prop('checked', true);
                $inPersonWrapper.removeClass('activate').hide();
                $('.payment-select').css({ 'visibility': 'visible' });
                break;
        }
    });

    //payment type change event
    $(document).on('change', 'input[name="PaymentType"]', function () {
        let val = $('input[name="PaymentType"]:checked').val(), $gatways = $(".payment-select"), $btnText = $('.gateway button.next-panel > .text');
        switch (val) {
            case "ByGateway":
                $('.payment-select').css({ 'visibility': 'visible' });
                $btnText.text('ثبت');
                break;
            case "InPerson":
                $('.payment-select').css({ 'visibility': 'hidden' });
                $btnText.text('ثبت و آپلود مدارک');
                break;
        }
    });

    //set border color to radio input wrappers
    $(document).on('change', '.radio-wrapper input[type="radio"]', function () {
        
        let name = $(this).attr('name');
        let $radios = $('.radio-wrapper:has(input[type="radio"][name="' + name + '"])').removeClass('activate');
        $(this).closest('.radio-wrapper').addClass('activate');
    });
});
//change panels
var panel = function () {
    let idx = 0, $panels = $('.panels').children(), $steps = $('.timeline-item:not(.period)'), start = $steps.index($('.timeline-item > .timeline-marker.current').parent());

    this.forward = function (jump) {
        jump = typeof jump !== 'undefined' ? jump : 1;
        idx += jump;
        change();
    }
    this.backward = function () {
        if (idx > 0) {
            jump = typeof jump !== 'undefined' ? jump : 1;
            idx -= jump;
        }
        change();
    }
    function change() {
        console.log(idx)
        $panels.removeClass('active'), $steps.removeClass('done').find('.timeline-marker').removeClass('current');
        $panels.eq(idx).addClass('active'), $steps.eq(start + idx).find('.timeline-marker').addClass('current');
        $steps.each(function (i, step) {
            if (i === start + idx) return false;
            $(step).addClass('done');
        });
    }
    return this;
}();

