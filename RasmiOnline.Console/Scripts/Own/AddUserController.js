var submitFormButton = 'BUTTON#btnSubmitChange';


$(function () {
    userMng.init();
});

var userMng = {
    init: function () {

        $(document).off("click", submitFormButton)
            .on("click", submitFormButton,
                function (evt) {
                    evt.preventDefault();
                    userMng.actions.submitForm();
                });

    },
    actions: {
        submitForm: function () {
            var form = $('FORM#AddUserModel_FormControls');
            var submitUrl = form.attr('data-submit-action');

            if (form.valid() === false) {
                return false;
            }

            $.ajax({
                url: submitUrl,
                data: form.serialize().replace(/Result./gi, ""),
                type: 'POST'
            })
                //
                .done(function (result) {
                    if (result.IsSuccessful === false) {
                        swal({
                            title: 'خطا',
                            text: result.Message,
                            confirmButtonColor: "#f73232",
                            showCancelButton: false,
                            confirmButtonText: "باشه",
                        }, function (isConfirm) { if (isConfirm) { } });
                    } else {
                        swal({
                            title: 'افزودن کاربر جدید',
                            text: 'کاربر جدید با موفقیت افزوده شد',
                            confirmButtonColor: "#32c787",
                            showCancelButton: false,
                            confirmButtonText: "باشه",
                        }, function (isConfirm) { if (isConfirm) { location.reload(); } });

                    }
                })
                //
                .fail(function (xhr, status) {
                    console.log('error in submit add new user');
                });
        }
    }
};