var btnSendButton = 'BUTTON#btnSubmitForm';
var wordCounter = 'SPAN.word-counter-tarea';

var drpMessagingType = 'SELECT#MessagingType';
var txtReciver = 'INPUT#Reciver';
var txtMessageText = 'TEXTAREA#Message';

var validCharCount = 0;

$(function () {
    sendMsgManage.init();
});

var sendMsgManage = {
    init: function () {
        $(document).on("change", drpMessagingType,
            function (evt) {
                evt.preventDefault();
                let $this = $(this);

                $(txtReciver).val('');
                $(txtReciver).removeClass('only-digit');
                switch ($this.val()) {
                    case 'Email':
                        validCharCount = 1000;
                        $(txtReciver).attr('placeholder', 'example@gmail.com');
                        break;
                    case 'Sms':
                        validCharCount = 500;
                        $(txtReciver).attr('placeholder', '09128765432');
                        $(txtReciver).addClass('only-digit');
                        break;
                    default:
                        validCharCount = 0;
                        $(txtReciver).removeAttr('placeholder');
                        break;
                }
                $(wordCounter).html(validCharCount + ' حرف مانده');
                $(txtMessageText).attr('maxlength', validCharCount);
            });

        $(document).on("keyup", txtMessageText,
            function (evt) {
                let $this = $(this);
                if ($(drpMessagingType).val() === 'PleaseSelect') {
                    $(drpMessagingType).focus();
                    $this.val('');
                    return false;
                }
                var len = validCharCount - ($this.val().length);
                $(wordCounter).html(len + ' حرف مانده');
            });

        $(document).on("click", btnSendButton,
            function (evt) {
                evt.preventDefault();
                let $this = $(this);

                if ($(drpMessagingType).val() === undefined || $(drpMessagingType).val() === 'PleaseSelect') {
                    $(drpMessagingType).focus();
                    return false;
                }

                sendMsgManage.actions.submitForm();
            });
    },
    actions: {
        submitForm: function () {
            var form = $('FORM#Message_FormControls');
            var submitUrl = form.attr('data-submit-action')

            if (form.valid() === false) {
                return false;
            }

            $.ajax({
                url: submitUrl,
                data: form.serialize(),
                type: 'POST'
            }).done(function (result) {
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
                        title: 'ارسال پیام',
                        text: 'پیام شما با موفقیت ارسال شد',
                        confirmButtonColor: "#32c787",
                        showCancelButton: false,
                        confirmButtonText: "باشه",
                    }, function (isConfirm) { if (isConfirm) { location.reload(); } });
                }
            }).fail(function (xhr, status) {
                console.log("error in send '" + $(drpMessagingType).val() + "' message");
            });
        }
    }
};