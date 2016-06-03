(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectMapController', function ($scope, uiGmapGoogleMapApi, mapOptions) {

        	$scope.projectsFiltered = $scope.$parent.projectsFiltered;
            $scope.map = { 
                center: mapOptions.center, 
                zoom: mapOptions.zoom,
                markersEvents: {
                    click: function(marker, eventName, model) {
                        console.log('Click marker');
                        $scope.map.window.model = model;
                        $scope.map.window.show = true;
                    }
                },
                window: {
                    marker: {},
                    show: false,
                    closeClick: function() {
                        this.show = false;
                    },
                    options: {maxWidth:400} // defined when map is loaded
                },
                options: {
                    styles: mapOptions.styleArray
                }
            };

            uiGmapGoogleMapApi.then(function(maps) {
                // offset to fit the custom icon
                $scope.map.window.options.pixelOffset = new google.maps.Size(0, -35, 'px', 'px');
            });

			$scope.$parent.$watchCollection('projectsFiltered', function(newValue) {
	            $scope.projectsFiltered = newValue;
	        });

        });
    

})();