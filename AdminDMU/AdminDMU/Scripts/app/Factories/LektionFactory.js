angular.module('myApp')
    .factory('LektionFactory', ['$http', function ($http) {
        var urlBase = 'api/Lektion';
        var LektionFactory = {};

        LektionFactory.getLektioner = function () {
            return $http.get(urlBase);
        };

        LektionFactory.deleteLektion = function (id) {
            return $http.delete(urlBase + "/" + id);
        };

        return LektionFactory;

    }]);