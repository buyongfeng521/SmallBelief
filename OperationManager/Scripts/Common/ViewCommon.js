//BootStrap_File_Input
//Sigle
function View_File_Input_Init(ctrlName,fileSize) {
    var control = $("#" + ctrlName);
    control.fileinput({
        showUpload: false,
        showCaption: false,
        browseClass: "btn btn-xs btn-primary",
        removeClass: "btn btn-xs btn-default",
        maxFileSize: fileSize,

        allowedFileExtensions: ['jpg', 'png', 'gif'],
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>"
    });
}
//Multiple
function View_Files_Input_Init(ctrlName, uploadUrl,fileSize,filesNum) {
    var control = $("#" + ctrlName);
    //相册
    control.fileinput({
        uploadClass: "btn btn-xs btn-primary",
        showCaption: false,
        browseClass: "btn btn-xs btn-primary",
        removeClass: "btn btn-xs btn-default",
        uploadUrl: uploadUrl,//'/Goods/GoodsGalleryUpload',
        allowedFileExtensions: ['jpg', 'png', 'gif'],
        enctype: 'multipart/form-data',
        overwriteInitial: false,
        maxFileSize: fileSize,
        maxFilesNum: filesNum,
        uploadAsync: false,
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        layoutTemplates: {
            actions: '<div class="file-actions">\n' +
                    '    <div class="file-footer-buttons">\n' +
                    '        {delete}' +
                    '    </div>\n' +
                    '    <div class="file-upload-indicator" tabindex="-1" title="{indicatorTitle}">{indicator}</div>\n' +
                    '    <div class="clearfix"></div>\n' +
                    '</div>'
        }
    });
}