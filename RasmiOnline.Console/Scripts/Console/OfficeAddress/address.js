/// <reference path="../jquery-1.10.2.min.js"/>

var txtSearchUserName = 'INPUT#Result_SearchUserName';
var showUserFullNameFGroup = 'DIV#ShowUserFullName DIV.form-group';
var showUserFullNameFControl = 'DIV#ShowUserFullName DIV.form-control';


function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

$(document).on('ready', function () {
 
    $(document).off("input", txtSearchUserName)
           .on("input", txtSearchUserName,
               function (evt) {
                   evt.preventDefault();
                   let $this = $(this);
                   if (validateEmail($this.val())) { // regex
                       $(showUserFullNameFControl).html('کمی صبر کنید...');
                       $.ajax({
                           url: $this.attr('data-action-url') + '?username=' + $this.val(),
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

                   } else {
                       $(showUserFullNameFGroup).removeClass('has-warning has-danger has-success').addClass('has-warning');
                       $(showUserFullNameFControl).removeClass('form-control-warning form-control-danger form-control-success').addClass('form-control-warning');
                       $(showUserFullNameFControl).html('نام کاربری به درستی وارد نشده است');
                   }
               });

    //show modal event for add & editing address
    $(document).on('click', '.add-address-button,.address-edit,.add-office-address', function () {
        let $elm = $(this);;
        $('#modal .modal-title').html($elm.data('modal-title'));
        $('#modal .modal-body').html(threeDotLoader).load($elm.data('modal-url'), function () {
            $.validator.unobtrusive.parse($('#modal'));
            $('#modal').modal();
            fireGoogleMap();
        });
    });
     

    //change active status for office address
    $(document).on('click', '.address-update-status', function () {
        let $elm = $(this);
        swalConfirm(function () {
            $.post($elm.data('url'), function (rep) {
                if (rep.IsSuccessful) {
                    location.reload();
                }
                else notify(false, 'خطایی رخ داده است، دوباره سعی نمایید.');
            })
        });

    });

    //submit address form in modal
    $(document).on('click', '#address-form button', function () {
        submitAjaxForm($(this), function (rep) {
            if (rep.IsSuccessful)
                location.reload();
        });
    });

});
