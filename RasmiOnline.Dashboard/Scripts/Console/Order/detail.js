/// <reference path="../../jquery-1.10.2.min.js" />
$(document).on('ready', function () {
    fireDropZone($('#upload-identity-attachments'), function (rep, dz) {
        if (rep.IsSuccessful) {
            dz.removeAllFiles();
            let $wrapper = $('#Identity .attchs-wrapper').html(threeDotLoader);
            $.get($wrapper.data('url'), function (rep) { $wrapper.html(rep); });
        }
    });
});