(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .factory('userService', function($http, $auth, $q, $rootScope) {

        var apiUrl = '/api/v1/';
        var userService = {};

        userService.login = function (credentials) {
            return $q(function(resolve, reject) {
                // Login with Satellizer
                $auth.login(credentials).then(function() {

                    console.log("Logging in...");

                    // Get the user info
                    $http.get('/api/v1/authenticate/user').then(function(response) {
                        // object to string to store in localstorage
                        var user = JSON.stringify(response.data.user);
                        localStorage.setItem('user', user);

                        // User is now logged in
                        $rootScope.authenticated = true;
                        $rootScope.currentUser = response.data.user;

                        console.log("Succesfully logged in: ", $rootScope.currentUser);

                        resolve();
                    });

                // Handle errors
                }, function(request) {
                    reject(request);
                    console.log("Login error: ", request);
                });
            });
        }

        userService.logout = function() {
            return $q(function(resolve) {
                $auth.logout().then(function() {
                    localStorage.removeItem('user');
                    $rootScope.authenticated = false;
                    $rootScope.currentUser = null;
                    console.log("Logged out");
                    resolve();
                });
            });
        }

        return userService;
    });
})();