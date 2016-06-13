angular.module("app.controllers").controller("tasksListCtrl", [
    "$scope", "$rootScope", "$state", "$stateParams", "tasksService",
    function ($scope, $rootScope, $state, $stateParams, tasksService) {
        $scope.tasks = {};
        $scope.isNew = false;

        $scope.init = function () {
            var id = $stateParams.id;
            $scope.isNew = id == "new";
            if (!$scope.isNew)
                tasksService.getById(id).then(function (data) { $scope.tasksList = data });
        }

        $scope.init();
    }
]);