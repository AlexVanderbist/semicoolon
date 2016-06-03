(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectMapController', function ($scope, uiGmapGoogleMapApi) {
        	var styleArray=[
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

        	$scope.projectsFiltered = $scope.$parent.projectsFiltered;
            $scope.map = { 
                center: { 
                    latitude: 51.218686, 
                    longitude: 4.417458 
                }, 
                zoom: 14,
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
                    styles: styleArray
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