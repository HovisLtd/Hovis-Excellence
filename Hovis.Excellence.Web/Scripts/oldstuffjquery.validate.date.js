$(function () {
	$.validator.methods.date = function (value, element) {
		if ($.browser.webkit) {

			//ES - Chrome does not use the locale when new Date objects instantiated:
			var d = new Date();

			return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
		}
		else {

			return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
		}
	};

    // Correct date validation error in Chrome and Safari
	jQuery.validator.methods["date"] = function (value, element) {
	    var shortDateFormat = "dd-mm-yyyy";
	    var res = true;
	    try {
	        $.datepicker.parseDate(shortDateFormat, value);
	    } catch (error) {
	        res = false;
	    }
	    return res;
	};

    // fix date validation for chrome
	jQuery.extend(jQuery.validator.methods, {
	    date: function (value, element) {
	        var isChrome = window.chrome;
	        // make correction for chrome
	        if (isChrome) {
	            var d = new Date();
	            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
	        }
	            // leave default behavior
	        else {
	            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(value));
	        }
	    }
	});
});

