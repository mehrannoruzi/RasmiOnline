///<reference path="../../jquery-1.10.2.min.js"/>

//message business
var alertBox = function () {

    var alertBox = function () { };

    alertBox.elm = function ($elm) { return $elm }
    //hide it
    alertBox.hide = function () {
        this.elm.removeClass('alert-danger').removeClass('alert-success').css({ 'visibility': 'hidden' });
        this.elm.find('small').html('');
    };

    //show success message
    alertBox.success = function (msg) {
        this.elm.css({ 'visibility': 'visible' }).removeClass('alert-danger').addClass('alert-success').find('small').html(msg);
    };

    //show error message
    alertBox.error = function (msg) {
        this.elm.css({ 'visibility': 'visible' }).removeClass('alert-success').addClass('alert-danger').find('small').html(msg);
    };
    return alertBox;
}

var msg = new alertBox();

$(document).on('ready', function () {

    getUrlVars();

    var ref = getCookie("ref");
    if (ref != "") {
        $("#ReferralCode").val(ref);
    }

    //navigate between blocks
    var gotoBlock = function (blockId) {
        console.log(blockId);
        //$('.login100-form-title-1').text($(blockId).data('title'));
        $('.form-block').removeClass('active');

        $(blockId).addClass('active');
    };

    $(document).on('click', '.inner-nav', function () {
        gotoBlock($(this).attr('href'));
    });

    //sign up event
    $(document).on('click', '#btn-sign-up', function () {
        console.log('here it is');
        let $btn = $(this);
        let $frm = $btn.closest('form');
        if (!$frm.valid()) return;
        ajaxBtn.inProgress($btn);
        msg.elm = $frm.find('.alert');
        $.post($frm.attr('action'), $frm.serialize())
            .done(function (rep) {
                ajaxBtn.normal();
                if (rep.IsSuccessful) {
                    window.location.href = rep.Result;
                }
                else {
                    msg.error(rep.Message);
                    console.log(rep);
                }

            })
            .fail(function (e) {
                ajaxBtn.normal();
                msg.error('خطایی رخ داده است، دوباره سعی نمایید.');
                console.log(e.responseText);
            });

    });

    //confirm mobile
    $(document).on('click', '#btn-confirm-code', function () {
        let $btn = $(this), $frm = $btn.closest('form');
        msg.elm = $frm.find('.alert');
        ajaxBtn.inProgress($btn);
        $.post($frm.attr('action'), $frm.serialize())
            .done(function (rep) {
                ajaxBtn.normal();
                console.log(rep);
                if (rep.IsSuccessful)
                    window.location.href = rep.Result;
                else {
                    msg.error(rep.Message);
                    console.log(rep);
                }
            })
            .fail(function () {
                ajaxBtn.normal();
                console.log('error');
            });
    });

    //resend sms
    $(document).on('click', '.sms-try .resend', function () {
        $(this).hide();
        sms.send();
    });

    //sign in event
    $(document).on('click', '.btn-sign-in', function () {
        let $btn = $(this);
        let $frm = $btn.closest('form');
        console.log($frm.serialize());
        if (!$frm.valid()) return;
        ajaxBtn.inProgress($btn);
        msg.elm = $frm.find('.alert');
        $.post($frm.attr('action'), $frm.serialize())
            .done(function (rep) {
                ajaxBtn.normal();
                if (rep.IsSuccessful) {
                    window.location.href = rep.Result;
                }
                else {
                    msg.error(rep.Message);
                    console.log(rep);
                }

            })
            .fail(function (e) {
                //$btn.text(txt);
                ajaxBtn.normal();
                msg.error('خطایی رخ داده است، دوباره سعی نمایید.');
                console.log(e.responseText);
            });
    });

    //Recover Password
    $(document).on('click', '#btn-recovery', function () {
        console.log('fired');
        let $btn = $(this), $frm = $btn.closest('.form');
        msg.elm = $frm.find('.alert');
        ajaxBtn.inProgress($btn);
        let usName = $('#RecoveryUserName').val();
        if (usName == '') {
            $input.addClass('input-validation-error');
            return;
        }

        $.post($frm.attr('action'), { recoveryUserName: usName })
            .done(function (rep) {
                ajaxBtn.normal();
                console.log(rep);
                if (rep.IsSuccessful)
                    gotoBlock('#signin-form');
                else {
                    msg.error(rep.Message);
                    console.log(rep);
                }
            })
            .fail(function () {
                ajaxBtn.normal();
                console.log('error');
            });
    });

    //send sms Biz
    var sms = function () {
        var tryCount = 1;
        var int = null, $wrapper = $('.sms-try');
        this.send = function () {
            tryCount++;
            var timeInSec = 60;
            if (int != null)
                clearInterval(int);
            if (tryCount <= 3) {
                $.post($wrapper.data('url'), { UserId: $('#UserId').val() })
                    .done(function (rep) {
                        if (rep.IsSuccessful) {
                            $wrapper.find('.counter').show().find('.val').text(timeInSec);
                            int = setInterval(function () {
                                timeInSec--;
                                $wrapper.find('.val').text(timeInSec);
                                if (timeInSec == 0) {
                                    timeInSec = 60;
                                    clearInterval(int);
                                    $('.sms-try .counter').hide();
                                    $('.sms-try .resend').show();
                                }

                            }, 1000);
                        }
                        else {
                            msg.error(rep.Message);
                        }
                    })
                    .fail(function () {
                        msg.error(errorMsg);
                    });
            }
            else {
                $('#confirm-from button').prop('disabled', true);
                msg.error('تلاش بیشتر از 3 مرتبه مجاز نمی باشد.');
            }
        };
        return this;
    }();

});

function openNav() {
    document.getElementById("mySidenav").style.right = "0";
}

function closeNav() {
    document.getElementById("mySidenav").style.right = "-100%";
}

//$(function () {
//    $('[data-toggle="tooltip"]').tooltip()
//});
var queryString;
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.toLowerCase().indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    queryString = vars;
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}