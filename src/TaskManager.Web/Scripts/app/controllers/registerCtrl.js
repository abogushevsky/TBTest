angular.module("app.controllers").controller("registerCtrl", [
    "$scope", "$rootScope", "$state", "authService",
    function ($scope, $rootScope, $state, authService) {
        $scope.model = {
            Email: null,
            FirstName: null,
            LastName: null,
            Password: null,
            ConfirmPassword: null
        };

        $scope.showError = false;
        $scope.errorText = "Не удалось зарегистрироваться";

        $scope.register = function () {
            $scope.showError = false;
            authService.register($scope.model)
                .then(
                    function() { $state.go("main"); },
                    function (err) {
                        if (err.data.error_description)
                            $scope.errorText = err.data.error_description;
                        $scope.showError = true;
                    });
        };
    }
]);