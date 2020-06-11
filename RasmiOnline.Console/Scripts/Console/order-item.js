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
            priceInOtherLangs: item.Price_OthersLang,
            copyPrice: item.CopyPrice,
            copyPriceInOtherLangs: item.CopyPrice_OthersLang,
            totalPrice: (orderItem.inEnglish ? item.Price : item.Price_OthersLang),
            serverSideRemove: false
        };
        let $tr = $('<tr class="item pricing-item" data-IsPricingItem="' + item.IsPricingItem + '" data-id="' + item.PricingItemId + '" data-added="true">' +
            '<td class="remove"><i class="zmdi zmdi-close-circle"></i></td>' +
            '<td >' + item.DocumentType + '</td>' +
            '<td>' + item.PricingItemUnitText + '</td>' +
            '<td class="price">' +
            (this.userIsAdmin ? ('<input dir="ltr" type="numbe" name="price" value="' + (orderItem.inEnglish ? item.Price : item.Price_OthersLang) + '"/>') : (orderItem.inEnglish ? item.Price : item.Price_OthersLang)) +
            '</td>' +
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
            info.totalPrice.toString().cThSeperator() +
            '</td>' +
            '</tr>');
        $tr.data('info', info).insertBefore(orderItem.$wrapper.find('.plus-wrapper'));
        orderItem.update($tr);

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
                    notify(rep.IsSuccessful, rep.Message);
                })
                .fail(function (e) {
                    $elm.html($elmText);
                    console.log(e.responseTest);
                    notify(false, errorMsg);
                });
        }
        orderItem.sum();
    },
    update: function ($elm) {
        //==================== Update Official Record Item
        if ($elm.hasClass('pricing-item')) {
            let $officialRecord = orderItem.$wrapper.find('.official-record-item');
            if ($officialRecord.length > 0) {
                let totalCount = 0;
                orderItem.$wrapper.find('.pricing-item .doc-count').each(function () { totalCount += parseInt($(this).val()); });
                let $oRCount = $officialRecord.find('.doc-count');
                if ($oRCount.is('select')) $oRCount.val(totalCount.toString());
                else $oRCount.text(totalCount.toString());
                let totalCopyCount = 0;
                orderItem.$wrapper.find('.pricing-item .copy-count').each(function () { totalCopyCount += parseInt($(this).val()); });
                let $oRCopy = $officialRecord.find('.copy-count');
                if ($oRCopy.is('select')) $oRCopy.val(totalCopyCount.toString());
                else $oRCopy.text(totalCopyCount.toString());
                orderItem.update($officialRecord);
            }
        }

        //==================== Update Current Record Item
        let info = $elm.data('info');
        let $price = $elm.find('input[name="price"]');
        let price = parseInt($price.length > 0 ? $price.val() : (orderItem.inEnglish ? info.price : info.priceInOtherLangs));
        let $count = $elm.find('.doc-count');
        let count = parseInt($count.is('select') ? $count.val() : $count.text());
        let $copy = $elm.find('.copy-count');
        let copy = parseInt($copy.is('select') ? $copy.val() : $copy.text());
        let totalCopyPrice = info.copyPrice ? ((orderItem.inEnglish ? info.copyPrice : info.copyPriceInOtherLangs) * count * copy) : 0;
        if ($elm.hasClass('official-record-item')) info.totalPrice = Math.floor(price * (count + copy));
        else info.totalPrice = Math.floor(price * count + totalCopyPrice);
        //================ Update UI
        $elm.find('.total-copy-price').text(totalCopyPrice.toString().cThSeperator());
        console.log(info.totalPrice);
        $elm.find('.total-price').text(info.totalPrice.toString().cThSeperator());
        this.sum();
    },

    changeLang: function () {
        let oi = this;
        let $items = oi.$wrapper.find('.pricing-item');
        $items.each(function (idx, elm) {
            let info = $(this).data('info');
            $(this).find('input[name="price"]').val(orderItem.inEnglish ? info.price : info.priceInOtherLangs);
            orderItem.update($(this));
            if (idx === $items.length - 1)
                $(this).sum();
        });

    },
    sum: function () {
        let totalSum = 0;
        let $items = orderItem.$wrapper.find('.pricing-item,.official-record-item,.discount-item');
        $items.each(function (idx, elm) {
            let info = $(elm).data('info');
            totalSum += parseInt(info.totalPrice);
            if (idx === $items.length - 1)
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
    $(document).on('change', '.copy-count,.doc-count,input[name="price"]', function () {
        orderItem.update($(this).closest('tr'));
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

