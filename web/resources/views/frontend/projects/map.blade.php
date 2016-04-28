@extends('layouts.app')

@section('content')

<div id="map"></div>
<script>
	$(document).ready(function($){

		var projects = JSON.parse('{!! json_encode($projects) !!}');

		console.log(projects);

		var defaultMap = JSON.parse('{!! json_encode(config('cms.defaultmap')) !!}');

		var markers = [];
		var map;
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

		function initMap(defaultMap) {
		  map = new google.maps.Map(document.getElementById('map'), map_options);
		}

		function drop() {
		  clearMarkers();
		  for (var i = 0; i < projects.length; i++) {
		    addMarkerWithTimeout(projects[i], i * 200);
		  }
		}

		function addMarkerWithTimeout(project, timeout) {
		  window.setTimeout(function() {
		    markers.push(new google.maps.Marker({
		      position: {lat: parseFloat(project.lat), lng: parseFloat(project.lng)},
		      map: map,
		      animation: google.maps.Animation.DROP
		    }));
		  }, timeout);
		}

		function clearMarkers() {
		  for (var i = 0; i < markers.length; i++) {
		    markers[i].setMap(null);
		  }
		  markers = [];
		}

		initMap(defaultMap);
		drop();
	});
</script>

@endsection