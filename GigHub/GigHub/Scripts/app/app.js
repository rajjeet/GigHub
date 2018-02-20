var GigsController = function() {

    var initAttendance = function () {
        $(".js-toggle-attendance").click(function (e) {
            var button = $(e.target);
            if (button.hasClass("btn-default")) {
                $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                    .done(function () {
                        button
                            .removeClass("btn-default")
                            .addClass("btn-info")
                            .text("Going");
                    })
                    .fail(function () {
                        alert("Something failed!");
                    });
            } else {
                $.ajax({
                        url: "/api/attendances/" + button.attr("data-gig-id"),
                        method: "DELETE"
                    })
                    .done(function () {
                        button
                            .removeClass("btn-info")
                            .addClass("btn-default")
                            .text("Going?");
                    })
                    .fail(function () {
                        alert("Something failed!");
                    });
            }
        });

    };

    var initFollowing = function() {
        $(".js-toggle-follow").click(function (e) {
            var button = $(e.target);
            $.post("/api/followings", { followeeId: button.attr("data-followee-id") })
                .done(function () {
                    button
                        .text("Unfollow");
                })
                .fail(function () {
                    alert("Something failed!");
                });
        });

    };

    return {
        initAttendance: initAttendance,
        initFollowing: initFollowing
    };
}();

