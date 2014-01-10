

$(document).ready(function () {
    $("#StaffForm").validate();
});

$(function () {

    $("#Add").click(function () {
        if ($("#StaffForm").valid()) {
            var staff = {
                Name: $("#Name").val(),
                BirthDay: $("#BirthDay").val(),
                School: $("#School").val(),
                Address: $("#Address").val(),
                WorkExperience: $("#WorkExperience").val(),
                SelfAssessment: $("#SelfAssessment").val(),
            };

            $("#Add").attr("disabled", "disabled");

            $.ajax({
                //url: "@Url.Action("AddStaff", "Staff")",
                url: "Staff/AddStaff",
                type: "post",
                data: staff,
                success: function () {
                    //window.location.href = "@Url.Action("Index", "Staff")";
                    window.location.href = "Staff/Index";
                },
                error: function () {
                    //window.location.href = "@Url.Action("Error", "Staff")";
                    window.location.href = "Staff/Error";
                }
            });
        }

    });


    $("#Search").click(function () {
        var searchName = $("#SearchName").val();
        location.href = "/Staff/Search?parameter=" + searchName;
    });

    $("#Edit").click(function () {
        if ($("#editName").val() == "") { alert("Name is not empty!!"); return false; }
        var staff = {
            Id: $("#editId").val(),
            Name: $("#editName").val(),
            BirthDay: $("#editBirthDay").val(),
            School: $("#editSchool").val(),
            Address: $("#editAddress").val(),
            WorkExperience: $("#editWorkExperience").val(),
            SelfAssessment: $("#editSelfAssessment").val(),
            Picture: $("#editPicture").val(),
            Attachment: $("#editAttachment").val(),
            Lock: $("#editLock").val()
        };

        $("#Edit").attr("disabled", "disabled");

        $.ajax({
            //url: "@Url.Action("Edit", "Staff")",
            url: "Staff/AddStaff",
            type: "post",
            data: staff,
            success: function () {
                //window.location.href = "@Url.Action("Index", "Staff")";
                window.location.href = "Staff/Index";
            },
            error: function () {
                //window.location.href = "@Url.Action("Error", "Staff")";
                window.location.href = "Staff/Error";
            }
        });

    });

    $("#btnUploadPic").click(function () {
        $("#uploadPic").submit();

    });

    $("#btnUploadAtt").click(function () {
        $("#uploadAtt").submit();
    });
    $("#GoToList").click(function () {
        //window.location.href ="@Url.Action("Index","Staff")";
        window.location.href = "Staff/Index";
    });
});




$("body").on("click", "#EditStaff", function () {

    var $this = $(this);
    var id = $this.attr("data-id");
    var selectTr = $("tr[data-id='" + id + "']");
    var name = selectTr.children("td[data-member='Name']").text();
    var birthDay = selectTr.children("td[data-member='BirthDay']").text();
    var school = selectTr.children("td[data-member='School']").text();
    var address = selectTr.children("td[data-member='Address']").text();
    var workExperience = selectTr.children("td[data-member='WorkExperience']").text();
    var selfAssessment = selectTr.children("td[data-member='SelfAssessment']").text();
    var picture = selectTr.children("td[data-member='Picture']").text();
    var attachment = selectTr.children("td[data-member='Attachment']").text();
    var lock = selectTr.children("td[data-member='Lock']").text();
    //var data = function(day) { throw new Error("Not implemented"); };
    $("#editId").val(id);
    $("#editName").val(name);
    if (birthDay != "") {
        $("#editBirthDay").val(new Date(birthDay));
    }

    $("#editSchool").val(school);
    $("#editAddress").val(address);
    $("#editWorkExperience").val(workExperience);
    $("#editSelfAssessment").val(selfAssessment);
    //$("#editPicture").val(picture);
    //$("#editAttachment").val(attachment);
    $("#editLock").val(lock);
    if (picture != "") {
        $("#picScr").attr("src", "Images/StaffImage/" + picture);
    } else {
        $("#picScr").attr("src", "Images/nodata.jpg");
    }

    if (attachment != "") {

        $("#showAtt").after("<a href='Doc/" + attachment + "'>" + attachment + "</a>");

    }

    $("#staffPicId").val(id);
    $("#staffAttId").val(id);
});