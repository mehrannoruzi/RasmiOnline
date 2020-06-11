/// <reference path="../jquery-1.10.2.min.js" />
var errorMsg = "خطای رخ داده است، دوباره سعی نمایید";
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

$(document).on('ready', function () {
    $(document).on('click', ':not(.ajax-auto-complete)', function () {
        $('.auto-complete-items').html('').css({ 'display': 'none' });
    });

    //for triggering button click event on pressing enter
    $(document).on('keydown', 'input[data-trigger-click]', function (e) {
        if (e.keyCode == 13) {
            let btnId = $(this).data('trigger-click');
            $(btnId).trigger('click');
        }
    });

    if (getCookie("_.ASPXAUTH") != '' && getCookie("_.ASPXAUTH") != null) {
        $(".signInPanel").removeClass("hide");
    } else {
        $(".signInPanel").addClass("hide");
    }
});

//for adding persian calander plugin
$('.pdate').each(function () {
    var thisElm = $(this);
    $(this).Zebra_DatePicker();
});

$('.endate').each(function () {
    var thisElm = $(this);
    $(this).EnZebra_DatePicker({
        icon_position: 'left'
    });
});
//author: sh.b
//description: 
//1-for data-type= 'dynamic' element must have data-target with init value
//2-for data-type= 'static' element must have data-location value(location)
var fireGoogleMap = function () {
    $('.google-map:not(.fired)').each(function () {
        let $elm = $(this).addClass('fired');
        let type = $elm.data('type');
        //location of this type of map can be changed...
        if (type === 'dynamic') {
            let $target = $($elm.data('target'));
            let arr = $('#Location').val().split(',');
            let defLoc = new google.maps.LatLng(arr[0], arr[1]);
            let selectorMap = new google.maps.Map($elm[0],
                {
                    zoom: 15,
                    center: defLoc,
                });

            // marker STARTS
            let selectorMarker = new google.maps.Marker({
                position: defLoc,
                title: "Click to view info!",
                icon: '/content/images/icon/MyLocationMarkerIcon.png'
            });
            selectorMarker.setMap(selectorMap)
            //Listen for any clicks on the map.
            google.maps.event.addListener(selectorMap, 'click', function (event) {
                //Get the location that the user clicked.
                var clickedLocation = event.latLng;
                //If the marker hasn't been added.
                if (selectorMarker === false) {
                    //Create the marker.
                    selectorMarker = new google.maps.Marker({
                        position: clickedLocation,
                        map: selectorMap,
                        draggable: true //make it draggable
                    });
                    //Listen for drag events!
                    google.maps.event.addListener(marker, 'dragend', function (event) {
                        markerLocation();
                    });
                } else {
                    //Marker has already been added, so just change its location.
                    selectorMarker.setPosition(clickedLocation);
                }
                //Get the marker's location.
                var currentLocation = selectorMarker.getPosition();
                $target.val(currentLocation.lat() + ',' + currentLocation.lng());
            });
        }
            //location of this type of map is static ...
        else if (type === 'static') {
            let latLang = $elm.data('location').split(',');
            var map = new google.maps.Map($elm[0], {
                center: new google.maps.LatLng(latLang[0], latLang[1]),
                zoom: typeof $elm.data('zoom') !== 'undefined' ? parseInt($elm.data('zoom')) : 15,
                scrollwheel: false,
                navigationControl: false,
                mapTypeControl: false,
                scaleControl: false,
                draggable: false,
            });
            new google.maps.Marker({
                position: new google.maps.LatLng(latLang[0], latLang[1]),
                map: map,
                icon: '/content/images/icon/MyLocationMarkerIcon.png'
            });
        }
    });
};

//--- loaders
var threeDotLoader = '<span class="crisp-1epg1l4 crisp-ukuhcj"><span class="crisp-h5bnuo crisp-6t8xxc"></span><span class="crisp-h5bnuo crisp-6t8xxc"></span><span class="crisp-h5bnuo crisp-6t8xxc"></span></span>';
var $circularLoader = '<div class="page-loader__spinner btn-loader__spinner btn-loader" style="float:left;right :0;padding :18px;top:0px;background-color :#ffffff26;color: white;"><svg viewBox="25 25 50 50"><circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle></svg></div>';

/*--------------------------------------
            submit ajax form
---------------------------------------*/
var submitAjaxForm = function ($btn, successFun, errorFunc) {
    let $frm = $btn.closest('form');
    if (!$frm.valid()) return;
    ajaxBtn.inProgress($btn);
    $.post($frm.attr('action'), $frm.serialize())
        .done(function (rep) {
            $btn.removeClass('waves-effect');
            if (rep.IsSuccessful) {
                if (typeof successFun === 'function') successFun(rep);
            }
            else {
                if (typeof errorFunc === 'function')
                    errorFunc(rep)
                else
                    notify(rep.IsSuccessful, rep.Message);
                console.log(rep);
            }
            ajaxBtn.normal();
        })
        .fail(function (e) {
            $btn.removeClass('waves-effect');
            ajaxBtn.normal();
            notify(false, 'خطایی رخ داده است، دوباره سعی نمایید.');
            console.log(e.responseText);
        });
}

/*--------------------------------------
show confirm modal before sepecified action
---------------------------------------*/
var swalConfirm = function (confirmedAction) {
    swal({
        title: '',
        text: "آیا مطمن هستید؟",
        confirmButtonColor: "#4285F4",
        showCancelButton: true,
        confirmButtonText: "بله",
        cancelButtonText: "نه"
    },
        function (isConfirm) {
            if (isConfirm && typeof confirmedAction === 'function') {
                confirmedAction();
            } else {

                //canceled
            }
        });
}

/*--------------------------------------
    Bootstrap Notify Notifications
---------------------------------------*/
var notify = function (successful, message) {
    $.notify({
        icon: 'zmdi zmdi-' + (successful ? 'check' : 'alert-triangle'),
        title: '',
        message: message,
        url: ''
    }, {
        element: 'body',
        type: (successful ? 'success' : 'danger'),
        allow_dismiss: true,
        placement: {
            from: 'top',
            align: 'left'
        },
        offset: {
            x: 20,
            y: 20
        },
        spacing: 10,
        z_index: 1031,
        delay: 2500,
        timer: 1000,
        url_target: '_blank',
        mouse_over: false,
        animate: {
            enter: 'animated fadeInDown',
            exit: 'animated fadeOutUp'
        },
        template: '<div data-notify="container" class="alert alert-dismissible alert-{0} alert--notify" role="alert">' +
            '<span data-notify="icon"></span> ' +
            '<span data-notify="title">{1}</span> ' +
            '<span data-notify="message">{2}</span>' +
            '<div class="progress" data-notify="progressbar">' +
            '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
            '</div>' +
            '<a href="{3}" target="{4}" data-notify="url"></a>' +
            '<button type="button" aria-hidden="true" data-notify="dismiss" class="alert--notify__close"><i class="zmdi zmdi-close"></i></button>' +
            '</div>'
    });
};

$(function () {
    baseJavascript.init();
});

var baseJavascript = {
    init: function () {

        //$(document).off('keyup', 'INPUT.only-digit', function () { }).on('keyup', 'INPUT.only-digit', function () {
        //    baseJavascript.tools.defaultDigitValue($(this));
        //});

        $(document).off('keypress', 'INPUT.only-digit', function () { }).on('keypress', 'INPUT.only-digit', function (event) {
            return baseJavascript.tools.isNumberKey(event);
        });
    },
    tools: {
        defaultDigitValue: function (element) {
            if (element) {
                if (element.val() === '' || parseInt(element.val() <= 0)) {
                    element.val('0');
                    return false;
                }
            }
        },
        isNumberKey: function (evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        },
        toEnglishDigits: function () {
            var num_dic = {
                '۰': '0',
                '۱': '1',
                '۲': '2',
                '۳': '3',
                '۴': '4',
                '۵': '5',
                '۶': '6',
                '۷': '7',
                '۸': '8',
                '۹': '9',
            }

            return parseInt(this.replace(/[۰-۹]/g, function (w) {
                return num_dic[w]
            }));
        }
    }
};

var ajaxAutoComplete = function ($elm, appenderFunc, enterFunc) {
    var activeResultIdx = -1, items, $items;
    let $resultWrapper = $elm.siblings('.auto-complete-items');
    $elm.on("input", function () {
        if ($elm.val() != null && $elm.val() != '') {
            $(threeDotLoader).appendTo($elm.parent()).addClass('auto-comlete-loader')
                .css({
                    "position": "absolute",
                    "top": "50%",
                    "left": "15px",
                    "margin-top": "-13px",
                }).find('.crisp-h5bnuo').css({ "background-color": "#000" });
            $.get($elm.data('url'), { str: $elm.val() })
                .done(function (res) {
                    $elm.parent().find('.auto-comlete-loader').remove();
                    items = res;
                    if (items.length > 0) {
                        activeResultIdx = -1;
                        $resultWrapper.html(appenderFunc(items)).css({ 'display': 'block' }).children().each(function (idx) {
                            $(this).attr("data-item", JSON.stringify(items[idx])).on('click', function (e) {
                                enterFunc(items[idx]);
                                $elm.val('');
                            });
                        });

                        $items = $resultWrapper.find('li');
                    }
                    else {
                        //$resultWrapper.html('<li style="padding:10px;overflow:hidden;"><span style="float:right;">نتیجه ای یافت نشد</span></li>');
                        $resultWrapper.html('').css({ 'display': 'none' });
                    }
                });
        }
        else {
            items = [];
            $resultWrapper.html('').css({ 'display': 'none' });
        }
    });

    $elm.on("keydown", function (e) {
        if ($resultWrapper.children().length > 0) {
            //key down
            if (e.keyCode == 40) {
                $items.removeClass('active');
                if (activeResultIdx == $items.length - 1) activeResultIdx = 0;
                else activeResultIdx++;
                $items[activeResultIdx].classList.add('active');
            }
            else if (e.keyCode == 38) {
                $items.removeClass('active');
                if (activeResultIdx <= 0) activeResultIdx = $items.length - 1;
                else activeResultIdx--;
                $items[activeResultIdx].classList.add('active');
            }
            else if (e.keyCode == 13 && items.length > 0 && activeResultIdx > -1) {
                enterFunc(items[activeResultIdx]);
                $resultWrapper.css({ 'display': 'none' }).children().remove();
                $elm.val('');
            }
            else if (e.keyCode === 27) $resultWrapper.html('').css({ 'display': 'none' });
        }

    });

}

var ajaxBtn = function () {
    var _$btn, _$icon, _$btnHtml, btnH;
    this.inProgress = function ($btn, $loader) {
        _$btn = $btn;
        $loader = typeof $loader !== 'undefined' ? $loader : $circularLoader;
        _$btn.prop('disabled', true);
        if (_$btn.find('.icon').length > 0) {
            _$icon = $btn.find('.icon');
            _$btnHtml = _$icon.html();
            _$icon.html($loader);
        }
        else {
            btnH = _$btn.outerHeight(true);
            _$btnHtml = _$btn.html();
            _$btn.css({ "height": btnH + "px" }).html($loader);
        }

    };
    this.normal = function () {
        _$btn.prop('disabled', false);
        if (_$btn.find('.icon').length > 0) {
            _$icon.html(_$btnHtml);
        }
        else {
            _$btn.html(_$btnHtml);
        }
    };
    return this;
}();


$(document).on('click', '.setting .send-sms, .setting .send-email', function () {
    submitAjaxForm($(this), function (rep) {
        notify(true, rep.Message);
    });
});