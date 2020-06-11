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
            //$btn.text(txt);
            ajaxBtn.normal();
            msg.error('خطایی رخ داده است، دوباره سعی نمایید.');
            console.log(e.responseText);
        });
});


$(document).on('ready', function () {
    orderItem.inEnglish = ($('#LangType').val() == 'Fa_En');
    orderItem.$wrapper = $('#items-wrapper');
    //overrride add method for mobile
    orderItem.add = function (item) {
        if (orderItem.$wrapper.find('.pricing-item[data-id=' + item.PricingItemId + ']').length === 0) {
            {
                let info = {
                    orderItemId: 0,
                    pricingItemId: item.PricingItemId,
                    price: item.Price,
                    copyPrice: item.CopyPrice,
                    priceInOtherLangs: item.Price_OthersLang,
                    copyPriceInOtherLangs: item.CopyPrice_OthersLang,
                    totalPrice: item.Price,
                    serverSideRemove: false
                };
                let $item =
                    $(`<div class="item mobile-item pricing-item" data-id="` + item.PricingItemId + `" data-added="true">
                        <div class="title">
                            <div class="remove"><i class="zmdi zmdi-close-circle"></i></div>
                            <h5 class="text">`+ item.DocumentType + `</h5>
                        </div>
                        <div class="props">
                            <div class="prop-group w-mob-half">
                                <span class="lbl">واحد:</span>
                                <span>`+ item.PricingItemUnitText + `</span>
                            </div>
                            <div class="prop-group w-mob-half">
                                <span class="lbl">قیمت واحد:</span>
                                <span class="val price">`+ (orderItem.inEnglish ? item.Price : item.Price_OthersLang) + `</span>
                            </div>
                            <div class="prop-group w-mob-half">
                                <span class="lbl">تعداد:</span>
                                <div class="val select-count">
                                    <select class="doc-count">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                    </select>
                                </div>
                            </div>
                            <div class="prop-group w-mob-half">
                                <span class="lbl">نسخه اضافی:</span>
                                <div class="val select-copy-count">
                                    <select class="copy-count">
                                        <option value="0">0</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                    </select>
                                </div>
                            </div>
                            <div class="prop-group">
                                <span class="lbl">هزینه نسخه اضافی:</span>
                                <span class="val total-copy-price">0</span>
                            </div>
                        </div>
                        <div class="total">
                            <span class="lbl">قیمت کل:</span>
                            <span class="val total-price">`+ (orderItem.inEnglish ? item.Price : item.Price_OthersLang) + `</span>
                        </div>
                    </div>`);
                $item.data('info', info);
                $('.order-items').flickity('append', $item);
            }
        };
        addModal.close();
        let itemsCount = $('.order-items .mobile-item').length;
        $('.order-items').flickity('select', itemsCount - 1);
    };

    $('.order-items').flickity({
        rightToLeft: true,
        contain: true,
        pageDots: false
    });

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
            
            let $wrapper = $('#upload-translation-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });

    //send added or changed order items to server
    $(document).on('click', ".btn-submit-order-items", function () {
        let items = [];

        $('.order-items .pricing-item').each(function (idx, elm) {
            let info = $(this).data('info');
            let item = {
                OrderId: parseInt($('#OrderId').val()),
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
        let info = $officialRecordItem.data('info')
        items.push({
            OrderId: parseInt($('#OrderId').val()),
            OrderItemId: info.orderItemId,
            OrderItemType: "OfficialRecordItem",
            Copy: $officialRecordItem.find('.copy-count').val(),
            Count: $officialRecordItem.find('.doc-count').val(),
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
            data: JSON.stringify({ orderId: $('#OrderId').val(), langType: $('#LangType').val(), items: items }),
            success: function (rep) {
                ajaxBtn.normal();

                if (rep.IsSuccessful) {
                    notify(true, rep.Message);
                    $('.order-items .item').each(function () {
                        let info = $(this).data('info');
                        info.serverSideRemove = true;
                        console.log(info);
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

    //FIRING PLUGIN
    $(document).on('click', '[href="#items-wrapper"]', function () {
        setTimeout(function () {
            $('.order-items').flickity('resize');
        }, 300)
        
    });

    //change lang event
    $(document).on('change', '#LangType', function () {
        var $table = $(".tbl-order-items");
        $('.order-items .pricing-item').data('changed', true);
        let inEnglish = ($(this).val() == 'Fa_En');
        if (orderItem.inEnglish != inEnglish) {
            let $hint = $('.lang-type-wrapper .hint');
            $hint.fadeIn();
            setTimeout(function () {
                $hint.hide();
            }, 1700);
        }
        orderItem.inEnglish = inEnglish;

        orderItem.sum();
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