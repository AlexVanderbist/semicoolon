(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('AuthController', AuthController);


    function AuthController($auth, $state, $scope, $http, $rootScope) {
        
        $scope.loginError = false;
        $scope.loginErrorText;

        $scope.login = function() {

            var credentials = {
                email: $scope.email,
                password: $scope.password
            }

            $auth.login(credentials).then(function() {

                // Return an $http request for the now authenticated user
                return $http.get('/api/v1/authenticate/user');

            // Handle errors
            }, function(error) {
                $scope.loginError = true;
                $scope.loginErrorText = error.data.error;

            // This is the return from the GET for authenticate/user
            }).then(function(response) {

                // object to string to store in localstorage
                var user = JSON.stringify(response.data.user);
                localStorage.setItem('user', user);

                // User is now logged in
                $rootScope.authenticated = true;
                $rootScope.currentUser = response.data.user;

                $state.go('projects');
            });
        }

    }

})();