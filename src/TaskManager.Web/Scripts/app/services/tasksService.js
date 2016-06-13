angular.module("app.services").factory("tasksService", [
    "$http",
    function ($http) {
        return {
            getList: function() {
                return $http.get("api/tasks").then(
                    function (resp) { return resp.data; },
                    function (err) { console.log(err); });
            }
        }
    }
]);