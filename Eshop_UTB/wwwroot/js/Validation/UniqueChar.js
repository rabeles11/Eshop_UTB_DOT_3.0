$.validator.addMethod('UniqChar',
	function unique_char(str1, uniqchar) {
		var str = str1;
		var uniql = "";
		for (var x = 0; x < str.length; x++) {

			if (uniql.indexOf(str.charAt(x)) == -1) {
				uniql += str[x];

			}
		}
		if (uniql.length < uniqchar) {
			return false;
		}
		return true;
	});

$.validator.unobtrusive.adapters.add('UniqChar',
	['char'],
	function (options) {
		var element = $(options.form).find('#Password')[0];
		options.rules['UniqChar'] = [element, parseInt(options.params['char'])];
		options.messages['UniqChar'] = options.message;
	});