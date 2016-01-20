

appModule.controller('LektionController', ['$scope', 'LektionFactory', function ($scope, LektionFactory) {
    $scope.greetings = 'Mikkel!';

    

    getLektioner();

    
    //Henter alle lektioner
    function getLektioner() {
        LektionFactory.getLektioner().success(function (inLektion) {
            $scope.lektioner = inLektion;
        })
        .error(function (error) {
            $scope.status = 'Unable to load lektionsdata' + error.message;
         
        });
    }

    $scope.deleteLektion = function deleteLektion(id) {
        LektionFactory.deleteLektion(id).success(function () {
            for (var i = 0; i < $scope.lektioner.length; i++) {
                var lektion = $scope.lektioner[i];
                if (lektion.Id == id) {
                    $scope.lektioner.splice(i, 1);
                    break;
                }
            }
        }).error(function (error) {

        });
    };


}]);