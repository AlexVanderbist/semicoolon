(function() {

    'use strict';

    angular
        .module('antwerpApp', ['ui.router', 'satellizer'])

        .config(function($stateProvider, $urlRouterProvider, $authProvider) {

            // Satellizer configuration that specifies which API route the JWT should be retrieved from
            $authProvider.loginUrl = '/api/v1/authenticate';

            // Default route when something else is requested other than the states bellow
            $urlRouterProvider.otherwise('/auth');
            
            // States in ui-router
            var templateUrlPrefix = '../angular/';
            $stateProvider
                .state('auth', {
                    url: '/auth',
                    templateUrl: templateUrlPrefix + 'views/authView.html',
                    controller: 'AuthController'
                })
                .state('projects', {
                    url: '/projects',
                    templateUrl: templateUrlPrefix + 'views/projectView.html',
                    controller: 'ProjectController'
                });
        });
})();