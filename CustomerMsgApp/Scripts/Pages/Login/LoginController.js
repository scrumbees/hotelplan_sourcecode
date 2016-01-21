
var role = [];

app.controller('logincontroller', function ($scope, loginService, $cookies) {

    $('body').keypress(function (e) {
        if (e.which == 13) {  // enter key pressed
            $scope.Login();
        }
    });

    $scope.Login = function () {
        var flag = true;
        var user = {
            Email: $scope.userName,
            Password: $scope.password
        }
        flag = ValidateBusinessForm();
        if (!flag) {
            return flag;
        }


        var UserLogin = loginService.Login($scope.userName, $scope.password);

        UserLogin.then(function (p1) {
            if (p1.data) {


                var userdata = p1.data;

                $.each(userdata, function (i) {
                    var roleId = userdata[i].RoleId;
                    role.push(roleId);
                });
                localStorage['role'] = role;
                window.location.href = "/Views/messagelog.html";

                $cookies.putObject('user', p1.data);
                var f = $cookies.get('user');
                //window.location.href = "/Views/Customer.html";
            }
            else {
                showmsg($('#AddEditMsg_Event'), 3, "Invalid username or password");
            }
        }, function (err) {
            showmsg($('#AddEditMsg_Event'), 3, "Invalid username or password");
        });
    }
});

function ValidateBusinessForm() {
    var flag = true;
    if (!ValidateRequiredField($("#txtUserName"), 'UserName Required', 'after')) {
        flag = false;
    }
    if (!ValidateRequiredField($("#txtPassword"), 'Password Required', 'after')) {
        flag = false;
    }
    return flag;
}