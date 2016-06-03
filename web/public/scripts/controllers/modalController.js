(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('modalController', function ($scope, mapOptions) {
        	
            $scope.map = { 
                center: mapOptions.center, 
                zoom: mapOptions.zoom,
                options: {styles: mapOptions.styleArray}
            };

        });
    
})();