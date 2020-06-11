var orderItem = {
    inEnglish: true,
    inMobileMode: false,
    $wrapper: null,
    userIsAdmin: false,
    getDropItems: function (start, end) {
        var options = "";
        for (var i = start; i < end; i++) {
            options += '<option value="' + i + '">' + i + '</option>';
        }
        return options;
    },
    add: function (item) {

        let info = {
            orderItemId: 0,
            pricingItemId: item.PricingItemId,
            price: item.Price,
            copyPrice: item.CopyPrice,
            priceInOtherLangs: item.Price_OthersLang,
            copyPriceInOtherLangs: item.CopyPrice_OthersLang,
            totalPrice: (orderItem.inEnglish ? item.Price : item.Price_OthersLang),
            serverSideRemove: false
        };
        console.log('price' + info.totalPrice);
        let $tr = $('<tr class="item pricing-item" data-IsPricingItem="' + item.IsPricingItem + '" data-id="' + item.PricingItemId + '" data-added="true">' +
            '<td class="remove"><i class="zmdi zmdi-close-circle"></i></td>' +
            '<td >' + item.DocumentType + '</td>' +
            '<td>' + item.PricingItemUnitText + '</td>' +
            '<td class="price">' + (orderItem.inEnglish ? item.Price : item.Price_OthersLang) + '</td>' +

            '<td>' +
            '<div class="select-count">' +
            '<select class="doc-count">' +
            this.getDropItems(1, 100) +
            '</select>' +
            '</div>' +
            '</td > ' +

            '<td>' +
            '<div class="select-copy-count">' +
            '<select class="copy-count">' +
            this.getDropItems(0, 100) +
            '</select>' +
            '</td>' +
            '<td class="total-copy-price">0</td>' +
            '<td class="total-price">' +
            (this.userIsAdmin ? ('<input dir="ltr" type="number" value="' + info.totalPrice.toString() + '"/>') : info.totalPrice) +
            '</td>' +
            '</tr>');
        $tr.data('info', info).insertBefore(orderItem.$wrapper.find('.plus-wrapper'));

    },
    delete: function ($elm, info) {
        let $item = $elm.closest('.item');
        $.each($(".mustly-used-item"), function (idx, item) {
            if ($(item).attr('data-PricingItemId') == $elm.closest('tr').attr('data-id')) {
                $(item).removeClass('activate');
            }
        });

        if (!info.serverSideRemove) {
            try {
                $('.order-items').flickity('remove', $item);
            }
            catch (e) {
                $item.remove();
            }
        }
        else {
            let $elmText = $elm.html();
            $elm.html(threeDotLoader);
            $.post(orderItem.$wrapper.data('remove-url'), { id: info.orderItemId })
                .done(function (rep) {
                    $elm.html($elmText);
                    if (rep.IsSuccessful) {
                        try {
                            $('.order-items').flickity('remove', $item);
                        }
                        catch (e) {
                            $item.remove();
                        }
                        orderItem.sum();
                    }
                    notify(rep.IsSuccessful, rep.Message)
                })
                .fail(function (e) {
                    $elm.html($elmText);
                    console.log(e.responseTest);
                    notify(false, errorMsg);
                });
        }
        orderItem.sum();
    },
    changeCount: function ($elm, updateOfficialRecordInputs) {
        updateOfficialRecordInputs = typeof updateOfficialRecordInputs === 'undefined' ? true : false;
        let count;
        let copy;
        let price;
        let copyPrice;
        let totalCopyPrice = 0;
        let info = $elm.data('info');
        if (info.priceInOtherLangs) {
            price = orderItem.inEnglish ? info.price : info.priceInOtherLangs;
            copyPrice = orderItem.inEnglish ? info.copyPrice : info.copyPriceInOtherLangs;
        }
        else {
            price = info.price;
            copyPrice = info.copyPrice;
        }

        count = parseInt($elm.find('.doc-count').val());

        copy = parseInt($elm.find('.copy-count').val());

        totalCopyPrice = count * copy * copyPrice;
        info.totalPrice = Math.floor(price * count + totalCopyPrice);

        //update ui
        $elm.find('.price').text(price.toString().cThSeperator());
        $elm.find('.total-copy-price').text(totalCopyPrice.toString().cThSeperator());
        let $totalPrice = $elm.find('.total-price');
        if ($totalPrice.find('input').length > 0) $totalPrice.find('input').val(info.totalPrice.toString());
        else $totalPrice.text(info.totalPrice.toString().cThSeperator());
        //include official record
        let totalCount = 0;
        orderItem.$wrapper.find('.pricing-item .doc-count').each(function () { totalCount += parseInt($(this).val()); });
        console.log('totalCount: ' + totalCount);
        let totalCopyCount = 0;
        orderItem.$wrapper.find('.pricing-item .copy-count').each(function () { totalCopyCount += parseInt($(this).val()); });
        let $officialRecord = orderItem.$wrapper.find('.official-record-item');
        if ($officialRecord.length > 0) {
            let info = $officialRecord.data('info');
            //update ui
            let isEditable = typeof $officialRecord.data('editable') !== 'undefined' ? $officialRecord.data('editable') : false;
            if (isEditable) {
                //when pricing item changed
                if (updateOfficialRecordInputs) {
                    $officialRecord.find('.doc-count').val(totalCount.toString());
                    $officialRecord.find('.copy-count').val(totalCopyCount.toString());
                    info.totalPrice = info.price * (totalCount + totalCopyCount);
                }
                else
                    info.totalPrice = (parseInt($officialRecord.find('.doc-count').val()) + parseInt($officialRecord.find('.copy-count').val())) * info.price;
            }
            else {
                $officialRecord.find('.count').text(totalCount.toString());
                $officialRecord.find('.copy').text(totalCopyCount.toString());
                info.totalPrice = info.price * (totalCount + totalCopyCount);
            }
            let $oRTotalPrice = $officialRecord.find('.total-price');
            if ($oRTotalPrice.find('input').length > 0) $oRTotalPrice.find('input').val(info.totalPrice.toString());
            else $totalPrice.text(info.totalPrice.toString().cThSeperator());
        }
        this.sum();
    },
    changeLang: function () {
        let oi = this;
        let $items = oi.$wrapper.find('.pricing-item,.official-record-item,.discount-item');
        $items.each(function (idx, elm) {
            oi.changeCount($(elm));
            if (idx == $items.length - 1)
                oi.sum();
        });

    },
    sum: function () {
        updateOfficialRecordInputs = typeof updateOfficialRecordInputs !== 'undefined' ? updateOfficialRecordInputs : true;
        automate = typeof automate !== 'undefined' ? automate : true;

        let totalSum = 0;
        let $items = orderItem.$wrapper.find('.pricing-item,.official-record-item,.discount-item');
        $items.each(function (idx, elm) {
            let info = $(elm).data('info');
            totalSum += parseInt(info.totalPrice);
            if (idx == $items.length - 1)
                orderItem.$wrapper.find('.total-sum').data('total-sum', totalSum).text(" تومان " + totalSum.toString().cThSeperator());
        });
    },
    initAutoComplete: function () {
        $('.ajax-auto-complete').each(function () {
            let $wrapper = $(this).closest('.order-items');
            ajaxAutoComplete($(this), function (items) {
                return items.map(x => ('<li title="افزودن"><div>' + x.DocumentType + '<span>قیمت پایه/' + x.PricingItemUnitText + ':            ' + (orderItem.inEnglish ? x.Price.toString().cThSeperator() : x.Price_OthersLang.toString().cThSeperator()) + '</span></div></li>'))
            }, function (item) {
                if (orderItem.$wrapper.find('.item[data-id=' + item.PricingItemId + ']').length === 0) {
                    orderItem.add(item);
                    orderItem.sum();
                    $.each($(".mustly-used-item"), function (idx, itm) {
                        if ($(itm).attr('data-PricingItemId') == item.PricingItemId) {
                            $(itm).addClass('activate');
                        }
                    });
                }
            });
        });
    }
};

$(document).ready(function () {

    //set order items wrapper
    orderItem.$wrapper = $('.order-items');

    //init auto complete for every specified elements 
    orderItem.initAutoComplete();

    //remove row 
    $(document).on('click', '.order-items .remove', function () {
        var info = $(this).closest('.item').data('info');
        let $mostlyUsed = $('.mustly-used-item[data-id=' + info.pricingItemId + ']');
        if ($mostlyUsed.length > 0) {
            $mostlyUsed.removeClass('activate');
        }
        orderItem.delete($(this), info);
    });

    //close auto comlete result ul 
    $(document).on('click', ':not(.order-items .auto-complete-items)', function () {
        $('.add .auto-complete-items').css({ 'display': 'none' }).html('');
    });

    //change count event
    $(document).on('change', '.pricing-item .copy-count,.pricing-item .doc-count', function () {
        $(this).closest('.item').data('changed', 'true');
        orderItem.changeCount($(this).closest('.pricing-item'));
    });

    //change official record input event
    $(document).on('change', '.official-record-item .copy-count,.official-record-item .doc-count', function () {
        orderItem.changeCount($(this).closest('.official-record-item'), false);
    });

    //change total price event
    $(document).on('change', '.total-price input', function () {
        let totalSum = 0;
        let $items = orderItem.$wrapper.find('.pricing-item,.official-record-item,.discount-item');
        $items.each(function (idx, elm) {
            totalSum += parseInt($(this).find('.total-price input').val());
            if (idx == $items.length - 1)
                orderItem.$wrapper.find('.total-sum').data('total-sum', totalSum).text(" تومان " + totalSum.toString().cThSeperator());
        });

    });


    //adding most used pricing item
    $(document).on('click', '.mustly-used-item', function () {
        var info = $(this).data('info');
        if ($(this).hasClass('activate')) {
            orderItem.delete(orderItem.$wrapper.find(".item[data-id='" + info.PricingItemId + "']").find(".remove"), { serverSideRemove: false });
            $(this).removeClass('activate');
            //return;
        }
        else if (orderItem.$wrapper.find('.item[data-id=' + info.PricingItemId + ']').length === 0) {
            $(this).addClass('activate');
            orderItem.add($(this).data("info"));
            orderItem.sum();
        }

    });

    //
    $(document).on('click', '.btn-show-modal', function () {
        addModal.open();
    });

    //
    $(document).on('click', '.close-modal', function () {
        addModal.close();
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

        orderItem.changeLang();
    });
});

var addModal = function () {
    var opened = false;
    this.open = function () {
        opened = true;
        $('.order-item-add-modal').removeClass('fadeOutDown').addClass('fadeInUp').show();
    };
    this.close = function () {
        opened = false;
        $('.order-item-add-modal').removeClass('fadeInUp').addClass('fadeOutDown').hide();
    };
    return this;
}();

