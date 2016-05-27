(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', projectController);  

    function projectController($http, $scope, $auth, $rootScope, $state) {
        $scope.users;
        $scope.error;

        $scope.getProjects = function() {

            // This request will hit the index method in the AuthenticateController
            // on the Laravel side and will return the list of users
            $http.get('/api/v1/projects').success(function(projects) {
                $scope.projects = projects.projects;
            }).error(function(error) {
                $scope.error = error;
            });
        }

    }
    
})();