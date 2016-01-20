
function SetSidebarHeight()
{
    var minheight = $(window).height();
    if ($('#wrapper').height() > minheight) {
        minheight = $('#wrapper').height()
    }
    $('#sidebar-wrapper').css("min-height", (minheight - 55) + 'px').css("height", "0px");
}

function PostData(url, jsondata, callbackfunction) {
    if (jsondata == '') {

        $.ajax({
            type: "POST",
            async: "true",
            contentType: "application/json",
            dataType: "json",
            url: url,
            success: function (html) {
                if (html != null) {
                    if (html.IsSessionExpired != null) {
                        if (html.IsSessionExpired == true) {
                            //window.location.href = "/Customer/Login";
                            return;
                        }
                    }
                }
                eval(callbackfunction + "(" + JSON.stringify(html) + ")");
            },
            error: function (request, status, error) {
                //alert('Error: ' + error);
            }

        });
    }
    else {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            url: url,
            data: JSON.stringify(jsondata),
            success: function (html) {
                if (html != null) {
                    if (html.IsSessionExpired != null) {
                        if (html.IsSessionExpired == true) {
                            window.location.href = "/Employee/Login";
                            return;
                        }
                    }
                }
                eval(callbackfunction + "(" + JSON.stringify(html) + ")");
            },
            error: function (request, status, error) {
                //alert('Error: ' + JSON.stringify(error));
            }
        });
    }
}

function ValidateRequiredField(ctrl, ValidationMessage, ValidationPosition, defValue) {
    try {
        ClearMessage(ctrl);
        if ($(ctrl).val() == '' || $(ctrl).val() == defValue) {
            PrintMessage(ctrl, ValidationMessage, ValidationPosition);
            return false;
        }

        return true;

    } catch (e) {
        //alert(e);
    }
    return true;
}
function ValidateRequiredField2(ctrl, vctrl, ValidationMessage, ValidationPosition, defValue) {
    try {
        ClearMessage(vctrl);
        if ($(ctrl).val() == '' || $(ctrl).val() == defValue) {
            PrintMessage(vctrl, ValidationMessage, ValidationPosition);
            return false;
        }

        return true;

    } catch (e) {
        //alert(e);
    }
    return true;
}

function PrintMessage(ctrl, Message, ValidationPosition) {
    if (ValidationPosition == "after") {
        $('<span class="errormsg" style="color:red;">' + Message + '</span>').insertAfter($(ctrl));
    }
    else {
        $('<span class="errormsg">' + Message + '</span>').insertBefore($(ctrl));
    }
}

function ClearMessage(ctrl) {
    $(ctrl).next('.errormsg').remove();
    $(ctrl).prev('.errormsg').remove();
}

function showmsg(ctrl, type, msg2) {
    $($(ctrl)).html('');
    if (type == 1) {
        cls = 'alert-success';

    }
    if (type == 2) {
        cls = 'alert-info';
    }
    else if (type == 3) {
        cls = 'alert-danger';
    }
    var msg = '<div class="alert ' + cls + ' alert-dismissable">'
    msg += '<button class="close" data-dismiss="alert" aria-hidden="true" type="button">×</button>' +
          '<p>' + msg2 + '<p>';
    msg += '</div>';


    $(ctrl).append(msg);
}

function SetTimeOut(Id) {
    window.setTimeout(function () {
        $(".alert").slideUp(500, function () {
            $(this).hide();
        });
    }, 2000);
}


function CommonDialog() {
    $("#DeleteDialog").dialog({
        autoOpen: false,
        width: 300,
        open: function (event, ui) {
            $(this).find('#lblName').text($(this).data("name"))
        }
    });

    CommonDialogButtons();
}

function CommonDialogButtons() {
    $('#btnDelCancel').click(function () {
        $("#DeleteDialog").dialog('close');
    })
}
function DeletePopup(control) {

    var idname = $(control).attr("IDName");
    var idname = idname.split(':');
    $("#deleteModal").data("id", parseInt(idname[0])).data("name", idname[1]);
    $("#lblName").text(idname[1]);

    $("#deleteModal").modal();
}

function CheckConfirmPassword(ctrl1, ctrl2, ValidationPosition) {
    if ($(ctrl1).val() != '' && $(ctrl2).val() != '') {
        ClearMessage(ctrl1);
        ClearMessage(ctrl2);
        if ($(ctrl1).val() != $(ctrl2).val()) {
            PrintMessage(ctrl2, _validate_Password_ConfirmPassword, ValidationPosition);
            return false;
        }
    }
    return true;
}

function CheckEmail(ctrl, ValidationPosition) {
    if ($(ctrl).val() != '') {
        ClearMessage(ctrl);
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test($(ctrl).val())) {
            PrintMessage(ctrl, "Invalid email id", ValidationPosition);
            return false;
        }
    }
    return true;
}

function CheckGender(ctrl1, ctrl2, ValidationPosition) {
    if ($(ctrl1).prop("checked") == false && $(ctrl2).prop("checked") == false) {
        ClearMessage(ctrl1);
        ClearMessage(ctrl2);
        PrintMessage(ctrl2, 'Gender Required', ValidationPosition);
        return false;
    }
    ClearMessage(ctrl1);
    ClearMessage(ctrl2);
    return true;
}

function TimeTrace(value) {
    if (value != '' && value != null) {
        var data = value;
        var DatabaseTime = data.split("T");
        var CleanTime = DatabaseTime[1].split(".");
        return CleanTime[0];
    }
}

function ddmmyyTommdddyy(value) {
    if (value != '' && value != null) {
        var data = value;
        var m = data.split('/');
        var formatedDate = m[1] + '/' + m[0] + '/' + m[2];
        return formatedDate;
    }
}

function mmdddyyToddmmyy(   ) {
    if (value != '' && value != null) {
        var data = value;
        var m = data.split('/');
        var formatedDate = m[1] + '/' + m[0] + '/' + m[2];
        return formatedDate;
    }
}

function DateFormatter(value) {
    
    if (value != '' && value != null) {
        var data = value;
        var m = data.split(/[T-]/);
        var d = new Date(parseInt(m[0]), parseInt(m[1]) - 1, parseInt(m[2]));

        var curr_date = d.getDate();
        var curr_month = d.getMonth() + 1
        var curr_year = d.getFullYear();
        var formatedDate = curr_date + '/' + curr_month + '/' + curr_year;
        
        return formatedDate;
    }
}

function capitalize(str) {
    str = str.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });
    return str;
}

function getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    if (age < 0)
        age = -(age);
    return age;
}

function ConcateContact(value)
{
    if (value.countryCode != undefined && value.countryCode != "")
    { value.Phone1 = "+" + value.countryCode + "_" + value.Phone1; }
    else
    { value.Phone1= value.Phone1; }
    return value;
}

function ReplaceContact(value) {
    if(value!= '' && value!= null)
    {
        var data = value;
        var k = data.replace(/_/g, "-");
        return k;
    }
}

function SplitContact(value) {
    if (value != '' && value != null) {
        var k = new Object();
        var data = value;
        if (data.indexOf('_') === -1) {
            k.Phone = data;
        }
        else {
            arr = data.split("_");
            mystring1 = arr[0];
            mystring2 = arr[1];
            var arr1 = mystring1.split("+");
            mystring3 = arr1[0];
            mystring4 = arr1[1];
            k.countrycode = mystring4;
            k.Phone = mystring2;
        }
        return k;
    }
}

function DateFormatterMMDDYYYY(value) {
    if (value.length < 11)
    {
        return value;
    }
    else {
        if (value != '' && value != null) {
            var data = value;
            var m = data.split(/[T-]/);
            var d = new Date(parseInt(m[0]), parseInt(m[1]) - 1, parseInt(m[2]));
            var curr_date;
            var curr_month;
            var curr_year;
            var DateSplitNew = data.split('/');
            curr_date = d.getDate();
            curr_month = d.getMonth() + 1
            curr_year = d.getFullYear();
            var formatedDate = curr_month + '/' + curr_date + '/' + curr_year;
            return formatedDate;
        }
    }
}
function SetTimeOut(Id) {
    window.setTimeout(function () {
        $(".alert").slideUp(500, function () {
            $(this).hide();
        });
    }, 2000);
}