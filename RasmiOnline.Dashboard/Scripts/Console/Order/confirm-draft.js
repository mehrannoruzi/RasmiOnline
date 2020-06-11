/// <reference path="../jquery-1.10.2.min.js" />
$(document).on('ready', function () {
    $(document).on('change', '#IsConfirmedByClient', function () {
        console.log($(this).prop('checked'));
        $(this).val(($(this).prop('checked')).toString());
    });

    $(document).on('click', '#btn-confirm-draft', function () {
        let $btn = $(this);
        submitAjaxForm($btn);
    });

});