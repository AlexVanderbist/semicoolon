(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('modalController', function ($scope) {
        	
            $scope.map = { 
                center: { 
                    latitude: 51.218686, 
                    longitude: 4.417458 
                }, 
                zoom: 14
            };

        });
    
})();