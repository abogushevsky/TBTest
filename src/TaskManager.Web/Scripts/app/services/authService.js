angular.module("app.services").factory("authService", [
    "$http", "$q", "$timeout",
    function ($http, $q, $timeout) {
        return {
            login: function (userName, password) {
                var request = "grant_type=password&UserName=" + userName + "&Password=" + password;
                return $http.post("Token", request);
            },
            register: function(regModel) {
                return $http.post("api/Account/Register", regModel);
            }
        }
    }
]);