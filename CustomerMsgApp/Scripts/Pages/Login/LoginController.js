
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

                    //if (role == 1) {
                    //    window.location.href = "/Views/Customer.html";
                    //}
                    //else if (role == 2) {
                    //    window.location.href = "/Views/messagelog.html";
                    //}
                    //else if (role == 3) {
                    //    window.location.href = "/Views/User.html";
                    //}
                    
                    role.push(roleId);
                });


                localStorage['role'] = role;
                window.location.href = "/Views/messagelog.html";

                //if (jQuery.inArray(3, role) != -1) {
                //    window.location.href = "/Views/User.html";
                //   // localStorage['role'] = 3
                //}
                //else if (jQuery.inArray(2, role) != -1) {
                //    window.location.href = "/Views/messagelog.html";
                //    //localStorage['role'] = 2
                //}
                //else if (jQuery.inArray(1, role) != -1) {
                //    window.location.href = "/Views/Customer.html";
                //  //  localStorage['role'] = 3
                //}
              
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