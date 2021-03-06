@extends('layouts.app')

@section('content')

<div id="map"></div>
<script>
	$(document).ready(function($){

		var projects = {!! $projects !!};

		console.log(projects);

		var defaultMap = JSON.parse('{!! json_encode(config('cms.defaultmap')) !!}');

		var markers = [];

		var mapStyle = [{"featureType":"road","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"poi","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"labels.text","stylers":[{"visibility":"off"}]}];

		var map_options = {
	      	center: new google.maps.LatLng(defaultMap.lat, defaultMap.lng),
	      	zoom: parseInt(defaultMap.zoom),
	      	panControl: false,
	      	zoomControl: true,
	      	mapTypeControl: false,
	      	streetViewControl: false,
	      	mapTypeId: google.maps.MapTypeId.ROADMAP,
	      	scrollwheel: false,
	      	styles: mapStyle,
			zoomControlOptions: {
				style: google.maps.ZoomControlStyle.SMALL,
				position: google.maps.ControlPosition.BOTTOM_CENTER
			}
	    }

	    var infoWindow = new google.maps.InfoWindow();

		google.maps.event.addListener(infoWindow, 'closeclick', function () {
			radiusCircle.setRadius(0);
		});

	    var map = new google.maps.Map(document.getElementById('map'), map_options);

		var radiusCircle = new google.maps.Circle();

		//circle.setMap(null);

	    function infoWindowText(project) {
	    	return '<div id="content">'+
				'<div id="siteNotice">'+
				'</div>'+
				'<h1 id="firstHeading" class="firstHeading">'+project.name+'</h1>'+
				'<div id="bodyContent">'+
				'<p><strong>'+project.locationText+'</strong></p>'+
				'<p>'+project.description+'</p>'+
				'<p><a href="/projecten/'+project.id+'">Bekijk project</a></p>'+
				'</div>'+
				'</div>';
	    }

		function addMarkers() {
		  for (var i = 0; i < projects.length; i++) {
		    addMarkerWithTimeout(projects[i], i * 500);
		  }
		}

		function pinSymbol(color) {
		    return {
		        path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z M -2,-30 a 2,2 0 1,1 4,0 2,2 0 1,1 -4,0',
		        fillColor: color,
		        fillOpacity: 1,
		        strokeColor: '#000',
		        strokeWeight: 2,
		        scale: 1,
		   };
		}

		function addMarkerWithTimeout(project, timeout) {
		  window.setTimeout(function() {

		  	var marker = new google.maps.Marker({
		      position: {lat: parseFloat(project.lat), lng: parseFloat(project.lng)},
		      map: map,
		      animation: google.maps.Animation.DROP,
		      title: project.name,
		      icon: pinSymbol(project.theme.hex_color)
		    });

		    marker.addListener('click', function() {
		    	infoWindow.setContent(infoWindowText(project));
                infoWindow.open(map, marker);

                var options = {
					strokeColor: '#000000',
					strokeOpacity: 0.5,
					strokeWeight: 1,
					fillColor: project.theme.hex_color,
					fillOpacity: 0.30,
					map: map,
					center: new google.maps.LatLng(project.lat, project.lng),
					radius: parseInt(project.radius)
				};

				radiusCircle.setOptions(options);
			});

		    markers.push(marker);


		  }, timeout);
		}

		addMarkers();
	});
</script>

@endsection