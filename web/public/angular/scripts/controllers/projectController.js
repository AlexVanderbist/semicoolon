(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', projectController);  

    function projectController($scope, $auth, $rootScope, $state, projects, themes, filterFilter) {

        // bind some scope stuff
        $scope.projects = projects.data.projects;
        $scope.themes = themes.data.themes;
        $scope.projectsFiltered = [];
        $scope.filter = [];

        // Add lat en lng in full
        angular.forEach($scope.projects, function(value, key) {
            value.latitude = value.lat;
            value.longitude = value.lng;
        });


        $scope.$watchCollection('filter', function(newValue) {
            console.log('dddd');
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