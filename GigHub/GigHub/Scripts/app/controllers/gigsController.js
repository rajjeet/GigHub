﻿var GigsController = function (attendanceService) {
    var button;

    var initAttendance = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
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

    var fail = function () {
        alert("Something failed!");
    };

    return {
        initAttendance: initAttendance,
        initFollowing: initFollowing
    }
}(AttendanceService);

