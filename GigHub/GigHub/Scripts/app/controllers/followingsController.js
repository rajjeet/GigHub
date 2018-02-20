var followingController = function (followingsService) {
    var button;

    var init = function () {
        $(".js-toggle-follow").click(function (e) {
            button = $(e.target);

            var followeeId = button.attr("data-followee-id");

            if (button.hasClass("btn-default"))
                followingsService.createFollowing(followeeId, done, fail);
            else
                followingsService.deleteFollowing(followeeId, done, fail);
        });
    }

    var fail = function () {
        alert("Something failed!");
    };

    var done = function () {
        var text = button.text() === "Follow" ? "Following" : "Follow";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    return {
        init: init
    }

}(FollowingsService);