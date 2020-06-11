/// <reference path="../jquery-1.10.2.min.js" />
$(document).on('ready', function () {
    //add new order name and refresh list
    $(document).on('click', ".btn-add-order-name", function () {
        submitAjaxForm($(this), function (rep) {
            $(rep.Result).insertBefore('.add-order-name');
            $('.btn-add-order-name').closest('form')[0].reset();
        });
    });

    $(document).on('click', ".btn-add-order-desc", function () {
        submitAjaxForm($(this), function (rep) {
            $(rep.Result).insertBefore('.add-order-desc');
            $('.btn-add-order-desc').closest('form')[0].reset();
        });
    });

 

    //remove order name
    $(document).on('click', ".order-name-item .remove", function () {
        let $elm = $(this), $elmContent = $elm.html();
        $elm.html(threeDotLoader);
        $.post($elm.data('url'))
            .done(function (rep) {
                $elm.html($elmContent);
                if (rep.IsSuccessful) {
                    $elm.closest('.order-name-item').remove();
                }
                else notify(false, rep.Message);
            })
            .fail(function (e) {
                $elm.html($elmContent);
            });
    });
});