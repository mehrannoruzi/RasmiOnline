/// <reference path="../../jquery-1.10.2.min.js" />
$(document).on('ready', function () {
    orderItem.userIsAdmin = true;

    $(document).on('click', '.submit-order', function () {
        let $btn = $(this);
        let $frm = $(this).closest('form');
        if (!$frm.valid()) return;

        var items = [];
        orderItem.$wrapper.find('.pricing-item').each(function () {
            let info = $(this).data('info');
            items.push({
                PricingItemId: info.pricingItemId,
                OrderItemType:'PricingItem',
                Count: $(this).find('.doc-count').val(),
                Copy: $(this).find('.copy-count').val(),
                TotalPrice: $(this).find('.total-price input').val()
            });
        });
        let $oRItem = orderItem.$wrapper.find('.official-record-item');
        let info = $oRItem.data('info');
        items.push({
            PricingItemId: info.pricingItemId,
            OrderItemType: 'OfficialRecordItem',
            Count: $oRItem.find('.doc-count').val(),
            Copy: $oRItem.find('.copy-count').val(),
            TotalPrice: $oRItem.find('.total-price input').val()
        });
        if (items.length === 0) { notify(false, 'لطفا حداقل یکی از اقلام را اننخاب نمایید'); return; }
        $('.summary .total-table tr.total td.total-sum > .val').text($("#tbl-order-items").find('td[total-sum] .val').text());
        ajaxBtn.inProgress($btn);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            url: $frm.data('action'),
            data: JSON.stringify({
                model: {
                    OrderTitle: $("#OrderTitle").val(),
                    LangType: $('#LangType').val(),
                    OfficeUserId: $('#OfficeUserId').val(),
                    User: {
                        Email: $('#Email').val(),
                        MobileNumber: $('#MobileNumber').val()
                    },
                    OrderItems: items
                }
            }),
            success: function (rep) {
                ajaxBtn.normal();
                console.log(rep);
                if (rep.IsSuccessful) window.location.href = $btn.data('redirect-url') + '/' + rep.Result
                else {
                    ajaxBtn.normal();
                    console.log(rep);
                    notify(false, rep.Message);
                }
            },
            error: function (e) { ajaxBtn.normal(); notify(false, errorMsg); console.log(e.responseText); }
        });

    });
});