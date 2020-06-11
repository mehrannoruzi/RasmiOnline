var orderStatus = '.chbx_according';

var orderStatusMobile = 'DIV.menu-item-select';
var showOrderBoardList = 'SPAN#ShowOrderBoardList';
var orderBoardMobile = 'DIV.order-board-mobile';
var orderAccordionTitle = 'SPAN.order-accordion-title';


$(function () { orderBoardManage.init(); });

var orderBoardManage = {
    init: function () {
        $(orderStatus).change(function () {
            if (!this.checked) {
                console.log('checked');
                let $this = $(this);
                orderBoardManage.actions.showList($this.attr('data-url'), $this.attr('data-type'));
            }
        });

        $(document).off("click", orderStatusMobile).on("click", orderStatusMobile,
               function (evt) {
                   evt.preventDefault();
                   let $this = $(this);
                   $(orderAccordionTitle).html($this.attr('data-desc'));
                   orderBoardManage.actions.showListMobile($this.attr('data-url'), $this.attr('data-type'));
               });

        $(document).off("click", orderBoardMobile).on("click", orderBoardMobile,
         function (evt) {
             evt.preventDefault();
             if ($(orderBoardMobile).attr('data-collapse-state') === 'close') {
                 $(orderBoardMobile).attr('data-collapse-state', 'open')
                 $(showOrderBoardList + ' i').removeClass('zmdi-chevron-down').addClass('zmdi-chevron-up');
                 $(orderBoardMobile).animate({ 'max-height': '300px', 'height': '300px' }, 300);
                 setTimeout(function () {
                     $(orderBoardMobile + ' DIV.menu-item').css('display', 'block');
                 }, 200);
             } else {
                 $(orderBoardMobile).attr('data-collapse-state', 'close')
                 $(showOrderBoardList + ' i').removeClass('zmdi-chevron-up').addClass('zmdi-chevron-down');
                 $(orderBoardMobile).animate({ 'height': '50px' }, 300);
                 setTimeout(function () {
                     $(orderBoardMobile + ' Div.menu-item').css('display', 'none');
                 }, 200);
             }
         });
    },
    actions: {
        showList: function (url, status) {
            let $resultContainer = $('DIV#' + status + '_ItemsList');
            orderBoardManage.tools.loadingOverlay.show(status);

            $.ajax({
                url: url + (status === 'AllOrderCounts' ? '' : '?orderStatus=' + status),
                data: JSON.stringify({}),
                type: 'GET',
                contentType: 'text/html'
            })
                .done(function (result) {
                    $resultContainer.html(result);
                    orderBoardManage.tools.loadingOverlay.hide(status);
                })
                .fail(function (xhr, status) {
                    console.log('error in get and show Order List with Status');
                    orderBoardManage.tools.loadingOverlay.hide(status);
                });
        },
        showListMobile: function (url, status) {
            let $resultContainer = $('DIV#itemsList_Mobile');
            orderBoardManage.tools.loadingOverlay.show(status);

            $.ajax({
                url: url + (status === 'AllOrderCounts' ? '' : '?orderStatus=' + status),
                data: JSON.stringify({}),
                type: 'GET',
                contentType: 'text/html'
            }).done(function (result) {
                $resultContainer.html(result);
                orderBoardManage.tools.loadingOverlay.hide(status);
            }).fail(function (xhr, status) {
                console.log('error in get and show Order List with Status');
                orderBoardManage.tools.loadingOverlay.hide(status);
            });
        }
    },
    tools: {
        loadingOverlay: {
            overlay: '<div class="page-loader" style="position:absolute;opacity:0.5;background-color:#ccc;">' +
                '<div class="page-loader__spinner">' +
                '<svg viewBox="25 25 50 50">' +
                '<circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle>' +
                '</svg>' +
                '</div>' +
                '</div>',
            show: function (status) {
                let $loadingOver = $('DIV#' + status + '_loadingOver');

                if ($loadingOver) $loadingOver.prepend(orderBoardManage.tools.loadingOverlay.overlay);
            },
            hide: function (status) {
                let $loadingOver = $('DIV#' + status + '_loadingOver');

                if ($loadingOver) $loadingOver.find('DIV.page-loader').remove();
            }
        }
    }
};

$(document).ready(function () {

    $(".btnReadTransaction").click(function () {
        $this = $(this);
        console.log($this.attr('data-transactionid'))
        ajaxBtn.inProgress($this);

        $.ajax({
            url: "/Transaction/Read",
            type: 'POST',
            data: {
                transactionId: $this.attr('data-transactionid'),
                isOffice: $this.attr('data-isOffice')
            },
            cache: false,
            dataType: 'json',
            success: function (rep) {
                ajaxBtn.normal($this);
                if (rep.IsSuccessful) {
                    $(".transaction_" + $this.attr('data-transactionid')).fadeOut();
                    $("#transaction-count").html(parseInt($("#transaction-count").html()) - 1);
                }
                else {

                    notify(false, rep.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ajaxBtn.normal($this);
                notify(false, errorMsg);
                console.log(jqXHR.responseText);
            }
        });
    });
});