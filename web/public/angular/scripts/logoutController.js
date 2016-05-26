(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('logoutController', function(userService, $state) {

            console.log("etyf");
            
            userService.logout().then(function(){
                // Logout succesfull
                $state.go('auth');
            });
        });
    
})();