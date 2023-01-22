
// init DOM
$(document).ready(function () {
    harpoon.initCommon();

    var prevItem = null;
    $("#layout-topbar-menu li").each(function (i) {
        if (prevItem == null) {
            prevItem = this;
            return;
        }

        currTop = $(this).position().top;
        prevTop = $(prevItem).position().top;

        if (prevTop < currTop) {
            backColor = $(prevItem).css("background-color");
            $(prevItem).css("border-right-color", backColor);
        }

        prevItem = this;
    });

    // main menu fix for IE
    $("#layout-topbar-menu li").last().css("border-right", "none");
});


(function (context) {

    var LOADING_ARTICLE_ITEM = ".article-preview-box";

    context.onSuccessCommentFormPostAction = function(isError) {
        $('#Captcha').val("");
        context.updateCaptchaImage();
    }

    context.updateCaptchaImage = function() {
        var normalUrl = $(".captcha-box a img").attr("data-normal-url");
        $(".captcha-box a img").attr("src", normalUrl + "&timestamp=" + new Date().getTime());
    }

    context.search = function() {
        window.location.href = "/search/" + $("#layout-search input[name=search-text]").val();
    }

    context.searchByEnter = function(event) {
        // check fo Enter key
        if (event.keyCode==13) {
            context.search();
            return false;
        }
        return true;
    }

    context.loadMoreArticles = function() {
        var loadedCount = getLoadedArticleCount();
        var totalCount = $("#article-list-container").attr("data-total-count");

        if (loadedCount >= totalCount) {
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/Article/GetList',
            data: {
                skippingCount: loadedCount
            },
            success: function (resultData) {
                $(LOADING_ARTICLE_ITEM).last().after(resultData);

                if (getLoadedArticleCount() >= totalCount) {
                    $("#article-loading-link-box").remove();
                }
            }
        });
    }

    function getLoadedArticleCount() {
        return $(LOADING_ARTICLE_ITEM).size();
    }

})(harpoon);

