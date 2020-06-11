var selectedRoleId = 0;
var isValidNumber = false;

var txtSearchMobileNumber = 'INPUT#Result_SearchMobileNumber';
var showUserFullNameFGroup = 'DIV#ShowUserFullName DIV.form-group';
var showUserFullNameFControl = 'DIV#ShowUserFullName DIV.form-control';

var userInRoleManagement = 'A[add-user-in-role-modal]';
var deleteUserInRole = 'A[data-user-in-role-delete-url]';
var submitFormAddUserInRole = 'BUTTON#AddUserInRoleSubmitForm';


var viewInRoleManagement = 'A[add-view-in-role-modal]';
var deleteViewInRole = 'A[data-view-in-role-delete-url]';
var submitFormAddViewInRole = 'BUTTON#AddViewInRoleSubmitForm';


$(function () {
    roleMng.init();
});

var roleMng = {
    init: function () {

        $(document).off("click", userInRoleManagement)
            .on("click", userInRoleManagement,
                function (evt) {
                    evt.preventDefault();
                    let entityName = $(this).attr('add-user-in-role-modal');
                    let formUrl = $(this).attr('data-add-user-in-role-url');
                    let modalTitle = $(this).attr('data-modal-title');
                    selectedRoleId = $(this).attr('data-role-id');
                    modalManagment.actions.getForm(entityName, formUrl, modalTitle);
                });

        $(document).off("click", submitFormAddUserInRole)
            .on("click", submitFormAddUserInRole,
                function (evt) {
                    evt.preventDefault();
                    roleMng.actions.userInRole.add();
                });

        $(document).off("click", deleteUserInRole)
            .on("click", deleteUserInRole,
                function (evt) {
                    evt.preventDefault();
                    roleMng.actions.userInRole.delete($(this).attr('data-user-in-role-delete-url'));
                });

        $(document).off("input", txtSearchMobileNumber)
            .on("input", txtSearchMobileNumber,
                function (evt) {
                    evt.preventDefault();
                    isValidNumber = false;
                    let $this = $(this);
                    if ($this.val().length === 11) {
                        roleMng.actions.userInRole.searchUser($this.attr('data-action-url') + '&mobileNumber=' + $this.val())
                    } else {
                        $(showUserFullNameFGroup).removeClass('has-warning has-danger has-success').addClass('has-warning');
                        $(showUserFullNameFControl).removeClass('form-control-warning form-control-danger form-control-success').addClass('form-control-warning');
                        $(showUserFullNameFControl).html('شماره موبایل وارد نشده است');
                        roleMng.actions.userInRole.refresh();
                    }
                });




        $(document).off("click", viewInRoleManagement)
            .on("click", viewInRoleManagement,
                function (evt) {
                    evt.preventDefault();
                    let entityName = $(this).attr('add-view-in-role-modal');
                    let formUrl = $(this).attr('data-add-view-in-role-url');
                    let modalTitle = $(this).attr('data-modal-title');
                    selectedRoleId = $(this).attr('data-role-id');
                    modalManagment.actions.getForm(entityName, formUrl, modalTitle);
                });

        $(document).off("click", submitFormAddViewInRole)
            .on("click", submitFormAddViewInRole,
                function (evt) {
                    evt.preventDefault();
                    roleMng.actions.viewInRole.add();
                });

        $(document).off("click", deleteViewInRole)
            .on("click", deleteViewInRole,
                function (evt) {
                    evt.preventDefault();
                    roleMng.actions.viewInRole.delete($(this).attr('data-view-in-role-delete-url'));
                });
    },
    actions: {
        userInRole: {
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
                        $(showUserFullNameFControl).html(result.IsSuccessful ? result.Result : result.Message);

                        isValidNumber = result.IsSuccessful;

                        roleMng.actions.userInRole.refresh();
                    })
                    .fail(function () {
                        modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                    });
            },
            add: function () {

                if (isValidNumber === false) {
                    modalManagment.messageBar.showMessage('error', 'شماره موبایل معتبر نمی باشد');
                    return false;
                }

                var form = $('FORM#UserInRole_FormControls');
                var submitUrl = form.attr('data-submit-action');

                if (form.valid() === false) {
                    return false;
                }

                $.ajax({
                    url: submitUrl,
                    data: form.serialize().replace(/Result./gi, ""),
                    type: 'POST'
                })
                    .done(function (result) {
                        if (result.IsSuccessful === false) {
                            modalManagment.messageBar.showMessage('error', result.Message);
                        } else {
                            modalManagment.messageBar.showMessage('success', result.Message);
                            roleMng.actions.userInRole.refresh();
                        }
                    })
                    .fail(function (xhr, status) {
                        modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                    });
            },
            delete: function (url) {
                $.ajax({
                    url: url,
                    contentType: 'application/json; charset=utf-8',
                    type: 'GET',
                    dataType: 'json'
                })
                    .done(function (result) {
                        if (result.IsSuccessful === false) {
                            modalManagment.messageBar.showMessage('error', result.Message);
                        } else {
                            roleMng.actions.userInRole.refresh();
                        }
                    })
                    .fail(function () {
                        modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                    });
            },
            refresh: function () {
                let $wrapper = 'DIV[data-modal-name="UserInRole_Modal"] DIV#UserRoles';
                if (isValidNumber === true) {
                    loadingOverlayRole.show($wrapper);
                    $.ajax({
                        url: '/Role/GetUserRoleList?mobileNumber=' + $(txtSearchMobileNumber).val(),
                        contentType: 'application/json; charset=utf-8',
                        type: 'GET',
                        dataType: 'html'
                    })
                        .done(function (result) {
                            $($wrapper).html(result);
                            loadingOverlayRole.hide($wrapper);
                        })
                        .fail(function () {
                            loadingOverlayRole.hide($wrapper);
                            modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                        });
                } else {
                    $($wrapper).html('<div class="empty-row">هیچ نقشی یافت نشد</div>');
                }
            }
        },
        viewInRole: {
            add: function () {
                var form = $('FORM#ViewInRole_FormControls');
                var submitUrl = form.attr('data-submit-action');

                if (form.valid() === false) {
                    return false;
                }

                $.ajax({
                    url: submitUrl,
                    data: form.serialize().replace(/Result./gi, ""),
                    type: 'POST'
                })
                    .done(function (result) {
                        if (result.IsSuccessful === false) {
                            modalManagment.messageBar.showMessage('error', result.Message);
                        } else {
                            roleMng.actions.viewInRole.refresh();
                        }
                    })
                    .fail(function (xhr, status) {

                    });
            },
            delete: function (url) {
                $.ajax({
                    url: url,
                    contentType: 'application/json; charset=utf-8',
                    type: 'GET',
                    dataType: 'json'
                })
                    .done(function (result) {
                        if (result.IsSuccessful === false) {
                            modalManagment.messageBar.showMessage('error', result.Message);
                        } else {
                            roleMng.actions.viewInRole.refresh();
                        }
                    })
                    .fail(function () {
                        modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                    });
            },
            refresh: function () {
                let $wrapper = 'DIV[data-modal-name="ViewInRole_Modal"] DIV.modal-body';
                loadingOverlayRole.show($wrapper);
                $.ajax({
                    url: '/Role/GetAddViewInRole?id=' + selectedRoleId,
                    contentType: 'application/json; charset=utf-8',
                    type: 'GET',
                    dataType: 'html'
                })
                    .done(function (result) {
                        $($wrapper).html(result);
                        loadingOverlayRole.hide($wrapper);
                    })
                    .fail(function () {
                        loadingOverlayRole.hide($wrapper);
                        modalManagment.messageBar.showMessage('error', 'خطا در ارسال اطلاعات');
                    });
            }
        }
    }
};

var loadingOverlayRole = {
    overlay: '<div class="page-loader" style="position:absolute;opacity:0.5;background-color:#ccc;">' +
        '<div class="page-loader__spinner">' +
        '<svg viewBox="25 25 50 50">' +
        '<circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle>' +
        '</svg>' +
        '</div>' +
        '</div>',
    show: function (loadingWrapperSelector) {
        let $loadingOver = $(loadingWrapperSelector);

        if ($loadingOver) $loadingOver.prepend(loadingOverlay.overlay);
    },
    hide: function (loadingWrapperSelector) {
        let $loadingOver = $(loadingWrapperSelector);

        if ($loadingOver) $loadingOver.find('DIV.page-loader').remove();
    }
};