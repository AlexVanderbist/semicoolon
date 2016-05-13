@extends('layouts.backend')

@section('title', $project->exists ? 'Wijzig '.$project->name : 'Nieuw project aanmaken')

@section('content')
    {!! Form::model($project, [
        'method' => $project->exists ? 'put' : 'post',
        'route' => $project->exists ? ['backend.projects.update', $project->id] : ['backend.projects.store'],
        'class' => 'form-horizontal'
    ]) !!}

    <div class="form-group">
        {!! Form::label('name','Naam') !!}
        {!! Form::text('name', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('description','Beschrijving') !!}
        {!! Form::textarea('description', null, ['class' => 'form-control', 'id'=>'description']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Exacte locatie') !!}

        <div class="form-group">
            {!! Form::label('address', 'Snel zoeken', ['class'=>'col-sm-2 control-label']) !!} 
            <div class="col-sm-6">
                {!! Form::text('address', null, ['class' => 'form-control', 'id'=>'search-address']) !!}
            </div>
        </div>

        <div class="form-group">
            {!! Form::label('radius', 'Radius gebied', ['class'=>'col-sm-2 control-label']) !!}
            <div class="col-sm-2">
                {!! Form::number('radius', null, ['class' => 'form-control', 'id'=>'radius']) !!}
            </div>
        </div>

        {!! Form::hidden('lat', null, ['class' => 'form-control', 'id'=>'lat']) !!}
        {!! Form::hidden('lng', null, ['class' => 'form-control', 'id'=>'lng']) !!}
        <div id="locationSelector" style="height: 400px;"></div>
    </div>

    <div class="form-group">
        {!! Form::label('locationText', 'Locatie') !!}
        {!! Form::text('locationText', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('theme_id','Thema') !!}
        {!! Form::select('theme_id', [
                '' => ''
            ] + $themes, null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('youtube_url','Youtube video','Aangemaakt door') !!}
        <p>Plak hier de youtubeURL</p>
        {!! Form::text('youtube_url', null, ['class' => 'form-control', 'id' => 'yturl']) !!}
        <div class="col-md-6 col-md-offset-3" id="yt">
            <div class="ytpreview embed-responsive embed-responsive-16by9">
                <iframe class="center-block" id="ytpreview" width="560" height="315" src="" frameborder="0" allowfullscreen></iframe>
            </div>
        </div>  
    </div>

    {!! Form::submit($project->exists ? 'Project opslaan' : 'Nieuw project maken', ['class' => 'btn btn-primary']) !!}
    <a href="{{ route('backend.projects.index') }}" class="">of ga terug naar alle projecten.</a>

    {!! Form::close() !!}

    <script>

        // Editor ///////
        $('#description').trumbowyg({
            //
        });

        // Map //////////

        var defaultMap = JSON.parse('{!! json_encode(config('cms.defaultmap')) !!}');

        $('#locationSelector').locationpicker({
            location: {latitude: ($('#lat').val() || parseFloat(defaultMap.lat)), longitude: $('#lng').val() || parseFloat(defaultMap.lng)},
            radius: $('#radius').val() || parseInt(defaultMap.radius),
            zoom: 13,
            scrollwheel: true,
            inputBinding: {
                latitudeInput: $('#lat'),
                longitudeInput: $('#lng'),
                radiusInput: $('#radius'),
                locationNameInput: $('#search-address')
            },
            enableAutocomplete: true,
            enableReverseGeocode: true
        });

        function getID(url){
            var regExp = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;
            var match = url.match(regExp);
            return (match&&match[7].length==11)? match[7] : false;
        }

        $( document ).ready(function() {
            $('#ytpreview').attr("src","https://www.youtube.com/embed/" + getID($('#yturl').val()));
            if($('#yturl').val() == '' || getID($('#yturl').val()) == false){         
                $( '#yt' ).fadeOut(900);
            }
            else{$( '#yt' ).fadeIn(900);}
        });

        $('#yturl').bind('input', function() {
            $('#ytpreview').attr("src","https://www.youtube.com/embed/" + getID($(this).val()));
            if($('#yturl').val() == '' || getID($('#yturl').val()) == false){         
                $( '#yt' ).fadeOut(900);
            }
            else{$( '#yt' ).fadeIn(900);}
            console.log(getID($('#yturl').val()));
        });
    </script>

@endsection