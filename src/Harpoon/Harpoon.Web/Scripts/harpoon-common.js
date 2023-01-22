
$.fn.exists = function () {
    return this.length !== 0;
}

var harpoon = {};

(function (context) {

    var COMMENT_SUBMIT_BUTTON = "#comment-form-box input[type='submit']";

    // executed from document.ready
    context.initCommon = function () {
        // Disable Unobtrusive OnKeyUp (http://stackoverflow.com/questions/8022695/asp-net-mvc-3-jquery-validation-disable-unobtrusive-onkeyup)
        if ($.validator !== undefined) {
			$.validator.setDefaults({
				onkeyup: false
			});
		};

        // init expander for comment content
        var commentContent = $("#CommentContent");
        if (commentContent.exists()) {
            commentContent.TextAreaExpander();
        }
    }

    context.setFieldErrorMessage = function(fieldName, message) {
        var innerHtml = "<span for='" + fieldName + "' generated='true' class=''>" + message + "</span>";
        var foundSelector = $(".field-validation-valid[data-valmsg-for='" + fieldName +  "']");
        foundSelector.html(innerHtml);
        foundSelector.removeClass("field-validation-valid").addClass("field-validation-error");
    }

    context.onSuccessCommentForm = function (data) {
        if (data.isError) {
            if (data.captchaError !== undefined) {
                harpoon.setFieldErrorMessage("Captcha", data.captchaError); 
            }
        }
        else {
            // ok
            var comment = $(data.commentHtml).hide().attr("data-new-comment", "");
            $("#comment-list-container").append(comment[0].outerHTML);
            $("#comment-list-container [data-new-comment]").slideDown(1000).removeAttr("data-new-comment");
            harpoon.updateCommentCount();

            $("#comment-list-container").removeClass("hidden");
            // clear fields
            $('#CommentContent').val("");
        }

        // clear fields
        if (harpoon.onSuccessCommentFormPostAction != undefined) {
            harpoon.onSuccessCommentFormPostAction(data.isError);
        }
        enableCommentButton();
    }

    context.onFailureCommentForm = function () {
        enableCommentButton();
    }

    context.onBeginCommentForm = function () {
        $(".comment-form-error-box").empty();
        disableFormControl(COMMENT_SUBMIT_BUTTON);
    }

    context.updateCommentCount = function () {
        var commentCount = $("#comment-list-container .comment-box").size();
        $("#comment-count").html(commentCount);

        return commentCount;
    }

    context.slideCommentMarkup = function() {
        $(".comment-markup-box").slideToggle("slow");
    }

    function enableCommentButton() {
        enableFormControl(COMMENT_SUBMIT_BUTTON);
    }

    function disableFormControl(value) {
        element = getElementByPath(value);
        element.attr('disabled', 'disabled');
    }

    function enableFormControl(value) {
        element = getElementByPath(value);
        element.removeAttr('disabled');
    }

    function getElementByPath(value) {
        if (isStringValue(value)) {
            return $(value);
        }

        return value;
    }

    function isStringValue(value) {
        return typeof (value) == 'string';
    }

})(harpoon);
