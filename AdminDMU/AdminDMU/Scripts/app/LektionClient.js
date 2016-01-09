var LektionApp = angular.module('LektionApp', [])

LektionApp.controller('LektionController', function ($scope, LektionService) {

    getLektioner();
    function getLektioner() {
        LektionService.getLektioner()
        .success(function ())

    }

    $scope.message = "Infrgistics";
});

LektiionApp.factory('LektionService', ['$http', function ($http) {

    var LektionService = {};
    LektionService.getLektioner = function () {
        return $http.get('/api/Lektion');
    };

    return LektionService;

}]);