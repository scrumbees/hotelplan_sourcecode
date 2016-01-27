
var app = angular.module("CustomerMsgApp", ['ui.router', 'ngCookies']);
if (document.location.hostname == "localhost")
    domain = '/CustomerMsgApp';
else
    domain = '';
app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

    $stateProvider

      .state('Customer', {
          url: '/Customer',
          templateUrl: '/Views/Customer.html',
          controller: "customercontroller"
      })
     .state('Login', {
         url: '/Login',
         templateUrl: '/Views/Login.html',
         controller: "logincontroller"
     })

    $urlRouterProvider.otherwise('/Dashboard');
});
