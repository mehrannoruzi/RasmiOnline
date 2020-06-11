/// <reference path="../../jquery-1.10.2.min.js" />
$(document).on('ready', function () {
    //init order item
    orderItem.$wrapper = $('.order-row');
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
                    $(`<div class="item mobile-item pricing-item" data-id="` + item.PricingItemId+`" data-added="true">
                        <div class="title">
                            <span class="remove"><i class="zmdi zmdi-close-circle"></i></span>
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

    //save order event
    $(document).on('click', '.save-order', function () {
        saveDetailedOrder($(this));
    });

    //change lang event
    $(document).on('change', '#LangType', function () {
        var $table = $(".tbl-order-items");
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



var saveDetailedOrder = function ($btn) {
    let url = $btn.data('url');

    //create model
    var items = [];
    orderItem.$wrapper.find('.pricing-item').each(function () {
        let info = $(this).data('info');
        items.push({
            PricingItemId: info.pricingItemId,
            Count: $(this).find('.doc-count').val(),
            Copy: $(this).find('.copy-count').val(),
        });
    });

    //if (!validateOrderTitle()) {
    //    return;
    //}
    if (items.length === 0) { notify(false, 'لطفا حداقل یکی از اقلام را اننخاب نمایید'); return; }
    //$('.summary .total-table tr.total td.total-sum > .val').text($("#tbl-order-items").find('td[total-sum] .val').text());
    ajaxBtn.inProgress($btn);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        url: url,
        data: JSON.stringify({
            model: {
                OrderTitle: $("#orderTitle").val(),
                LangType: $('#LangType').val(),
                OfficeUserId: $('#OfficeUserId').val(),
                OrderItems: items
            }
        }),
        success: function (rep) {
            ajaxBtn.normal();
            if (rep.IsSuccessful) {
                $('.summary').prepend(rep.Result.Summary);
                $('.summary').prepend(rep.Result.Header);
                panel.forward();
                $('.final-items').flickity({
                    rightToLeft: true,
                    contain: true,
                    pageDots: false
                });
            }
            else {
                ajaxBtn.normal();
                console.log(rep);
                notify(false, rep.Message);
            }
        },
        error: function (e) { ajaxBtn.normal(); notify(false, errorMsg); console.log(e.responseText); }
    });
};