angular.module("app.controllers").controller("tasksListCtrl", [
    "$scope", "$rootScope", "$state", "tasksService",
    function ($scope, $rootScope, $state, tasksService) {
        $scope.tasksList = [];

        $scope.init = function() {
            tasksService.getList().then(function(data) { $scope.tasksList = data });
        }

        $scope.init();

        $scope.getCatName = function(name) {
            if (!name || name == "undefined")
                return "Без категории";

            return name;
        };

        $scope.editTask = function(id) {
            $state.go("task", { id: id });
        };
    }
]);