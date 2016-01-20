
app.service('customerService', function ($http) {

    this.SendMessage = function (Customer) {
        $("#overlay img").show();
        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/CheckPassword",
            data: Customer
        })
        return request;
    };

    this.CheckExists = function (Customer) {
        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/CheckExists",
            data: Customer
        })
        return request;
    };

    this.GetTourOpCode = function () {
        return $http.get("/api/CustomerAPI/GetTourOpCode");
    };
    this.GetDeparturePoint = function () {
        return $http.get("/api/CustomerAPI/GetDeparturePoint");
    };
    this.GetArrivalPoint = function () {
        return $http.get("/api/CustomerAPI/GetArrivalPoint");
    };
    this.GetTravelDate = function () {
        return $http.get("/api/CustomerAPI/GetTravelDate");
    };
    this.GetTravelDirection = function () {
        return $http.get("/api/CustomerAPI/GetTravelDirection");
    };
    this.GetTransportCarrier = function () {
        return $http.get("/api/CustomerAPI/GetTransportCarrier");
    };
    this.GetTransportNumber = function () {
        return $http.get("/api/CustomerAPI/GetTransportNumber");
    };
    this.GetTransportType = function () {
        return $http.get("/api/CustomerAPI/GetTransportType");
    };
    this.GetTransportChain = function () {
        return $http.get("/api/CustomerAPI/GetTransportChain");
    };
    this.GetCountryName = function () {
        return $http.get("/api/CustomerAPI/GetCountryName");
    };
    this.GetResortName = function (Id) {
        return $http.get("/api/CustomerAPI/GetResortName/" + Id);
    };
    this.GetAccommodationName = function (Id) {
        return $http.get("/api/CustomerAPI/GetAccommodationName/" + Id);
    };
});
