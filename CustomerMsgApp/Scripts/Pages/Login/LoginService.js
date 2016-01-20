
app.service('loginService', function ($http) {
    this.Login = function (email, password) {
        var Login = {
            UserName: email,
            Password: password
        };

        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/UserLogin",
            data: Login
        });
        return request;

        //$.ajax({
        //    type: "POST",
        //    async: "true",
        //    url: "/api/CustomerAPI/UserLogin",
        //    data: Login,
        //    success: function (html) {
        //        debugger;
        //    },
        //error: function (request, status, error) {
        //    //alert('Error: ' + error);
        //}
        //});
        //return $http.get("/api/CustomerAPI/Login");
    }
});