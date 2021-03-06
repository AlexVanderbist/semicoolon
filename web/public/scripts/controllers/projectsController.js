(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectsController', projectsController);  

    function projectsController($scope, $auth, $rootScope, $state, projects, themes, filterFilter) {

        // bind some scope stuff
        $scope.projects = projects.data.projects;
        $scope.themes = themes.data.themes;
        $scope.projectsFiltered = [];
        $scope.filter = [];

        angular.forEach($scope.themes, function(value,key) {
            $scope.filter[value.id] = true;
            //$scope.$apply();
        });

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

        // Add lat en lng in full
        angular.forEach($scope.projects, function(value, key) {
            value.latitude = value.lat;
            value.longitude = value.lng;
            value.icon = iconSymbol(value.theme.hex_color);
        });

        $scope.$watchCollection('filter', function(newValue) {
            $scope.projectsFiltered = filterFilter($scope.projects, $scope.filterByTheme);
        });

        $scope.filterByTheme = function (project) {
            // Display the project if
            //console.log($scope.filter[project.theme_id], noFilter($scope.filter));
            return $scope.filter[project.theme_id] || noFilter($scope.filter);
        };

        function noFilter(filterObj) {
            for (var key in filterObj) {
                if (filterObj[key]) {
                    // There is at least one checkbox checked
                    return false;
                }
            }
            // No checkbox was found to be checked
            return true;
        }

    }
    
})();