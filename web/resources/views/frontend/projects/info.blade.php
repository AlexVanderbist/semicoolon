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
        @if ($project->youtube_id !== '')
        <div class="col-md-6 col-md-offset-3">
            <div class="ytpreview embed-responsive embed-responsive-16by9">
                <iframe class="center-block" width="560" height="315" src="https://www.youtube.com/embed/{{$project->youtube_id}}" frameborder="0" allowfullscreen></iframe>
            </div>
        </div>
        @endif
    </div>
    <div class="frontstages container">
        <h2>Fases</h2>
        <div class="timeline">
        @foreach($project->stages as $stage)
            <div class="fase">
                <div class="fasedate col-md-4 col-xs-4"><h4>{{$stage->startdate->formatLocalized('%A %d %B %Y')}}</h4></div> <!-- in right format and Dutch -->
                <div class="fasedescription col-md-8 col-xs-8">
                    <h3>{!! $stage->name !!}</h3>
                    <p>{!! $stage->description !!}</p>
                    <p class="enddate">Eindigd op {{$stage->enddate->formatLocalized('%A %d %B %Y')}}</p>
                </div>
            </div>
        @endforeach
        </div>
    </div>
    <div class="projectpictures container">
        <h2>Foto's</h2>
        @foreach($project->images as $image)
            <div class="imagediv">
                <a href="../{{$image->filename}}" data-lightbox="{{$project->name}}" data-title="{{$project->name}}">
                <img src="../{{$image->filename}}" alt="{{$project->name}}" class="image">
                </a>
            </div>
        @endforeach
    </div>
    <div class="projectproposals container">
        <h2>Stellingen</h2>
        @foreach($project->proposals as $proposal)
            {!! $proposal->description !!}
            @if($proposal->type == '1')
            <p>ja/nee</p>
            @elseif($proposal->type == '2')
            <p>0 tot 5</p>
            @endif
        @endforeach
    </div>
    <div class="reactions container">
        <h2>Reacties</h2>
        <hr>
        @foreach($project->opinions as $loopedOpinion)
            <div><strong>{!! $loopedOpinion->user_id != 0 ? ($loopedOpinion->posted_by->admin ? $loopedOpinion->posted_by->fullname . ' | ADMIN' : $loopedOpinion->posted_by->fullname) : "Anoniem"!!}</strong>
                {!! $loopedOpinion->created_at->diffForHumans() !!}
                @if (Auth::check() && (Auth::user()->admin || $opinion->posted_by->id === Auth::id()))
                <a href="{{ route('frontend.projects.opiniondestroy', [$project->id, $loopedOpinion->id]) }}">
                    verwijderen
                </a>
                @endif
            </div> 
            <div>{{$loopedOpinion->opinion}}</div>
        @endforeach

        @if($reactionsallowed) <!-- when the comment deadline is passed -->
            <p><span class="glyphicon glyphicon-ban-circle"></span>Reacties zijn niet meer toegestaan</p>
        @else
        <div class="reactform">
            {!! Form::model($opinion, [
                'method' => 'post',
                'route' => ['frontend.projects.opinionstore', $project->id],
                'class' => 'form-horizontal'
            ]) !!}

            <div class="form-group">
                {!! Form::label('opinion','Reageer') !!}
                <p>Ingelogd als <strong>{!! Auth::check() ? (Auth::user()->admin ? Auth::user()->fullname . ' | ADMIN' : Auth::user()->fullname) : 'Anoniem' !!}</strong></p>
                {!! Form::textarea('opinion', null, ['class' => 'form-control', 'size' => '10x3', 'placeholder' => 'Uw reactie ...']) !!}
            </div>

            {!! Form::submit('Reageren', ['class' => 'btn btn-primary']) !!}
            {!! Form::close() !!}
        </div>
        @endif

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

        lightbox.option({
          'resizeDuration': 200,
          'alwaysShowNavOnTouchDevices' : true,
          'wrapAround': true
        })
    });
</script>
@endsection