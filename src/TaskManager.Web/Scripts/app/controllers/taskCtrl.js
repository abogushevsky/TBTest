﻿angular.module("app.controllers").controller("taskCtrl", [
    "$scope", "$rootScope", "$state", "$stateParams", "tasksService", "categoriesService",
    function ($scope, $rootScope, $state, $stateParams, tasksService, categoriesService) {
        $scope.task = {};
        $scope.categories = [];
        $scope.isNew = false;
        $scope.catControlVisible = false;
        $scope.newCatName = null;

        $scope.init = function() {
            var id = $stateParams.id;
            $scope.isNew = id == "new";

            //список категорий и сама задача загружаются синхронно друг относительно друга,
            //чтобы к моменту загрузки задачи все категории точно были загружены
            categoriesService.getList()
                .then(function(data) {
                        $scope.categories = data;
                        if (!$scope.isNew)
                            tasksService.getById(id)
                                .then(
                                    function(data) { $scope.tasksList = data },
                                    function(err) {
                                        alert(err
                                            .Message
                                            ? err.Message
                                            : "Ошибка при получении задачи");
                                    });
                    },
                    function(err) {
                        alert(err.Message ? err.Message : "Ошибка при получении категорий");
                    });
        };

        $scope.init();

        $scope.setCatControlVisibility = function(isVisible) {
            $scope.catControlVisible = isVisible;
            if (!$scope.catControlVisible)
                $scope.newCatName = null;
        };

        $scope.addCategory = function() {
            if ($scope.newCatName && $scope.newCatName.length > 0) {
                categoriesService.addCategory({ Name: $scope.newCatName })
                    .then(
                        function (data) {
                            categoriesService.getById(data)
                                .then(function(data) {
                                    $scope.categories.push(data);
                                    $scope.catControlVisible = false;
                                    $scope.newCatName = null;
                                }, function (err) { });
                        },
                        function (err) { alert(err.Message ? err.Message : "Не удалось сохранить категорию"); }
                    );
            }
        };
    }
]);