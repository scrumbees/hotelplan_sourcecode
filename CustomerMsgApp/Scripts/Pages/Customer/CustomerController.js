
var CustomerSerach = new Object();
var _CustomorList = null;
var Mobilecount = 0;
var MailCount = 0;

app.controller('customercontroller', function ($scope, customerService, $cookies) {

    var arrlocl = localStorage['role'].toString().split(',');
    $.each(arrlocl, function (a) {
        if (arrlocl[a] == "1") {
            $('#CustMenu').show();
        }
        if (arrlocl[a] == "2") {
            bootbox.confirm("You are not allowed to access this page.", function (result) {
                if (result == true) {
                    window.location.href = "/Views/Login.html";
                }
                else {
                    window.location.href = "/Views/Login.html";
                }
            });
        }
        if (arrlocl[a] == "3") {
            $('#UsrMenu').show();
            $('#MesgMenu').show();
            $('#CustMenu').show();
        }
    })

    $scope.divSendMessage = true;
    $scope.format = 'yyyy-MM-dd';
    var isDefault = false;
    $("#txtMessageEmail").prop('disabled', true);

    getAllCustomerList();
    GetTourOpCode();
    GetDeparturePoint();
    GetArrivalPoint();
    GetTravelDate();
    GetTravelDirection();
    GetTransportCarrier();
    GetTransportNumber();
    GetTransportType();
    GetTransportChain();
    GetCountryName();
    //GetResortName();
    //GetAccommodationName();

    function getAllCustomerList() {
        setData();
    }

    $scope.Search = function (Customer) {
        CustomerSerach.TourOpCode = Customer != undefined ? Customer.TourOpCode : "";
        CustomerSerach.DirectOrAgent = Customer != undefined ? Customer.DirectOrAgent : "";
        CustomerSerach.StartDate = $("#StartDate").val();
        CustomerSerach.DeparturePoint = Customer != undefined ? Customer.DeparturePoint : "";
        CustomerSerach.ArrivalPoint = Customer != undefined ? Customer.ArrivalPoint : "";
        CustomerSerach.TravelDate = Customer != undefined ? Customer.TravelDate : "";
        CustomerSerach.TravelDepatureTime = $("#TravelDepatureTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM")
        CustomerSerach.TravelArrivalTime = $("#TravelArrivalTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM");
        CustomerSerach.TravelDirection = Customer != undefined ? Customer.TravelDirection : "";
        CustomerSerach.TransportCarrier = Customer != undefined ? Customer.TransportCarrier : "";
        CustomerSerach.TransportNumber = Customer != undefined ? Customer.TransportNumber : "";
        CustomerSerach.TransportType = Customer != undefined ? Customer.TransportType : "";
        CustomerSerach.TransportChain = Customer != undefined ? Customer.TransportChain : "";
        CustomerSerach.CountryName = Customer != undefined ? Customer.CountryName : "";
        CustomerSerach.ResortName = Customer != undefined ? Customer.ResortName : "";
        CustomerSerach.AccommodationName = Customer != undefined ? Customer.AccommodationName : "";
        refreshDatatable();
    }

    function setData() {

        $('#CustomorListTBL').dataTable({
            "bFilter": false,
            "bInfo": true,
            "scrollX": true,
            "bServerSide": true,
            "bLengthChange": true,

            "sAjaxSource": "/api/CustomerAPI/GetAllNotification/",
            "dom": 'T<"clear">lfrtip',
            tableTools: {
                "sSwfPath": "/content/swf/copy_csv_xls_pdf.swf",
                "columnDefs": [
                    { "width": "200px", "targets": 0 },
                ],
                "aButtons": [
                ]
            },
            "fnServerData": function (sSource, aoData, fnCallback) {

                aoData.push({ "name": "TourOpCode", "value": CustomerSerach.TourOpCode });
                aoData.push({ "name": "DirectOrAgent", "value": CustomerSerach.DirectOrAgent });
                aoData.push({ "name": "StartDate", "value": CustomerSerach.StartDate });
                aoData.push({ "name": "DeparturePoint", "value": CustomerSerach.DeparturePoint });
                aoData.push({ "name": "ArrivalPoint", "value": CustomerSerach.ArrivalPoint });
                aoData.push({ "name": "TravelDate", "value": CustomerSerach.TravelDate });
                aoData.push({ "name": "TravelDepatureTime", "value": CustomerSerach.TravelDepatureTime });
                aoData.push({ "name": "TravelArrivalTime", "value": CustomerSerach.TravelArrivalTime });
                aoData.push({ "name": "TravelDirection", "value": CustomerSerach.TravelDirection });
                aoData.push({ "name": "TransportCarrier", "value": CustomerSerach.TransportCarrier });
                aoData.push({ "name": "TransportNumber", "value": CustomerSerach.TransportNumber });
                aoData.push({ "name": "TransportType", "value": CustomerSerach.TransportType });
                aoData.push({ "name": "TransportChain", "value": CustomerSerach.TransportChain });
                aoData.push({ "name": "CountryName", "value": CustomerSerach.CountryName });
                aoData.push({ "name": "ResortName", "value": CustomerSerach.ResortName });
                aoData.push({ "name": "AccommodationName", "value": CustomerSerach.AccommodationName });

                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource,
                    crossDomain: true,
                    "data": aoData,
                    "success": function (json) {
                        if (json.aaData != null) {
                            Mobilecount = json.MobileCount;
                            MailCount = json.EmailCount;
                            $("#BindCount").html("<div> Total Mobile SMS Count: <span id='spanMobilecount'>" + Mobilecount + "</span></div><div> Total Email Count: " + MailCount + "</div>")
                            _CustomorList = json.aaData;
                        }
                        fnCallback(json);
                    },
                    "error": function (json) {
                    }
                });
            },
            "aoColumns": [
                {
                    "sTitle": "Ref",
                    "mDataProp": "BookingRef"
                },
                {
                    "sTitle": "TO",
                    "mDataProp": "TourOpCode"
                },
                {
                    "sTitle": "Name",
                    "mDataProp": "FullName"
                },
                {
                    "sTitle": "SMS",
                    "mDataProp": "MobileNo"
                },
                {
                    "sTitle": "Channel",
                    "mDataProp": "DirectOrAgent"
                },
                {
                    "sTitle": "TravelDate",
                    "mDataProp": "TravelDate"
                },
                {
                    "sTitle": "From",
                    "mDataProp": "DeparturePoint"
                },
                {
                    "sTitle": "To",
                    "mDataProp": "ArrivalPoint"
                },
                {
                    "sTitle": "Flight",
                    "mDataProp": "TransportNumber"
                },
                {
                    "sTitle": "Chain",
                    "mDataProp": "FlightChain"
                }
            ],
            "bDestroy": true
        });
    }

    $scope.Send = function (Customer) {
        var flag = true;

        flag = ValidateMessage();
        if (!flag) {
            return false;
        }
        else {
            $("#myModal").modal();
            //var checkdata = customerService.CheckExists(Customer);
            //checkdata.then(function (cus) {
            //    var res = cus.data;
            //    if (!res) {
            //        flag = false;
            //    }
            //    if (flag) {
            //        $("#myModal").modal();
            //    }
            //    else {
            //        //alert('already sent');
            //        //$("#ModalMessage").modal();
            //    }
            //});
        }
    }

    $scope.SendMessage = function (Customer) {
        var flag = true;
        flag = ValidatePassword();
        if (!flag) {
            return flag;
        }

        CustomerSerach.TourOpCode = Customer.TourOpCode;
        CustomerSerach.DirectOrAgent = Customer.DirectOrAgent;
        CustomerSerach.StartDate = $("#StartDate").val();
        CustomerSerach.DeparturePoint = Customer.DeparturePoint;
        CustomerSerach.ArrivalPoint = Customer.ArrivalPoint;
        CustomerSerach.TravelDate = Customer.TravelDate;
        CustomerSerach.TravelDepatureTime = $("#TravelDepatureTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM")
        CustomerSerach.TravelArrivalTime = $("#TravelArrivalTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM");
        CustomerSerach.TravelDirection = Customer.TravelDirection;
        CustomerSerach.TransportCarrier = Customer.TransportCarrier;
        CustomerSerach.TransportNumber = Customer.TransportNumber;
        CustomerSerach.TransportType = Customer.TransportType;
        CustomerSerach.TransportChain = Customer.TransportChain;
        CustomerSerach.CountryName = Customer.CountryName;
        CustomerSerach.ResortName = Customer.ResortName;
        CustomerSerach.AccommodationName = Customer.AccommodationName;
        CustomerSerach.MessageText = Customer.MessageText;
        CustomerSerach.Password = Customer.Password;
        Customer.Mobilecount = Mobilecount;
        CustomerSerach.SendCountSMS = Customer.Mobilecount;

        var sendMessage = customerService.SendMessage(Customer);
        $scope.divSendMessage = false;
        sendMessage.then(function (cus) {
            var res = cus.data;
            if (res == '1') {
                $("#overlay img").hide();
                $scope.divSendMessage = false;
                $scope.divMessageSuccess = true;
                $scope.divDone = true;
                setTimeout(function () {
                    $scope.divMessageSuccess = false;
                    $scope.divDone = false;
                }, 100)
            }
            else if (res == '2') {
                $("#overlay img").hide();
                $scope.divSendMessage = true;
                $scope.divMessageSuccess = false;
                $scope.divDone = false;
                $scope.divMessageError = false;
                $scope.divMessageErrorCount = true;

                $("#divMessageErrorCount").css('opacity', '10');
                $('#divMessageErrorCount').css('display', 'block');
                window.setTimeout(function () {
                    $("#divMessageErrorCount").fadeTo(500, 0).slideUp(500, function () {
                        $(this).hide();
                    });
                }, 2000);
            }
            else if (res == '3') {
                $("#overlay img").hide();
                $scope.divSendMessage = true;
                $scope.divMessageSuccess = false;
                $scope.divDone = false;
                $scope.divMessageError = true;
                $scope.divMessageErrorCount = false;

                $("#divMessageError").css('opacity', '10');
                $('#divMessageError').css('display', 'block');
                window.setTimeout(function () {
                    $("#divMessageError").fadeTo(500, 0).slideUp(500, function () {
                        $(this).hide();
                    });
                }, 2000);
            }
            else {
                $("#overlay img").hide();
                $scope.divSendMessage = false;
                $scope.divMessageSuccess = true;
                $scope.divDone = true;
                setTimeout(function () {
                    $scope.divMessageSuccess = false;
                    $scope.divDone = false;
                }, 100)
            }

        }, function error(data) {
        });
    }

    function refreshDatatable() {
        $('#CustomorListTBL').dataTable().fnDraw();
    }

    $scope.close = function (Customer) {

        Customer.Message = '';
        Customer.Password = '';
        Customer.SendCountSMS = '';
        $scope.divSendMessage = true;
        $scope.divMessageSuccess = false;
        $scope.divDone = false;
        $scope.divMessageError = false;
        $scope.divMessageErrorCount = false;
        $("#overlay img").hide();
    }

    function GetTourOpCode() {
        var promiseGet = customerService.GetTourOpCode();
        promiseGet.then(function (p1) {
            $scope.TourOpCode = p1.data
        }, function (err) {
            console.log("Error");
        });
    }

    function GetDeparturePoint() {
        var promiseGet = customerService.GetDeparturePoint();
        promiseGet.then(function (p1) { $scope.DeparturePoint = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetArrivalPoint() {
        var promiseGet = customerService.GetArrivalPoint();
        promiseGet.then(function (p1) { $scope.ArrivalPoint = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTravelDate() {
        var promiseGet = customerService.GetTravelDate();
        promiseGet.then(function (p1) { $scope.TravelDate = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTravelDirection() {
        var promiseGet = customerService.GetTravelDirection();
        promiseGet.then(function (p1) { $scope.TravelDirection = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportCarrier() {
        var promiseGet = customerService.GetTransportCarrier();
        promiseGet.then(function (p1) { $scope.TransportCarrier = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportNumber() {
        var promiseGet = customerService.GetTransportNumber();
        promiseGet.then(function (p1) { $scope.TransportNumber = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportType() {
        var promiseGet = customerService.GetTransportType();
        promiseGet.then(function (p1) { $scope.TransportType = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportChain() {
        var promiseGet = customerService.GetTransportChain();
        promiseGet.then(function (p1) { $scope.TransportChain = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetCountryName() {
        var promiseGet = customerService.GetCountryName();
        promiseGet.then(function (p1) {
            $scope.CountryName = p1.data
        }, function (err) {
            console.log("Error");
        });
    }

    $scope.GetResortName = function () {
        if ($scope.Customer.CountryName != '' && $scope.Customer.CountryName != undefined) {
            var promiseGet = customerService.GetResortName($scope.Customer.CountryName);
            promiseGet.then(function (p1) {
                $scope.ResortName = p1.data
            }, function (err) {
                console.log("Error");
            });
        }
        else {
            $scope.ResortName = [],
            $scope.Customer.ResortName = '';
        }
    }

    $scope.GetAccommodationName = function () {
        if ($scope.Customer.ResortName != '' && $scope.Customer.ResortName != undefined) {
            var promiseGet = customerService.GetAccommodationName($scope.Customer.ResortName);
            promiseGet.then(function (p1) {
                $scope.AccommodationName = p1.data
            }, function (err) {
                console.log("Error");
            });
        }
        else {
            $scope.AccommodationName = [],
            $scope.Customer.AccommodationName = '';
        }
    }

    $scope.Clear = function (Customer) {
        $('#msgmaxlength').text('');
        $scope.EmailEnable == false;
        $("#overlay img").hide();
        $('input').val('');
        $('select').val('');
        CustomerSerach = {};
        getAllCustomerList();
    }

    $scope.Done = function (Customer) {
        $('#myModal').modal('hide');
        Customer.Message = '';
        Customer.Password = '';
        Customer.SendCountSMS = '';
        $scope.divSendMessage = true;
        $scope.divMessageSuccess = false;
        $scope.divDone = false;
        $scope.divMessageError = false;
        $scope.divMessageErrorCount = false;
        Customer.MessageEmail = '';
        $('#msgmaxlength').text('');
        $scope.EmailEnable == false;
        $("#overlay img").hide();
    }

    $scope.EnableEmail = function (Customer) {
        if ($scope.EmailEnable == true) {
            $("#txtMessageEmail").prop('disabled', false);
        }
        else {
            $("#txtMessageEmail").prop('disabled', true);
            Customer.MessageEmail = '';
        }
    }
});

function ValidatePassword() {
    var flag = true;
    if (!ValidateRequiredField($("#txtPasswordSend"), 'Password required', 'after')) {
        flag = false;
    }
    if (!ValidateRequiredField($("#txtSendCountSMS"), 'Message count required', 'after')) {
        flag = false;
    }
    return flag;
}

function ValidateMessage() {
    var flag = true;

    if (!ValidateRequiredField($("#txtMessage"), 'Message Required', 'after')) {
        flag = false;
    }
    return flag;
}