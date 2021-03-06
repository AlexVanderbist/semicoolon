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

    .config(function($stateProvider, $urlRouterProvider, $authProvider, $provide, $httpProvider, uiGmapGoogleMapApiProvider) {

        // Satellizer configuration that specifies which API route the JWT should be retrieved from
        $authProvider.loginUrl = '/api/v1/authenticate';

        // Default route when something else is requested other than the states bellow
        $urlRouterProvider.otherwise('/');
        $urlRouterProvider.when('/projects', '/projects/map');

        // States in ui-router
        var templateUrlPrefix = 'views/';
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
                abstract: true,
				params: {
					status: null
				}
            })
            .state('user.login', {
                url: '/login',
                templateUrl: templateUrlPrefix + 'loginView.html',
                controller: 'authController'
            })
            .state('user.register', {
                url: '/register',
                templateUrl: templateUrlPrefix + 'registrationView.html',
                controller: 'registrationController'
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
                    project: function(projectService, $stateParams) {
                        return projectService.get($stateParams.id);
                    }
                }
            })
            .state('projects', {
                url: '/projects',
                templateUrl: templateUrlPrefix + 'projectsView.html',
                controller: 'projectsController',
                resolve: {
                    projects: function(projectService) {
                        return projectService.getAll();
                    },
                    themes: function(projectService) {
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
                    var userService = $injector.get('userService');

                    // Instead of checking for a status code of 400 which might be used
                    // for other reasons in Laravel, we check for the specific rejection
                    // reasons to tell us if we need to redirect to the login state
                    var rejectionReasons = ['token_not_provided', 'token_expired', 'token_absent', 'token_invalid'];

                    // Loop through each rejection reason and redirect to the login
                    // state if one is encountered
                    angular.forEach(rejectionReasons, function(value, key) {

                        if (rejection.data.error === value) {
                            // TODO: Try to refresh the token

                            // If we get a rejection corresponding to one of the reasons
                            // in our array, we know we need to authenticate the user so
                            // we can remove the current user from local storage
                            localStorage.removeItem('user');
                            userService.logout();

                            // Send the user to the auth state so they can login
                            $state.go('user.login', {status: 'Je moet ingelogd zijn om dit te doen.'});
                        }
                    });

                    return $q.reject(rejection);
                }
            };
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

    .run(function($rootScope, $state, userService) {

        $rootScope.$on('$stateChangeStart', function(event, toState) {

            console.log('stateChangeStart');

            // Grab the user from local storage and parse it to an object
            var user = JSON.parse(localStorage.getItem('user'));

            // If there is a user he might be authenticated, if not he will be redirected via the above authredirect
            if (user) {

                // set rootscope to pretend we've already loaded the user
                $rootScope.currentUser = user;
                $rootScope.authenticated = true;

                // request user info to trigger loggedout thingy
                userService.sessionInfo().then(function(response){
                    // succes; set actual data
                    $rootScope.currentUser = response.data.user;
                });

                // If the user tried going to auth.login, redirect him somewhere else lol
                if (toState.name === "user.login") {
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
    })

    .filter('htmlToPlaintext', function() {
        return function(text) {
            return text ? String(text).replace(/<[^>]+>/gm, '') : '';
        };
    })

    .filter('truncate', function() {
            return function(text, length, end) {
                if (isNaN(length))
                    length = 10;

                if (end === undefined)
                    end = "...";

                if (text.length <= length || text.length - end.length <= length) {
                    return text;
                } else {
                    return String(text).substring(0, length - end.length) + end;
                }

            };
    })

    .factory('mapOptions', function() {
        return {
            zoom : 14,
            center: {
                latitude: 51.218686,
                longitude: 4.417458
            },
            styleArray: [
                {
                    "featureType": "administrative",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#444444"
                        }
                    ]
                },
                {
                    "featureType": "administrative.province",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "hue": "#ff0000"
                        }
                    ]
                },
                {
                    "featureType": "administrative.locality",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#cf0039"
                        }
                    ]
                },
                {
                    "featureType": "administrative.neighborhood",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#626262"
                        }
                    ]
                },
                {
                    "featureType": "landscape",
                    "elementType": "all",
                    "stylers": [
                        {
                            "color": "#f2f2f2"
                        }
                    ]
                },
                {
                    "featureType": "landscape.man_made",
                    "elementType": "all",
                    "stylers": [
                        {
                            "visibility": "on"
                        }
                    ]
                },
                {
                    "featureType": "poi",
                    "elementType": "all",
                    "stylers": [
                        {
                            "visibility": "on"
                        },
                        {
                            "hue": "#ff0000"
                        }
                    ]
                },
                {
                    "featureType": "poi",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "poi",
                    "elementType": "geometry.fill",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "poi.attraction",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#cf0039"
                        }
                    ]
                },
                {
                    "featureType": "poi.attraction",
                    "elementType": "labels.text.stroke",
                    "stylers": [
                        {
                            "visibility": "on"
                        }
                    ]
                },
                {
                    "featureType": "poi.business",
                    "elementType": "all",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "poi.business",
                    "elementType": "geometry.fill",
                    "stylers": [
                        {
                            "visibility": "on"
                        }
                    ]
                },
                {
                    "featureType": "poi.government",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#cf0039"
                        }
                    ]
                },
                {
                    "featureType": "poi.place_of_worship",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#cf0039"
                        }
                    ]
                },
                {
                    "featureType": "poi.school",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "color": "#cf0039"
                        }
                    ]
                },
                {
                    "featureType": "road",
                    "elementType": "all",
                    "stylers": [
                        {
                            "saturation": -100
                        },
                        {
                            "lightness": 45
                        }
                    ]
                },
                {
                    "featureType": "road.highway",
                    "elementType": "all",
                    "stylers": [
                        {
                            "visibility": "simplified"
                        }
                    ]
                },
                {
                    "featureType": "road.arterial",
                    "elementType": "labels.icon",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "transit",
                    "elementType": "all",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "water",
                    "elementType": "all",
                    "stylers": [
                        {
                            "color": "#1964b2"
                        },
                        {
                            "visibility": "on"
                        },
                        {
                            "saturation": "0"
                        },
                        {
                            "lightness": "70"
                        }
                    ]
                }
            ]

        };
    });
})();
