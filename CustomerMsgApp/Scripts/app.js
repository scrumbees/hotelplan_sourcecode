/// <reference path="E:\projects\Application\medical\medicalApplication\medicalApplication\Html/Appointment/AppointmentBook.html" />

var app = angular.module("CustomerMsgApp", ['ui.router', 'ngCookies']);
if (document.location.hostname == "localhost")
    domain = '/CustomerMsgApp';
else
    domain = '';
app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {


    //$httpProvider.defaults.headers.common = {};
    //$httpProvider.defaults.headers.get = {};
    //$httpProvider.defaults.headers.post = {};
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