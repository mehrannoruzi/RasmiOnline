/*! 
 * Copyright (c) 2018 kingofday.ir
 * 
 * multi file uploader like google
 * 
 * Author: shahrooz.bazrafshan@gmail.com
 * 
 * Version: 1.2.0
 *
 */
var pigeonDic = {
    defaultError: "operation failed Retry please",
    loading: "loading ...",
    uploadDone: 'uploaded',
    maxFileCountReached: 'maximum number of files is {0}.',
    maxFileSizeExceeded: 'maximum size of each file is {0}',
};
(function ($) {
    $.fn.pigeon = function (args) {
        var $elm = $(this);
        //default opt
        args = typeof args !== 'undefined' ? args : {};
        args.showThumbnail = typeof args.showThumbnail !== 'undefined' ? args.showThumbnail : true;
        args.url = typeof args.url !== 'undefined' ? args.url : $elm.data('url');
        args.maxFileCount = typeof args.maxFileCount !== 'undefined' ? args.maxFileCount : 10;
        args.maxFileSize = typeof args.maxFileSize !== 'undefined' ? args.maxFileSize : 5;//MB
        args.extentions = typeof args.extentions !== 'undefined' ? args.extentions : '*';
        args.checkedIcon = typeof args.checkedIcon !== 'undefined' ? args.checkedIcon : 'zmdi zmdi-check';
        args.errorIcon = typeof args.errorIcon !== 'undefined' ? args.errorIcon : 'zmid zmdi-icon';

        var id = $elm.attr('id');
        //add wrapper
        $('#' + id + '-pigeon-wrapper').remove();
        var $wrapper = $('<div></div>', { class: 'pigeon-wrapper', id: id + '-pigeon-wrapper' });
        var $parent = $elm.parent().append($wrapper);
        var $input = $('<input/>', {
            type: 'file',
            class: 'pigeon-input',
            id: id + 'pigeon-input',
            multiple: 'true',
            accept: args.extentions

        });
        $wrapper.append($input);
        //
        var toolTipTemplate = '<div class="tooltip {0}" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>';
        //
        var filesCount = 0;
        $input.off('change').on('change', function (e) {
            var files = e.target.files;
            if (files && files.length) {
                //check file count
                if (files.length + filesCount > args.maxFileCount) {
                    $elm.tooltip({
                        title: pigeonDic.maxFileCountReached.replace('{0}', args.maxFileCount),
                        trigger: 'manual',
                        placement: 'top',
                        template: toolTipTemplate.replace('{0}', 'danger')
                    });
                    $elm.tooltip('show');
                    return;
                }
                //check files size
                if (Object.keys(files).some(function (k) { return files[k].size > (args.maxFileSize * 1024 * 1024) })) {
                    $elm.tooltip({
                        title: pigeonDic.maxFileSizeExceeded.replace('{0}', args.maxFileSize),
                        trigger: 'manual',
                        placement: 'top',
                        template: toolTipTemplate.replace('{0}', 'danger')
                    });
                    $elm.tooltip('show');
                    return;
                }
                for (var i = 0; i < files.length; i++) {
                    upload(files[i]);
                }

            }
        });

        $elm.off('click').on('click', function () { $input.val(null).trigger('click') });
        //remove tooltip
        $(document).on('click', function () { $elm.tooltip('dispose'); });
        //upload all files to server & append loader
        var upload = function (file) {

            var data = new FormData();
            var xhr = new XMLHttpRequest();
            xhr.withCredentials = false;
            data.append('file', file);
            //add record
            let $record = $('<div></div>', {
                id: new Date().getUTCMilliseconds(),
                class: 'pigeon-record',
                title: pigeonDic.loading,
                "data-toggle": "tooltip",
                "data-placement": "top",
            });
            //get response
            xhr.onreadystatechange = function (oEvent) {

                if (xhr.readyState === 4) {
                    console.log('reponse:');
                    console.log(xhr.status);
                    if (xhr.status === 200) {
                        if (xhr.response.IsSuccessful) {
                            filesCount++;
                            if (typeof args.success !== 'undefined') args.success(request.response);
                            setState($record, pigeonDic.uploadDone, true);
                        }
                        else {
                            setState($record, request.response.Message, false);
                        }
                        console.log(xhr.response);
                    } else {
                        $record.attr('title', pigeonDic.defaultError).addClass('danger');
                        //showErrorMessage($record, 'بده دو');
                        setState($record, pigeonDic.defaultError, false);
                    }
                }
            };
            let $a = $('<a></a>', {
                class: 'pigeon-details',
                title: file.name
            }).html('<span class="name">' + file.name.substring(0, Math.min(file.name.length, 15)) + '</span>' + '(' + Math.floor(file.size / (1024)) + 'KB)');
            //add prgressbar
            $record.append($a);
            //add thumbnail
            if (args.showThumbnail) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $a.prepend($('<img />', {
                        class: 'pigeon-thumbnail',
                        src: e.target.result
                    }));
                    //$thumbImg.attr('src', );
                }
                reader.readAsDataURL(file);
            }
            $record.append('<div class="progress-wrapper"><div class="progress"></div></div>');
            let $progress = $('<div></div>', {
                class: 'progress-bar progress-bar-success',
                style: 'width:1%',
                role: 'progressbar',
                aria_valuenow: '1',
                aria_valuemin: '0',
                aria_valuemax: '100'
            });
            $record.find('.progress').append($progress);
            $wrapper.append($record);
            xhr.upload.addEventListener('progress', function (e) {
                var p = (e.loaded / e.total) * 100;

                $progress.css({ width: p + '%' });
            },false);

            xhr.responseType = 'json';

            // Send POST request to the server side script
            xhr.open('post', args.url);

            xhr.send(data);
        };
        //set tooltip for each file
        var setState = function ($elm, message, success) {
            $elm.attr('title', message).addClass(success ? 'success' : 'danger');
            $elm.find('.progress-wrapper').remove();
            var $i = $('<i class="zmdi zmdi-' + (success ? 'check' : 'close') + '"></i>');
            $elm.append($i);
            $('#' + $elm.attr('id')).tooltip({
                template: toolTipTemplate.replace('{0}', (success ? 'success' : 'danger'))
            });
            if (!success) {
                $i.on('click', function () {
                    $(this).closest('.pigeon-record').tooltip('dispose').remove();
                });
            }
        };
        return this;
    };
}(jQuery));