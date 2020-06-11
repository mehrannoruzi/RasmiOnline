/// <reference path="../../jquery-1.10.2.min.js" />

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
            ajaxBtn.normal();
            msg.error('خطایی رخ داده است، دوباره سعی نمایید.');
            console.log(e.responseText);
        });
});

$(document).on('ready', function () {

    orderItem.userIsAdmin = true;

    $(".btnRemoveTransaction").click(function () {
        $this = $(this);
        console.log($this.attr('data-transactionid'))
        ajaxBtn.inProgress($this);

        $.ajax({
            url: "/Transaction/RemoveTransaction",
            type: 'POST',
            data: {
                transactionId: $this.attr('data-transactionId'),
                orderId: $this.attr('data-orderId')
            },
            cache: false,
            dataType: 'json',
            success: function (rep) {
                ajaxBtn.normal($this);
                if (rep.IsSuccessful) {
                    $(".transaction_" + $this.attr('data-transactionId') + "_" + $this.attr('data-orderId')).fadeOut();
                }
                else {

                    notify(false, rep.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ajaxBtn.normal($this);
                notify(false, errorMsg);
                console.log(jqXHR.responseText);
            }
        });
    });

    orderItem.inEnglish = ($('#LangType').val() === 'Fa_En');
    //update order
    $(document).on('click', '.btn-update-order', function () {
        submitAjaxForm($(this), function (rep) {
            notify(rep.IsSuccessful, rep.Message);
        });

    });


    $(".attachments-type-select").click(function () {
        $(".attachments-type-select").removeClass("activate");
        $(this).addClass("activate");
        $(".dropzone-uploader").addClass('hide');
        $(".dropzone-uploader-" + $(this).attr('data-type')).removeClass('hide');
    });

    //fire dropzone js for uploading attachments
    fireDropZone($('#upload-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#upload-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });

    fireDropZone($('#upload-translation-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#upload-translation-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });

    fireDropZone($('#upload-identity-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#upload-identity-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });

    fireGoogleMap();

    //send added or changed order items to server
    $(document).on('click', ".btn-submit-order-items", function () {
        let items = [];
        $('.order-items .pricing-item').each(function (idx, elm) {
            console.log($(this).find('input[name="price"]').val());
            let info = $(this).data('info');
            let item = {
                OrderId: parseInt($('#OrderId').val()),
                Price: $(this).find('input[name="price"]').val(),
                OrderItemType: "PricingItem",
                Copy: $(elm).find('.copy-count').val(),
                Count: $(elm).find('.doc-count').val(),
                PricingItemId: $(elm).data('id'),
                Description: $(elm).find('.description').text(),

            };
            if ($(elm).data('added')) {
                items.push(item);
            }
            else if ($(elm).data('changed')) {
                item.OrderItemId = info.orderItemId;
                items.push(item);
            }

        });
        //official record
        let $officialRecordItem = $('.order-items .official-record-item');
        let info = $officialRecordItem.data('info');
        items.push({
            OrderId: parseInt($('#OrderId').val()),
            OrderItemId: info.orderItemId,
            OrderItemType: "OfficialRecordItem",
            Price: $officialRecordItem.find('input[name="price"]').val(),
            Copy: $officialRecordItem.find('.copy-count').val(),
            Count: $officialRecordItem.find('.doc-count').val()
        });

        if (items.length === 0) {
            notify(false, "هیچ آیتمی وجود ندارد");
            return;
        }

        let $btn = $(this), url = $btn.data('url');

        ajaxBtn.inProgress($btn);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            url: url,
            data: JSON.stringify({
                orderId: $('#OrderId').val(),
                langType: $('#LangType').val(),
                items: items
            }),
            success: function (rep) {
                ajaxBtn.normal();

                if (rep.IsSuccessful) {
                    notify(true, rep.Message);
                    $('.order-items .item').each(function () {
                        let info = $(this).data('info');
                        info.serverSideRemove = true;
                        info.orderItemId = rep.Result.find(x => x.PricingItemId == info.pricingItemId).OrderItemId;
                        $(this).data('info', info);
                    });
                }
                else notify(false, rep.Message);
            },
            error: function (e) {
                ajaxBtn.normal();
                console.log(e);
            }
        });
    });

    //submit order details
    $(document).on('click', '.btn-submit-order-details', function () {
        var wrapper = $('.wrapper-frm');
        if (!wrapper.valid()) {
            notify(false, "لطفا فیلد های مورد نیاز را پر نمایید");
            return;
        }
        var orderId = $('#OrderId').val();
        var orderDetails = [];
        var orderItemId;
        $('.tbl-order-details .order-item').each(function (idx1, orderItem) {
            orderItemId = $(orderItem).data('id');
            $(orderItem).find('.order-detail').each(function (idx2, orderDetail) {
                orderDetails.push({
                    OrderDetailId: $(orderDetail).data('id'),
                    OrderId: orderId,
                    OrderItemId: orderItemId,
                    Title: $(orderDetail).find('.detail-title').val(),
                    Count: $(orderDetail).find('.doc-count').val(),
                    Copy: $(orderDetail).find('.copy-count').val(),
                    Description: $(orderDetail).find('.description').val(),
                });
            });
        });
        let $btn = $(this);
        let url = $btn.data('url');
        ajaxBtn.inProgress($btn);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            url: url,
            data: JSON.stringify({ orderDetails: orderDetails }),
            success: function (rep) {
                ajaxBtn.normal();

                if (rep.IsSuccessful) {
                    notify(true, rep.Message);
                    $('.wrapper-frm').replaceWith(rep.Result);
                    $.validator.unobtrusive.parse($('.wrapper-frm'));
                }
                else notify(false, rep.Message);
            },
            error: function (e) {
                ajaxBtn.normal();
                console.log(e);
            }
        });
    });

    //remove attachment
    $(document).on('click', ".attch-item .remove", function () {
        let $elm = $(this), $elmContent = $elm.html();
        $.post($elm.data('url'))
            .done(function (rep) {
                $elm.html($elmContent);
                if (rep.IsSuccessful) {
                    $elm.closest('.attch-item').remove();
                }
                else notify(false, rep.Message);
            })
            .fail(function (e) {
                $elm.html($elmContent);
            });
    });

    //add transaction
    $(document).on('click', 'button[data-element-name="AddTransaction"]', function () {
        let $elm = $(this), $postUrl = $elm.attr('data-post-url');
        loadingOverlay.show();
        $.ajax({
            url: $postUrl,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    PaymentGatewayId: $('select#transactionObject_PaymentGatewayId').val(),
                    OrderId: $('input#OrderId').val(),
                    Price: $('input#transactionObject_Price').val(),
                    BankCardId: $('select#transactionObject_BankCardId').val(),
                    Description: $('input#transactionObject_Description').val(),
                    IsSuccess: $('select#transactionObject_IsSuccess').val(),
                    InsertDateSh: $('input#transactionObject_InsertDateSh').val(),
                    TrackingId: $('input#transactionObject_TrackingId').val()
                }
            ),
            type: 'POST',
            dataType: 'json'
        })
            .done(function (rep) {
                if (rep.IsSuccessful) {
                    notify(true, rep.Message);
                    reloadTransaction($('input#OrderId').val());
                }
                else notify(false, rep.Message);
                loadingOverlay.hide();
            })
            .fail(function () {
                notify(false, 'خطا در ارسال اطلاعات رخ داده است');
                loadingOverlay.hide();
            });
    });

});

// reload transaction list
var reloadTransaction = function (orderId) {
    let $postUrl = '/Transaction/AddableList?orderId=' + orderId;
    $.ajax({
        url: $postUrl,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        dataType: 'html'
    })
        .done(function (rep) {
            $('DIV#Transactions').html("<br /><br />" + rep);
        })
        .fail(function () {
            notify(false, 'خطا در دریافت اطلاعات رخ داده است');
        });
}

var loadingOverlay = {
    overlay: '<div class="page-loader" style="position:absolute;opacity:0.5;background-color:#ccc;">' +
        '<div class="page-loader__spinner">' +
        '<svg viewBox="25 25 50 50">' +
        '<circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle>' +
        '</svg>' +
        '</div>' +
        '</div>',
    show: function () {
        let $loadingOver = $('DIV.loadingOver');
        if ($loadingOver) $loadingOver.prepend(loadingOverlay.overlay);
    },
    hide: function () {
        let $loadingOver = $('DIV.loadingOver');
        if ($loadingOver) $loadingOver.find('DIV.page-loader').remove();
    }
};
