﻿
var LogSerach = new Object();
var _MessageLogList = null;

app.controller('msglogController', function ($scope, msglogService, $cookies) {

    var arrlocl = localStorage['role'].toString().split(',');
    $.each(arrlocl, function (a) {
        if (arrlocl[a] == "1") {
            bootbox.confirm("You are not allowed to access this page.", function (result) {
                if (result == true) {
                    window.location.href = "/Views/Login.html";
                }
                else {
                    window.location.href = "/Views/Login.html";
                }
            });
        }
        if (arrlocl[a] == "2") {
            $('#MesgMenu').show();
        }
        if (arrlocl[a] == "3") {
            $('#UsrMenu').show();
            $('#MesgMenu').show();
            $('#CustMenu').show();
        }
    })

    getAllLogList();

    function getAllLogList() {
        setData();
    }

    $scope.Search = function (Log) {
        LogSerach.SentTo = Log != undefined ? Log.SentTo : "";
        LogSerach.FirstName = Log != undefined ? Log.FirstName : "";
        LogSerach.Surname = Log != undefined ? Log.Surname : "";
        LogSerach.SentFrom = Log != undefined ? Log.SentFrom : "";
        LogSerach.Message = Log != undefined ? Log.Message : "";
        LogSerach.Status = Log != undefined ? Log.Status : "";
        LogSerach.LogComment = Log != undefined ? Log.LogComment : "";
        LogSerach.CreateDate = $("#CreateDate").val();
        refreshDatatable();
    }

    function setData() {

        $('#MessageLogListTBL').dataTable({
            "bFilter": false,
            "bInfo": true,
            "scrollX": true,
            "bServerSide": true,
            "bLengthChange": true,

            "sAjaxSource": "/api/CustomerAPI/GetAllMessageLog/",
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

                aoData.push({ "name": "SentTo", "value": LogSerach.SentTo });
                aoData.push({ "name": "FirstName", "value": LogSerach.FirstName });
                aoData.push({ "name": "Surname", "value": LogSerach.Surname });
                aoData.push({ "name": "FullName", "value": LogSerach.FullName });
                aoData.push({ "name": "SentFrom", "value": LogSerach.SentFrom });
                aoData.push({ "name": "Message", "value": LogSerach.Message });
                aoData.push({ "name": "Status", "value": LogSerach.Status });
                aoData.push({ "name": "LogComment", "value": LogSerach.LogComment });
                aoData.push({ "name": "CreateDate", "value": LogSerach.CreateDate });

                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource,
                    crossDomain: true,
                    "data": aoData,
                    "success": function (json) {
                        if (json.aaData != null) {
                            _MessageLogList = json.aaData;
                        }
                        fnCallback(json);
                    },
                    "error": function (json) {
                    }
                });
            },
            "aoColumns": [
                {
                    "sTitle": "SentTo",
                    "mDataProp": "SentTo"
                },
                {
                    "sTitle": "FullName",
                    "mDataProp": "FullName"
                },
                {
                    "sTitle": "SentFrom",
                    "mDataProp": "SentFrom"
                },
                {
                    "sTitle": "Message",
                    "mDataProp": "Message"
                },
                {
                    "sTitle": "Status",
                    "mDataProp": "Status"
                },
                {
                    "sTitle": "LogComment",
                    "mDataProp": "LogComment"
                },
                {
                    "sTitle": "SentDate",
                    "mDataProp": "CreateDate"
                }
            ],
            "bDestroy": true
        });
    }

    function refreshDatatable() {
        $('#MessageLogListTBL').dataTable().fnDraw();
    }

    $scope.Clear = function (Log) {
        $('input').val('');
        LogSerach = {};
        getAllLogList();
    }

});