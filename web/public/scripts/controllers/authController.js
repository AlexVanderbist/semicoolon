(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('authController', AuthController);


    function AuthController($state, $scope, userService) {
        
        $scope.loginError = false;
        $scope.loginErrorText;

        $scope.login = function() {

            var credentials = {
                email: $scope.email,
                password: $scope.password
            }
  
            userService.login(credentials).then(
                function () {
                    // Logged in
                    $state.go('projects');
                },
                function (error) {
                    // Login failed
                    $scope.loginError = true;
                    $scope.loginErrorText = error.data.error;
                }
            );
        }

    }

})();