(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('logoutController', function(userService, $state, $scope) {

            userService.logout().then(function(){
                // Logout succesfull
                $state.go('home.intro');
            });
        });

})();
