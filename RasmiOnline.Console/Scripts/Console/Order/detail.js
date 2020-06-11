/// <reference path="../../jquery-1.10.2.min.js" />
$(document).on('ready', function () {
    $(".attachments-type-select").click(function () {
        $(".attachments-type-select").removeClass("activate");
        $(this).addClass("activate");
        $(".dropzone-uploader").addClass('hide');
        $(".dropzone-uploader-" + $(this).attr('data-type')).removeClass('hide');
    });

    //fire dropzone js for uploading attachments
    fireDropZone($('#upload-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#upload-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });

    fireDropZone($('#upload-translation-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#upload-translation-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });

    fireDropZone($('#upload-identity-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#upload-identity-attachments').closest('.tab-pane').find('.attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });
    //fireDropZone($('#upload-identity-attachments'), function (rep, dz) {
    //    if (rep.IsSuccessful) {
    //        dz.removeAllFiles();
    //        let $wrapper = $('#Identity .attchs-wrapper').html(threeDotLoader);
    //        $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
    //    }
    //});
});