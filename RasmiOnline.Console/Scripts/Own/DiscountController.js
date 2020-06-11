var modalSendCodeToMobile = 'DIV[data-modal-name="Discount_Modal"]';
var modalDiscountCode = 'H3#discountCode';
var txtMobileNumber = 'INPUT#MobileNumber';
var btnSubmitSendMobile = 'BUTTON#submitSendMobile';
var lnkSendToMobile = 'A[data-discount-id]';
var submitFormButton = 'BUTTON#btnSubmitChange';

var txtCeiling = 'INPUT#Ceiling';
var drpDiscountType = 'SELECT#DiscountType';


$(function () {
    discountMng.init();
});

var discountMng = {
    init: function () {

        $(document).off("change", drpDiscountType)
            .on("change", drpDiscountType,
                function (evt) {
                    evt.preventDefault();
                    let $this = $(this);
                    $(txtCeiling).val('0');
                    switch ($this.val()) {
                        case "Value":
                        case "Percentage":
                            $(txtCeiling).attr('disabled', 'disabled');
                            break;
                        case "PercentageWithCeiling":
                            $(txtCeiling).removeAttr('disabled');
                            break;
                    }
                });

        $(document).off("click", lnkSendToMobile)
            .on("click", lnkSendToMobile,
                function (evt) {
                    evt.preventDefault();
                    $(txtMobileNumber).val('');
                    var discount = {
                        discountId: $(this).attr('data-discount-id'),
                        code: $(this).attr('data-discount-code')
                    };
                    discountMng.actions.showModal(discount);
                });

        $(document).off("click", btnSubmitSendMobile)
            .on("click", btnSubmitSendMobile,
                function (evt) {
                    evt.preventDefault();
                    let $id = $(this).attr('data-discount-id');
                    let $mobileNumeber = $(txtMobileNumber);
                    let $url = $(this).attr('data-send-discountCode-url');

                    if ($mobileNumeber.val() === '') {
                        $mobileNumeber.focus();
                        return false;
                    }

                    discountMng.actions.submitSendCodeToMobile($id, $mobileNumeber.val(), $url);
                });

        $(document).on('click', submitFormButton, function () {
            submitAjaxForm($(this), function (rep) {
                notify(true, rep.Message);
            }, function (rep) {
                notify(false, rep.Message);
            });
        });

    },
    actions: {
        showModal: function (discount) {
            $(modalDiscountCode).html('کد تخفیف: ' + discount.code);
            $(btnSubmitSendMobile).attr('data-discount-id', discount.discountId);
            $(modalSendCodeToMobile).modal();
        },
        submitSendCodeToMobile: function (discountId, mobileNumber, url) {
            $.ajax({
                url: url,
                data: { id: discountId, mobile: mobileNumber },
                type: 'POST'
            })
                //
                .done(function (result) {
                    if (result.IsSuccessful === false) {
                        notify(false, result.Message);
                    } else {
                        notify(true, 'درخواست شما با موفقیت ثبت شد.');
                        $(modalSendCodeToMobile).modal('hide');
                    }
                })
                //
                .fail(function (xhr, status) {
                    notify(false, 'در اجرای درخواست شما خطایی رخ داده است');
                    console.log('error in submit send discount code to mobile');
                });
        }
    }
};