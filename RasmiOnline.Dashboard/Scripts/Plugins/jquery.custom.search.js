var searchFormButton = 'BUTTON[data-element-name="SubmitSearchForm"]';
var resetFormButton = 'BUTTON[data-element-name="ResetSearchForm"]';

var searchFormButtonMobile = 'BUTTON[data-element-name="SubmitSearchFormMobile"]';
var resetFormButtonMobile = 'BUTTON[data-element-name="ResetSearchFormMobile"]';

var searchBoxMobile = 'DIV.search-box-mobile';
var collapsedMenusState = 'I.search-collapsed-state';
var menuSearchItem = 'DIV.menu-search-item';

$(function () {
    searchManagment.init();
});


var searchManagment = {
    init: function () {
        $(document).off("click", searchFormButton + ', ' + searchFormButtonMobile)
            .on("click", searchFormButton + ', ' + searchFormButtonMobile,
                function (evt) {
                    evt.preventDefault();
                    var entityName = $(this).attr('data-entity-name');
                    var formName = $(this).attr('data-form-name');
                    searchManagment.actions.search(entityName, formName);
                    $(searchBoxMobile).attr('data-collapse-state', 'close')
                    $(collapsedMenusState).removeClass('zmdi-chevron-up').addClass('zmdi-chevron-down');
                    $(menuSearchItem).animate({ 'height': '0px' }, 300);
                    setTimeout(function () {
                        $(menuSearchItem).css('display', 'none');
                    }, 200);
                });

        $(document).off("click", resetFormButton + ', ' + resetFormButtonMobile)
            .on("click", resetFormButton + ', ' + resetFormButtonMobile,
                function (evt) {
                    evt.preventDefault();
                    var formName = $(this).attr('data-form-name');
                    $('FORM#' + formName).trigger("reset");
                });

        $(document).off("click", searchBoxMobile)
            .on("click", searchBoxMobile,
                function (evt) {
                    evt.preventDefault();
                    if ($(searchBoxMobile).attr('data-collapse-state') === 'close') {
                        $(searchBoxMobile).attr('data-collapse-state', 'open')
                        $(collapsedMenusState).removeClass('zmdi-chevron-down').addClass('zmdi-chevron-up');
                        $(menuSearchItem).animate({ 'max-height': '400px', 'height': '400px' }, 300);
                        setTimeout(function () {
                            $(menuSearchItem).css('display', 'none').css('display', 'block');
                        }, 200);
                    } else {
                        $(searchBoxMobile).attr('data-collapse-state', 'close')
                        $(collapsedMenusState).removeClass('zmdi-chevron-up').addClass('zmdi-chevron-down');
                        $(menuSearchItem).animate({ 'height': '0px' }, 300);
                        setTimeout(function () {
                            $(menuSearchItem).css('display', 'none');
                        }, 200);
                    }
            });
    },
    actions: {
        search: function (entityName, formName) {
            let $form = $('FORM#' + formName);
            let $submitUrl = $form.attr('data-submit-action');
            let $resultContainer = $('DIV#' + entityName + '_SearchResultContainer');

            if ($form.valid() === false) {
                return false;
            }

            loadingOverlay.show();

            $.ajax({
                data: $form.serialize(),
                url: $submitUrl,
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html'
            })
                .done(function (result) {
                    $resultContainer.html(result);
                    fireJqueryDataTable();
                    loadingOverlay.hide();
                })
                .fail(function () {
                    loadingOverlay.hide();
                    console.log('error in get user searching...');
                });
        }
    }
};

var fireJqueryDataTable = function () {
    $("#data-table").DataTable(
        {
            autoWidth: !1,
            responsive: !0, lengthMenu: [[15, 30, 45, -1],
            ["15 رکورد", "30 رکورد", "45 رکورد", "تمامی رکورد ها"]],
            language: { searchPlaceholder: "در لیست زیر جستجو نمایید" },
            dom: "Blfrtip",
            buttons: [{
                extend: "excelHtml5",
                title: "Export Data"
            },
            { extend: "csvHtml5", title: "Export Data" },
            { extend: "print", title: "Material Admin" }
            ],
            initComplete: function (a, b) {
                $(this).closest(".dataTables_wrapper")
                    .prepend('<div class="dataTables_buttons hidden-sm-down actions"><span class="actions__item zmdi zmdi-print" data-table-action="print" /><span class="actions__item zmdi zmdi-fullscreen" data-table-action="fullscreen" /><div class="dropdown actions__item"><i data-toggle="dropdown" class="zmdi zmdi-download" /><ul class="dropdown-menu dropdown-menu-right"><a href="" class="dropdown-item" data-table-action="excel">اکسل</a><a href="" class="dropdown-item" data-table-action="csv">خروجی SVC</a></ul></div></div>')
            }
        });

    $(".dataTables_filter input[type=search]").focus(function () {
        $(this).closest(".dataTables_filter").addClass("dataTables_filter--toggled")
    });

    $(".dataTables_filter input[type=search]").blur(function () {
        $(this).closest(".dataTables_filter").removeClass("dataTables_filter--toggled")
    });
}

var loadingOverlay = {
    overlay: '<div class="page-loader" style="position:absolute;opacity:0.5;background-color:#ccc;">' +
        '<div class="page-loader__spinner">' +
        '<svg viewBox="25 25 50 50">' +
        '<circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle>' +
        '</svg>' +
        '</div>' +
        '</div>',
    show: function () {
        let $loadingOver = $('DIV#loadingOver');

        if ($loadingOver) $loadingOver.prepend(loadingOverlay.overlay);
    },
    hide: function () {
        let $loadingOver = $('DIV#loadingOver');

        if ($loadingOver) $loadingOver.find('DIV.page-loader').remove();
    }
};