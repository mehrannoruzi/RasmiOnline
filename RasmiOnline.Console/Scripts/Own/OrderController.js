var orderStatus = 'DIV.quick-stats__item';
var orderStatusMobile = 'DIV.menu-item-select';
var showOrderBoardList = 'SPAN#ShowOrderBoardList';
var orderBoardMobile = 'DIV.order-board-mobile';
var orderAccordionTitle = 'SPAN.order-accordion-title';


$(function () { orderBoardManage.init(); });

var orderBoardManage = {
    init: function () {
        $(document).off("click", orderStatus + ' , ' + orderStatusMobile)
            .on("click", orderStatus + ' , ' + orderStatusMobile,
                function (evt) {
                    evt.preventDefault();
                    let $this = $(this);
                    $(orderAccordionTitle).html($this.attr('data-desc'));
                    orderBoardManage.actions.showList($this.attr('data-type'));
            });

        $(document).off("click", orderBoardMobile)
            .on("click", orderBoardMobile,
                function (evt) {
                    evt.preventDefault();
                    if ($(orderBoardMobile).attr('data-collapse-state') === 'close') {
                        $(orderBoardMobile).attr('data-collapse-state', 'open')
                        $(showOrderBoardList + ' i').removeClass('zmdi-chevron-down').addClass('zmdi-chevron-up');
                        $(orderBoardMobile).animate({ 'max-height': '300px', 'height': '300px' }, 300);
                        setTimeout(function () {
                            $(orderBoardMobile + ' DIV.menu-item').css('display','block');
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
        showList: function (status) {

            let $resultContainer = $('DIV#Order_ItemsList');

            orderBoardManage.tools.loadingOverlay.show();

            $.ajax({
                url: '/OrderAdmin/GetOrderList' + (status === 'AllOrderCounts' ? '' : '?orderStatus=' + status),
                data: JSON.stringify({}),
                type: 'GET',
                contentType: 'text/html'
            })
                .done(function (result) {
                    $resultContainer.html(result);
                    //fireJqueryDataTable();
                    orderBoardManage.tools.loadingOverlay.hide();
                })
                .fail(function (xhr, status) {
                    console.log('error in get and show Order List with Status');
                    orderBoardManage.tools.loadingOverlay.hide();
                });
        }
    },
    tools: {
        loadingOverlay : {
            overlay: '<div class="page-loader" style="position:absolute;opacity:0.5;background-color:#ccc;">' +
                '<div class="page-loader__spinner">' +
                '<svg viewBox="25 25 50 50">' +
                '<circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle>' +
                '</svg>' +
                '</div>' +
                '</div>',
            show: function () {
                let $loadingOver = $('DIV#loadingOver');

                if ($loadingOver) $loadingOver.prepend(orderBoardManage.tools.loadingOverlay.overlay);
            },
            hide: function () {
                let $loadingOver = $('DIV#loadingOver');

                if ($loadingOver) $loadingOver.find('DIV.page-loader').remove();
            }
        }
    }
};