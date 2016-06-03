(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('modalController', function ($scope, mapOptions, $stateParams) {

            $scope.map = {
                center: mapOptions.center,
                zoom: mapOptions.zoom,
                options: {styles: mapOptions.styleArray}
            };

			$scope.status = $stateParams.status;
        });

})();
