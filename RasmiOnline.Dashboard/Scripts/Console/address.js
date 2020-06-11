/// <reference path="../jquery-1.10.2.min.js"/>
$(document).on('ready', function () {
    //
    $(".address_item__radio").click(function () {
        $(".address_item").removeClass("selected");
        $(this).parent().addClass("selected");
        $("#AddressId").val($(this).attr("data-addressId"));
        $(this).find(".rdb-address-box").prop("checked", true);
    });

    //show modal event for add & editing address
    $(document).on('click', '.add-address-button,.address-edit', function () {
        let $elm = $(this);;
        $('#modal .modal-title').html($elm.data('modal-title'));
        $('#modal .modal-body').html(threeDotLoader).load($elm.data('modal-url'), function () {
            $.validator.unobtrusive.parse($('#modal'));
            $('#modal').modal();
            fireGoogleMap();
        });
    });

    //remove address event
    $(document).on('click', '.address-remove', function () {
        let $elm = $(this);
        swalConfirm(function () {
            $.post($elm.data('url'), function (rep) {
                if (rep.IsSuccessful) {
                    $elm.closest('.address_item').remove();
                    $('.flickity-enabled').flickity('resize');
                }
                else notify(false, 'خطایی رخ داده است، دوباره سعی نمایید.');
            })
        });

    });

    //submit address form in modal
    $(document).on('click', '#address-form button', function () {
        submitAjaxForm($(this), function (rep) {
            if (rep.IsSuccessful)
                setTimeout(function () {
                    $('#modal').modal('toggle');

                    var idx = $('.flickity-enabled  .address_item').length;
                    $('.addreses-wrapper').html(rep.Result);
                    var addrs = $('.flickity-enabled').flickity({
                        rightToLeft: true,
                        contain: true,
                        pageDots: false
                    });
                    addrs.flickity('select', idx - 1);
                    fireGoogleMap();
                    // $('.flickity-enabled').flickity('select', idx);
                    //let url = window.location.href;
                    //if (url.indexOf("block=addresses") < 0)
                    //    url += "?block=addresses"
                    //window.location = url;

                }, 2000);
        });
    });

});
