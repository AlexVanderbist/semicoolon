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

            projectService.get = function (id) {
                return $http.get(apiUrl + 'projects/' + id);
            };

            projectService.themes = function () {
                return $http.get(apiUrl + 'themes');
            };

            projectService.opinions = function (id) {
                return $http.get(apiUrl + 'projects/' + id + '/opinions');
            };

            return projectService;
        });
})();