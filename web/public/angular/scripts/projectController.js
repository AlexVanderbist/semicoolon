(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('ProjectController', ProjectController);  

    function ProjectController($http, $scope, $auth, $rootScope, $state) {
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


        // MOVE ME TO A SERVICE PLEASE :(((
        $scope.logout = function() {

            $auth.logout().then(function() {
                localStorage.removeItem('user');
                $rootScope.authenticated = false;
                $rootScope.currentUser = null;

                $state.go('auth');
            });
        }

    }
    
})();