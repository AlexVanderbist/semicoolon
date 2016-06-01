(function() {

    'use strict';

    angular
        .module('antwerpApp', [
            'ui.router', 
            'satellizer', 
            'ui.bootstrap',
            'ngSanitize',
            'uiGmapgoogle-maps',
            'nemLogging',
            'youtube-embed'
        ])

        .config(function($stateProvider, $urlRouterProvider, $authProvider, $provide, $httpProvider,uiGmapGoogleMapApiProvider) {

            // Satellizer configuration that specifies which API route the JWT should be retrieved from
            $authProvider.loginUrl = '/api/v1/authenticate';

            // Default route when something else is requested other than the states bellow
            $urlRouterProvider.otherwise('/');
            $urlRouterProvider.when('/projects', '/projects/map');
            
            // States in ui-router
            var templateUrlPrefix = '../angular/views/';
            $stateProvider
                .state('home', {
                    templateUrl: templateUrlPrefix + 'modalBigView.html',
                    controller: 'modalController',
                    abstract: true
                })
                .state('home.intro', {
                    url: '/',
                    templateUrl: templateUrlPrefix + 'introView.html'
                })
                .state('user', {
                    templateUrl: templateUrlPrefix + 'modalView.html',
                    controller: 'modalController',
                    abstract: true
                })
                .state('user.login', {
                    url: '/login',
                    templateUrl: templateUrlPrefix + 'loginView.html',
                    controller: 'authController'
                })
                .state('user.logout', {
                    url: '/logout',
                    templateUrl: templateUrlPrefix + 'logoutView.html',
                    controller: 'logoutController'
                })
                .state('project', {
                    url: '/project/{id:int}',
                    templateUrl: templateUrlPrefix + 'projectView.html',
                    controller: 'projectController',
                    resolve: {
                        project: function (projectService, $stateParams) {
                            return projectService.get($stateParams.id);
                        }
                    }
                })
                .state('projects', {
                    url: '/projects',
                    templateUrl: templateUrlPrefix + 'projectsView.html',
                    controller: 'projectsController',
                    resolve: {
                        projects: function (projectService) {
                            return projectService.getAll();
                        },
                        themes: function (projectService) {
                            return projectService.themes();
                        }
                    }
                })
                .state('projects.list', {
                    url: '/list',
                    templateUrl: templateUrlPrefix + 'projectListView.html',
                    controller: 'projectListController'
                })
                .state('projects.map', {
                    url: '/map',
                    templateUrl: templateUrlPrefix + 'projectMapView.html',
                    controller: 'projectMapController'
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

            uiGmapGoogleMapApiProvider.configure({
                key: 'AIzaSyDhTfQGWyjyP7vj3t_GFtOrF7-mbGsVLAY',
                //v: '3.20', //defaults to latest 3.X anyhow
                libraries: 'places'
            });
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

            $rootScope.$on('$stateChangeError', function(event) {
                $state.go('projects');
            });
        })
        .filter('dateToISO', function() {
            return function(input) {
                input = new Date(input).toISOString();
                return input;
            };
        });
})();