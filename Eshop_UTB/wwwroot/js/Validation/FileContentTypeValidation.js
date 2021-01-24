$.validator.addMethod('filecontent',
    function (value, element, params) {

        var fileContentType = params[1];

        var fileContentFromFile = "";
        if (element && element.files && element.files.length > 0) {
            fileContentFromFile = element.files[0].type;
        }
        if (!value || fileContentFromFile && fileContentFromFile != "" && fileContentFromFile.toLowerCase().includes(fileContentType.toLowerCase())) {

            return true;
        }

        return false;
});

$.validator.unobtrusive.adapters.add('filecontent', ['type'],
    function (options)
{
    var element = $(options.form).find('#fileupload')[0];

    options.rules['filecontent'] = [element,options.params['type']];
    options.messages['filecontent'] = options.message;
});