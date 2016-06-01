(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', function ($scope, $rootScope, $state, project) {

            // returns google maps icon object with symbol in given color
            var iconSymbol = function (color) {
                return {
                    path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z M -2,-30 a 2,2 0 1,1 4,0 2,2 0 1,1 -4,0',
                    fillColor: color,
                    fillOpacity: 1,
                    strokeColor: '#000',
                    strokeWeight: 2,
                    scale: 1,
               };
            };
            
            // bind some scope stuff
            $scope.project = project.data.project;
            $scope.project.latitude = $scope.project.lat;
            $scope.project.longitude = $scope.project.lng;
            $scope.project.icon = iconSymbol($scope.project.theme.hex_color);

            console.log($scope.project);

            $scope.map = { 
                center: { 
                    latitude: $scope.project.lat, 
                    longitude: $scope.project.lng 
                }, 
                zoom: 15
            };
        });
    
})();