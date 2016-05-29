(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectMapController', function ($scope) {
        	
        	$scope.projectsFiltered = $scope.$parent.projectsFiltered;
            $scope.map = { center: { latitude: 51.218686, longitude: 4.417458 }, zoom: 14 };

			$scope.$parent.$watchCollection('projectsFiltered', function(newValue) {
	            $scope.projectsFiltered = newValue;
	        });
        });
    
})();