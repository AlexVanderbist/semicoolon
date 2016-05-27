(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', projectController);  

    function projectController($scope, $auth, $rootScope, $state, projects) {

        $scope.projects = projects.data.projects;

    }
    
})();