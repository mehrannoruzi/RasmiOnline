/// <reference path="../../jquery-1.10.2.min.js" />
var getDefaultImageUrl = function (fileName) {
    let ext = fileName.toLowerCase().split('.').reverse()[0];
    switch (ext) {
        case "png":
        case "gif":
        case "jpeg":
        case "jpg":
            return fileName;
        case "pdf":
            return "/Content/Images/Attachments/pdf.png";
        case "zip":
            return "/Content/Images/Attachments/archive.png";
        case "rar":
            return "/Content/Images/Attachments/rar.png";
        case "txt":
        case "doc":
        case "docx":
            return "/Content/Images/Attachments/archive.png";
        case "mp3":
        case "wav":
            return "/Content/Images/Attachments/music.png";
        case "mp4":
        case "mkv":
            return "/Content/Images/Attachments/archive.png";
        default:
            return "/Content/Images/Attachments/unknown.png";
    }

};
$(document).on('ready', function () {

    $("#upload-order-files").pigeon({
        url: $(this).attr('data-url')
    });

    $("#upload-identity-files").pigeon({
        url: $(this).attr('data-url')
    });
    //$('.upload-wrapper').each(function () {
    //    let $wrapper = $(this);
    //    fireDropZone($(this), function (rep, dz, file) {
    //        $('.dz-default.dz-message').css({'display':'inline-block'});
    //    },
    //        function (e) {
    //            notify(false, errorMsg);
    //        });
    //});

    $(document).on('click', '.btn-upload', function (e) {
        let $btn = $(this);
        let id = $btn.attr('id');
        $('[data-btn="#' + id + '"]').trigger('click');
    });

    $(document).on('change', '.uploader', function (e) {
        let $up = $(this), files = e.target.files;
        let opt = typeof $up.data('opt') !== 'undefined' ? $up.data('opt') : {};
        opt.fileNumberLimit = typeof opt.fileNumberLimit !== 'undefined' ? opt.fileNumberLimit : 10;
        opt.fileSizeLimit = typeof opt.fileSizeLimit !== 'undefined' ? opt.fileSizeLimit : 10;//In MB
        opt.fileNameMaxLength = typeof opt.fileNameMaxLength !== 'undefined' ? opt.fileNameMaxLength : 18;
        if (FileReader && files && files.length) {
            if (files.length > opt.fileNumberLimit) {
                notify(false, "حداکثر تعداد آپلود در هر بار {0} می باشد".replace("{0}", opt.fileNumberLimit));
                return;
            }
            for (var i = 0; i < files.length; i++) {
                if (files[i].size > (10 * 1024 * 1024)) {
                    notify(false, "حداکثر حجم هر فایل {0} مگابایت می باشد".replace("{0}", opt.fileSizeLimit));
                    return;
                }
                if (files[i].name.length > opt.fileNameMaxLength) {
                    notify(false, "حداکثر طول نام فایل {0} کاراکتر می باشد".replace("{0}", opt.fileNameMaxLength));
                    return;
                }
                console.log(files[i]);
            }

            upload($up, files, $up.parent());
        }

    });

});

var upload = function ($up, files, $wrapper) {
    if (FileReader && files && files.length) {
        var data = new FormData();
        $.each(files, function (key, value) {
            data.append(key, value);
        });
        ajaxBtn.inProgress($($up.data('btn')));

        $.ajax({
            url: $up.data('url'),
            type: 'POST',
            data: data,
            cache: false,
            dataType: 'json',
            processData: false, // Don't process the files
            contentType: false, // Set content type to false as jQuery will tell the server its a query string request
            success: function (rep) {
                if (rep.IsSuccessful) {
                    $.each(files, function (key, value) {
                        var fr = new FileReader();
                        fr.onload = function () {
                            ajaxBtn.normal();
                            let url = getDefaultImageUrl(files[key].name);
                            console.log(url);
                            let $img = $('<img />', {
                                class: 'uploader-thumb',
                                src: (url == files[key].name ? fr.result : url)
                            });
                            $wrapper.append($img);
                        }
                        fr.readAsDataURL(value);
                    });
                }
                else {
                    ajaxBtn.normal();
                    notify(false, rep.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ajaxBtn.normal();
                notify(false, errorMsg);
                console.log(jqXHR.responseText);
            }
        });
    }

}