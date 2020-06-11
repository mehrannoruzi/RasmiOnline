/// <reference path="../../jquery-1.10.2.min.js" />
$(document).on('ready', function () {

    $(document).on('click', '.next-panel', function () {
        changePanelFactory($(this).data('strategy'));
    });

    $(document).on('click', '.prev-panel', function () {
        changePanelFactory($(this).data('strategy'));
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

var changePanelFactory = function (strategy) {
    switch (strategy) {
        case 'save':
            saveDetailedOrder();
            break;
        case 'f':
            panel.forward();
            break;
        case 'b':
            panel.backward();
            break;
        default:
            break
    }
};
var saveDetailedOrder = function () {
    let $btn = $('.panels > .active button.next-panel');
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
    $('.summary .total-table tr.total td.total-sum > .val').text($("#tbl-order-items").find('td[total-sum] .val').text());
    ajaxBtn.inProgress($btn);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        url: url,
        data: JSON.stringify({
            model: {
                OrderTitle: $("#orderTitle").val(),
                LangType: $('#LangType').val(),
                OfficeUserId : $('#OfficeUserId').val(),
                OrderItems: items
            }
        }),
        success: function (rep) {
            ajaxBtn.normal();
            if (rep.IsSuccessful) {
                $('.summary').prepend(rep.Result.Summary);
                $('.summary').prepend(rep.Result.Header);

            }
            else {
                ajaxBtn.normal();
                console.log(rep);
                notify(false, rep.Message);
            }
            panel.forward();
        },
        error: function (e) { ajaxBtn.normal(); notify(false, errorMsg); console.log(e.responseText); }
    });
};

