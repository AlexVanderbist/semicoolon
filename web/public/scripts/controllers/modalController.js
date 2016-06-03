(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('modalController', function ($scope, $stateParams) {

            $scope.map = {
                center: {
                    latitude: 51.218686,
                    longitude: 4.417458
                },
                zoom: 14
            };

			$scope.status = $stateParams.status;
        });

})();
