
app.service('userService', function ($http) {

    this.AddNewUser = function (User) {
        var request = $http({
            method: "post",
            url: "/api/UserAPI/AddNewUser",
            data: User
        })
        return request;
    };

    this.AddUserRole = function (UserRole) {
        var request = $http({
            method: "post",
            url: "/api/UserAPI/AddUserRole",
            data: UserRole
        })
        return request;
    };

    this.GetRoleName = function () {
        return $http.get("/api/UserAPI/GetRoleName");
    };

    this.GetAllUser = function () {
        return $http.get("/api/UserAPI/GetAllUser");
    };

    this.GetUser = function (Id) {
        return $http.get("/api/UserAPI/GetUser/" + Id);
    }

    this.GetUserRole = function (UserId) {
        return $http.get("/api/UserAPI/GetUserRole/" + UserId);
    }

    this.DeleteUserRole = function (Id) {
        var request = $http({
            method: "Delete",
            url: "/api/UserAPI/DeleteUserRole/" + Id,
        })
        return request;
    };

});