/// <reference path="../jquery-1.10.2.min.js"/>

var txtSearchUserName = 'INPUT#Result_SearchUserName';
var showUserFullNameFGroup = 'DIV#ShowUserFullName DIV.form-group';
var showUserFullNameFControl = 'DIV#ShowUserFullName DIV.form-control';

$(document).on('ready', function () {
    chnlManager.init();

    //show modal event for add & editing channel
    $(document).on('click', '.add-channel-button,.channel-edit,.add-channel', function () {
        let $elm = $(this);;
        $('#modal .modal-title').html($elm.data('modal-title'));
        $('#modal .modal-body').html(threeDotLoader).load($elm.data('modal-url'), function () {
            $.validator.unobtrusive.parse($('#modal'));
            $('#modal').modal();
        });
    });

    //submit channel form in modal
    $(document).on('click', '#channel-form button', function () {
        submitAjaxForm($(this), function (rep) {
            if (rep.IsSuccessful)
                location.reload();
        });
    });

});

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

var chnlManager = {
    init: function () {
        $(document).off("input", txtSearchUserName)
            .on("input", txtSearchUserName,
                function (evt) {
                    evt.preventDefault();
                    let $this = $(this);
                    if (validateEmail($this.val())) { // regex
                        chnlManager.actions.searchUser($this.attr('data-action-url') + '?username=' + $this.val())
                    } else {
                        $(showUserFullNameFGroup).removeClass('has-warning has-danger has-success').addClass('has-warning');
                        $(showUserFullNameFControl).removeClass('form-control-warning form-control-danger form-control-success').addClass('form-control-warning');
                        $(showUserFullNameFControl).html('نام کاربری به درستی وارد نشده است');
                    }
                });
    },
    actions: {
        searchUser: function (url) {
            $(showUserFullNameFControl).html('کمی صبر کنید...');

            $.ajax({
                url: url,
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json'
            })
                .done(function (result) {
                    $(showUserFullNameFGroup).removeClass('has-warning has-danger has-success').addClass('has-' + (result.IsSuccessful ? 'success' : 'danger'));
                    $(showUserFullNameFControl).removeClass('form-control-warning form-control-danger form-control-success').addClass('form-control-' + (result.IsSuccessful ? 'success' : 'danger'));
                    $(showUserFullNameFControl).html(result.IsSuccessful ? result.Result.FirstName : result.Message);
                    if (result.IsSuccessful) {
                        $("#UserId").val(result.Result.UserId);
                    }
                })
                .fail(function () {
                    modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                });
        }
    }
};