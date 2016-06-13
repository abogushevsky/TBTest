angular.module("app.controllers", []);
angular.module("app.directives", []);
angular.module("app.services", []);

angular.module("app", ["ui.router", "app.controllers", "app.services", "ui.bootstrap", "ngRoute", "angular.filter"]).config([
    "$stateProvider", "$urlRouterProvider", "$httpProvider",
    function($stateProvider, $urlRouterProvider, $httpProvider) {
        $urlRouterProvider.otherwise("/");

        $httpProvider.interceptors.push([
            "$q", "$location", function($q, $location) {
                return {
                    request: function(config) { return config },
                    requestError: function(rejection) { $q.reject(rejection); },
                    response: function(response) {
                        if (response.status == "401") {
                            $location.path("/auth");
                        }
                        return response;
                    },
                    responseError: function(rejection) {
                        if (rejection.status == "401") {
                            $location.path("/auth");
                            return rejection;
                        } else {
                            return $q.reject(rejection);
                        }
                    }
                }
            }
        ]);

        $stateProvider
            .state("auth",
            {
                url: "/auth",
                templateUrl: "Templates/Auth.html",
                controller: "authCtrl"
            })
            .state("register",
            {
                url: "/register",
                templateUrl: "Templates/Register.html",
                controller: "registerCtrl"
            })
            .state("main",
            {
                url: "/",
                templateUrl: "Templates/TasksList.html",
                controller: "tasksListCtrl"
            })
            .state("task",
            {
                url: "/task/:id",
                templateUrl: "Templates/TaskForm.html",
                controller: "taskCtrl"
            });
    }
]);

angular.module("app.directives", [])
        .directive('convertToNumber', [function () {
        return {
            require: 'ngModel',
            link: function(scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function(val) { return (val ? parseInt(val, 10) : null) });
                ngModel.$formatters.push(function(val) { return (val ? "" + val : null) });
            }
        }
    }]);

angular.module("app").run(["$route", "$rootScope", "$location", "$http", function ($route, $rootScope, $location, $http) {
}]);