var addNewItemButton = 'A[add-new-item-modal]';
var editItemButton = 'A[edit-item-modal]';
var deleteItemButton = 'A[data-delete-url]';
var submitFormButton = 'BUTTON[data-element-name="SubmitModalForm"]';


$(function () {
    modalManagment.init();
});


var modalManagment = {
    init: function () {
        //
        $(document).off("click", addNewItemButton)
            .on("click", addNewItemButton,
            function (evt) {
                evt.preventDefault();
                var entityName = $(this).attr('add-new-item-modal');
                var formUrl = $(this).attr('data-form-url');
                var modalTitle = $(this).attr('data-modal-title');
                modalManagment.actions.getForm(entityName, formUrl, modalTitle);
            });

        //
        $(document).off("click", editItemButton)
            .on("click", editItemButton,
            function (evt) {
                evt.preventDefault();
                var entityName = $(this).attr('edit-item-modal');
                var editUrl = $(this).attr('data-edit-url');
                var modalTitle = $(this).attr('data-modal-title');
                modalManagment.actions.getForm(entityName, editUrl, modalTitle);
            });

        //
        $(document).off("click", deleteItemButton)
            .on("click", deleteItemButton,
            function (evt) {
                evt.preventDefault();
                var deleteUrl = $(this).attr('data-delete-url');
                var modalTitel = $(this).attr('data-modal-title');
                modalManagment.actions.deleteItem(deleteUrl, modalTitel);
            });

        //
        $(document).off("click", submitFormButton)
            .on("click", submitFormButton,
            function (evt) {
                evt.preventDefault();
                var entityName = $(this).attr('data-entity-name');
                modalManagment.actions.submitForm(entityName);
            });
    },
    actions: {
        //
        getForm: function (entityName, url, modalTitle) {
            $.ajax({
                url: url,
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html'
            })
                //
                .done(function (result) {
                    $('DIV[data-modal-name="' + entityName + '_Modal"] DIV.modal-body').html(result);
                    $('DIV[data-modal-name="' + entityName + '_Modal"] H5.modal-title').html(modalTitle);
                    $('DIV[data-modal-name="' + entityName + '_Modal"]').modal();

                    $.validator.unobtrusive.parse($('FORM#' + entityName + '_FormControls'));
                    $('INPUT[data-mask]').each(function () {
                        $(this).mask($(this).data('mask'));
                    });

                    $('.pdate').each(function () {
                        var thisElm = $(this);
                        $(this).Zebra_DatePicker();
                    });

                    $('SELECT.select2').each(function () {
                        var thisElm = $(this);
                        $(this).select2();
                    });

                })
                //
                .fail(function () {
                    modalManagment.messageBar.showMessage('error', 'خطا در ارسال درخواست');
                });
        },
        //
        submitForm: function (entityName) {
            var form = $('FORM#' + entityName + '_FormControls');
            var submitUrl = form.attr('data-submit-action');


            //For custome callback function ---> data-callback-func == custome function name
            $('INPUT[data-callback-func], SELECT[data-callback-func]').each(function (index, value) {
                var callbackFunc = eval($(this).data('callback-func'));
                if (typeof callbackFunc === 'function') {
                    callbackFunc();
                }
            });

            if (form.valid() === false) {
                return false;
            }

            //
            $.ajax({
                url: submitUrl,
                data: form.serialize().replace(/Result./gi, ""),
                type: 'POST'
            })
                //
                .done(function (result) {
                    if (result.IsSuccessful === false) {
                        modalManagment.messageBar.showMessage('error', result.Message);
                    } else {
                        location.reload();
                    }
                })
                //
                .fail(function (xhr, status) {

                });
        },
        //
        deleteItem: function (url, confirmTitle) {
            swal({
                title: confirmTitle,
                text: 'آیا از حذف این مورد اطمینان دارید؟',
                confirmButtonColor: "#32c787",
                showCancelButton: true,
                confirmButtonText: "بله",
                cancelButtonText: "خیر",
            }, function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: url,
                        contentType: 'application/json; charset=utf-8',
                        type: 'GET',
                        dataType: 'json'
                    })
                        //
                        .done(function (result) {
                            if (result.IsSuccessful === false) {
                                modalManagment.messageBar.showMessage('error', result.Message);
                            } else {
                                location.reload();
                            }
                        })
                        //
                        .fail(function () {
                            modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                        });
                }
            });
        }
    },
    callback: function () {

    },
    messageBar: {
        messageType: {
            error: { titleColor: "#cc4542", msgClass: "modal-message-error" },
            success: { titleColor: "#019c2f", msgClass: "modal-message-success" },
            warning: { titleColor: "#e6ab0c", msgClass: "modal-message-warning" },
        },
        showMessage: function (messageType, messageText) {
            let $modalMessageBox = $('DIV.modal-show-message');

            $modalMessageBox.removeAttr('class');
            $modalMessageBox.attr('class');
            $modalMessageBox.addClass('modal-show-message');

            switch (messageType) {
                case 'error':
                    $modalMessageBox.addClass(modalManagment.messageBar.messageType.error.msgClass);
                    $modalMessageBox.html('<span style="background-color:' + modalManagment.messageBar.messageType.error.titleColor + ';">' + $modalMessageBox.data('msg-error-title') + '</span>' + messageText);
                    break;
                case 'success':
                    $modalMessageBox.addClass(modalManagment.messageBar.messageType.success.msgClass);
                    $modalMessageBox.html('<span style="background-color:' + modalManagment.messageBar.messageType.success.titleColor + ';">' + $modalMessageBox.data('msg-success-title') + '</span>' + messageText);
                    break;
                case 'warning':
                    $modalMessageBox.addClass(modalManagment.messageBar.messageType.warning.msgClass);
                    $modalMessageBox.html('<span style="background-color:' + modalManagment.messageBar.messageType.warning.titleColor + ';">' + $modalMessageBox.data('msg-warning-title') + '</span>' + messageText);
                    break;
            }
            $modalMessageBox.fadeIn('slow').delay(3000).fadeOut('slow');
        }
    }
};
