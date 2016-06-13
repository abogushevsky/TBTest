angular.module("app.services").factory("authService", [
    "$http", "$q", "$timeout",
    function ($http, $q, $timeout) {
        return {
            login: function(userName, password) {
                console.log("on login");
            },
            register: function(userModel) {
                console.log("on register");
            }
        }
    }
]);