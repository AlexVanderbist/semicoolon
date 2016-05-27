(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .factory('projectService', function($http, $auth, $q) {

            var apiUrl = '/api/v1/';
            var projectService = {};

            projectService.getAll = function () {
                return $http.get(apiUrl + 'projects');
            };

            return projectService;
        });
})();