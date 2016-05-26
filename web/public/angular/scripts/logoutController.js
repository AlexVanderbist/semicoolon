(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('logoutController', function(userService, $state) {
            
            userService.logout().then(function(){
                // Logout succesfull
                $state.go('login');
            });
        });
    
})();