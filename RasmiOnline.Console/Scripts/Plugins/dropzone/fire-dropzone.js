//for adding dropzone plugin

function fireDropZone(elm, successFunc, failedFunc) {
    var upErrMsg = "";
    var targetInputId = elm.attr('data-target-input');
    var removePhysically = typeof elm.attr('data-remove-physically') !== 'undefined' ? elm.attr('data-remove-physically') : true;
    var multiple = typeof elm.attr('data-multiple') !== 'undefined' ? elm.attr('data-multiple') : 'true';
    var message = multiple === 'true' ? "فایلهای خود را اینجا رها کنید" : "فایل خود را اینجا رها کنید";
    if (typeof elm.attr('data-message') !== 'undefined')
        message = elm.attr('data-message');
    var error = false;
    return $(elm).dropzone({
        renameFilename: function (fileName) {
            var ascii = /^[ -~\t\n\r]+$/;
            if (!ascii.test(fileName)) return "noneAscciFileName." + fileName.split('.').reverse()[0];
            else return fileName;
        },
        dictDefaultMessage: typeof elm.data('default-message') !== 'undefined' ? elm.data('default-message') : message,
        uploadMultiple: multiple,
        parallelUploads: 10,
        //true,false
        addRemoveLinks: typeof elm.data('enanble-edit') !== 'undefined' ? elm.attr('enanble-edit') : false,
        //upload action method 
        url: typeof elm.attr('data-upload-url') !== 'undefined' ? elm.attr('data-upload-url') : '/shared/upload',
        params: { savingPath: typeof elm.attr('data-savingPath') !== 'undefined' ? elm.attr('data-savingPath') : '/Files/' },
        //maxFiles: typeof elm.data('max-files') !== 'undefined' ? parseInt(elm.data('max-files')) : 10,
        //maximum size of file in MB 500 MB
        maxFilesize: 10,
        //exceptable extentions seperated by commo: image/*,application/pdf,.psd
        acceptedFiles: typeof elm.attr('data-acceptedFiles') !== 'undefined' ? elm.attr('data-acceptedFiles') : "",
        sending: function (file) {
            error = false;
            upErrMsg = "";
            if (file.name.length > 200) {
                upErrMsg = "حداکثر مجاز برای طول نام فایل، 200 کارکتر میباشد!"
                this.removeFile(file);
            }

        },
        //after upload
        success: function (file, rep) {
            console.log(rep);
            if (!rep.IsSuccessful) {
                if (this.files.length == 1)
                    this.removeAllFiles(true);
                else {
                    var ref;
                    if (file.previewElement) {
                        if ((ref = file.previewElement) != null) {
                            ref.parentNode.removeChild(file.previewElement);
                        }
                    }
                }
                notify(false, rep.Message)
            }
            successFunc(rep, this,file);
        },
        error: function (file, errorMessage, e) {
            console.log(e);
            if (!error) {
                this.removeAllFiles(true);
                error = true;
                if (typeof failedFunc === 'function') {
                    failedFunc();
                }
                else {
                    notify(false, 'خطایی رخ داده است، لطفا دوباره سعی نمایید')

                }
            }
        },
        accept: function (file, done) {
            let maxFiles = 10;
            let maxLength = 200;
            if (this.files.length <= maxFiles) {
                if (file.name.length > maxLength) {
                    notify(false, 'حداکثر طول نام فایل ها {0} کاراکتر می باشد'.replace('{0}', maxLength))
                    this.removeFile(file);
                    //done("Filename exceeds 30 characters!");
                }
                else { done(); }
            }
            else {
                this.removeFile(file);
                notify(false,'حداکثر تعداد فایل مجاز برای آپلود {0} میباشد'.replace('{0}', maxFiles));
            }

        },
    });
}