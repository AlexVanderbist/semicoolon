@extends('layouts.app')

@section('title', $project->name)

@section('content')

    <div class="container">
        <h1>{{$project->name}}</h1>
        <hr>
        <h3>{{$project->locationText}}</h3>
        <p><span>door {{$project->creator->full_name}} op {{$project->created_at}}</span></p>
        <div id="smallmap"></div>
        <p>{!! $project->description !!}</p>
        @if ($project->youtube_url !== '')
        <div class="col-md-6 col-md-offset-3">
            <div class="ytpreview embed-responsive embed-responsive-16by9">
                <iframe class="center-block" width="560" height="315" src="https://www.youtube.com/embed/{{$project->youtube_id}}" frameborder="0" allowfullscreen></iframe>
            </div>
        </div>
        @endif
    </div>
    <div class="reactions container">
        <h2>Reacties</h2>
        <hr>
        @foreach($opinions as $opinion)
            <div><strong>{!! $opinion->user_id != 0 ? ($opinion->posted_by->admin ? $opinion->posted_by->fullname . ' [ADMIN]' : $opinion->posted_by->fullname) : "Anoniem"!!}</strong>
                @if (Auth::user()->admin)
                <a href="{{ route('frontend.projects.opiniondestroy', $opinion->id) }}">
                    verwijderen
                </a>
                @endif
            </div> 
            <div>{{$opinion->opinion}}</div>
        @endforeach
        <div class="reactform">
            {!! Form::model($opinion, [
                'method' => 'post',
                'route' => ['frontend.projects.opinionstore', $project->id],
                'class' => 'form-horizontal'
            ]) !!}

            <div class="form-group">
                {!! Form::label('opinion','Reageer') !!}
                <p>Ingelogd als <strong>{!! Auth::check() ? (Auth::user()->admin ? Auth::user()->fullname . ' [ADMIN]' : Auth::user()->fullname) : 'Anoniem' !!}</strong></p>
                {!! Form::textarea('opinion', null, ['class' => 'form-control', 'size' => '10x3', 'placeholder' => 'Uw reactie ...']) !!}
            </div>

            {!! Form::submit('Reageren', ['class' => 'btn btn-primary']) !!}
            {!! Form::close() !!}
        </div>
    </div>

<script>
    $(document).ready(function($){

        var projects = {!! $project !!};

        /*console.log(projects);*/

        var defaultMap = JSON.parse('{!! json_encode(config('cms.defaultmap')) !!}');

        var markers = [];

        var mapStyle = [{"featureType":"road","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"poi","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"labels.text","stylers":[{"visibility":"off"}]}];

        var map_options = {
            center: new google.maps.LatLng(projects.lat, projects.lng),
            zoom: parseInt(defaultMap.zoom)+2,
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
        var map = new google.maps.Map(document.getElementById('smallmap'), map_options);

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

            markers.push(marker);


          }, timeout);
        }

        addMarkerWithTimeout(projects, 500);
    });
</script>
@endsection