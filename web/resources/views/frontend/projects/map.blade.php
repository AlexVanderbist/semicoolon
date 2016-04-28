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

		function initMap(defaultMap) {
		  map = new google.maps.Map(document.getElementById('map'), {
		    zoom: parseFloat(defaultMap.zoom),
		    center: {lat: parseFloat(defaultMap.lat), lng: parseFloat(defaultMap.lng)}
		  });
		}

		function drop() {
		  clearMarkers();
		  for (var i = 0; i < projects.length; i++) {
		    addMarkerWithTimeout(projects[i], i * 200);
		  }
		}

		function addMarkerWithTimeout(position, timeout) {
		  window.setTimeout(function() {
		    markers.push(new google.maps.Marker({
		      position: position,
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