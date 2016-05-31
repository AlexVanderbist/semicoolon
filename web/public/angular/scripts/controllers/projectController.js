(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', function ($scope, $rootScope, $state, project) {

            // bind some scope stuff
            $scope.project = project.data.project;

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