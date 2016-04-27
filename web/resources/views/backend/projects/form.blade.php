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
        {!! Form::label('project_creator','Aangemaakt door') !!}
        {!! Form::text('project_creator', Auth::user()->id, ['class' => 'form-control', 'readonly']) !!}
    </div>

    {!! Form::submit($project->exists ? 'Project opslaan' : 'Nieuw project maken', ['class' => 'btn btn-primary']) !!}
    <a href="{{ route('backend.projects.index') }}" class="">of ga terug naar alle projecten.</a>

    {!! Form::close() !!}

    <script>
        $('#locationSelector').locationpicker({
            location: {latitude: ($('#lat').val() || 51.218686), longitude: $('#lng').val() || 4.417458},
            radius: 300,
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
    </script>

@endsection