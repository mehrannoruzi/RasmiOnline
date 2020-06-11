$(document).ready(function () {
    $(document).off("click", 'UL#orderPrice LI[data-id]')
        .on("click", 'UL#orderPrice LI[data-id]',
            function (evt) {
                evt.preventDefault();
                let $templateId = $(this).attr('data-id');
                getPriceItems($templateId, function (data) {
                    let $tabContent = $('DIV.tab-content');
                    $tabContent.html(data);
                }, function () { console.log('Error in load Pricing items with AJAX!'); });
            });

    $(document).on("mouseover", ".user-1", function () {
        $(this).addClass('active');
    });
    $(document).on("mouseleave", ".user-1", function () {
        $(this).removeClass('active');
    });

    $(document).on('mouseover', ".user-2", function () {
        $(this).addClass('active');
        $(".user-1").addClass('active');
    });
    $(document).on("mouseleave", ".user-2", function () {
        $(this).removeClass('active');
        $(".user-1").removeClass('active');
    });

    $(document).on('mouseover', ".user-3", function () {
        $(this).addClass('active');
        $(".user-2").addClass('active');
        $(".user-1").addClass('active');
    });
    $(document).on("mouseleave", ".user-3", function () {
        $(this).removeClass('active');
        $(".user-2").removeClass('active');
        $(".user-1").removeClass('active');
    });

    $(document).on('mouseover', ".user-4", function () {
        $(this).addClass('active');
        $(".user-3").addClass('active');
        $(".user-2").addClass('active');
        $(".user-1").addClass('active');
    });
    $(document).on("mouseleave", ".user-4", function () {
        $(this).removeClass('active');
        $(".user-3").removeClass('active');
        $(".user-2").removeClass('active');
        $(".user-1").removeClass('active');
    });

    $(document).on("click", ".user-1", function () {
        $(".box-user .no-active").removeClass('selected');
        $(this).addClass('selected');
    });
    $(document).on("click", ".user-2", function () {
        $(".box-user .no-active").removeClass('selected');
        $(this).addClass('selected');
        $(".user-1").addClass('selected');
    });
    $(document).on("click", ".user-3", function () {
        $(".box-user .no-active").removeClass('selected');
        $(this).addClass('selected');
        $(".user-2").addClass('selected');
        $(".user-1").addClass('selected');
    });
    $(document).on("click", ".user-4", function () {
        $(".box-user .no-active").removeClass('selected');

        $(this).addClass('selected');
        $(".user-3").addClass('selected');
        $(".user-2").addClass('selected');
        $(".user-1").addClass('selected');
    });

    $(document).on("click", ".classroom-select", function () {
        $(".classroom-select").removeClass('active');
        $(this).addClass('active');
    });
});

var getPriceItems = function (templateId, succesFunc, failedFunc) {
    if (templateId > 0) {
        $.ajax({
            url: 'https://panel.RasmiOnline.com/PricingItem/GetPricingItemTable?templateId=' + templateId,
            data: JSON.stringify({}),
            type: 'GET',
            contentType: 'text/html'
        })
            .done(function (result) {
                succesFunc(result);
            })
            .fail(function (xhr, status) {
                failedFunc();
            });
    }
    else {
        failedFunc();
    }
};

var getPriceItemsWithCategoryId = function (categoryId, succesFunc, failedFunc) {
    if (categoryId > 0) {

        $.ajax({
            url: 'https://panel.RasmiOnline.com/PricingItem/GetPricingItemTableWithCategoryId?categoryId=' + categoryId,
            data: JSON.stringify({}),
            type: 'GET',
            contentType: 'text/html'
        })
            .done(function (result) {
                succesFunc(result);
            })
            .fail(function (xhr, status) {
                failedFunc();
            });
    }
    else {
        failedFunc();
    }
};

var getPricingItemAccordion = function (succesFunc, failedFunc) {
    $.ajax({
        url: 'https://panel.RasmiOnline.com/PricingItem/GetPricingItemAccordion',
        data: JSON.stringify({}),
        type: 'GET',
        contentType: 'text/html'
    })
        .done(function (result) {
            succesFunc(result);
        })
        .fail(function (xhr, status) {
            failedFunc();
        });
};






//var queryString;
//function getUrlVars() {
//    var vars = [], hash;
//    var hashes = window.location.href.slice(window.location.href.toLowerCase().indexOf('?') + 1).split('&');
//    for (var i = 0; i < hashes.length; i++) {
//        hash = hashes[i].split('=');
//        vars.push(hash[0]);
//        vars[hash[0]] = hash[1];
//    }
//    queryString = vars;
//}

//function setCookie(name, value, days) {
//    var expires = "";
//    if (days) {
//        var date = new Date();
//        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
//        expires = "; expires=" + date.toUTCString();
//    }
//    document.cookie = name + "=" + (value || "") + expires + "; path=/;domain=rasmionline.com";
//}

//function getCookie(cname) {
//    var name = cname + "=";
//    var decodedCookie = decodeURIComponent(document.cookie);
//    var ca = decodedCookie.split(';');
//    for (var i = 0; i < ca.length; i++) {
//        var c = ca[i];
//        while (c.charAt(0) == ' ') {
//            c = c.substring(1);
//        }
//        if (c.indexOf(name) == 0) {
//            return c.substring(name.length, c.length);
//        }
//    }
//    return "";
//}
//function uniqueClick(refrence) {
//    $.ajax({
//        url: 'https://panel.RasmiOnline.com/Channel/Visit',
//        data: { refrence: refrence },
//        type: 'POST',
//        contentType: 'text/html'
//    }).done(function (result) {
//        succesFunc(result);
//    }).fail(function (xhr, status) {
//        failedFunc();
//    });
//}

//$(document).on('ready', function () {

//    getUrlVars();
//    if (queryString["ref"] != undefined) {
//        if (getCookie(queryString["ref"]) != "") {
//            uniqueClick(queryString["ref"]);
//        }
//        setCookie("ref", queryString["ref"], 180);
//        $("#ReferralCode").val(queryString["ref"]);
//    }
//});