(function() {

    'use strict';

    angular
        .module('antwerpApp', [
            'ui.router', 
            'satellizer', 
            'ui.bootstrap',
            'ngSanitize'
        ])

        .config(function($stateProvider, $urlRouterProvider, $authProvider, $provide, $httpProvider) {

            // Satellizer configuration that specifies which API route the JWT should be retrieved from
            $authProvider.loginUrl = '/api/v1/authenticate';

            // Default route when something else is requested other than the states bellow
            $urlRouterProvider.otherwise('/projects');
            
            // States in ui-router
            var templateUrlPrefix = '../angular/views/';
            $stateProvider
                .state('login', {
                    url: '/login',
                    templateUrl: templateUrlPrefix + 'loginView.html',
                    controller: 'authController'
                })
                .state('logout', {
                    url: '/logout',
                    templateUrl: templateUrlPrefix + 'logoutView.html',
                    controller: 'logoutController'
                })
                .state('projects', {
                    url: '/projects',
                    templateUrl: templateUrlPrefix + 'projectView.html',
                    controller: 'projectController'
                });


            function redirectWhenLoggedOut($q, $injector) {

                return {
                    responseError: function(rejection) {

                        // Need to use $injector.get to bring in $state or else we get
                        // a circular dependency error
                        var $state = $injector.get('$state');

                        // Instead of checking for a status code of 400 which might be used
                        // for other reasons in Laravel, we check for the specific rejection
                        // reasons to tell us if we need to redirect to the login state
                        var rejectionReasons = ['token_not_provided', 'token_expired', 'token_absent', 'token_invalid'];

                        // Loop through each rejection reason and redirect to the login
                        // state if one is encountered
                        angular.forEach(rejectionReasons, function(value, key) {

                            if(rejection.data.error === value) {
                                // TODO: Try to refresh the token

                                // If we get a rejection corresponding to one of the reasons
                                // in our array, we know we need to authenticate the user so 
                                // we can remove the current user from local storage
                                localStorage.removeItem('user');

                                // Send the user to the auth state so they can login
                                $state.go('login');
                            }
                        });

                        return $q.reject(rejection);
                    }
                }
            }

            // Setup for the $httpInterceptor
            $provide.factory('redirectWhenLoggedOut', redirectWhenLoggedOut);

            // Push the new factory onto the $http interceptor array
            $httpProvider.interceptors.push('redirectWhenLoggedOut');
        })

        .run(function($rootScope, $state) {

            $rootScope.$on('$stateChangeStart', function(event, toState) {

                // Grab the user from local storage and parse it to an object
                var user = JSON.parse(localStorage.getItem('user'));            

                // If there is a user he might be authenticated, if not he will be redirected via the above authredirect
                if(user) {


                    $rootScope.authenticated = true;
                    $rootScope.currentUser = user;

                    // If the user tried going to auth.login, redirect him somewhere else lol
                    if(toState.name === "login") {


                        event.preventDefault();
                        $state.go('projects');
                    }       
                }
            });
        });
})();