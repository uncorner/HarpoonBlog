
// init DOM
$(document).ready(function () {
    harpoon.initCommon();
    harpoon.initChapterGrid();
});

(function (context) {

    context.initTagLine = function() {
        var tagLine = $("#TagLine");

        tagLine.tagit({
            allowSpaces: true,
            singleField: true,
            singleFieldDelimiter: ';',
            caseSensitive: false,
            autocomplete: {delay: 200},

            tagSource: function (search, showChoices) {
                $.ajax({
                    url: "/Admin/Tag/GetAvailible",
                    type: "post",
                    traditional: true,
                    data: {
                        excludedTags: tagLine.tagit("assignedTags"),
                        pattern: search.term
                    },
                    success: function (choices) {
                        showChoices(choices);
                    }
                });
            }
        });
    }

    context.onSuccessCommentDelete = function (data) {
        if (data.id === null || data.id === undefined) {
            return;
        }

        var commentId = "#comment-" + data.id;
        $(commentId).remove();

        var count = harpoon.updateCommentCount();
        if (count == 0) {
            document.location.href = '/admin';
        }
    };

    context.initChapterGrid = function () {
        var sortableNode = $(".chapter-grid tbody");

        sortableNode.sortable({
            helper: fixHelper,
            opacity: 0.85,
            distance: 1,
            axis: "y",
            update: function (event, ui) {
                // disable grid
                sortableNode.sortable("disable")
                    .find("tr").addClass("post-grid-disabled");
                ui.item.addClass("post-grid-moved-item");

                var serialized = sortableNode.sortable("serialize", { key: "itemIds" });

                $.ajax({
                    type: 'POST',
                    url: '/Admin/Chapter/Sort',
                    data: serialized,
                    complete: function (request, status) {
                        if (status === "success") {
                            sortableNode.sortable("refresh");
                        }
                        else {
                            sortableNode.sortable("cancel");
                        }

                        // enable grid
                        sortableNode.sortable("enable")
                            .find("tr").removeClass("post-grid-disabled")
                        ui.item.removeClass("post-grid-moved-item");
                    }
                });
            }
        })
        .disableSelection();
    }

    // Return a helper with preserved width of cells
    var fixHelper = function (e, ui) {
        ui.children().each(function () {
            $(this).width($(this).width());
        });
        return ui;
    };

})(harpoon);
