
var _UserList = null;

app.controller('usercontroller', function ($scope, userService, $cookies, $compile) {

    GetRoleName();
    function GetRoleName() {
        var promiseGet = userService.GetRoleName();
        promiseGet.then(function (p1) { $scope.RoleName = p1.data }, function (err) {
            console.log("Error");
        });
    }

    getAllUserList();
    function getAllUserList() {
        var promisePost = userService.GetAllUser();
        promisePost.then(function (p1) {
            $scope.Users = p1.data
        }, function (err) {
            console.log("Error");
        });
    }

    function setData() {

        $('#UserListTBL').dataTable({
            "bFilter": true,
            "bInfo": true,
            "bServerSide": false,
            "sAjaxDataProp": "",
            "sAjaxSource": "/api/UserAPI/GetAllUser/",
            "dom": 'T<"clear">lfrtip',
            tableTools: {
                "sSwfPath": "/content/swf/copy_csv_xls_pdf.swf",
                "columnDefs": [
                    { "width": "200px", "targets": 0 },
                ],
            },
            "fnServerData": function (sSource, aoData, fnCallback) {

                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource,
                    crossDomain: true,
                    "data": aoData,
                    "success": function (json) {
                        if (json.aaData != null) {
                            _UserList = json.aaData;
                        }
                        fnCallback(json);
                    },
                    "error": function (json) {
                    }
                });
            },
            "aoColumns": [
                {
                    "sTitle": "UserName",
                    "mDataProp": "UserName"
                },
                {
                    "sTitle": "Password",
                    "mDataProp": "Password"
                },
                {
                    "sTitle": "FirstName",
                    "mDataProp": "FirstName"
                },
                {
                    "sTitle": "LastName",
                    "mDataProp": "LastName"
                },
                {
                    "sTitle": "Email",
                    "mDataProp": "Email"
                },
                {
                    "sTitle": "MobileNo",
                    "mDataProp": "MobileNo"
                },
                {
                    "sTitle": "CreateDate",
                    "mDataProp": "CreateDate"
                },
                {
                    "sTitle": "Edit",
                    "mRender": function (data, type, full) {
                        return '<a onclick="editData(' + full.Id + ')">Edit</a>';
                    }
                }
            ],
            "bDestrpy": true
        });
    }

    $scope.edit = function (user) {
        var promiseGetSingle = userService.GetUser(user.Id);
        promiseGetSingle.then(function (pl) {
            var res = pl.data;

            $scope.Id = res[0].Id;
            $scope.UserName = res[0].UserName;
            $scope.Password = res[0].Password;
            $scope.FirstName = res[0].FirstName;
            $scope.LastName = res[0].LastName;
            $scope.Email = res[0].Email;
            $scope.MobileNo = res[0].MobileNo;

            var promiseGet = userService.GetUserRole(user.Id);
            promiseGet.then(function (p2) {
                angular.forEach(p2.data, function (value, key) {
                    var roleid = value.RoleId;
                    $scope.selection.push(roleid);
                });
            });
            $("#adduser").modal('show');
        },
        function (errorPl) {
            console.log("Error");
        });
    }

    $scope.selection = [];
    $scope.toggleSelection = function toggleSelection(role) {
        var idx = $scope.selection.indexOf(role);
        if (idx > -1) {
            $scope.selection.splice(idx, 1);
        }
        else {
            $scope.selection.push(role);
        }
    }

    $scope.Save = function () {
        //alert($scope.Id);
        var flag = true;

        flag = ValidateMessage();

        if (flag == true) {
            var User = {
                Id: $scope.Id,
                UserName: $scope.UserName,
                Password: $scope.Password,
                FirstName: $scope.FirstName,
                LastName: $scope.LastName,
                Email: $scope.Email,
                MobileNo: $scope.MobileNo,
                CreateDate: new Date(),
            };

            var adddata = userService.AddNewUser(User);
            adddata.then(function (cus) {
                var res = cus.data;
                if (res != 0) {

                    if ($scope.selection.length > 0) {
                        var deleteUserRole = userService.DeleteUserRole(res);
                        deleteUserRole.then(function (delRole) {

                            angular.forEach($scope.selection, function (value, key) {
                                var UserRole = new Object();
                                UserRole.UserId = res;
                                UserRole.RoleId = value;
                                var addrole = userService.AddUserRole(UserRole);
                                addrole.then(function (usrrole) {
                                });
                            });


                            if ($scope.Id != 0 && $scope.Id != null) {
                                alert('Successfully updated');
                            }
                            else {
                                alert('Successfully added');
                            }
                            $("#adduser").modal('hide');
                            $scope.clearDetail();
                            getAllUserList();
                        });

                    }
                }
            });
        }
    }

    $scope.clearDetail = function () {
        $('.errormsg').remove();
        $scope.Id = 0;
        $scope.UserName = null;
        $scope.Password = null;
        $scope.FirstName = null;
        $scope.LastName = null;
        $scope.Email = null;
        $scope.MobileNo = null;
        $scope.selection = [];
        getAllUserList();
    }

    $scope.checkUserNameExist = function () {
        $("#txtUserName").next('.errormsg').remove();
        $("#txtUserName").prev('.errormsg').remove();
        if ($scope.Id == undefined) {
            $scope.Id = 0;
        }
        else if ($scope.Id == null) {
            $scope.Id = 0;
        }

        var check = userService.CheckUserNameExist($scope.UserName, $scope.Id);
        check.then(function (p) {
            if (p.data > 0) {
                $('<span class="errormsg">' + "UserName already exist" + '</span>').insertAfter($("#txtUserName"));
            }
            else {
                $("#txtUserName").next('.errormsg').remove();
                $("#txtUserName").prev('.errormsg').remove();
            }
        });
    }

    function ValidateMessage() {
        var flag = true;

        if (!ValidateRequiredField($("#txtUserName"), 'UserName Required', 'after')) {
            flag = false;
        }
        if (!ValidateRequiredField($("#txtPassword"), 'Password Required', 'after')) {
            flag = false;
        }
        if (!ValidateRequiredField($("#txtFirstName"), 'First Name Required', 'after')) {
            flag = false;
        }
        if (!ValidateRequiredField($("#txtLastName"), 'Last Name Required', 'after')) {
            flag = false;
        }
        if ($scope.selection.length == 0) {
            $("#roleerrorMsg").text("Please select Role");
        }
        else {
            $("#roleerrorMsg").text('');
        }

        return flag;
    }
});