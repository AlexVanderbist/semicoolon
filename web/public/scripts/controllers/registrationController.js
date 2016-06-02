(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('registrationController', function ($state, $scope, userService) {

	        $scope.registrationError = false;
			$scope.newUser = {};

	        $scope.register = function() {

	            userService.register($scope.newUser).then(
	                function () {
	                    // Registered
	                    $state.go('user.login'); // add status message
	                },
	                function (error) {
	                    // Login failed
						console.log(error);
	                    if (error.status == 422) {
	                    	$scope.errors = error.data;
	                    }
	                }
	            );
	        };

	    });

})();
