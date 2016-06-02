(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', function ($scope, $rootScope, $stateParams, project, projectService) {

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
            $scope.map = { 
                center: { 
                    latitude: $scope.project.lat, 
                    longitude: $scope.project.lng 
                }, 
                zoom: 15
            };

            $scope.project = project.data.project;
            $scope.project.latitude = $scope.project.lat;
            $scope.project.longitude = $scope.project.lng;
            $scope.project.icon = iconSymbol($scope.project.theme.hex_color);
            $scope.project.opinions = [];
            $scope.opinionsInitLoading = true;

            console.log($scope.project);

            projectService.opinions($stateParams.id).then(function(response){
                // opinions loaded
                $scope.project.opinions = response.data.opinions;
            }, function(response){
                // loading opinions failed
                // do nothing for now
            });
        });
    
})();