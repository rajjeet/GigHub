var GigsController = function () {
    var button;

    var initAttendance = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    toggleAttendance = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) {
            $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                .done(done)
                .fail(fail);
        } else {
            $.ajax({
                    url: "/api/attendances/" + button.attr("data-gig-id"),
                    method: "DELETE"
                })
                .done(done)
                .fail(fail);
        }

    };

    var initFollowing = function () {
        $(".js-toggle-follow").click(function (e) {
            button = $(e.target);
            $.post("/api/followings", { followeeId: button.attr("data-followee-id") })
                .done(function () {
                    button
                        .text("Unfollow");
                }) 
                .fail(fail);
        });

    };

    var done = function () {
        var text = (button.text() === "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function() {
        alert("Something failed!");
    };

    return {
        initAttendance: initAttendance,
        initFollowing: initFollowing
    };
}();

