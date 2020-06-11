/// <reference path="../jquery-1.10.2.min.js"/>
$(document).on('ready', function () {

    //show modal event for add & editing address
    $(document).on('click', '.add-bankcard-button,.bankcard-edit', function () {
        let $elm = $(this);;
        $('#modal .modal-title').html($elm.data('modal-title'));
        $('#modal .modal-body').html(threeDotLoader).load($elm.data('modal-url'), function () {
            $.validator.unobtrusive.parse($('#modal'));
            $('#modal').modal();
            fireGoogleMap();
        });
    });


  

    //submit address form in modal
    $(document).on('click', '#bankcard-form button', function () {
        submitAjaxForm($(this), function (rep) {
            if (rep.IsSuccessful)
                location.reload();
        });
    });

});