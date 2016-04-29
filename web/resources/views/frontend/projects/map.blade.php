@extends('layouts.app')

@section('content')

<div id="map"></div>
<script>
	$(document).ready(function($){

		var projects = JSON.parse('{!! json_encode($projects) !!}');

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
	    var map = new google.maps.Map(document.getElementById('map'), map_options);

	    function infoWindowText(project) {
	    	return '<div id="content">'+
				'<div id="siteNotice">'+
				'</div>'+
				'<h1 id="firstHeading" class="firstHeading">'+project.name+'</h1>'+
				'<div id="bodyContent">'+
				'<p>'+project.locationText+'</p>'+
				'<p><a href="/projecten/'+project.id+'">Bekijk project</a></p>'+
				'</div>'+
				'</div>';
	    }

		function addMarkers() {
		  for (var i = 0; i < projects.length; i++) {
		    addMarkerWithTimeout(projects[i], i * 500);
		  }
		}

		function addMarkerWithTimeout(project, timeout) {
		  window.setTimeout(function() {

		  	var marker = new google.maps.Marker({
		      position: {lat: parseFloat(project.lat), lng: parseFloat(project.lng)},
		      map: map,
		      animation: google.maps.Animation.DROP,
		      title: project.name
		    });

		    marker.addListener('click', function() {
		    	infoWindow.setContent(infoWindowText(project));
                infoWindow.open(map, marker);
			});

		    markers.push(marker);


		  }, timeout);
		}

		addMarkers();
	});
</script>

@endsection